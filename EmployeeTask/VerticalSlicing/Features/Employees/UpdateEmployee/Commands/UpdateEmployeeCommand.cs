using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Queries;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Commands
{
    public record UpdateEmployeeCommand(
    int EmployeeId,
    string FullName,
    string JobTitle,
    decimal Salary,
    bool IsActive) : IRequest<Result>;
    public class UpdateEmployeeCommandHandler : BaseRequestHandler<UpdateEmployeeCommand, Result>
    {
        public UpdateEmployeeCommandHandler(RequestParameters requestParameters): base(requestParameters) { }

        public override async Task<Result> Handle(UpdateEmployeeCommand request,CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }
            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(request.EmployeeId));

            if (!employeeResult.IsSuccess)
                return Result.Failure(employeeResult.Error);

            var employee = employeeResult.Data;
            string oldData = $"Old: Name={employee.FullName}, Salary={employee.Salary}";

            employee.FullName = request.FullName;
            employee.JobTitle = request.JobTitle;
            employee.Salary = request.Salary;
            employee.IsActive = request.IsActive;
 
            _unitOfWork.Repository<Employee>().Update(employee);
            await _unitOfWork.SaveChangesAsync();

            string newData = $"New: Name={employee.FullName}, Salary={employee.Salary}";
            await _auditService.LogAsync(int.Parse(userId), "Update", "Employee", $"{oldData} -> {newData}");

            return Result.Success();
        }
    }

}
