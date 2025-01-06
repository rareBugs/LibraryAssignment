using Application.Authors.Queries.GetAuthorById;
using Domain.Models;
using Domain.Repositories;
using FakeItEasy;

namespace Tests.FakeItEasy
{
    [TestFixture]
    public class GetAuthorByIdQueryHandlerTests
    {
        private IGenericRepository<Author> _repositoryMock;
        private GetAuthorByIdQueryHandler _getAuthorByIdCommand;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = A.Fake<IGenericRepository<Author>>();
            _getAuthorByIdCommand = new GetAuthorByIdQueryHandler(_repositoryMock);
        }

        [Test]
        public async Task GetAuthorByIdQueryHandler_ReturnsAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var expectedAuthor = new Author(firstName: "John", lastName: "Johnson");

            A.CallTo(() => _repositoryMock.GetByIdAsync(authorId)).Returns(expectedAuthor);

            // Act
            var result = await _getAuthorByIdCommand.Handle(new GetAuthorByIdQuery(authorId), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.That(result.IsSuccess, Is.True, "Expected result to indicate success.");
            Assert.That(result.Data, Is.EqualTo(expectedAuthor), "The returned author does not match the expected author.");
        }

        [Test]
        public async Task GetAuthorByIdQueryHandler_ReturnsFailure_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();

            A.CallTo(() => _repositoryMock.GetByIdAsync(authorId)).Returns((Author)null);

            // Act
            var result = await _getAuthorByIdCommand.Handle(new GetAuthorByIdQuery(authorId), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.That(result.IsSuccess, Is.False, "Expected result to indicate failure.");
            Assert.That(result.ErrorMessage, Is.EqualTo("Author not found."), "Expected error message does not match.");
        }
    }
}
