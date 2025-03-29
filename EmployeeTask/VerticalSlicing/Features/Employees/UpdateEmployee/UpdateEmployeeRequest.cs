namespace EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee
{
    public record UpdateEmployeeRequest(
    int EmployeeId,
    string FullName,
    string JobTitle,
    decimal Salary,
    bool IsActive
    );
}
