using System.Linq;
using System.Threading.Tasks;
using Notification.Models;

namespace Notification.Repository
{
    public interface IEmailRepository
    {
        IQueryable<Email> GetQueryableAsync();
        Task<Email> AddAsync(Email email);
        Task<Email> UpdateAsync(long id, Email updatedEmail);
        Task<Email> GetByIdAsync(long id);
    }
}