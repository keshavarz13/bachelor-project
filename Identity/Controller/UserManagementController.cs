using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Controller.Contracts;
using Identity.Services;
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
        [Route("users")]
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userManagementService.GetUsers();
        }
    }
}