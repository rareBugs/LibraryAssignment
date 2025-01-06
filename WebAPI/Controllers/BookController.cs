using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetAllBooks;
using Application.Books.Queries.GetBookById;
using Application.DTOs.BookDto;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger <BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Book/GetAllBooks
        [HttpGet("GetAllBooks")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllBooks()
        {
            _logger.LogInformation("Start processing GetAllBooks request.");
            try
            {
                var result = await _mediator.Send(new GetAllBooksQuery());
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved books. Count: {BookCount}", result.Data.Count);
                    return Ok(new { message = result.Message, data = result.Data });
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve books. Reason: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while retrieving books.");
                return HandleException(ex);
            }
        }

        // GET: api/Book/GetBookById
        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            _logger.LogInformation("Processing request to retrieve book with ID: {BookId}", bookId);
            try
            {
                var result = await _mediator.Send(new GetBookbyIdQuery(bookId));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved book with ID: {BookId}", bookId);
                    return Ok(new { message = result.Message, data = result.Data });
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve book with ID: {BookId}. Reason: {ErrorMessage}", bookId, result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving book with ID: {BookId}", bookId);
                return HandleException(ex);
            }
        }

        // POST: api/Book/CreateBook
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand createBookCommand)
        {
            _logger.LogInformation("Processing request to create a new book.");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("CreateBook: The provided book data is invalid.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(createBookCommand);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully created a new book with title: {BookTitle}", createBookCommand.Title);
                    return CreatedAtAction(nameof(GetBookById), new { bookId = result.Data.Id }, result.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to create a new book. Reason: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new book.");
                return HandleException(ex);
            }
        }

        // DELETE: api/Book/DeleteBook
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            _logger.LogInformation("Processing request to delete a book with ID: {BookId}", id);
            try
            {
                var result = await _mediator.Send(new DeleteBookCommand(id));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully deleted book with ID: {BookId}", id);
                    return Ok(new { message = result.Message });
                }
                else
                {
                    _logger.LogWarning("Failed to delete book with ID: {BookId}. Reason: {ErrorMessage}", id, result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting book with ID: {BookId}", id);
                return HandleException(ex);
            }
        }

        // PUT: api/Book/UpdateBook
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto updateBookDto)
        {
            _logger.LogInformation("Processing request to update book with ID: {BookId}", id);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("UpdateBook: The provided book data is invalid.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(new UpdateBookCommand(id, updateBookDto));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully updated book with ID: {BookId}", id);
                    return Ok(new { message = result.Message });
                }
                else
                {
                    _logger.LogWarning("Failed to update book with ID: {BookId}. Reason: {ErrorMessage}", id, result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating book with ID: {BookId}", id);
                return HandleException(ex);
            }
        }

        // Private error handler
        private IActionResult HandleException(Exception ex)
        {
            _logger.LogError("Internal server error: {ExceptionMessage}", ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { error = ex.Message });
        }
    }
}