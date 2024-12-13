using Database.Databases;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public UpdateAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = _fakeDatabase.Authors.FirstOrDefault(x => x.Id == request.AuthorId);
            if (authorToUpdate == null)
                throw new Exception("Author not found");

            authorToUpdate.FirstName = request.UpdateAuthorDto.FirstName;
            authorToUpdate.LastName = request.UpdateAuthorDto.LastName;
            return Task.FromResult(authorToUpdate);
        }
    }
}