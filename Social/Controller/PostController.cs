using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Controller.Contracts;
using Social.Services;

namespace Social.Controller
{
    [Route("api/v1/post")]
    [AllowAnonymous]
    public class PostController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("review")]
        [Authorize]
        public async Task<IActionResult> SendReview([FromBody] PostInputDto inputDto)
        {
            var followerUun = Convert.ToInt32(GetClaimsByName("UUN"));
            try
            {
                return Ok(await _postService.SendPost(followerUun, inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("comment")]
        [Authorize]
        public async Task<IActionResult> SendComment([FromBody] PostInputDto inputDto)
        {
            var followerUun = Convert.ToInt32(GetClaimsByName("UUN"));
            try
            {
                return Ok(await _postService.SendPost(followerUun, inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("qout")]
        [Authorize]
        public async Task<IActionResult> SendQuotation([FromBody] PostInputDto inputDto)
        {
            var followerUun = Convert.ToInt32(GetClaimsByName("UUN"));
            try
            {
                return Ok(await _postService.SendPost(followerUun, inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("feed")]
        public async Task<IActionResult> GetFeed()
        {
            var followerUun = Convert.ToInt32(GetClaimsByName("UUN"));
            try
            {
                return Ok(await _postService.GetFeed(followerUun));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> GetPostDetails(long id)
        {
            try
            {
                return Ok(await _postService.GetPostDetails(id));
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