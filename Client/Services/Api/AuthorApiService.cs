using System.Net.Http.Json;
using Client.Interfaces.Api;
using Shared.DTOs.Authors;

namespace Client.Services.Api;

public class AuthorApiService : IAuthorApiService
{
    private readonly HttpClient _http;

    public AuthorApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AuthorDto>> GetAuthorsAsync()
    {
        return await _http.GetFromJsonAsync<List<AuthorDto>>("api/authors")
            ?? new List<AuthorDto>();
    }

    public async Task<AuthorDetailsDto> CreateAuthorAsync(CreateUpdateAuthorDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/authors", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthorDetailsDto>()
            ?? throw new InvalidOperationException("Failed to create author");
    }

    public async Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<AuthorDetailsDto?>($"api/authors/{id}");
    }

    public async Task<bool> UpdateAuthorAsync(int id, CreateUpdateAuthorDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/authors/{id}", dto);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/authors/{id}");
        response.EnsureSuccessStatusCode();
        return true;
    }
}