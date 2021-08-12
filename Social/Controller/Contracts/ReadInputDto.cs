namespace Social.Controller.Contracts
{
    public class ReadInputDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public short Score { get; set; }
        public int Page { get; set; }
        public string ReadStatus { get; set; }
    }
}