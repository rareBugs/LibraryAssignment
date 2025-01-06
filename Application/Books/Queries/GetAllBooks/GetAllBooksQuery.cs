using Domain.Models;
using MediatR;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<OperationResults<List<Book>>>{}
}
