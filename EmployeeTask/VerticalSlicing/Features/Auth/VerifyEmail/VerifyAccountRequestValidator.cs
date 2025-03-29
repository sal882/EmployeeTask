using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail
{
    public class VerifyAccountRequestValidator : AbstractValidator<VerifyAccountRequest>
    {
        public VerifyAccountRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.OTP).NotEmpty().WithMessage("OTP is required");
        }
    }
}
