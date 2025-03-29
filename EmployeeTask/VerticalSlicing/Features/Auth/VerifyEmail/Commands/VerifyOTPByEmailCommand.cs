using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Common.CommonQueries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail.Commands
{
    public record VerifyOTPByEmailCommand(string PhoneNumber, string OTP) : IRequest<Result<bool>>;

    public class VerifyOTPCommandHandler : BaseRequestHandler<VerifyOTPByEmailCommand, Result<bool>>
    {
        public VerifyOTPCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(VerifyOTPByEmailCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.PhoneNumber));
            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            var user = userResult.Data;

            if (user.VerificationOTPExpiration is not null && user.VerificationOTPExpiration < DateTime.Now)
            {
                return Result.Failure<bool>(UserErrors.OTPExpired);
            }

            if (user.VerificationOTP is not null && user.VerificationOTP != request.OTP)
            {
                return Result.Failure<bool>(UserErrors.InvalidOTP);
            }

            user.VerificationOTP = null;
            user.VerificationOTPExpiration = null;
            user.IsVerified = true;

            await _unitOfWork.Repository<User>().SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
