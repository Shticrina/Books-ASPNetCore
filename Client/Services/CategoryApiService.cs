using System.Net.Http.Json;
using Client.Interfaces;
using Shared.DTOs.Categories;

namespace Client.Services;

public class CategoryApiService : ICategoryApiService
{
    private readonly HttpClient _http;

    public CategoryApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        return await _http.GetFromJsonAsync<List<CategoryDto>>("api/categories")
            ?? new List<CategoryDto>();
    }

    public async Task<CategoryDetailsDto> CreateCategoryAsync(CreateUpdateCategoryDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/categories", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CategoryDetailsDto>()
            ?? throw new InvalidOperationException("Failed to create category");
    }

    public async Task<CategoryDetailsDto?> GetCategoryByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<CategoryDetailsDto?>($"api/categories/{id}");
    }

    public async Task<bool> UpdateCategoryAsync(int id, CreateUpdateCategoryDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/categories/{id}", dto);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/categories/{id}");
        response.EnsureSuccessStatusCode();
        return true;
    }
}