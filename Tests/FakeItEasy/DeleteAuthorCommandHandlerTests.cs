using Application.Authors.Commands.Commands.DeleteAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Domain.Models;
using Domain.Repositories;
using FakeItEasy;

namespace Tests.FakeItEasy
{
    [TestFixture]
    public class DeleteAuthorCommandHandlerTests
    {
        private IGenericRepository<Author> _repositoryMock;
        private DeleteAuthorCommandHandler _deleteAuthorCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = A.Fake<IGenericRepository<Author>>();
            _deleteAuthorCommandHandler = new DeleteAuthorCommandHandler(_repositoryMock);
        }

        [Test]
        public async Task DeleteAuthorCommandHandler_DeletesAuthor_WhenAuthorExists()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            var authorToDelete = new Author("Wkwkwk", "llll");
            authorToDelete.GetType().GetProperty("Id").SetValue(authorToDelete, authorId);

            var deleteCommand = new DeleteAuthorCommand(authorId);

            A.CallTo(() => _repositoryMock.GetByIdAsync(authorId)).Returns(authorToDelete);
            A.CallTo(() => _repositoryMock.DeleteAsync(authorToDelete)).Returns(Task.CompletedTask);

            // Act
            var result = await _deleteAuthorCommandHandler.Handle(deleteCommand, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Expected the result to indicate success.");
            Assert.That(result.Data.Id, Is.EqualTo(authorId), "Expected the deleted author ID to match the input.");

            A.CallTo(() => _repositoryMock.DeleteAsync(authorToDelete)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task DeleteAuthorCommandHandler_ReturnsFailure_WhenAuthorDoesNotExist()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            var deleteCommand = new DeleteAuthorCommand(authorId);

            A.CallTo(() => _repositoryMock.GetByIdAsync(authorId)).Returns((Author)null);

            // Act
            var result = await _deleteAuthorCommandHandler.Handle(deleteCommand, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Expected the result to indicate failure.");
            Assert.That(result.ErrorMessage, Is.EqualTo("Author not found."), "Expected error message to indicate the author was not found.");

            A.CallTo(() => _repositoryMock.DeleteAsync(A<Author>.Ignored)).MustNotHaveHappened();
        }
    }
}
