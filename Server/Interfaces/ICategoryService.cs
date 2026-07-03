using Shared.DTOs.Categories;

namespace Server.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();

    Task<CategoryDetailsDto?> GetCategoryByIdAsync(int id);

    Task<CategoryDetailsDto> CreateCategoryAsync(CreateUpdateCategoryDto dto);

    Task<bool> UpdateCategoryAsync(int id, CreateUpdateCategoryDto dto);

    Task<bool> DeleteCategoryAsync(int id);
}