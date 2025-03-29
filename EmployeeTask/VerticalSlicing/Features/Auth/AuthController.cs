using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword;
using EmployeeTask.VerticalSlicing.Features.Auth.RefreshTokens.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.RevokePassword.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.SignIn.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.SignIn;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Orchestrator;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp;
using EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail;

namespace EmployeeTask.VerticalSlicing.Features.Auth
{
    public class AuthController:BaseController
    {
        public AuthController(ControllerParameters controllerParameters) : base(controllerParameters) { }
        [HttpPost("SignUp")]
        public async Task<Result> Register([FromForm] SignUpRequest viewModel)
        {
            var command = viewModel.Map<SignUpOrchestrator>();
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpPost("VerifyAccount")]
        public async Task<Result<bool>> Verify(VerifyAccountRequest request)
        {
            var command = request.Map<VerifyOTPByEmailCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("Login")]
        public async Task<Result<SignInResponse>> Login(SignInRequest request)
        {
            var command = request.Map<SignInCommand>();
            var result = await _mediator.Send(command);
            if (result.Data == null || string.IsNullOrEmpty(result.Data.RefreshToken))
            {
                return Result.Failure<SignInResponse>(UserErrors.InvalidCredentials);
            }
            CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);
            return result;

        }
        [HttpPost("RefreshToken")]
        public async Task<Result<SignInResponse>> RefreshToken()
        {
            var refreshToken = CookieHelper.GetRefreshTokenCookie(Request);
            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));
            if (!result.IsSuccess)
                return Result.Failure<SignInResponse>(UserErrors.InvalidRefreshToken);
            CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);

            return result;

        }

        [HttpPost("RevokeToken")]
        public async Task<Result<bool>> RevokeToken(string? token)
        {
            var result = await _mediator.Send(new RevokeTokenCommand(token ?? Request.Cookies["refreshToken"]));
            if (string.IsNullOrEmpty(token))
                return Result.Failure<bool>(UserErrors.TokenIsRequired);
            return result;
        }

        [HttpPost("ForgotPassword")]
        public async Task<Result<bool>> ForgotPassword(ForgetPasswordRequest request)
        {
            var command = request.Map<ForgotPasswordCommand>();

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpPost("ResetPassword")]
        public async Task<Result<bool>> ResetPassword(ResetPasswordCommand request)
        {
            var command = request.Map<ResetPasswordCommand>();
            var response = await _mediator.Send(command);

            return response;
        }
    }
}
