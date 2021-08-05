using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Notification.Controller.Contracts;
using Notification.Models;
using Notification.Repository;

namespace Notification.Services.Imp
{
    public class SmsService : ISmsService
    {
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;

        public SmsService(ISmsRepository smsRepository, IMapper mapper)
        {
            _smsRepository = smsRepository;
            _mapper = mapper;
        }

        public async Task<SmsOutputDto> SendSms(SmsInputDto sms)
        {
            var mappedSms = _mapper.Map<Sms>(sms);
            var createdSms = await SaveToDatabase(mappedSms);
            BackgroundJob.Enqueue(() => SendToProvider(createdSms));
            return _mapper.Map<SmsOutputDto>(createdSms);
        }

        public async Task<List<SmsOutputDto>> GetSms()
        {
            return _mapper.Map<List<SmsOutputDto>>(
                await _smsRepository.GetQueryableAsync().OrderByDescending(x => x.Id).ToListAsync());
        }

        public async Task<List<SmsOutputDto>> GetSmsByFilter(string phoneNumber,
            DateTime? startCreationTime,
            DateTime? endCreationTime,
            DateTime? startReceivingTime,
            DateTime? endReceivingTime,
            string smsStatus)
        {
            var queryable = _smsRepository.GetQueryableAsync();
            if (!string.IsNullOrEmpty(phoneNumber))
                queryable = queryable.Where(x => x.ReceiverPhoneNumber == phoneNumber);

            if (!string.IsNullOrEmpty(smsStatus))
                queryable = queryable.Where(x =>
                    x.SmsStatus == (SmsStatus)Enum.Parse(typeof(SmsStatus), smsStatus, true));

            if (startCreationTime != null)
                queryable = queryable.Where(x => x.CreationTime >= startCreationTime);

            if (endCreationTime != null)
                queryable = queryable.Where(x => x.CreationTime <= endCreationTime);

            if (startReceivingTime != null)
                queryable = queryable.Where(x => x.ReceivingTime >= startReceivingTime);

            if (endReceivingTime != null)
                queryable = queryable.Where(x => x.ReceivingTime <= endReceivingTime);

            queryable = queryable.OrderByDescending(x => x.Id);
            return _mapper.Map<List<SmsOutputDto>>(await queryable.ToListAsync());
        }

        public async Task<SmsOutputDto> GetSmsById(long id)
        {
            return _mapper.Map<SmsOutputDto>(await _smsRepository.GetQueryableAsync().Where(x => x.Id == id)
                .FirstOrDefaultAsync());
        }

        private async Task<Sms> SaveToDatabase(Sms sms)
        {
            sms.CreationTime = DateTime.Now;
            sms.SmsStatus = SmsStatus.Pending;
            return await _smsRepository.AddAsync(sms);
        }

        public async Task<Sms> SendToProvider(Sms sms)
        {
            sms.ReceivingTime = DateTime.Now;
            BackgroundJob.Schedule(() => UpdateStatus(sms.Id), TimeSpan.FromMinutes(2));
            return sms;
        }
        [AutomaticRetry(Attempts = 0)]
        public async Task UpdateStatus(long id)
        {
            var sms = await _smsRepository.GetByIdAsync(id);
            sms.SmsStatus = SmsStatus.Sent;
            await _smsRepository.UpdateAsync(id, sms);
        }

        public async Task UpdateSms()
        {
            var unsentSms = await _smsRepository.GetQueryableAsync().Where(x => x.SmsStatus == SmsStatus.Pending)
                .Take(10).ToListAsync();
            foreach (var sms in unsentSms)
            {
                await UpdateStatus(sms.Id);
            }
        }
    }
}