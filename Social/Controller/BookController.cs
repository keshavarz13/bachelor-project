using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetBooks(string name, DateTime? startCreationTime, DateTime? endCreationTime,
            int authorId)
        {
            try
            {
                return Ok(await _bookService.GetBookByFilter(name,startCreationTime,endCreationTime, authorId));
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
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookId(long id)
        {
            try
            {
                return Ok(await _bookService.GetBookById(id));
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

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                return Ok(_bookService.GetCategories());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}