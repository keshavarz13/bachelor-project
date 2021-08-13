using System.Collections;

namespace Social.Controller.Contracts
{
    public class UserReportOutputDto
    {
        public int UserUniqueNumber { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public IList Roles { get; set; }
    }
}