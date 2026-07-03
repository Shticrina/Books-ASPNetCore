using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.DTOs.Categories;
using Server.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/Category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDetailsDto>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    // POST: api/Category
    [HttpPost]
    public async Task<ActionResult<CategoryDetailsDto>> PostCategory(CreateUpdateCategoryDto dto)
    {
        var categoryDetails = await _categoryService.CreateCategoryAsync(dto);
        return CreatedAtAction(
            nameof(GetCategory),
            new { id = categoryDetails.Id },
            categoryDetails);
    }

    // PUT: api/Category/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, CreateUpdateCategoryDto dto)
    {
        await _categoryService.UpdateCategoryAsync(id, dto);
        return NoContent();
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
       return NoContent();
    }
}