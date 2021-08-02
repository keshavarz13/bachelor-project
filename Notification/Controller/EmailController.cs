using Notification.Services;

namespace Notification.Controller
{
    public class EmailController
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
    }
}