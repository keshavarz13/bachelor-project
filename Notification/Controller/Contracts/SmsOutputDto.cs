using System;
using Notification.Models;

namespace Notification.Controller.Contracts
{
    public class SmsOutputDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ReceivingTime { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string SmsStatus { get; set; }
    }
}