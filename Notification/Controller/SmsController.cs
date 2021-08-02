using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Models;
using Notification.Repository;
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
        public async Task<IActionResult> Send([FromBody] Sms inputDto)
        {
            return Ok(await _smsService.SendSms(inputDto));
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _smsService.GetSms());
        }
    }
}