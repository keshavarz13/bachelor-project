using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Identity.Controller.Contracts;
using Identity.Services;

namespace Identity.Controller
{
    [Route("api/v1/user-management")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public AuthenticateController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputDto inputDtop)
        {
            try
            {
                return Ok(await _userManagementService.GenerateJwtToken(inputDtop));
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterInputDto inputDto)
        {
            try
            {
                return Ok(await _userManagementService.RegisterUser(inputDto, UserRoles.User));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterInputDto inputDto)
        {
            try
            {
                return Ok(await _userManagementService.RegisterUser(inputDto, UserRoles.Admin));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost]
        [Route("register-writer")]
        public async Task<IActionResult> RegisterWriter([FromBody] RegisterInputDto inputDto)
        {
            try
            {
                return Ok(await _userManagementService.RegisterUser(inputDto, UserRoles.Writer));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}