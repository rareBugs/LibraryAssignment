using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResults <Author>>
    {
        private readonly IGenericRepository<Author> _genericRepository;

        public UpdateAuthorCommandHandler(IGenericRepository<Author> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<OperationResults<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = await _genericRepository.GetByIdAsync(request.AuthorId);

            if (authorToUpdate == null)
                return OperationResults<Author>.FailureResult("Author not found.");

            authorToUpdate.FirstName = request.UpdateAuthorDto.FirstName;
            authorToUpdate.LastName = request.UpdateAuthorDto.LastName;

            await _genericRepository.UpdateAsync(authorToUpdate);

            return OperationResults<Author>.SuccessResult(authorToUpdate);
        }
    }
}