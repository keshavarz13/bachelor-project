using AutoMapper;
using Social.Controller.Contracts;
using Social.Models;

namespace Social.Options
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookOutputDto>();
            CreateMap<BookInputDto, Book>();
            CreateMap<Post, PostOutputDto>();
            CreateMap<PostInputDto, Post>();
        }
    }
}