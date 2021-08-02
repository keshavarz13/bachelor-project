using System.Collections.Generic;
using System.Threading.Tasks;
using Notification.Models;

namespace Notification.Services
{
    public interface ISmsService
    {
        Task<Sms> SendSms(Sms sms);
        Task<List<Sms>> GetSms();
    }
}