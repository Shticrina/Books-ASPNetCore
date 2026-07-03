using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Authors
{
    public class CreateUpdateAuthorDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, MinimumLength = 2)]
        public string Bio { get; set; } = string.Empty;
    }
}