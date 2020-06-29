using AutoMapper;
using BookStore.Data.Models.Entities;
using BookStore.Web.Models.Authors;
using BookStore.Web.Models.Books;
using BookStore.Web.Models.Publishers;

namespace BookStore.Web.Mapping
{
    public class BookStoreMappingProfile : Profile
    {
        public BookStoreMappingProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>()
                .ForMember(dest => dest.IsDeleted, mo => mo.Ignore());

            CreateMap<Publisher, PublisherModel>();
            CreateMap<PublisherModel, Publisher>()
                .ForMember(dest => dest.IsDeleted, mo => mo.Ignore());

            CreateMap<Book, BookModel>()
                .ForMember(dest => dest.AvailableAuthors, mo => mo.Ignore())
                .ForMember(dest => dest.AvailablePublishers, mo => mo.Ignore())
                .ForMember(dest => dest.AuthorName, mo => mo.MapFrom(entity => entity.Author.AuthorName))
                .ForMember(dest => dest.PublisherName, mo => mo.MapFrom(entity => entity.Publisher.PublisherName));
            CreateMap<BookModel, Book>()
                .ForMember(dest => dest.Author, mo => mo.Ignore())
                .ForMember(dest => dest.Publisher, mo => mo.Ignore())
                .ForMember(dest => dest.IsDeleted, mo => mo.Ignore());
        }
    }
}
