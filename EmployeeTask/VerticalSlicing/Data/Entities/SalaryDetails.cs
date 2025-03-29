namespace EmployeeTask.VerticalSlicing.Data.Entities
{
    public class SalaryDetails:BaseEntity
    {
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public Employee Employee { get; set; }
    }
}
