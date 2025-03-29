using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Queries
{
    public record GetEmployeeByEmailQuery(string Email) : IRequest<Result<Employee>>;

    public class GetEmployeeByEmailQueryHandler : BaseRequestHandler<GetEmployeeByEmailQuery, Result<Employee>>
    {

        public GetEmployeeByEmailQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Employee>> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Repository<Employee>()
                        .FirstAsync(e=>e.Email == request.Email);

            if (employee == null)
            {
                return Result.Failure<Employee>(UserErrors.UserNotFound);
            }

            return Result.Success(employee);
        }
    }
}
