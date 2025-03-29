using AutoMapper;
using EmployeeTask.VerticalSlicing.Features.Roles.AssignRoleToUser.Commands;
using EmployeeTask.VerticalSlicing.Features.Roles;

namespace EmployeeTask.VerticalSlicing.Features.Roles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AssignRoleToUserRequest, AddRoleToUserCommand>();

        }
    }
}
