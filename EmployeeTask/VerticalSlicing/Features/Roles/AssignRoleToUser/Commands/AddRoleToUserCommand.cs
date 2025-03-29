using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Common.CommonQueries;
using EmployeeTask.VerticalSlicing.Features.Roles;
using EmployeeTask.VerticalSlicing.Features.Roles.Common;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Roles.AssignRoleToUser.Commands
{
    public record AddRoleToUserCommand(int userId, string roleName) : IRequest<Result<bool>>;
    public class AddRoleToUserCommandHandler : BaseRequestHandler<AddRoleToUserCommand, Result<bool>>
    {
        public AddRoleToUserCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
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
            //var userRolesResult = await _mediator.Send(new GetUserRoleByIdQuery(userId, role.Id));

            //if (userRolesResult.IsSuccess)
            //{
            //    return Result.Failure<Role>(RoleErrors.RoleAlreadyExists);
            //}
            return Result.Success(role);
        }
    }
}
