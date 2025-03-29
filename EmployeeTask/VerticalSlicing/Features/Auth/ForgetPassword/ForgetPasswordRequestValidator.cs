using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword
{
    public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required");
        }
    }
}
