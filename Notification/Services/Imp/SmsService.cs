using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Sms> SendSms(Sms sms)
        {
            return await _smsRepository.AddAsync(sms);
        }
        
        public async Task<List<Sms>> GetSms()
        {
            return await _smsRepository.GetQueryableAsync().ToListAsync();
        }
    }
}