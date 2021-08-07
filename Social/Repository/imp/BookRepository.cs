using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Social.Data;
using Social.Models;

namespace Social.Repository.imp
{
    public class BookRepository : IBookRepository
    {
        
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;

        public BookRepository(IMapper mapper, SocialDbContext dbContext)
        {
            this._mapper = mapper;
            this._db = dbContext;
        }
        
        public IQueryable<Book> GetQueryableAsync()
        {
            return _db.Book.AsQueryable();
        }
        
        public async Task<Book> AddAsync(Book book)
        {
            var result = await _db.Book.AddAsync(book);
            await SaveDataChanges("There was a problem registering a new book in the database");
            return book;
        }

        public async Task<Book> UpdateAsync(long id, Book updatedBook)
        {
            var book  = await this.GetByIdAsync(id);
            book.BookCategory = updatedBook.BookCategory;
            book.Name = updatedBook.Name;
            book.Summery = updatedBook.Summery;
            book.AuthorId = updatedBook.AuthorId;
            book.AuthorName = updatedBook.AuthorName;
            book.CreationTime = updatedBook.CreationTime;
            await SaveDataChanges("There was a problem updatin a book in the database");
            return book;

        }
        
        public async Task<Book> GetByIdAsync(long id)
        {
            return await _db.Book.SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<bool> SaveDataChanges(string technicalErrorMessage)
        {
            try
            {
                var result = await _db.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(technicalErrorMessage);
            }
        }
    }
}