using Shared.DTOs.Books;

namespace Server.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetBooksAsync();

    Task<BookDetailsDto?> GetBookByIdAsync(int id);

    Task<BookDetailsDto> CreateBookAsync(CreateBookDto dto);

    Task<bool> UpdateBookAsync(int id, UpdateBookDto dto);

    Task<bool> DeleteBookAsync(int id);
}