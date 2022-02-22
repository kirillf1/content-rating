using Microsoft.EntityFrameworkCore;
using Rating.Domain;
using Rating.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Infrastructure.Data
{
    public class RatingDbContext : DbContext, IRatingDbContext
    {
        public RatingDbContext(DbContextOptions<RatingDbContext> options) : base(options)
        {

        }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<UserContentRating> UserContentRatings => Set<UserContentRating>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasKey(prop => prop.Id);
            modelBuilder.Entity<Room>().HasMany(room => room.Users).WithMany(u=>u.Rooms);
            modelBuilder.Entity<Room>().HasMany(room => room.Contents).WithOne().OnDelete(DeleteBehavior.Cascade);
           
            modelBuilder.Entity<User>().ToTable("Users").HasKey(prop => prop.Id);
            modelBuilder.Entity<User>().HasMany(u => u.RatedContent);
            modelBuilder.Entity<User>().Property(u=>u.UserType).HasConversion<string>().HasMaxLength(50);

            modelBuilder.Entity<Content>().ToTable("Content").HasKey(prop => prop.Id);
            modelBuilder.Entity<Content>().HasMany(c => c.RatedByUsers);

            modelBuilder.Entity<UserContentRating>().HasKey(u => new { u.UserId, u.ContentId });
            modelBuilder.Entity<UserContentRating>().HasOne(u => u.User).WithMany(u=>u.RatedContent).HasForeignKey(u=>u.UserId);
            modelBuilder.Entity<UserContentRating>().HasOne(u => u.Content).WithMany(u => u.RatedByUsers).HasForeignKey(u => u.ContentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
