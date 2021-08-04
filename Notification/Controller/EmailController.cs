using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Controller.Contracts;
using Notification.Services;

namespace Notification.Controller
{
    [Route("api/v1/email")]
    [AllowAnonymous]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] EmailInputDto inputDto)
        {
            return Ok(await _emailService.SendEmail(inputDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get(string receiverEmailAddress, DateTime? startCreationTime,
            DateTime? endCreationTime, DateTime? startReceivingTime, DateTime? endReceivingTime, string emailStatus)
        {
            return Ok(await _emailService.GetEmailByFilter(receiverEmailAddress, startCreationTime, endCreationTime,
                startReceivingTime, endReceivingTime, emailStatus));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _emailService.GetEmailById(id));
        }
    }
}
