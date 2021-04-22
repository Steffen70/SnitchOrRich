using API.Entities;
using API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SnitchPollAppUser>().HasKey(sp => new { sp.SnitchPollId, sp.AppUserId });

            builder.Entity<SnitchPollAppUser>()
                .HasOne(sp => sp.SnitchPoll)
                .WithMany(sp => sp.SnitchPollAppUsers)
                .HasForeignKey(sp => sp.SnitchPollId);

            builder.Entity<SnitchPollAppUser>()
                .HasOne(sp => sp.AppUser)
                .WithMany(au => au.SnitchPollAppUsers)
                .HasForeignKey(sp => sp.AppUserId);
                
            //Write Fluent API configurations here

            builder.ApplyUtcDateTimeConverter();
        }
    }
}