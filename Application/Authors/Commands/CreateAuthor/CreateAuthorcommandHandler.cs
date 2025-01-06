using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorcommandHandler : IRequestHandler<CreateAuthorCommand, OperationResults<Author>>
    {
        private readonly IGenericRepository<Author> _genericRepository;

        public CreateAuthorcommandHandler(IGenericRepository<Author> genericRepository)
        {
            _genericRepository = genericRepository;
        }


        public async Task<OperationResults<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.CreateAuthorDto.FirstName) || string.IsNullOrEmpty(request.CreateAuthorDto.LastName))
                return OperationResults<Author>.FailureResult("First or last name empty.");
            

            var newAuthor = new Author(request.CreateAuthorDto.FirstName, request.CreateAuthorDto.LastName);

            await _genericRepository.AddAsync(newAuthor);

            return OperationResults<Author>.SuccessResult(newAuthor);
        }
    }
}