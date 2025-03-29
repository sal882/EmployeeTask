using EmployeeTask.VerticalSlicing.Features.Roles;
using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Roles
{
    public class AssignRoleToUserRequestValidator : AbstractValidator<AssignRoleToUserRequest>
    {
        public AssignRoleToUserRequestValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("RoleName is required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId Is required");
        }
    }
}
