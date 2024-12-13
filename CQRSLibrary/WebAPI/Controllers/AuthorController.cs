using Application.Authors.Commands.Commands.DeleteAuthor;
using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAllauthors;
using Application.Authors.Queries.GetAuthorById;
using Application.DTOs.AuthorDto;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Author/GetAllAuthors
        [Authorize]
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(authors);
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/Author/GetAuthorById
        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            try
            {
                var author = await _mediator.Send(new GetAuthorByIdQuery(authorId));
                return Ok(author);
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/Author/CreateAuthor
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            try
            {
                if (createAuthorDto == null)
                    return BadRequest();

                var authorToAdd = new Author(createAuthorDto.FirstName, createAuthorDto.LastName);
                var createdAuthor = await _mediator.Send(new CreateAuthorCommand(authorToAdd));

                return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/Author/DeleteAuthor
        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAuthorCommand(id));
                return Ok(result);
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/Author/UpdateAuthor
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateAuthorCommand(id, updateAuthorDto));
                return Ok(result);
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            // Log the exception (not implemented here)
            return StatusCode((int)HttpStatusCode.InternalServerError, new { error = ex.Message });
        }
    }
}