using System.Linq;
using System.Threading.Tasks;
using Social.Models;

namespace Social.Repository
{
    public interface IReadRepository
    {
        IQueryable<Read> GetQueryableAsync();
        Task<Read> AddAsync(Read read);
        Task<Read> UpdateAsync(long id, Read updatedRead);
        Task<Read> GetByIdAsync(long id);
    }
}