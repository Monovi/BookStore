using BookStore.Web.Models.Books;
using System.Threading.Tasks;

namespace BookStore.Web.ModelFactories.Interfaces
{
    public partial interface IBookModelFactory
    {
        Task PrepareBookModel(BookModel model);
    }
}
