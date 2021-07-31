using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;  

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {
        }  
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.HasIndex(u => u.NormalizedUserName).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.NormalizedEmail).IsUnique();
                entity.HasIndex(u => u.PhoneNumber).IsUnique();
            });
        }  
    }  
}