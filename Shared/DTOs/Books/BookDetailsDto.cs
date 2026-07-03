using Shared.DTOs.Authors;
using Shared.DTOs.Categories;

namespace Shared.DTOs.Books
{
    public class BookDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public AuthorDto Author { get; set; } = new();

        public CategoryDto Category { get; set; } = new();
    }
}