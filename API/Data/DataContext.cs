using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.Helpers;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Family> Families { get; set; }
        public DbSet<Snitch> Snitches { get; set; }
        public DbSet<Rich> RichEntries { get; set; }
        public DbSet<SnitchPoll> SnitchPolls { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(a => a.HasIndex(u => u.Username).IsUnique());
            //Write Fluent API configurations here

            builder.ApplyUtcDateTimeConverter();
        }
    }
}