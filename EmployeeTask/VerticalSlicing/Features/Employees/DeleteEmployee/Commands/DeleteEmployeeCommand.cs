using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Queries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.DeleteEmployee.Commands
{
    public record DeleteEmployeeCommand(int EmployeeId):IRequest<Result<bool>>;
    public class DeleteEmployeeCommandHandler : BaseRequestHandler<DeleteEmployeeCommand, Result<bool>>
    {
        public DeleteEmployeeCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }
            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(request.EmployeeId));

            if (!employeeResult.IsSuccess)
                return Result.Failure<bool>(employeeResult.Error);

            if (employeeResult.Data.IsActive == true)
                return Result.Failure<bool>(EmployeeErrors.InvalidDeletion);

            var employee = employeeResult.Data;

            _unitOfWork.Repository<Employee>().DeleteById(employee.Id);
            await _unitOfWork.SaveChangesAsync();

            await _auditService.LogAsync(int.Parse(userId), "Delete", "Employee",
           $"Deleted Employee {employee.Id} - {employee.FullName}");  

            return Result.Success(true);
        }
    }

}
