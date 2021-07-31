using AutoMapper;
using Identity.Controller.Contracts;
using Identity.Models;

namespace Identity.Options
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserReportOutputDto>();
        }
    }
}