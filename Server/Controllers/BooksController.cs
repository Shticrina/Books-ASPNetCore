using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Books;
using Server.Interfaces;
using Shared.Responses;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<BookDto>>>> GetBooks()
    {
        var books = await _service.GetBooksAsync();

        return Ok(new ApiResponse<List<BookDto>>
        {
            Success = true,
            Data = books
        });
    }

    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BookDetailsDto>>> GetBook(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(new ApiResponse<BookDetailsDto>
        {
            Success = true,
            Data = book
        });
    }

    // POST: api/Book
    [HttpPost]
    public async Task<ActionResult<ApiResponse<BookDetailsDto>>> PostBook(CreateBookDto dto)
    {
        var book = await _service.CreateBookAsync(dto);

        return CreatedAtAction(
            nameof(GetBook),
            new { id = book.Id },
            new ApiResponse<BookDetailsDto>
            {
                Success = true,
                StatusCode = 201,
                Message = "Book created successfully.",
                Data = book
            });
    }
    
    // PUT: api/Book/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> PutBook(int id, UpdateBookDto dto)
    {
        await _service.UpdateBookAsync(id, dto);
        return Ok(new ApiResponse<bool>
        {
            Success = true
        });
    }

    // DELETE: api/Book/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBook(int id)
    {
        await _service.DeleteBookAsync(id);
        return Ok(new ApiResponse<bool>
        {
            Success = true
        });
    }
}