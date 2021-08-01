namespace Notification.Models
{
    public class Email : NotificationBase
    {
        public string ReceiverEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public EmailStatus EmailStatus { get; set; }
    }

    public enum EmailStatus
    {
        Pending = 1,
        Sent = 2,
        Failed = 3
    }
}