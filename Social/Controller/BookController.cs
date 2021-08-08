using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Controller.Contracts;
using Social.Services;

namespace Social.Controller
{
    [Route("api/v1/book")]
    [AllowAnonymous]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                return Ok(await _bookService.GetBooks());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookInputDto inputDto)
        {
            try
            {
                return Ok(await _bookService.AddNewBook(inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditBook(long id, [FromBody]BookInputDto inputDto)
        {
            try
            {
                return Ok(await _bookService.UpdateBook(id, inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete]
        public async Task<IActionResult> RemoveBook()
        {
            throw new NotImplementedException();
        }
    }
}