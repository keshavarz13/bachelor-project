using Microsoft.EntityFrameworkCore;
using Social.Models;

namespace Social.Data
{
    public class SocialDbContext : DbContext
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Read> Read { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>();
            builder.Entity<Post>();
            builder.Entity<Follow>();
            builder.Entity<Read>();
        }
    }
}