using Microsoft.EntityFrameworkCore;

namespace Notification.Data
{
    public class NotificationDbContext : DbContext  
    {  
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)  
        {
        }  
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);
        }  
    }  
}