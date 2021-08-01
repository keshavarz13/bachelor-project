using System.Linq;
using System.Threading.Tasks;
using Notification.Models;

namespace Notification.Repository
{
    public interface ISmsRepository
    {
        IQueryable<Sms> GetQueryableAsync();
        Task<Sms> AddAsync(Sms email);
        Task<Sms> UpdateAsync(long id, Sms updatedEmail);
        Task<Sms> GetByIdAsync(long id);
    }
}