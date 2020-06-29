using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.Models.Publishers;
using FluentValidation;
using System.Linq;

namespace BookStore.Web.Validation.Publishers
{
    public class PublisherModelValidator : AbstractValidator<PublisherModel>
    {
        public PublisherModelValidator(IPublisherService publisherService)
        {
            RuleFor(x => x.PublisherName)
                .NotEmpty()
                .WithMessage("Publisher name is required.")
                .Must((model, publisherName) =>
                {
                    Publisher publisher = publisherService.GetPublishers().FirstOrDefault(x => x.PublisherName == publisherName && x.Id != model.Id);
                    return publisher == null;
                })
                .WithMessage("There is another publisher with the same name!");
        }
    }
}
