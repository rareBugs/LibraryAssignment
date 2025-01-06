using Application.DTOs.AuthorDto;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<OperationResults<Author>>
    {
        public Author NewAuthor { get; }

        public CreateAuthorDto CreateAuthorDto;

        public CreateAuthorCommand(CreateAuthorDto craeteAuthor)
        {
            CreateAuthorDto = craeteAuthor;
        }        
    }
}