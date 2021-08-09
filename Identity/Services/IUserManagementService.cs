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
        Task<List<UserReportOutputDto>> GetUsers();
        Task<List<UserReportOutputDto>> GetUserByPhoneNumber(string phoneNumber);
        Task<List<UserReportOutputDto>> GetUserByEmail(string email);
        Task<IdentityResult> RemoveUser(string username);
        Task<UserReportOutputDto> GetUserByUun(int uun);
        Task<List<UserReportOutputDto>> GetUserByUserName(string userName);
        Task<List<UserReportOutputDto>> GetUsersByUuns(List<int> uuns);
    }
}