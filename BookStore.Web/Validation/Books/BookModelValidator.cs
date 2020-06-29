using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.Models.Books;
using FluentValidation;
using System.Linq;

namespace BookStore.Web.Validation.Books
{
    public class BookModelValidator : AbstractValidator<BookModel>
    {
        public BookModelValidator(IBookService bookService)
        {
            RuleFor(x => x.BookTitle)
                .NotEmpty()
                .WithMessage("Title is required.")
                .Must((model, bookTitle) =>
                {
                    Book book = bookService.GetBooks().FirstOrDefault(x => x.BookTitle == bookTitle && x.Id != model.Id);
                    return book == null;
                })
                .WithMessage("There is another book with the same name!");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("Please select an author.");

            RuleFor(x => x.PublisherId)
                .NotEmpty()
                .WithMessage("Please select a publisher.");
        }
    }
}
