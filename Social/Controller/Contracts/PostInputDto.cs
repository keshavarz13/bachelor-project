using Social.Models;

namespace Social.Controller.Contracts
{
    public class PostInputDto
    {
        public string Context { get; set; }
        public PostType PostType { get; set; }
        public long? RelatedPost { get; set; }
        public long? RelatedBook { get; set; }
    }
}