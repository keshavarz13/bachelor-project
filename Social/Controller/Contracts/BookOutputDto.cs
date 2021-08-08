using System;

namespace Social.Controller.Contracts
{
    public class BookOutputDto
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public long? AuthorId { get; set; }
        public string BookCategory { get; set; }
        public string Summery { get; set; }
    }
}