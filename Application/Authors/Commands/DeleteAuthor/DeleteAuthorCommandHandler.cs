using Application.Authors.Commands.Commands.DeleteAuthor;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler <DeleteAuthorCommand, OperationResults<Author>>
    {
        private readonly IGenericRepository<Author> _genericRepository;

        public DeleteAuthorCommandHandler(IGenericRepository<Author> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<OperationResults<Author>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            Author authorToDelete = await _genericRepository.GetByIdAsync(request.AuthorId);

            if (authorToDelete == null)
                return OperationResults<Author>.FailureResult("Author not found.");
            

            await _genericRepository.DeleteAsync(authorToDelete);

            return OperationResults<Author>.SuccessResult(authorToDelete);
        }
    }
}