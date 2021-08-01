using System;
using System.ComponentModel.DataAnnotations;

namespace Notification.Models
{
    public class NotificationBase
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ReceivingTime { get; set; }
        [MaxLength(100)]
        public string SubjectId { get; set; }
        [MaxLength(100)]
        public string Subject { get; set; }
    }
}