using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.Common;
using EmployeeTask.VerticalSlicing.Features.Common.CommonQueries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword.Commands
{
    public record ResetPasswordCommand(string Email, string OTP, string NewPassword, string ConfirmPassword) : IRequest<Result<bool>>;

    public class ResetPasswordCommandHandler : BaseRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        public ResetPasswordCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = userResult.Data;

            if (user == null)
                return Result.Failure<bool>(UserErrors.UserNotFound);

            if (user.PasswordResetOTP != request.OTP)
                return Result.Failure<bool>(UserErrors.InvalidResetCode);

            if (user.PasswordResetOTPExpiration is not null && user.PasswordResetOTPExpiration < DateTime.Now)
            {
                return Result.Failure<bool>(UserErrors.OTPExpired);
            }

            if (user.PasswordResetOTP is not null && user.PasswordResetOTP != request.OTP)
            {
                return Result.Failure<bool>(UserErrors.InvalidResetCode);
            }

            if (request.NewPassword != request.ConfirmPassword)
                return Result.Failure<bool>(UserErrors.PasswordsDoNotMatch);

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);

            user.PasswordResetOTP = null;
            user.PasswordResetOTPExpiration = null;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
