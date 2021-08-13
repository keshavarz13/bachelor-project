using System;

namespace Social.Controller.Contracts
{
    public class PostOutputDto
    {
        public long Id { get; set; }
        public long CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public string Context { get; set; }
        public string PostType { get; set; }
        public long RelatedBook { get; set; }
        public string RelatedBookName { get; set; }
    }
}