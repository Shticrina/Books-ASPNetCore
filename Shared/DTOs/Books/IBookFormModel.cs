namespace Shared.DTOs.Books;

public interface IBookFormModel
{
    string Title { get; set; }

    string Isbn { get; set; }

    int PublishedYear { get; set; }

    int AuthorId { get; set; }

    int CategoryId { get; set; }
}