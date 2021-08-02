using System;

namespace Notification.Controller.Contracts
{
    public class EmailOutputDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ReceivingTime { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailStatus { get; set; }
    }
}