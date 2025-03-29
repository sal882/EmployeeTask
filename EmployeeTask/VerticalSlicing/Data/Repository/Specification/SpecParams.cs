namespace EmployeeTask.VerticalSlicing.Data.Repository.Specification;

public class SpecParams
{
    public string? SearchByName { get; set; }
    public string? SearchByJobTitle { get; set; }
    public string? Sort { get; set; }
    public int PageSize { get; set; } = 5;
    public int PageIndex { get; set; } = 1;
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public bool IncludeSalaryDetails { get; set; } = false;
}