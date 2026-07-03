using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.DTOs.Authors;
using Shared.DTOs.Books;
using Shared.DTOs.Categories;
using Server.Interfaces;

namespace Server.Services;

public class BookService: IBookService
{
    private readonly AppDbContext _db;
    private readonly ILogger<BookService> _logger;

    public BookService(AppDbContext db, ILogger<BookService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        var books = await _db.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                PublishedYear = b.PublishedYear,
                AuthorName = b.Author.Name,
                CategoryName = b.Category.Name
            })
            .ToListAsync();

        _logger.LogInformation("Books retrieved successfully.");
        return books;
    }

    public async Task<BookDetailsDto?> GetBookByIdAsync(int id)
    {
        var findBook = await _db.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id);

        var book = ValidateBookExists(findBook, id);

        var bookDetails = new BookDetailsDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            PublishedYear = book.PublishedYear,
            Author = new AuthorDto
            {
                Id = book.Author.Id,
                Name = book.Author.Name,
                Bio = book.Author.Bio
            },
            Category = new CategoryDto
            {
                Id = book.Category.Id,
                Name = book.Category.Name
            }
        };

        _logger.LogInformation("Book details retrieved successfully.");
        return bookDetails;
    }

    public async Task<BookDetailsDto> CreateBookAsync(CreateBookDto dto)
    {
        await ValidateAuthorExistsAsync(dto.AuthorId);
        await ValidateCategoryExistsAsync(dto.CategoryId);

        var newBook = new Book
        {
            Title = dto.Title,
            Isbn = dto.Isbn,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId,
            CategoryId = dto.CategoryId
        };

        _db.Books.Add(newBook);
        await _db.SaveChangesAsync();
        _logger.LogInformation(
            "Book '{Title}' created successfully with ID {BookId}.",
            newBook.Title,
            newBook.Id);

        return await GetBookByIdAsync(newBook.Id) ?? throw new NotFoundException("Failed to retrieve the newly created book.");
    }

    public async Task<bool> UpdateBookAsync(int id, UpdateBookDto dto)
    {
        var findBook = await _db.Books.FindAsync(id);
        await ValidateAuthorExistsAsync(dto.AuthorId);
        await ValidateCategoryExistsAsync(dto.CategoryId);
        var book = ValidateBookExists(findBook, id);

        book.Title = dto.Title;
        book.Isbn = dto.Isbn;
        book.PublishedYear = dto.PublishedYear;
        book.AuthorId = dto.AuthorId;
        book.CategoryId = dto.CategoryId;
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Book with ID {BookId} updated successfully.",
            book.Id);
        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var findBook = await _db.Books.FindAsync(id);
        var book = ValidateBookExists(findBook, id);

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Book with ID {BookId} deleted successfully.", book.Id);
        return true;
    }

    private async Task ValidateAuthorExistsAsync(int authorId)
    {
        var authorExists =
            await _db.Authors.AnyAsync(a => a.Id == authorId);

        if (!authorExists)
        {
            _logger.LogWarning(
                "Author with ID {AuthorId} was not found.",
                authorId);
            throw new BadRequestException("Author does not exist.");
        }
    }

    private async Task ValidateCategoryExistsAsync(int categoryId)
    {
        var categoryExists =
            await _db.Categories.AnyAsync(c => c.Id == categoryId);

        if (!categoryExists)
        {
            _logger.LogWarning(
            "Category with ID {CategoryId} was not found.",
                categoryId);
            throw new BadRequestException("Category does not exist.");
        }
    }

    private Book ValidateBookExists(Book? book, int id)
    {
        if (book is null)
        {
            _logger.LogWarning(
                "Book with ID {BookId} was not found.",
                id);

            throw new NotFoundException("Book does not exist.");
        }

        return book;
    }
}