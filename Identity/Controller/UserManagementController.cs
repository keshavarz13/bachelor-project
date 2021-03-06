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
        public async Task<List<UserReportOutputDto>> GetUsers()
        {
            return await _userManagementService.GetUsers();
        }
        
        
        [HttpGet]
        [Route("phone-number/{phoneNumber}")]
        public async Task<List<UserReportOutputDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _userManagementService.GetUserByPhoneNumber(phoneNumber);
        }
        
        [HttpGet]
        [Route("email/{email}")]
        public async Task<List<UserReportOutputDto>> GetUserByEmail(string email)
        {
            return await _userManagementService.GetUserByEmail(email);
        }
        
        [HttpGet]
        [Route("uun/{uun}")]
        public async Task<UserReportOutputDto> GetUserByEmail(int uun)
        {
            return await _userManagementService.GetUserByUun(uun);
        }
        
        [HttpGet]
        [Route("username/{username}")]
        public async Task<List<UserReportOutputDto>> GetUserByUsername(string username)
        {
            return await _userManagementService.GetUserByUserName(username);
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
        
        [HttpPost]
        [Route("bulk-users-by-uun")]
        public async Task<List<UserReportOutputDto>> GetBulkUsers([FromBody] List<int> uuns)
        {
            try
            {
                return await _userManagementService.GetUsersByUuns(uuns);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}