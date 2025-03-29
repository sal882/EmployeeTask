using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee.Commands
{
    public record AddNewEmployeeCommand(
    string FullName,
    string EmployeeCode,
    string JobTitle,
    decimal Salary,
    string Email,
    bool IsActive = true) : IRequest<Result<int>>;
    public class AddNewEmployeeCommandHandler : BaseRequestHandler<AddNewEmployeeCommand, Result<int>>
    {
        public AddNewEmployeeCommandHandler(RequestParameters requestParameters): base(requestParameters) { }
        public override async Task<Result<int>> Handle(AddNewEmployeeCommand request,CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<int>(UserErrors.UserNotAuthenticated);
            }

            var existingEmployee = await _unitOfWork.Repository<Employee>()
                .FirstAsync(e => e.EmployeeCode == request.EmployeeCode);

            if (existingEmployee != null)
                return Result.Failure<int>(EmployeeErrors.CodeAlreadyExist);

            var employee = new Employee
            {
                FullName = request.FullName,
                EmployeeCode = request.EmployeeCode,
                JobTitle = request.JobTitle,
                Salary = request.Salary,
                IsActive = request.IsActive,
                Email = request .Email,
            };

            await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            await _auditService.LogAsync(int.Parse(userId), "Create", "Employee",
            $"Added Employee {employee.Id} - {employee.FullName}");

            return Result.Success(employee.Id);
        }
    }
}
