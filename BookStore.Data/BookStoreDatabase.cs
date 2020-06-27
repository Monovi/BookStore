using BookStore.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BookStore.Data
{
    public class BookStoreDatabase : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Book> Books { get; set; }


        public BookStoreDatabase()
        {

        }

        public BookStoreDatabase(DbContextOptions<BookStoreDatabase> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string jsonFile = $"appsettings.{environment}.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: jsonFile, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("BookStoreContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(e =>
            {
                e.ToTable("Author");
                e.HasKey(x => x.Id);

                e.Property(x => x.AuthorName).IsRequired().HasMaxLength(200);
                e.Property(x => x.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Publisher>(e =>
            {
                e.ToTable("Publisher");
                e.HasKey(x => x.Id);

                e.Property(x => x.PublisherName).IsRequired().HasMaxLength(300);
                e.Property(x => x.PublisherAddress).HasMaxLength(500);
                e.Property(x => x.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Book>(e =>
            {
                e.ToTable("Book");
                e.HasKey(x => x.Id);

                e.Property(x => x.BookTitle).IsRequired().HasMaxLength(500);
                e.Property(x => x.IsDeleted).HasDefaultValueSql("((0))");

                e.HasOne(x => x.Author)
                    .WithMany()
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Publisher)
                    .WithMany()
                    .HasForeignKey(x => x.PublisherId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
