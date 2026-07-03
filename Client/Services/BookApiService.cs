using Shared.DTOs.Books;
using Client.Interfaces;
using System.Net.Http.Json;
using Shared.Responses;

namespace Client.Services;

public class BookApiService: BaseApiService, IBookApiService
{
    private readonly HttpClient _http;

    public BookApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<List<BookDto>>>("api/books");

        HandleResponse(response);

        return response!.Data ?? [];
    }

    public async Task<BookDetailsDto> CreateBookAsync(CreateBookDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/books", dto);

        var apiResponse = await response.Content
            .ReadFromJsonAsync<ApiResponse<BookDetailsDto>>();

        HandleResponse(apiResponse);

        return apiResponse!.Data!;
    }

    public async Task DeleteBookAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/books/{id}");
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

        HandleResponse(apiResponse);
    }

    public async Task<BookDetailsDto?> GetBookByIdAsync(int id)
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<BookDetailsDto>>($"api/books/{id}");

        HandleResponse(response);

        return response!.Data;
    }
    public async Task UpdateBookAsync(int id, UpdateBookDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/books/{id}", dto);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

        HandleResponse(apiResponse);
    }
}