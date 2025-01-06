using Application.Authors.Commands.UpdateAuthor;
using Application.DTOs.AuthorDto;
using Domain.Models;
using Domain.Repositories;
using FakeItEasy;

namespace Tests.FakeItEasy
{
    [TestFixture]
    public class UpdateAuthorCommandHandlerTests
    {
        private IGenericRepository<Author> _fakeRepository;
        private UpdateAuthorCommandHandler _updateAuthorCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fakeRepository = A.Fake<IGenericRepository<Author>>();
            _updateAuthorCommandHandler = new UpdateAuthorCommandHandler(_fakeRepository);
        }

        [Test]
        public async Task UpdateAuthorCommandHandler_UpdatesAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var updateDto = new UpdateAuthorDto
            {
                FirstName = "Nicolas",
                LastName = "Cake"
            };
            var updateCommand = new UpdateAuthorCommand(authorId, updateDto);

            var existingAuthor = new Author("Nicolas", "Cake");
            existingAuthor.GetType().GetProperty("Id").SetValue(existingAuthor, authorId);

            A.CallTo(() => _fakeRepository.GetByIdAsync(authorId)).Returns(existingAuthor);
            A.CallTo(() => _fakeRepository.UpdateAsync(A<Author>.Ignored)).Returns(Task.CompletedTask);

            // Act
            var result = await _updateAuthorCommandHandler.Handle(updateCommand, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Expected the update operation to succeed.");
            A.CallTo(() => _fakeRepository.UpdateAsync(A<Author>.That.Matches(author =>
                author.Id == authorId &&
                author.FirstName == updateDto.FirstName &&
                author.LastName == updateDto.LastName
            ))).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task UpdateAuthorCommandHandler_ReturnsFailure_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var updateDto = new UpdateAuthorDto
            {
                FirstName = "Nicolas",
                LastName = "Cake"
            };
            var updateCommand = new UpdateAuthorCommand(authorId, updateDto);

            A.CallTo(() => _fakeRepository.GetByIdAsync(authorId)).Returns((Author)null);

            // Act
            var result = await _updateAuthorCommandHandler.Handle(updateCommand, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Expected the update operation to fail.");
            Assert.That(result.ErrorMessage, Is.EqualTo("Author not found."), "Expected error message does not match.");
        }
    }

}
