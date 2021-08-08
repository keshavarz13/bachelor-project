using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Social.Controller.Contracts;
using Social.Models;

namespace Social.Services
{
    public interface IBookService
    {
        Task<List<BookOutputDto>> GetBooks();

        Task<List<BookOutputDto>> GetBookByFilter(string name, DateTime? startCreationTime, DateTime? endCreationTime,
            int authorId);

        Task<BookOutputDto> GetBookById(long id);

        Task<Book> AddNewBook(BookInputDto bookInputDto);

        Task<Book> UpdateBook(long id, BookInputDto book);

        Task<List<CategoryOutputDto>> GetCategories();
    }
}