using EmployeeTask.VerticalSlicing.Features.Auth.SignUp;
using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignUp
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required");

            RuleFor(x => x.PhoneNumber)
                     .NotEmpty().WithMessage("Phone number is required.")
                     .Matches(@"^01\d{9}$").WithMessage("Phone number must start with 01 and be 11 digits long.");

            RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                    .WithMessage("Password must be at least 8 characters long, and include at least one uppercase letter, one lowercase letter, one digit, and one special character");

            RuleFor(x => x.ConfirmPassword)
             .NotEmpty().WithMessage("Password is required.");

        }
    }
}
