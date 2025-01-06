using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, OperationResults<Author>>
    {
        private readonly IGenericRepository<Author> _genericRepository;

        public GetAuthorByIdQueryHandler(IGenericRepository<Author> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<OperationResults<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var foundAuthor = await _genericRepository.GetByIdAsync(request.AuthorId);

            if (foundAuthor == null)
                return OperationResults<Author>.FailureResult("Author not found.");
            
            return OperationResults<Author>.SuccessResult(foundAuthor);
        }
    }
}