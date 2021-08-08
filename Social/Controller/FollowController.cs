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
        [Route("basic-info/{uun}")]
        public async Task<IActionResult> FollowBasicInfo(int uun)
        {
            throw new NotImplementedException();
        }
        
        
        [HttpGet]
        [Route("followers/{uun}")]
        public async Task<IActionResult> Followers()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("following/{uun}")]
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