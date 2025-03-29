using AutoMapper;
using EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword.Commands;

namespace EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ForgetPasswordRequest, ForgotPasswordCommand>();

        }
    }
}
