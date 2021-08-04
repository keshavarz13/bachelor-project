using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notification.Controller.Contracts;
using Notification.Models;
using Notification.Repository;

namespace Notification.Services.Imp
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;

        public EmailService(IMapper mapper, IEmailRepository emailRepository)
        {
            _mapper = mapper;
            _emailRepository = emailRepository;
        }

        public async Task<EmailOutputDto> SendEmail(EmailInputDto emailInputDto)
        {
            var mappedEmail = _mapper.Map<Email>(emailInputDto);
            await SendToProvider(mappedEmail);
            return _mapper.Map<EmailOutputDto>(await SaveToDatabase(mappedEmail));
        }

        public async Task<List<EmailOutputDto>> GetEmail()
        {
            return _mapper.Map<List<EmailOutputDto>>(
                await _emailRepository.GetQueryableAsync().OrderByDescending(x => x.Id).ToListAsync());
        }

        public async Task<List<EmailOutputDto>> GetEmailByFilter(string receiverEmailAddress, DateTime? startCreationTime,
            DateTime? endCreationTime, DateTime? startReceivingTime, DateTime? endReceivingTime, string emailStatus)
        {
            var queryable = _emailRepository.GetQueryableAsync();
            if (!string.IsNullOrEmpty(receiverEmailAddress))
                queryable = queryable.Where(x => x.ReceiverEmailAddress == receiverEmailAddress);

            if (!string.IsNullOrEmpty(emailStatus))
                queryable = queryable.Where(x =>
                    x.EmailStatus == (EmailStatus)Enum.Parse(typeof(EmailStatus), emailStatus, true));

            if (startCreationTime != null)
                queryable = queryable.Where(x => x.CreationTime >= startCreationTime);

            if (endCreationTime != null)
                queryable = queryable.Where(x => x.CreationTime <= endCreationTime);

            if (startReceivingTime != null)
                queryable = queryable.Where(x => x.ReceivingTime >= startReceivingTime);

            if (endReceivingTime != null)
                queryable = queryable.Where(x => x.ReceivingTime <= endReceivingTime);

            queryable = queryable.OrderByDescending(x => x.Id);
            return _mapper.Map<List<EmailOutputDto>>(await queryable.ToListAsync());
        }

        public async Task<EmailOutputDto> GetEmailById(long id)
        {
            return _mapper.Map<EmailOutputDto>(await _emailRepository.GetQueryableAsync().Where(x => x.Id == id)
                .FirstOrDefaultAsync());
        }

        private async Task<Email> SaveToDatabase(Email email)
        {
            email.CreationTime = DateTime.Now;
            email.EmailStatus = EmailStatus.Pending;
            return await _emailRepository.AddAsync(email);
        }

        private async Task<Email> SendToProvider(Email email)
        {
            email.ReceivingTime = DateTime.Now;
            return email;
        }
    }
}