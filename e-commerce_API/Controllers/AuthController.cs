using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using e_commerce_API.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            //return Ok(result);
            return Ok(new { Message = "Registraion successfulry", token = result.Token, ExpireOn = result.ExpireOn });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenReguestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            //return Ok(result);
            return Ok(new { Message = "Login successfulry", token = result.Token, ExpireOn = result.ExpireOn });
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            //return Ok(model);
            return Ok(new { Message = "Add Role successfulry", model });
        }
    }
}
