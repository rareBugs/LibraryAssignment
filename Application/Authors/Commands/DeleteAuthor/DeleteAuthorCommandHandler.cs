using Application.Authors.Commands.Commands.DeleteAuthor;
using Database.Databases;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public DeleteAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Author> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToDelete = _fakeDatabase.Authors.FirstOrDefault(x => x.Id == request.AuthorId);
            if (authorToDelete == null)
                throw new Exception($"Author not found {request.AuthorId}");
            
            _fakeDatabase.Authors.Remove(authorToDelete);
            return Task.FromResult(authorToDelete);
        }
    }
}