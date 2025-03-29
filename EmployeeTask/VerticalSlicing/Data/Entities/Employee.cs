namespace EmployeeTask.VerticalSlicing.Data.Entities
{
    public class Employee:BaseEntity
    {
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public string Email { get; set; }
        public int? SalaryDetailsId { get; set; }
        public SalaryDetails SalaryDetails { get; set; }
    }
}
