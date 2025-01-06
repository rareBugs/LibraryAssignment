////using Application.Authors.Commands.Commands.DeleteAuthor;
////using Application.Authors.Commands.CreateAuthor;
////using Application.Authors.Commands.DeleteAuthor;
////using Application.Authors.Commands.UpdateAuthor;
////using Application.Authors.Queries.GetAllauthors;
////using Application.DTOs.AuthorDto;
////using Database.Databases;
////using Domain.Models;

////namespace Tests
////{
////    public class AuthorTests
////    {
////        [SetUp]
////        public void Setup()
////        {
////        }

////        [Test]
////        public async Task GetAllAuthorsQuery_ReturnsAllAuthors()
////        {
////            // Arrange
////            var fakeDatabase = new FakeDatabase();
////            var handler = new GetAllAuthorsQueryHandler(fakeDatabase);

////            // Act
////            var authors = await handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

////            // Assert
////            Assert.IsNotNull(authors, "Authors list should not be null.");
////        }

////        [Test]
////        public async Task GetAllAuthorsQuery_ReturnsSpecificAuthorById()
////        {
////            // Arrange
////            var fakeDatabase = new FakeDatabase();
////            var handler = new GetAllAuthorsQueryHandler(fakeDatabase);
////            var author = new Author("Test", "JoNesbøTest");
////            fakeDatabase.Authors.Add(author);

////            // Act
////            var authors = await handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

////            // Assert
////            Assert.IsNotNull(authors, "Authors list should not be null.");
////            Assert.IsTrue(authors.Any(x => x.Id == author.Id), "Specific author should exist in the list.");
////        }

////        [Test]
////        public async Task CreateAuthorCommand_AddsAuthorToList()
////        {
////            // Arrange
////            var fakeDatabase = new FakeDatabase();
////            var handler = new CreateAuthorcommandHandler(fakeDatabase);
////            var initialAuthorCount = fakeDatabase.Authors.Count;
////            var newAuthor = new Author("Test", "JoNesbøTest");

////            // Act
////            await handler.Handle(new CreateAuthorCommand(newAuthor), CancellationToken.None);

////            // Assert
////            Assert.AreEqual(initialAuthorCount + 1, fakeDatabase.Authors.Count, "Author count should increase.");
////            Assert.IsTrue(fakeDatabase.Authors.Contains(newAuthor), "New author should be added.");
////        }

////        [Test]
////        public async Task DeleteAuthorCommand_RemovesAuthorFromList()
////        {
////            // Arrange
////            var fakeDatabase = new FakeDatabase();
////            var handler = new DeleteAuthorCommandHandler(fakeDatabase);
////            var initialAuthorCount = fakeDatabase.Authors.Count;
////            var authorToDelete = fakeDatabase.Authors.First();

////            // Act
////            await handler.Handle(new DeleteAuthorCommand(authorToDelete.Id), CancellationToken.None);

////            // Assert
////            Assert.AreEqual(initialAuthorCount - 1, fakeDatabase.Authors.Count, "Author count should decrease.");
////            Assert.IsFalse(fakeDatabase.Authors.Contains(authorToDelete), "Author should be removed.");
////        }

////        [Test]
////        public async Task UpdateAuthorCommand_UpdatesAuthorDetails()
////        {
////            // Arrange
////            var fakeDatabase = new FakeDatabase();
////            var handler = new UpdateAuthorCommandHandler(fakeDatabase);
////            var authorToUpdate = fakeDatabase.Authors.First();
////            var updateAuthorDto = new UpdateAuthorDto
////            {
////                FirstName = "UpdatedFirstName",
////                LastName = "UpdatedLastName"
////            };

////            // Act
////            await handler.Handle(new UpdateAuthorCommand(authorToUpdate.Id, updateAuthorDto), CancellationToken.None);

////            // Assert
////            Assert.AreEqual(updateAuthorDto.FirstName, authorToUpdate.FirstName, "First name should be updated.");
////            Assert.AreEqual(updateAuthorDto.LastName, authorToUpdate.LastName, "Last name should be updated.");
////        }

////        [Test]
////        public async Task UpdateAuthorCommand_WithInvalidId_ThrowsException()
////        {
////            // Arrange
////            var fakeDatabase = new FakeDatabase();
////            var handler = new UpdateAuthorCommandHandler(fakeDatabase);
////            var updateAuthorDto = new UpdateAuthorDto
////            {
////                FirstName = "UpdatedFirstName",
////                LastName = "UpdatedLastName"
////            };

////            // Act & Assert
////            Assert.ThrowsAsync<Exception>(() => handler.Handle(new UpdateAuthorCommand(Guid.NewGuid(), updateAuthorDto), CancellationToken.None), "Exception should be thrown for invalid ID.");
////        }
////    }
////}