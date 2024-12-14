﻿using Database.Databases;
using Domain.Models;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        FakeDatabase _fakeDatabase;

        public UpdateBookCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = _fakeDatabase.Books.FirstOrDefault(x => x.Id == request.BookId);
            if (bookToUpdate == null)
                throw new Exception($"Book not found {request.BookId}");
            
            bookToUpdate.Title = request.UpdateBookDto.Title;
            bookToUpdate.Description = request.UpdateBookDto.Description;
            return Task.FromResult(bookToUpdate);
        }
    }
}