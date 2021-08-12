using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Social.Models
{
    public class Read
    {
        [Key] public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public short Score { get; set; }
        public int Page { get; set; }
        public ReadStatus ReadStatus { get; set; }
    }

    public enum ReadStatus
    {
        [Description("قبلا خوندم")]
        Read,
        [Description("دارم میخونم")]
        Reading,
        [Description("دوست دارم بخونم")]
        Interested
    }
}