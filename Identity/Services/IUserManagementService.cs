using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Controller.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services
{
    public interface IUserManagementService
    {
        Task<TokenOutputDto> GenerateJwtToken(LoginInputDto loginInputDto);
        Task<RegisterOutputDto> RegisterUser(RegisterInputDto inputDto, string type);
        Task<List<ApplicationUser>> GetUsers();
        Task<List<ApplicationUser>> GetUserByPhoneNumber(string phoneNumber);
        Task<List<ApplicationUser>> GetUserByEmail(string email);
        Task<IdentityResult> RemoveUser(string username);
    }
}