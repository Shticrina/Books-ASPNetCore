using Microsoft.EntityFrameworkCore;
using Server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Books)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId);

        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = 1,
                Name = "George Orwell",
                Bio = "English novelist"
            },
            new Author
            {
                Id = 2,
                Name = "J. R. R. Tolkien",
                Bio = "Fantasy writer"
            }
        );

        modelBuilder.Entity<Category>().HasData(
        new Category
            {
                Id = 1,
                Name = "Dystopian"
            },
            new Category
            {
                Id = 2,
                Name = "Fantasy"
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "1984",
                Isbn = "9780451524935",
                PublishedYear = 1949,
                AuthorId = 1,
                CategoryId = 1
            },
            new Book
            {
                Id = 2,
                Title = "The Hobbit",
                Isbn = "9780547928227",
                PublishedYear = 1937,
                AuthorId = 2,
                CategoryId = 2
            }
        );
    }
}