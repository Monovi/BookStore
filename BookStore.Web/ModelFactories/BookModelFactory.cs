using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.ModelFactories.Interfaces;
using BookStore.Web.Models.Books;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.ModelFactories
{
    public partial class BookModelFactory : IBookModelFactory
    {
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BookModelFactory(IAuthorService authorService,
            IPublisherService publisherService)
        {
            this._authorService = authorService;
            this._publisherService = publisherService;
        }

        public async Task PrepareBookModel(BookModel model)
        {
            List<Author> authors = await _authorService.GetAuthorsAsync();
            model.AvailableAuthors = authors.Select(x => new SelectListItem
            {
                Selected = model.AuthorId == x.Id,
                Text = x.AuthorName,
                Value = x.Id.ToString()
            }).ToList();
            model.AvailableAuthors.Insert(0, new SelectListItem
            {
                Selected = !model.AuthorId.HasValue,
                Text = "Select author",
                Value = string.Empty
            });

            List<Publisher> publishers = await _publisherService.GetPublishersAsync();
            model.AvailablePublishers = publishers.Select(x => new SelectListItem
            {
                Selected = model.PublisherId == x.Id,
                Text = x.PublisherName,
                Value = x.Id.ToString()
            }).ToList();
            model.AvailablePublishers.Insert(0, new SelectListItem
            {
                Selected = !model.PublisherId.HasValue,
                Text = "Select publisher",
                Value = string.Empty
            });
        }
    }
}
