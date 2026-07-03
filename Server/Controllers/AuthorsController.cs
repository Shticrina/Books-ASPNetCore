using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Authors;
using Server.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(AppDbContext context, IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // GET: api/Author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        var authors = await _authorService.GetAuthorsAsync();
        return Ok(authors);
    }

    // GET: api/Author/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return Ok(author);
    }

    // POST: api/Author
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorDetailsDto>> PostAuthor(CreateUpdateAuthorDto dto)
    {
        var authorDetails = await _authorService.CreateAuthorAsync(dto);
        return CreatedAtAction(
            nameof(GetAuthor),
            new { id = authorDetails.Id },
            authorDetails);
    }

    // PUT: api/Author/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, CreateUpdateAuthorDto dto)
    {
        await _authorService.UpdateAuthorAsync(id, dto);
        return NoContent();
    }


    // DELETE: api/Author/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }
}
