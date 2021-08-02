using AutoMapper;
using Notification.Controller.Contracts;
using Notification.Models;

namespace Notification.Options
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SmsInputDto, Sms>();
            CreateMap<EmailInputDto, Email>();
            CreateMap<Sms, SmsOutputDto>();
            CreateMap<Email, EmailOutputDto>();
        }
    }
}