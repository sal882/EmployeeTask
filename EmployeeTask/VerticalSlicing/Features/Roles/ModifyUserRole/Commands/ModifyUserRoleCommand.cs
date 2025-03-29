using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Common.CommonQueries;
using EmployeeTask.VerticalSlicing.Features.Roles.Common;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeTask.VerticalSlicing.Features.Roles.ModifyUserRole.Commands
{
    public record ModifyUserRoleCommand(int userId, string roleName) : IRequest<Result<bool>>;
    public class ModifyUserRoleCommandHandler : BaseRequestHandler<ModifyUserRoleCommand, Result<bool>>
    {

        public ModifyUserRoleCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(ModifyUserRoleCommand request, CancellationToken cancellationToken)
        {
            var adminId = _userState.ID;
            if (string.IsNullOrEmpty(adminId))
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }

            var adminUserResult = await _mediator.Send(new GetUserByIdQuery(int.Parse(adminId)));
            if (!adminUserResult.IsSuccess || adminUserResult.Data == null)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            var validationResult = await ValidateUserRole(request.userId, request.roleName);
            if (!validationResult.IsSuccess)
            {
                return Result.Failure<bool>(validationResult.Error);
            }
            var role = validationResult.Data;

            var resultuser = await _mediator.Send(new GetUserByIdQuery(request.userId));
            if (!resultuser.IsSuccess || resultuser.Data == null)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            var user = resultuser.Data;
            user.RoleId = role.Id;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }

        private async Task<Result<Role>> ValidateUserRole(int userId, string roleName)
        {
            var resultuser = await _mediator.Send(new GetUserByIdQuery(userId));
            if (!resultuser.IsSuccess || resultuser.Data == null)
            {
                return Result.Failure<Role>(UserErrors.UserNotFound);
            }

            var roleResult = await _mediator.Send(new GetRoleByNameQuery(roleName));
            if (!roleResult.IsSuccess || roleResult.Data == null)
            {
                return Result.Failure<Role>(RoleErrors.RoleNotFound);
            }
            var role = roleResult.Data;

            return Result.Success(role);
        }

    }
}