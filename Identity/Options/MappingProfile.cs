using AutoMapper;
using Identity.Controller.Contracts;

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