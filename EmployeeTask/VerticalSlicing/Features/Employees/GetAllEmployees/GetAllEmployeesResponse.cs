using EmployeeTask.VerticalSlicing.Data.Entities;

namespace EmployeeTask.VerticalSlicing.Features.Employees.GetAllEmployees
{
    public class GetAllEmployeesResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public SalaryDetails SalaryDetails { get; set; }

    }
}
