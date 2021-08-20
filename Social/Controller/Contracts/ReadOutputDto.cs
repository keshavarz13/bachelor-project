using System;
using Social.Models;

namespace Social.Controller.Contracts
{
    public class ReadOutputDto
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsEdited { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserNameTitle { get; set; }
        public long BookId { get; set; }
        public string BookName { get; set; }
        public short Score { get; set; }
        public int Page { get; set; }
        public string ReadStatus { get; set; }
    }
}