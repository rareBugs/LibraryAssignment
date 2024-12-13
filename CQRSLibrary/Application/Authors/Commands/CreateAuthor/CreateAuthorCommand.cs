using Application.DTOs.AuthorDto;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Author>
    {
        private CreateAuthorDto createAuthorDto;

        public CreateAuthorCommand(Author authorToAdd)
        {
            NewAuthor = authorToAdd;
        }

        public CreateAuthorCommand(CreateAuthorDto createAuthorDto)
        {
            this.createAuthorDto = createAuthorDto;
        }

        public Author NewAuthor { get; }
    }
}