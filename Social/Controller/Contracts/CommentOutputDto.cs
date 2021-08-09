using System;

namespace Social.Controller.Contracts
{
    public class CommentOutputDto
    {
        public long Id { get; set; }
        public long CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public string Context { get; set; }
    }
}