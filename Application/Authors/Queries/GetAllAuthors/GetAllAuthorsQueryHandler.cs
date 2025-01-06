using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Authors.Queries.GetAllauthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResults< List<Author>>>
    {
        private readonly IGenericRepository<Author> _genericRepository;
        private readonly IMemoryCache _memoryCache;
        private const string cacheKey = "authors";

        public GetAllAuthorsQueryHandler(IGenericRepository<Author> genericRepository, IMemoryCache memoryCache)
        {
            _genericRepository = genericRepository;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResults<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out List<Author> allAuthors))
            {
                allAuthors = (await _genericRepository.GetAllAsync()).ToList();
                _memoryCache.Set(cacheKey, allAuthors, TimeSpan.FromMinutes(30));

                if (allAuthors == null || allAuthors.Count == 0)
                    return OperationResults<List<Author>>.FailureResult("No authors found.");
            }

            return OperationResults<List<Author>>.SuccessResult(allAuthors);
        }
    }
}