namespace Social.Controller.Contracts
{
    public class BookInputDto
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public long? AuthorId { get; set; }
        public string BookCategory { get; set; }
        public string Summery { get; set; }
    }
}