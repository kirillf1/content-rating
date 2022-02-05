using Microsoft.EntityFrameworkCore;
using Rating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Infrastructure.Data
{
    public class RatingDbContext : DbContext
    {
        public RatingDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Room> Rooms => Set<Room>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasKey(prop => prop.Id);
            modelBuilder.Entity<Room>().HasMany(room => room.Users).WithOne();
            modelBuilder.Entity<User>().ToTable("Users").HasKey(prop => prop.Id);
            modelBuilder.Entity<Content>().ToTable("Content").HasKey(prop => prop.Id);
            modelBuilder.Entity<Room>().HasMany(room => room.Contents).WithOne();
            base.OnModelCreating(modelBuilder);
        }
    }
}
