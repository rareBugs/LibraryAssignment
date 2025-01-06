using Application.Authors.Commands.Commands.DeleteAuthor;
using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAllauthors;
using Application.Authors.Queries.GetAuthorById;
using Application.DTOs.AuthorDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMediator _mediator;
        

        public AuthorController(ILogger<AuthorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/Author/GetAllAuthors
        [HttpGet("GetAllAuthors")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllAuthors()
        {
            _logger.LogInformation("Retrieving authors from the database, please wait.");
            try
            {
                var result = await _mediator.Send(new GetAllAuthorsQuery());
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved authors from the database: {AuthorCount}", result.Data.Count);
                    return Ok(new { message = result.Message, data = result.Data });
                }
                else
                {
                    _logger.LogError("Error occurred while retrieving authors: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occurred while retrieving authors.");
                return HandleException(ex);
            }
        }

        // GET: api/Author/GetAuthorById
        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            try
            {
                var result = await _mediator.Send(new GetAuthorByIdQuery(authorId));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved author with ID: {AuthorId}", authorId);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve author with ID: {AuthorId}. Reason: {ErrorMessage}", authorId, result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occurred while retrieving author with ID: {AuthorId}", authorId);
                return HandleException(ex);
            }
        }

        // POST: api/Author/CreateAuthor
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            _logger.LogInformation("Processing request to create a new author.");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("CreateAuthorDto is invalid.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(new CreateAuthorCommand(createAuthorDto));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully created a new author with ID: {AuthorId}", result.Data.Id);
                    return CreatedAtAction(nameof(GetAuthorById), new { authorId = result.Data.Id }, result.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to create a new author. Reason: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occurred while creating a new author.");
                return HandleException(ex);
            }
        }

        // DELETE: api/Author/DeleteAuthor
        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            _logger.LogInformation("Processing request to delete author with ID: {AuthorId}", id);
            try
            {
                var result = await _mediator.Send(new DeleteAuthorCommand(id));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully deleted author with ID: {AuthorId}", id);
                    return Ok(new { message = result.Message });
                }
                else
                {
                    _logger.LogWarning("Failed to delete author with ID: {AuthorId}. Reason: {ErrorMessage}", id, result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occurred while deleting author with ID: {AuthorId}", id);
                return HandleException(ex);
            }
        }

        // PUT: api/Author/UpdateAuthor
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            _logger.LogInformation("Processing request to update author with ID: {AuthorId}", id);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("UpdateAuthorDto is invalid.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(new UpdateAuthorCommand(id, updateAuthorDto));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully updated author with ID: {AuthorId}", id);
                    return Ok(new { message = result.Message });
                }
                else
                {
                    _logger.LogWarning("Failed to update author with ID: {AuthorId}. Reason: {ErrorMessage}", id, result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error occurred while updating author with ID: {AuthorId}", id);
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