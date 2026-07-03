using Shared.DTOs.Books;

namespace Client.Interfaces;

public interface IBookApiService
{
    Task<List<BookDto>> GetBooksAsync();
    Task<BookDetailsDto?> GetBookByIdAsync(int id);
    Task<BookDetailsDto> CreateBookAsync(CreateBookDto dto);
    Task UpdateBookAsync(int id, UpdateBookDto dto);
    Task DeleteBookAsync(int id);
}