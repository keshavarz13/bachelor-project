using System.ComponentModel.DataAnnotations;

namespace Identity.Controller.Contracts
{
    public class RegisterInputDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
    }
}