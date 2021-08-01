namespace Notification.Models
{
    public class Sms : NotificationBase
    {
        public string ReceiverPhoneNumber { get; set; }
        public SmsStatus SmsStatus { get; set; }
    }
    public enum SmsStatus
    {
        Pending = 1,
        Sent = 2,
        Failed = 3
    }
}