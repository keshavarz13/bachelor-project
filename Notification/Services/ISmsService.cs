using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notification.Controller.Contracts;
using Notification.Models;

namespace Notification.Services
{
    public interface ISmsService
    {
        Task<SmsOutputDto> SendSms(Sms sms);
        Task<List<SmsOutputDto>> GetSms();
        Task<List<SmsOutputDto>> GetSmsByFilter(string phoneNumber, DateTime? startCreationTime,
            DateTime? endCreationTime, DateTime? startReceivingTime, DateTime? endReceivingTime, string smsStatus);
        Task<SmsOutputDto> GetSmsById(long id);
    }
}