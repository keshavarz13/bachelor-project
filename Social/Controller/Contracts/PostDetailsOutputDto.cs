using System;
using System.Collections.Generic;

namespace Social.Controller.Contracts
{
    public class PostDetailsOutputDto
    {
        public long Id { get; set; }
        public long CreatorUserId { get; set; }
        public string CreatorUserIdName { get; set; }
        public DateTime CreationTime { get; set; }
        public string Context { get; set; }
        public string PostType { get; set; }
        public long RelatedBook { get; set; }
        public string RelatedBookName { get; set; }
        public string Name { get; set; }
        public List<CommentOutputDto> Comments { get; set; }
    }
}