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
        [Route("review")]
        public async Task<IActionResult> SendReview()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> SendComment()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("qout")]
        public async Task<IActionResult> SendQuotation()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("feed")]
        public async Task<IActionResult> GetFeed()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> GetPostDetails(long id)
        {
            throw new NotImplementedException();
        }
    }
}