using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.DTOs.Categories;
using Server.Interfaces;

namespace Server.Services;

public class CategoryService: ICategoryService
{
    private readonly AppDbContext _db;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(AppDbContext db, ILogger<CategoryService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _db.Categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        _logger.LogInformation("Categories retrieved successfully.");
        return categories;
    }

    public async Task<CategoryDetailsDto?> GetCategoryByIdAsync(int id)
    {
        var findCategory = await _db.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
        var category = ValidateCategoryExists(findCategory, id);

        var categoryDetails = new CategoryDetailsDto
        {
            Id = category.Id,
            Name = category.Name
        };

        _logger.LogInformation("Category details retrieved successfully.");
        return categoryDetails;
    }

    public async Task<CategoryDetailsDto> CreateCategoryAsync(CreateUpdateCategoryDto dto)
    {
        var newCategory = new Category
        {
            Name = dto.Name
        };

        _db.Categories.Add(newCategory);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Category '{Name}' created successfully with ID {CategoryId}.",
            newCategory.Name,
            newCategory.Id);
        return await GetCategoryByIdAsync(newCategory.Id) ?? throw new NotFoundException("Failed to retrieve the newly created category.");
    }

    public async Task<bool> UpdateCategoryAsync(int id, CreateUpdateCategoryDto dto)
    {
        var findCategory = await _db.Categories.FindAsync(id);
        var category = ValidateCategoryExists(findCategory, id);

        category.Name = dto.Name;
        await _db.SaveChangesAsync();

        _logger.LogInformation("Category with ID {CategoryId} updated successfully.", category.Id);
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var findCategory = await _db.Categories.FindAsync(id);
        var category = ValidateCategoryExists(findCategory, id);

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Category with ID {CategoryId} deleted successfully.", category.Id);
        return true;
    }

    private Category ValidateCategoryExists(Category? category, int id)
    {
        if (category is null)
        {
            _logger.LogWarning(
                "Category with ID {CategoryId} was not found.",
                id);

            throw new NotFoundException("Category does not exist.");
        }

        return category;
    }
}