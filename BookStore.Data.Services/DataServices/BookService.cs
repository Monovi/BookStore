using BookStore.Data.Caching;
using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Services.DataServices
{
    public partial class BookService : IBookService
    {
        private const string BOOK_BY_ID_KEY = "BookStore.Books.id-{0}";
        private const string BOOK_PATTERN_KEY = "BookStore.Books.";

        private readonly IRepository<Book> _bookRepository;
        private readonly MemoryCacheManager _memoryCacheManager;

        public BookService(IRepository<Book> bookRepository,
            MemoryCacheManager memoryCacheManager)
        {
            this._bookRepository = bookRepository;
            this._memoryCacheManager = memoryCacheManager;
        }

        public async Task<Book> InsertBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            await _bookRepository.InsertAsync(book);
            _memoryCacheManager.RemoveByPattern(BOOK_PATTERN_KEY);
            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            await _bookRepository.UpdateAsync(book);
            _memoryCacheManager.RemoveByPattern(BOOK_PATTERN_KEY);
            return book;
        }

        public async Task<Book> DeleteBook(Book book, bool isHardDelete = false)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            if (!isHardDelete)
            {
                book.IsDeleted = true;
                await UpdateBook(book);
                return book;
            }
            else
            {
                Book _book = book;
                await _bookRepository.DeleteAsync(book);
                _memoryCacheManager.RemoveByPattern(BOOK_PATTERN_KEY);
                return _book;
            }
        }

        public async Task<Book> GetBookById(int bookId)
        {
            if (bookId <= 0)
                return null;

            string key = string.Format(BOOK_BY_ID_KEY, bookId);
            return await _memoryCacheManager.Get(key, async () => await _bookRepository.GetByIdAsync(bookId));
        }

        public async Task<List<Book>> GetBooksAsync(string bookTitle = null, int? authorId = null, int? publisherId = null, bool showDeleted = false)
        {
            IQueryable<Book> query = _bookRepository.Table;
            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(bookTitle))
                query = query.Where(x => x.BookTitle.Contains(bookTitle));
            if (authorId.HasValue)
                query = query.Where(x => x.AuthorId == authorId);
            if (publisherId.HasValue)
                query = query.Where(x => x.PublisherId == publisherId);

            List<Book> books = await query.OrderBy(x => x.BookTitle).ToListAsync();
            return books;
        }

        public List<Book> GetBooks(string bookTitle = null, int? authorId = null, int? publisherId = null, bool showDeleted = false)
        {
            IQueryable<Book> query = _bookRepository.Table;
            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(bookTitle))
                query = query.Where(x => x.BookTitle.Contains(bookTitle));
            if (authorId.HasValue)
                query = query.Where(x => x.AuthorId == authorId);
            if (publisherId.HasValue)
                query = query.Where(x => x.PublisherId == publisherId);

            List<Book> books = query.OrderBy(x => x.BookTitle).ToList();
            return books;
        }
    }
}
