using System.Linq;
using System.Threading.Tasks;
using Social.Models;

namespace Social.Repository
{
    public interface IBookRepository
    {
        IQueryable<Book> GetQueryableAsync();
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(long id, Book updatedBook);
        Task<Book> GetByIdAsync(long id);
    }
}