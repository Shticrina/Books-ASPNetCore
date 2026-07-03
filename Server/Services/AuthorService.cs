using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.DTOs.Authors;
using Server.Interfaces;

namespace Server.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(AppDbContext context, ILogger<AuthorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<AuthorDto>> GetAuthorsAsync()
    {
        var authors = await _context.Authors
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio
            })
            .ToListAsync();

        _logger.LogInformation("Authors retrieved successfully.");
        return authors;
    }

    public async Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id)
    {
        var findAuthor = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == id);
        var author = ValidateAuthorExists(findAuthor, id);

        var authorDetails = new AuthorDetailsDto
        {
            Id = author.Id,
            Name = author.Name,
            Bio = author.Bio
        };

        _logger.LogInformation("Author details retrieved successfully.");
        return authorDetails;
    }

    public async Task<AuthorDetailsDto> CreateAuthorAsync(CreateUpdateAuthorDto dto)
    {
        var newAuthor = new Author
        {
            Name = dto.Name,
            Bio = dto.Bio
        };

        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Author '{Name}' created successfully with ID {AuthorId}.",
            newAuthor.Name,
            newAuthor.Id);
        return await GetAuthorByIdAsync(newAuthor.Id) ?? throw new NotFoundException("Failed to retrieve the newly created author.");
    }

    public async Task<bool> UpdateAuthorAsync(int id, CreateUpdateAuthorDto dto)
    {
        var findAuthor = await _context.Authors.FindAsync(id);
        var author = ValidateAuthorExists(findAuthor, id);

        author.Name = dto.Name;
        author.Bio = dto.Bio;
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Author with ID {AuthorId} updated successfully.",
            author.Id);
        return true;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var findAuthor = await _context.Authors.FindAsync(id);
        var author = ValidateAuthorExists(findAuthor, id);

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Author with ID {AuthorId} deleted successfully.", author.Id);
        return true;
    }

    private Author ValidateAuthorExists(Author? author, int id)
    {
        if (author is null)
        {
            _logger.LogWarning(
                "Author with ID {AuthorId} was not found.",
                id);

            throw new NotFoundException("Author does not exist.");
        }

        return author;
    }
}