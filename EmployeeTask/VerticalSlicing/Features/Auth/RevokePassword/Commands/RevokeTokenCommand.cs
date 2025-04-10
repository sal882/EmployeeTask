﻿using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.RefreshTokens.Queries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Auth.RevokePassword.Commands
{
    public record RevokeTokenCommand(string token) : IRequest<Result<bool>>;

    public class RevokeTokenCommandHandler : BaseRequestHandler<RevokeTokenCommand, Result<bool>>
    {
        public RevokeTokenCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByRefreshToken(request.token));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.InvalidRefreshToken);
            }

            var user = userResult.Data;

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Result.Failure<bool>(UserErrors.InvalidRefreshToken);
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var userRepo = _unitOfWork.Repository<User>();

            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
