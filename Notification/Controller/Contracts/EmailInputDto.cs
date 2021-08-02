namespace Notification.Controller.Contracts
{
    public class EmailInputDto
    {
        public string Content { get; set; }
        public string SubjectId { get; set; }
        public string Subject { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string EmailSubject { get; set; }
    }
}