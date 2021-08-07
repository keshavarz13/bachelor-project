using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Social.Data;
using Social.Models;

namespace Social.Repository.imp
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;

        public FollowRepository(IMapper mapper, SocialDbContext dbContext)
        {
            this._mapper = mapper;
            this._db = dbContext;
        }
        
        public IQueryable<Follow> GetQueryableAsync()
        {
            return _db.Follow.AsQueryable();
        }
        
        public async Task<Follow> AddAsync(Follow follow)
        {
            var result = await _db.Follow.AddAsync(follow);
            await SaveDataChanges("There was a problem registering a new follow in the database");
            return follow;
        }

        public void Remove(Follow follow)
        {
            var deletingFollow = GetQueryableAsync()
                .SingleOrDefault(x => x.Followed == follow.Followed && x.Follower == follow.Follower);
            if (deletingFollow != null) _db.Remove((object)deletingFollow);
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