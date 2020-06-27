using BookStore.Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Data.Services.DataServices.Interfaces
{
    public partial interface IAuthorService
    {
        Task<Author> InsertAuthor(Author author);

        Task<Author> UpdateAuthor(Author author);

        Task<Author> DeleteAuthor(Author author, bool isHardDelete = false);

        Task<Author> GetAuthorById(int authorId);

        Task<List<Author>> GetAuthorsAsync(string authorName = null, bool showDeleted = false);

        List<Author> GetAuthors(string authorName = null, bool showDeleted = false);
    }
}
