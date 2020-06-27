using AutoMapper;
using BookStore.Data.Models.Entities;
using BookStore.Web.Models.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Mapping
{
    public class BookStoreMappingProfile : Profile
    {
        public BookStoreMappingProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>()
                .ForMember(dest => dest.IsDeleted, mo => mo.Ignore());
        }
    }
}
