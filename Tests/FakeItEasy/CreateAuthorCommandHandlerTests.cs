using Application.Authors.Commands.CreateAuthor;
using Application.DTOs.AuthorDto;
using Domain.Models;
using Domain.Repositories;
using FakeItEasy;

namespace Tests.FakeItEasy
{
    [TestFixture]
    public class CreateAuthorCommandHandlerTests
    {
        private IGenericRepository<Author> _repositoryMock;
        private CreateAuthorcommandHandler _handlerTest;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = A.Fake<IGenericRepository<Author>>();
            _handlerTest = new CreateAuthorcommandHandler(_repositoryMock);
        }

        [Test]
        public async Task CreateAuthorCommandHandler_CreatesAuthor_WhenValidDtoIsProvided()
        {
            var authorDto = new CreateAuthorDto
            {
                FirstName = "firstNameTest",
                LastName = "lastNameTest"
            };

            var createCommand = new CreateAuthorCommand(authorDto);

            // Arrange
            A.CallTo(() => _repositoryMock.AddAsync(A<Author>.That.Matches(a =>
            a.FirstName == authorDto.FirstName && a.LastName == authorDto.LastName)))
                .DoesNothing();

            // Act
            var result = await _handlerTest.Handle(createCommand, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.FirstName, Is.EqualTo("firstNameTest"));
            Assert.That(result.Data.LastName, Is.EqualTo("lastNameTest"));

            A.CallTo(() => _repositoryMock.AddAsync(A<Author>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task CreateAuthorCommandHandler_ReturnsFailure_WhenDtoIsInvalid()
        {
            // Arrange
            var invalidDto = new CreateAuthorDto
            {
                FirstName = "",
                LastName = ""
            };

            var createCommand = new CreateAuthorCommand(invalidDto);

            // Act
            var result = await _handlerTest.Handle(createCommand, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("First or last name empty."));

            A.CallTo(() => _repositoryMock.AddAsync(A<Author>.Ignored)).MustNotHaveHappened();
        }
    }
}