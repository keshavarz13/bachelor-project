using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Controller.Contracts;
using Social.Services;

namespace Social.Controller
{
    [Route("api/v1/reads")]
    public class ReadController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IReadService _readService;

        public ReadController(IReadService readService)
        {
            _readService = readService;
        }


        [HttpGet]
        [Route("user/{uun}")]
        public async Task<IActionResult> GetUserReads(int uun)
        {
            try
            {
                return Ok(await _readService.GetUserReads(uun));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("book/{bookId}")]
        public async Task<IActionResult> GetBookReads(int bookId)
        {
            try
            {
                return Ok(await _readService.GetBookReads(bookId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReads([FromBody]ReadInputDto inputDto)
        {
            try
            {
                return Ok(await _readService.AddReadStatus(inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateReads([FromBody]ReadInputDto inputDto)
        {
            try
            {
                return Ok(await _readService.UpdateReadStatus(inputDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}