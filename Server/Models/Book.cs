namespace Server.Models;

public class Book
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string Isbn { get; set; } = string.Empty;

    public int PublishedYear { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}