using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Controller.Contracts;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controller
{
    [Route("api/v1/user-management")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userManagementService.GetUsers();
        }
        
        
        [HttpDelete]
        [Route("user")]
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