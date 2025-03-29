using MediatR;
using Microsoft.AspNetCore.Identity;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.SignIn.Queries;
using EmployeeTask.VerticalSlicing.Features.Common.CommonQueries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignIn.Commands
{
    public record SignInCommand(string Email, string Password) : IRequest<Result<SignInResponse>>;

    public class SignInCommandHandler : BaseRequestHandler<SignInCommand, Result<SignInResponse>>
    {
        public SignInCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Result.Failure<SignInResponse>(UserErrors.InvalidCredentials);
            }

            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<SignInResponse>(UserErrors.InvalidCredentials);
            }

            var user = userResult.Data;
            if (!PasswordHasher.checkPassword(request.Password, user.PasswordHash) /*|| !user.IsEmailVerified*/)
            {
                return Result.Failure<SignInResponse>(UserErrors.InvalidCredentials);
            }

            var token = TokenGenerator.GenerateToken(user);


            var signInResponse = new SignInResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
            };

            var userRepo = _unitOfWork.Repository<User>();
            var refreshTokensResult = await _mediator.Send(new GetUserActiveRefreshTokensQuery(user.Id));

            if (refreshTokensResult.IsSuccess)
            {
                var refreshTokens = refreshTokensResult.Data;
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                signInResponse.RefreshToken = activeRefreshToken.Token;
            }
            else
            {
                var newRefreshToken = TokenGenerator.GenerateRefreshToken();
                user.RefreshTokens.Add(newRefreshToken);
                userRepo.Update(user);
                await userRepo.SaveChangesAsync();
                signInResponse.RefreshToken = newRefreshToken.Token;

            }

            return Result.Success(signInResponse);


        }

    }
}
