using System.Linq;
using System.Threading.Tasks;
using Social.Models;

namespace Social.Repository
{
    public interface IFollowRepository
    {
        IQueryable<Follow> GetQueryableAsync();
        Task<Follow> AddAsync(Follow follow);
        void Remove(Follow follow);
    }
}