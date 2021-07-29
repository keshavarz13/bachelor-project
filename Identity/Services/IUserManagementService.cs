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
        Task<IdentityResult> RemoveUser(string username);
    }
}