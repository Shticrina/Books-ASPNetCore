using Shared.DTOs.Authors;

namespace Client.Interfaces;

public interface IAuthorApiService
{
    Task<List<AuthorDto>> GetAuthorsAsync();

    Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id);

    Task<AuthorDetailsDto> CreateAuthorAsync(CreateUpdateAuthorDto dto);

    Task<bool> UpdateAuthorAsync(int id, CreateUpdateAuthorDto dto);

    Task<bool> DeleteAuthorAsync(int id);
}