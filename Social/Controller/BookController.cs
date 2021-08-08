using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Social.Controller
{
    [Route("api/v1/book")]
    [AllowAnonymous]
    public class BookController
    {
        public BookController()
        {
        }
        
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddBook()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> EditBook()
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete]
        public async Task<IActionResult> RemoveBook()
        {
            throw new NotImplementedException();
        }
    }
}