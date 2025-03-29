using AutoMapper;
using EmployeeTask.VerticalSlicing.Features.Auth.SignIn.Commands;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignIn
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignInRequest, SignInCommand>();
        }
    }
}
