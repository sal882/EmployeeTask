namespace EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee
{
    public class AddNewEmployeeRequest
    {
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public string Email { get; set; }
    }
}
