using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Common.CommonQueries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SendVerificationOTPToEmail
{
    public record SendVerificationOTPToEmailCommand(string Email) : IRequest<Result<bool>>;
    public class SendVerificationOTPToEmailCommandHandler : BaseRequestHandler<SendVerificationOTPToEmailCommand, Result<bool>>
    {
        public SendVerificationOTPToEmailCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public async override Task<Result<bool>> Handle(SendVerificationOTPToEmailCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }
            var user = userResult.Data;
            if (user.IsVerified)
            {
                return Result.Failure<bool>(UserErrors.EmailIsAlreadyVerified);
            }
            var otpCode = GenerateOTP();
            user.VerificationOTP = otpCode;
            user.VerificationOTPExpiration = DateTime.Now.AddMinutes(5);

            var userRepo = _unitOfWork.Repository<User>();
            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            var emailContent = $"Your OTP code to verify your Account is: {otpCode}";
            await _emailSenderHelper.SendEmailAsync(request.Email, "Verify Your Account", emailContent);

            return Result.Success(true);
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
