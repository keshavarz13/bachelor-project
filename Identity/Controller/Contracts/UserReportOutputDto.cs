namespace Identity.Controller.Contracts
{
    public class UserReportOutputDto
    {
        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string NormalizedUserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string NormalizedEmail { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}