using Shared.DTOs.Authors;

namespace Server.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAuthorsAsync();

    Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id);

    Task<AuthorDetailsDto> CreateAuthorAsync(CreateUpdateAuthorDto dto);

    Task<bool> UpdateAuthorAsync(int id, CreateUpdateAuthorDto dto);

    Task<bool> DeleteAuthorAsync(int id);
}