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
    public partial class AuthorService : IAuthorService
    {
        private const string AUTHOR_BY_ID_KEY = "BookStore.Authors.id-{0}";
        private const string AUTHOR_PATTERN_KEY = "BookStore.Authors.";

        private readonly IRepository<Author> _authorRepository;
        private readonly MemoryCacheManager _memoryCacheManager;

        public AuthorService(IRepository<Author> authorRepository,
            MemoryCacheManager memoryCacheManager)
        {
            this._authorRepository = authorRepository;
            this._memoryCacheManager = memoryCacheManager;
        }

        public async Task<Author> InsertAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException("author");

            await _authorRepository.InsertAsync(author);
            _memoryCacheManager.RemoveByPattern(AUTHOR_PATTERN_KEY);
            return author;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException("author");

            await _authorRepository.UpdateAsync(author);
            _memoryCacheManager.RemoveByPattern(AUTHOR_PATTERN_KEY);
            return author;
        }

        public async Task<Author> DeleteAuthor(Author author, bool isHardDelete = false)
        {
            if (author == null)
                throw new ArgumentNullException("author");

            if (!isHardDelete)
            {
                author.IsDeleted = true;
                await UpdateAuthor(author);
                return author;
            }
            else
            {
                Author _author = author;
                await _authorRepository.DeleteAsync(author);
                _memoryCacheManager.RemoveByPattern(AUTHOR_PATTERN_KEY);
                return _author;
            }
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            if (authorId <= 0)
                return null;

            string key = string.Format(AUTHOR_BY_ID_KEY, authorId);
            return await _memoryCacheManager.Get(key, async () => await _authorRepository.GetByIdAsync(authorId));
        }

        public async Task<List<Author>> GetAuthorsAsync(string authorName = null, bool showDeleted = false)
        {
            IQueryable<Author> query = _authorRepository.Table;
            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(authorName))
                query = query.Where(x => x.AuthorName.Contains(authorName));

            List<Author> authors = await query.OrderBy(x => x.AuthorName).ToListAsync();
            return authors;
        }

        public List<Author> GetAuthors(string authorName = null, bool showDeleted = false)
        {
            IQueryable<Author> query = _authorRepository.Table;
            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(authorName))
                query = query.Where(x => x.AuthorName.Contains(authorName));

            List<Author> authors = query.OrderBy(x => x.AuthorName).ToList();
            return authors;
        }
    }
}
