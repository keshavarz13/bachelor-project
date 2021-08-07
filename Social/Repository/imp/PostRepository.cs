using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Social.Data;
using Social.Models;

namespace Social.Repository.imp
{
    public class PostRepository : IPostRepository
    {
        
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;

        public PostRepository(IMapper mapper, SocialDbContext dbContext)
        {
            this._mapper = mapper;
            this._db = dbContext;
        }
        
        public IQueryable<Post> GetQueryableAsync()
        {
            return _db.Post.AsQueryable();
        }
        
        public async Task<Post> AddAsync(Post post)
        {
            var result = await _db.Post.AddAsync(post);
            await SaveDataChanges("There was a problem registering a new post in the database");
            return post;
        }

        public async Task<Post> UpdateAsync(long id, Post updatedPost)
        {
            var post  = await this.GetByIdAsync(id);
            post.Context = updatedPost.Context;
            post.RelatedBook = updatedPost.RelatedBook;
            await SaveDataChanges("There was a problem updatin a post in the database");
            return post;
        }
        
        public async Task<Post> GetByIdAsync(long id)
        {
            return await _db.Post.SingleOrDefaultAsync(x => x.Id == id);
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