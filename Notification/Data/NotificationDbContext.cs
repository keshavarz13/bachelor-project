using Microsoft.EntityFrameworkCore;
using Notification.Models;

namespace Notification.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {
        }

        public DbSet<Email> Email { get; set; }
        public DbSet<Sms> Sms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Email>();
            builder.Entity<Sms>();
        }
    }
}