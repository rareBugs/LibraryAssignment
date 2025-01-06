//using Application.Books.Commands.CreateBook;
//using Application.Books.Commands.DeleteBook;
//using Application.Books.Commands.UpdateBook;
//using Application.Books.Queries.GetAllBooks;
//using Application.Books.Queries.GetBookById;
//using Application.DTOs.BookDto;
//using Database.Databases;
//using Domain.Models;

//namespace Tests
//{
//    public class BookTests
//    {
//        private FakeDatabase _fakeDatabase;

//        [SetUp]
//        public void Setup()
//        {
//            _fakeDatabase = new FakeDatabase();
//        }

//        [Test]
//        public async Task CreateBookCommand_AddsBookToDatabase()
//        {
//            // Arrange
//            var handler = new CreateBookCommandHandler(_fakeDatabase);
//            var initialBookCount = _fakeDatabase.Books.Count;
//            var newBook = new Book("Test", "JoNesbøTest");

//            // Act
//            await handler.Handle(new CreateBookCommand(newBook), CancellationToken.None);

//            // Assert
//            Assert.AreEqual(initialBookCount + 1, _fakeDatabase.Books.Count, "Book count should increase.");
//            Assert.Contains(newBook, _fakeDatabase.Books, "New book should be added.");
//        }

//        [Test]
//        public async Task DeleteBookCommand_RemovesBookFromDatabase()
//        {
//            // Arrange
//            var handler = new DeleteBookCommandHandler(_fakeDatabase);
//            var initialBookCount = _fakeDatabase.Books.Count;
//            var bookToDelete = _fakeDatabase.Books.First();

//            // Act
//            await handler.Handle(new DeleteBookCommand(bookToDelete.Id), CancellationToken.None);

//            // Assert
//            Assert.AreEqual(initialBookCount - 1, _fakeDatabase.Books.Count, "Book count should decrease.");
//            Assert.IsFalse(_fakeDatabase.Books.Contains(bookToDelete), "Book should be removed.");
//        }

//        [Test]
//        public async Task GetAllBooksQuery_ReturnsAllBooks()
//        {
//            // Arrange
//            var handler = new GetAllBooksQueryHandler(_fakeDatabase);

//            // Act
//            var books = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

//            // Assert
//            Assert.IsNotNull(books, "Books should not be null.");
//            Assert.AreEqual(_fakeDatabase.Books.Count, books.Count, "Book count should match.");
//        }

//        [Test]
//        public async Task GetBookByIdQuery_ReturnsCorrectBook()
//        {
//            // Arrange
//            var handler = new GetBookbyIdQueryHandler(_fakeDatabase);
//            var bookToGet = _fakeDatabase.Books.First();

//            // Act
//            var book = await handler.Handle(new GetBookbyIdQuery(bookToGet.Id), CancellationToken.None);

//            // Assert
//            Assert.IsNotNull(book, "Book should not be null.");
//            Assert.AreEqual(bookToGet.Id, book.Id, "Book ID should match.");
//        }

//        [Test]
//        public async Task UpdateBookCommand_UpdatesBookDetails()
//        {
//            // Arrange
//            var handler = new UpdateBookCommandHandler(_fakeDatabase);
//            var bookToUpdate = _fakeDatabase.Books.First();
//            var updateBookDto = new UpdateBookDto
//            {
//                Title = "Updated Title",
//                Description = "Updated Description"
//            };

//            // Act
//            await handler.Handle(new UpdateBookCommand(bookToUpdate.Id, updateBookDto), CancellationToken.None);

//            // Assert
//            Assert.AreEqual(updateBookDto.Title, bookToUpdate.Title, "Title should be updated.");
//            Assert.AreEqual(updateBookDto.Description, bookToUpdate.Description, "Description should be updated.");
//        }
//    }
//}