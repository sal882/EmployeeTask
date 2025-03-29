using AutoMapper;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Employees.GetAllEmployees;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.ListRecipes
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, GetAllEmployeesResponse>();
                    
        }
    }
}
