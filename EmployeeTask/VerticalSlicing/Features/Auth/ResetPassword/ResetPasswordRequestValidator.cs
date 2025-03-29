﻿using EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword;
using FluentValidation;

namespace EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage("OTP is required");

            RuleFor(x => x.NewPassword)
                 .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required");
        }
    }
}
