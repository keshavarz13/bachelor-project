using System;
using System.ComponentModel.DataAnnotations;

namespace Social.Models
{
    public class Post
    {
        [Key] public long Id { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Context { get; set; }
        public PostType PostType { get; set; }
        public long RelatedPost { get; set; }
        public long RelatedBook { get; set; }
    }

    public enum PostType
    {
        Review = 1,
        Quotation = 2,
        Comment = 3
    }
}