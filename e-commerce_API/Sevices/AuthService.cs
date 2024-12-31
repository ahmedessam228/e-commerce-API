using e_commerce_API.Helper;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace e_commerce_API.Sevices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        private readonly Jwt _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> RoleManager, IOptions<Jwt> jwt)
        {
            _userManager = userManager;
            _RoleManager = RoleManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            var email = await _userManager.FindByEmailAsync(model.Email);
            var username = await _userManager.FindByNameAsync(model.Username);
            if (email is not null)
            {
                return new AuthModel { Message = "Email is Already Register" };
            }

            if (username is not null)
            {
                return new AuthModel { Message = "Name is Already Register" };
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpireOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Role = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName

            };

        }


        public async Task<AuthModel> GetTokenAsync(TokenReguestModel model)
        {
            var authmodel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authmodel.Message = "Email or Password InCorrect!";
                return authmodel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var roleslist = await _userManager.GetRolesAsync(user);

            authmodel.IsAuthenticated = true;
            authmodel.Email = user.Email;
            authmodel.UserName = user.UserName;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authmodel.ExpireOn = jwtSecurityToken.ValidTo;
            authmodel.Role = roleslist.ToList();


            return authmodel;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _RoleManager.RoleExistsAsync(model.RoleName))
            {
                return "Invalid User or RoleName!";
            }

            if (await _userManager.IsInRoleAsync(user, model.RoleName))
            {
                return "User is assign to role";
            }

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);

            return result.Succeeded ? string.Empty : "Something want Wrong";
        }





        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                    signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }




    }

}
