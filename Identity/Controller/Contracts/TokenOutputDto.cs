using System;

namespace Identity.Controller.Contracts
{
    public class TokenOutputDto
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}