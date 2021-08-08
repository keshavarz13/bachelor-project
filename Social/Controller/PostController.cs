using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Social.Controller
{
    [Route("api/v1/post")]
    [AllowAnonymous]
    public class PostController
    {
        public PostController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> SendReview()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> SendComment()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> SendQuotation()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetFeed()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetPostDetails()
        {
            throw new NotImplementedException();
        }
    }
}