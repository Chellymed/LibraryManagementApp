using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookType> BookTypes   { get; set; }
        public DbSet<Author> Authors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(ConfigureBookBuilder);
            modelBuilder.Entity<BookType>(ConfigureBookTypeBuilder);
            modelBuilder.Entity<Author>(ConfigureAuthorBuilder);

        }
        private void ConfigureBookBuilder(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.Author).WithMany(auth => auth.Books).HasForeignKey(bk => bk.AuthorId);
            builder.HasOne(b => b.BookType).WithMany(bc => bc.Books).HasForeignKey(bk => bk.BookTypeId);
        }
        private void ConfigureBookTypeBuilder(EntityTypeBuilder<BookType> builder)
        {
            builder.HasMany(bc => bc.Books).WithOne(b => b.BookType);
        }
        private void ConfigureAuthorBuilder(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(bc => bc.Books).WithOne(b => b.Author);
        }
        
    }
}
