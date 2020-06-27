using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.Models.Authors;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Validation.Authors
{
    public class AuthorModelValidator : AbstractValidator<AuthorModel>
    {
        public AuthorModelValidator(IAuthorService authorService)
        {
            RuleFor(x => x.AuthorName)
                .NotEmpty()
                .WithMessage("Author name is required.")
                .Must((model, authorName) =>
                {
                    Author author = authorService.GetAuthors().FirstOrDefault(x => x.AuthorName == authorName && x.Id != model.Id);
                    return author == null;
                })
                .WithMessage("There is another author with the same name!");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Please enter birth date.");
        }
    }
}
