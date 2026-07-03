using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Categories
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
    }
}