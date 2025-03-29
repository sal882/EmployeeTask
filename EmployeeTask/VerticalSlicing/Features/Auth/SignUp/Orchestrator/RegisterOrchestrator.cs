using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.SendVerificationOTPToEmail;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Commands;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Orchestrator
{
    public record SignUpOrchestrator(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword) : IRequest<Result<bool>>;
    public class RegisterOrchestratorHandler : BaseRequestHandler<SignUpOrchestrator, Result<bool>>
    {
        public RegisterOrchestratorHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(SignUpOrchestrator request, CancellationToken cancellationToken)
        {
            var command = request.Map<SignUpCommand>();

            var RegisterResult = await _mediator.Send(command);

            if (!RegisterResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserDoesntCreated);
            }

            await _mediator.Send(new SendVerificationOTPToEmailCommand(request.Email));


            return Result.Success(true);

        }
    }
}
