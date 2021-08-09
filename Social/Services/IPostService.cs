using System.Collections.Generic;
using System.Threading.Tasks;
using Social.Controller.Contracts;

namespace Social.Services
{
    public interface IPostService
    {
        Task<List<PostOutputDto>> GetFeed(int uun);
        Task<PostOutputDto> SendPost(int uun, PostInputDto inputDto);
        Task<PostDetailsOutputDto> GetPostDetails(long id);
    }
}