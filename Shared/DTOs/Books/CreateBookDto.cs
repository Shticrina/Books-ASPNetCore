using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Books
{
    public class CreateBookDto: IBookFormModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Title { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string Isbn { get; set; } = string.Empty;

        [Range(1700, 2029)]
        public int PublishedYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid author.")]
        public int AuthorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }
    }
}