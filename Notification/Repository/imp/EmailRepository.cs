using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Models;

namespace Notification.Repository.imp
{
    public class EmailRepository
    {
        private readonly IMapper _mapper;
        private readonly NotificationDbContext _db;

        public EmailRepository(IMapper mapper,
            NotificationDbContext dbContext)
        {
            this._mapper = mapper;
            this._db = dbContext;
        }
        
        public IQueryable<Email> GetQueryableAsync()
        {
            return _db.Email.AsQueryable();
        }
        
        public async Task<Email> AddAsync(Email email)
        {
            var result = await _db.Email.AddAsync(email);
            await SaveDataChanges("There was a problem registering a new SentFax in the database");
            return email;
        }

        public async Task<Email> UpdateAsync(long id, Email updatedEmail)
        {
            var email  = await this.GetByIdAsync(id);
            email.EmailStatus = updatedEmail.EmailStatus;
            email.EmailSubject = updatedEmail.EmailSubject;
            email.ReceiverEmailAddress = updatedEmail.ReceiverEmailAddress;
            email.Subject = updatedEmail.Subject;
            email.ReceivingTime = email.ReceivingTime;
            email.SubjectId = email.SubjectId;
            await SaveDataChanges("There was a problem updatin a SentFax in the database");
            return email;

        }
        
        public async Task<Email> GetByIdAsync(long id)
        {
            return await _db.Email.SingleOrDefaultAsync(x => x.Id == id);
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