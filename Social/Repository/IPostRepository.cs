using System.Linq;
using System.Threading.Tasks;
using Social.Models;

namespace Social.Repository
{
    public interface IPostRepository
    {
        IQueryable<Post> GetQueryableAsync();
        Task<Post> AddAsync(Post post);
        Task<Post> UpdateAsync(long id, Post updatedPost);
        Task<Post> GetByIdAsync(long id);
    }
}