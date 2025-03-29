using AutoMapper;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee.Commands;
using EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Commands;

namespace EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateEmployeeRequest, UpdateEmployeeCommand>();

            CreateMap<UpdateEmployeeCommand, Employee>();
        }
    }
}
