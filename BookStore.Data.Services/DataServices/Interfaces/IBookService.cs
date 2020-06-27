using BookStore.Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Data.Services.DataServices.Interfaces
{
    public partial interface IBookService
    {
        Task<Book> InsertBook(Book book);

        Task<Book> UpdateBook(Book book);

        Task<Book> DeleteBook(Book book, bool isHardDelete = false);

        Task<Book> GetBookById(int bookId);

        Task<List<Book>> GetBooksAsync(string bookTitle = null, int? authorId = null, int? publisherId = null, bool showDeleted = false);

        List<Book> GetBooks(string bookTitle = null, int? authorId = null, int? publisherId = null, bool showDeleted = false);
    }
}
