using Application.Authors.Queries.GetAllauthors;
using Domain.Models;
using Domain.Repositories;
using FakeItEasy;
using Microsoft.Extensions.Caching.Memory;

namespace Tests.FakeItEasy
{
    [TestFixture]
    public class GetAllAuthorsQueryHandlerTests : IDisposable
    {
        private IGenericRepository<Author> _repositoryMock;
        private GetAllAuthorsQueryHandler _handler;
        private IMemoryCache _memoryCache;

        private List<Author> GenerateTestAuthors()
        {
            return new List<Author> {
                new Author("Author1", "LastName1"),
                new Author("Author2", "LastName2")};
        }

        public void Dispose()
        {
            (_memoryCache as IDisposable)?.Dispose();
            _memoryCache = null;
            _repositoryMock = null;
            _handler = null;
        }

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = A.Fake<IGenericRepository<Author>>();
            _memoryCache = A.Fake<IMemoryCache>();

            var authors = GenerateTestAuthors();

            A.CallTo(() => _repositoryMock.GetAllAsync()).Returns(authors.AsQueryable());

            // Simulate cache miss
            object cacheValue;
            A.CallTo(() => _memoryCache.TryGetValue(A<object>.Ignored, out cacheValue)).Returns(false);

            // Simulate cache entry creation
            var cacheEntry = A.Fake<ICacheEntry>();
            A.CallTo(() => _memoryCache.CreateEntry(A<object>.Ignored)).Returns(cacheEntry);

            _handler = new GetAllAuthorsQueryHandler(_repositoryMock, _memoryCache);
        }

        [Test]
        public async Task GetAllAuthorsQueryHandler_ReturnsAuthors_WhenCacheIsEmpty()
        {
            // Act
            var result = await _handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.That(result.Data.Count, Is.EqualTo(2), "Expected two authors to be returned.");
            Assert.That(result.Data[0].FirstName, Is.EqualTo("Author1"), "First author's first name does not match.");
            Assert.That(result.Data[0].LastName, Is.EqualTo("LastName1"), "First author's last name does not match.");
            Assert.That(result.Data[1].FirstName, Is.EqualTo("Author2"), "Second author's first name does not match.");
            Assert.That(result.Data[1].LastName, Is.EqualTo("LastName2"), "Second author's last name does not match.");
        }

        [Test]
        public async Task GetAllAuthorsQueryHandler_QueriesRepository_WhenCacheMissOccurs()
        {
            // Act
            await _handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

            // Assert
            A.CallTo(() => _repositoryMock.GetAllAsync()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAllAuthorsQueryHandler_CreatesCacheEntry_WhenCacheMissOccurs()
        {
            // Act
            await _handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

            // Assert
            A.CallTo(() => _memoryCache.CreateEntry(A<object>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
