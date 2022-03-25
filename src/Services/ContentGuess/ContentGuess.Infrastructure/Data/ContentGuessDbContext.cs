using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Infrastructure.Data
{
    public class ContentGuessDbContext :  DbContext, IContentGuessDbContext
    {
        public ContentGuessDbContext(DbContextOptions<ContentGuessDbContext> options) : base(options)
        {

        }
        public DbSet<Content> Contents => Set<Content>();
        public DbSet<Tag> Tags => Set<Tag>();

        public DbSet<ContentType> ContentType => Set<ContentType>();

        public DbSet<Author> Author => Set<Author>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>().HasKey(prop => prop.Id);
            modelBuilder.Entity<Content>().HasMany(c => c.Tags).WithMany(u => u.Content);
            modelBuilder.Entity<Content>().HasOne(c => c.ContentInfo);
            modelBuilder.Entity<Content>().HasOne(c => c.ContentType);

            modelBuilder.Entity<ContentInfo>().HasOne(c => c.Author);

            modelBuilder.Entity<Tag>().HasKey(prop => prop.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tag>().HasMany(t => t.ChildTags).WithOne(t => t.ParentTag).HasForeignKey(t => t.ParentTagId);

            modelBuilder.HasPostgresExtension("uuid-ossp");
            base.OnModelCreating(modelBuilder);
        }
    }
}
