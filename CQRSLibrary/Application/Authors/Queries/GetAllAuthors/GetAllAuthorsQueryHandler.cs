﻿using Database.Databases;
using Domain.Models;
using MediatR;

namespace Application.Authors.Queries.GetAllauthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        FakeDatabase _fakeDatabase;

        public GetAllAuthorsQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_fakeDatabase.Authors);
            }

            catch
            {
                throw new Exception("Authors not found");
            }
        }
    }
}