using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Controller.Contracts;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controller
{
    [Route("api/v1/user")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<UserReportOutputDto>> GetUsers()
        {
            return await _userManagementService.GetUsers();
        }
        
        
        [HttpGet]
        [Route("phone-number/{phoneNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<List<UserReportOutputDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _userManagementService.GetUserByPhoneNumber(phoneNumber);
        }
        
        [HttpGet]
        [Route("email/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<List<UserReportOutputDto>> GetUserByEmail(string email)
        {
            return await _userManagementService.GetUserByEmail(email);
        }
        
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUser(string username)
        {
            try
            {
                return Ok(await _userManagementService.RemoveUser(username));
            }
            catch (ArgumentNullException e)
            {
                return BadRequest("Username is not exist");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}