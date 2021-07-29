using System.Threading.Tasks;
using Identity.Controller.Contracts;

namespace Identity.Services
{
    public interface IUserManagementService
    {
        Task<TokenOutputDto> GenerateJwtToken(LoginInputDto loginInputDto);
        Task<RegisterOutputDto> RegisterUser(RegisterInputDto inputDto, string type);
    }
}