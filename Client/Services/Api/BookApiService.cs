using Shared.DTOs.Books;
using Client.Interfaces.Api;
using System.Net.Http.Json;
using Shared.Responses;

namespace Client.Services.Api;

public class BookApiService: BaseApiService, IBookApiService
{
    private readonly HttpClient _http;

    public BookApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        return await GetAsync<List<BookDto>>(_http, "api/books");
    }

    public async Task<BookDetailsDto?> GetBookByIdAsync(int id)
    {
        return await GetAsync<BookDetailsDto>(_http, $"api/books/{id}");
    }

    public async Task<BookDetailsDto> CreateBookAsync(CreateBookDto dto)
    {
        return await PostAsync<CreateBookDto, BookDetailsDto>(
            _http,
            "api/books",
            dto);
    }

    public async Task UpdateBookAsync(int id, UpdateBookDto dto)
    {
        await PutAsync(
            _http,
            $"api/books/{id}",
            dto);
    }

    public async Task DeleteBookAsync(int id)
    {
        await DeleteAsync(
            _http,
            $"api/books/{id}");
    }
}