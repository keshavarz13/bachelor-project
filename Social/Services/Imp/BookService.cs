using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Social.Controller.Contracts;
using Social.Models;
using Social.Repository;

namespace Social.Services.Imp
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<List<BookOutputDto>> GetBooks()
        {
            return _mapper.Map<List<BookOutputDto>>(
                await _bookRepository.GetQueryableAsync().OrderByDescending(x => x.Id).ToListAsync());
        }

        public async Task<List<BookOutputDto>> GetBookByFilter(string name,
            DateTime? startCreationTime,
            DateTime? endCreationTime,
            int authorId)
        {
            var queryable = _bookRepository.GetQueryableAsync();
            if (!string.IsNullOrEmpty(name))
                queryable = queryable.Where(x => x.Name.Contains(name));

            if (startCreationTime != null)
                queryable = queryable.Where(x => x.CreationTime >= startCreationTime);

            if (endCreationTime != null)
                queryable = queryable.Where(x => x.CreationTime <= endCreationTime);
            
            if (authorId != 0)
                queryable = queryable.Where(x => x.AuthorId == authorId);
            
            queryable = queryable.OrderByDescending(x => x.Id);
            return _mapper.Map<List<BookOutputDto>>(await queryable.ToListAsync());
        }

        public async Task<BookOutputDto> GetBookById(long id)
        {
            return _mapper.Map<BookOutputDto>(await _bookRepository.GetQueryableAsync().Where(x => x.Id == id)
                .FirstOrDefaultAsync());
        }

        public async Task<Book> AddNewBook(BookInputDto bookInputDto)
        {
            var book = _mapper.Map<Book>(bookInputDto);
            book.CreationTime = DateTime.Now;
            return await _bookRepository.AddAsync(book);
        }
        
        public async Task<Book> UpdateBook(long id, BookInputDto bookInputDto)
        {
            var book = _mapper.Map<Book>(bookInputDto);
            return await _bookRepository.UpdateAsync(id ,book);
        }
    }
}