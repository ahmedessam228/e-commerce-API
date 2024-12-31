using e_commerce_API.Models;

namespace e_commerce_API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task <AuthModel> GetTokenAsync(TokenReguestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);


    }
}
