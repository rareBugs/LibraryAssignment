using Database.Databases;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorcommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public CreateAuthorcommandHandler(FakeDatabase database)
        {
            _fakeDatabase = database;
        }

        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _fakeDatabase.Authors.Add(request.NewAuthor);
                return await Task.FromResult(request.NewAuthor);
            }

            catch
            {
                throw new Exception("Failed adding author.");
            }
        }
    }
}