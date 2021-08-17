using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser: IdentityUser  
    {  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserUniqueNumber { get; set; }
        public string Name { get; set; }
    }  
}