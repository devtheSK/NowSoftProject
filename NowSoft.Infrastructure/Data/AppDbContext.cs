using Microsoft.EntityFrameworkCore;
using NowSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.Property(u => u.Balance).HasPrecision(18, 2); // Decimal precision
            });

            // Configure UserLoginHistory
            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.HasOne(ulh => ulh.User)
                    .WithMany()
                    .HasForeignKey(ulh => ulh.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
