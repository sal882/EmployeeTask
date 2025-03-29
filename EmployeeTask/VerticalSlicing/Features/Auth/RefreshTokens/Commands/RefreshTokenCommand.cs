using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.RefreshTokens.Queries;
using EmployeeTask.VerticalSlicing.Features.Auth.SignIn;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.RefreshTokens.Commands
{
    public record RefreshTokenCommand(string token) : IRequest<Result<SignInResponse>>;

    public class RefreshTokenCommandHandler : BaseRequestHandler<RefreshTokenCommand, Result<SignInResponse>>
    {
        public RefreshTokenCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<SignInResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByRefreshToken(request.token));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<SignInResponse>(UserErrors.InvalidRefreshToken);
            }

            var user = userResult.Data;

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Result.Failure<SignInResponse>(UserErrors.InvalidRefreshToken);
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = TokenGenerator.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            var userRepo = _unitOfWork.Repository<User>();
            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            var jwtToken = TokenGenerator.GenerateToken(user);

            var loginResponse = new SignInResponse
            {
                Id = user.Id,
                Email = user.Email,
                Token = jwtToken,
                RefreshToken = newRefreshToken.Token
            };

            return Result.Success(loginResponse);

        }
    }
}
