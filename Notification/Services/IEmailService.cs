using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notification.Controller.Contracts;
using Notification.Models;

namespace Notification.Services
{
    public interface IEmailService
    {
        Task<EmailOutputDto> SendEmail(EmailInputDto emailInputDto);
        Task<List<EmailOutputDto>> GetEmail();
        Task<List<EmailOutputDto>> GetEmailByFilter(string receiverEmailAddress, DateTime? startCreationTime,
            DateTime? endCreationTime, DateTime? startReceivingTime, DateTime? endReceivingTime, string emailStatus);
        Task<EmailOutputDto> GetEmailById(long id);
        Task<Email> SendToProvider(Email email);
        Task UpdateStatus(long id);
        Task UpdateUnsentEmail();
    }
}