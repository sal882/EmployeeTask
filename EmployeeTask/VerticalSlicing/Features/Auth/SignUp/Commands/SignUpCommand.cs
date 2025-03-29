using MediatR;
using EmployeeTask.Abstractions.Const;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Queries;
using EmployeeTask.VerticalSlicing.Features.Roles;
using EmployeeTask.VerticalSlicing.Features.Roles.Common;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Queries;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Commands
{
    public record SignUpCommand(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword) : IRequest<Result>;
    public class RegisterCommandHandler : BaseRequestHandler<SignUpCommand, Result>
    {
        public RegisterCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new CheckUserExistsQuery(request.FullName, request.Email));

            if (userExists.Data)
            {
                return Result.Failure<bool>(UserErrors.UserAlreadyExists);
            }
            var employeeResult = await _mediator.Send(new GetEmployeeByEmailQuery(request.Email));
            bool isEmployee = employeeResult.IsSuccess && employeeResult.Data != null;

            //var user = request.Map<User>();
            var student = request.Map<User>();


            if (request.Password != request.ConfirmPassword)
                return Result.Failure<bool>(UserErrors.PasswordsDoNotMatch);


            student.PasswordHash = PasswordHasher.HashPassword(request.Password);

            var roleName = isEmployee ? DefaultRoles.Employee : DefaultRoles.User;
            var roleResult = await _mediator.Send(new GetRoleByNameQuery(roleName));

            if (!roleResult.IsSuccess || roleResult.Data == null)
            {
                return Result.Failure<bool>(RoleErrors.RoleNotFound);
            }

            student.RoleId = roleResult.Data.Id;

            var studentRepo = _unitOfWork.Repository<User>();

            await studentRepo.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();



            return Result.Success();

        }
    }
}
