using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs.Books
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;
    }
} 