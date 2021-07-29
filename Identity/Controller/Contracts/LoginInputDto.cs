using System.ComponentModel.DataAnnotations;

namespace Identity.Controller.Contracts
{
    public class LoginInputDto  
    {  
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
    }
}