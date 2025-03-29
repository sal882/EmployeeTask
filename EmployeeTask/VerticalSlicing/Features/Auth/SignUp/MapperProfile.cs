using AutoMapper;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.SignIn;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Orchestrator;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignUp
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignUpRequest, SignUpCommand>();
            CreateMap<SignUpOrchestrator, SignUpCommand>();
            CreateMap<SignUpRequest, SignUpOrchestrator>();
            //CreateMap<SignUpCommand, User>();
            CreateMap<SignUpCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());


        }
    }
}
