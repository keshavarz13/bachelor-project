using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Models;

namespace Notification.Repository.imp
{
    public class SmsRepository : ISmsRepository
    {
        private readonly IMapper _mapper;
        private readonly NotificationDbContext _db;

        public SmsRepository(IMapper mapper,
            NotificationDbContext dbContext)
        {
            this._mapper = mapper;
            this._db = dbContext;
        }
        
        public IQueryable<Sms> GetQueryableAsync()
        {
            return _db.Sms.AsQueryable();
        }
        
        public async Task<Sms> AddAsync(Sms email)
        {
            var result = await _db.Sms.AddAsync(email);
            await SaveDataChanges("There was a problem registering a new SentFax in the database");
            return result.Entity;
        }

        public async Task<Sms> UpdateAsync(long id, Sms updatedEmail)
        {
            var email  = await this.GetByIdAsync(id);

            email.Subject = updatedEmail.Subject;
            email.ReceivingTime = email.ReceivingTime;
            email.SubjectId = email.SubjectId;
            await SaveDataChanges("There was a problem updatin a SentFax in the database");
            return email;

        }
        
        public async Task<Sms> GetByIdAsync(long id)
        {
            return await _db.Sms.SingleOrDefaultAsync(x => x.Id == id);
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