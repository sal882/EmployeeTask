using EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee.Commands;
using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee
{
    public class AddNewEmployeeValidator : AbstractValidator<AddNewEmployeeCommand>
    {
        public AddNewEmployeeValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required");
            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Employee code is required")
                .Matches(@"^[A-Z0-9-]+$").WithMessage("Invalid employee code format");
            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Job title is required");
            RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be positive");
        }
    }
}
