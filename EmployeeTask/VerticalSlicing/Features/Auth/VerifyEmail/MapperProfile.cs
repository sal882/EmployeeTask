using AutoMapper;
using EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail.Commands;

namespace EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VerifyAccountRequest, VerifyOTPByEmailCommand>();
        }
    }
}
