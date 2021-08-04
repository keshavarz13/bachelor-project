using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Controller.Contracts;
using Notification.Services;

namespace Notification.Controller
{
    [Route("api/v1/sms")]
    [AllowAnonymous]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SmsInputDto inputDto)
        {
            return Ok(await _smsService.SendSms(inputDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get(string phoneNumber, DateTime? startCreationTime,
            DateTime? endCreationTime, DateTime? startReceivingTime, DateTime? endReceivingTime, string smsStatus)
        {
            return Ok(await _smsService.GetSmsByFilter(phoneNumber, startCreationTime, endCreationTime,
                startReceivingTime, endReceivingTime, smsStatus));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _smsService.GetSmsById(id));
        }
    }
}