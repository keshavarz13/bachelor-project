using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Social.Data;
using Social.Models;

namespace Social.Repository.imp
{
    public class ReadRepository : IReadRepository
    {
         
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;

        public ReadRepository(IMapper mapper, SocialDbContext dbContext)
        {
            this._mapper = mapper;
            this._db = dbContext;
        }
        
        public IQueryable<Read> GetQueryableAsync()
        {
            return _db.Read.AsQueryable();
        }
        
        public async Task<Read> AddAsync(Read read)
        {
            read.CreationTime = DateTime.Now;
            read.LastModificationTime = read.CreationTime;
            var result = await _db.Read.AddAsync(read);
            await SaveDataChanges("There was a problem registering a new read in the database");
            return read;
        }

        public async Task<Read> UpdateAsync(long id, Read updatedRead)
        {
            var read  = await this.GetByIdAsync(id);
            read.Page = updatedRead.Page;
            read.Score = updatedRead.Score;
            read.LastModificationTime = DateTime.Now;
            read.ReadStatus= updatedRead.ReadStatus;
            await SaveDataChanges("There was a problem updatin a read in the database");
            return read;

        }
        
        public async Task<Read> GetByIdAsync(long id)
        {
            return await _db.Read.SingleOrDefaultAsync(x => x.Id == id);
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