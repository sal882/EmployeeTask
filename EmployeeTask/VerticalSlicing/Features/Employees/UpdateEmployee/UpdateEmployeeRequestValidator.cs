using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Commands;
using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee ID is required");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be positive");
        }
    }
}
