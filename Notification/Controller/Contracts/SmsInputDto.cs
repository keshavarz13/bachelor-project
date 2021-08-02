using System;
using Notification.Models;

namespace Notification.Controller.Contracts
{
    public class SmsInputDto
    {
        public string Content { get; set; }
        public string SubjectId { get; set; }
        public string Subject { get; set; }
        public string ReceiverPhoneNumber { get; set; }
    }
}