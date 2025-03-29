using AutoMapper;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee.Commands;

namespace EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AddNewEmployeeRequest, AddNewEmployeeCommand>();
            CreateMap<AddNewEmployeeCommand, Employee>();
        }
    }
}
