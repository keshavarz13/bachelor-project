namespace Social.Controller.Contracts
{
    public class FollowBasicInfoOutputDto
    {
        public int FollowingsCount { get; set; }
        public int FollowersCount { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Rules { get; set; }
        public int UserId { get; set; }
    }
}