using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Social.Controller
{
    [Microsoft.AspNetCore.Components.Route("api/v1/Follow")]
    [AllowAnonymous]
    public class FollowController
    {
        public FollowController()
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> Follow()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public async Task<IActionResult> FollowBasicInfo()
        {
            throw new NotImplementedException();
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Followers()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public async Task<IActionResult> Following()
        {
            throw new NotImplementedException();
        }
        
        [HttpPut]
        public async Task<IActionResult> UnFollow()
        {
            throw new NotImplementedException();
        }
    }
}