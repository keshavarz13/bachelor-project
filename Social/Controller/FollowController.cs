using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Services;

namespace Social.Controller
{
    [Microsoft.AspNetCore.Components.Route("api/v1/Follow")]
    public class FollowController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost]
        [Route("{followedUun}")]
        [Authorize]
        public async Task<IActionResult> Follow(int followedUun)
        {
            var followerUun = Convert.ToInt32(GetClaimsByName("UUN"));
            try
            {
                return Ok(await _followService.Follow(followerUun, followedUun));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("basic-info/{uun}")]
        public async Task<IActionResult> FollowBasicInfo(int uun)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("followers/{uun}")]
        public async Task<IActionResult> Followers(int uun)
        {
            try
            {
                return Ok(await _followService.GetFollowers(uun));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("following/{uun}")]
        public async Task<IActionResult> Following(int uun)
        {
            try
            {
                return Ok(await _followService.GetFollowings(uun));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{followedUun}")]
        [Authorize]
        public async Task<IActionResult> UnFollow(int followedUun)
        {
            var followerUun = Convert.ToInt32(GetClaimsByName("UUN"));
            try
            {
                 _followService.UnFollow(followerUun, followedUun);
                 return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        private string GetClaimsByName(string name)
        {
            //First get user claims    
            var claims = User.Claims.ToList();
            //Filter specific claim    
            return claims?.Where(x => x.Type == name).FirstOrDefault()?.Value;
        }
    }
}