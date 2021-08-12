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
            CreateMap<Post, PostDetailsOutputDto>();
            CreateMap<Post, CommentOutputDto>();
            CreateMap<PostInputDto, Post>();
            CreateMap<Read, ReadOutputDto>();
            CreateMap<ReadInputDto, Read>();
        }
    }
}