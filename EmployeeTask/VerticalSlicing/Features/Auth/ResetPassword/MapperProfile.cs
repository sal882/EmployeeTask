using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword.Commands;

namespace EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ResetPasswordRequest, ResetPasswordCommand>();

        }
    }
}
