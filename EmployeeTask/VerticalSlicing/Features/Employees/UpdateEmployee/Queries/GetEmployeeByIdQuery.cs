using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Queries
{
    public record GetEmployeeByIdQuery(int EmployeeId) : IRequest<Result<Employee>>;

    // Handler
    public class GetEmployeeByIdQueryHandler: BaseRequestHandler<GetEmployeeByIdQuery, Result<Employee>>
    {
        public GetEmployeeByIdQueryHandler(RequestParameters requestParameters): base(requestParameters) { }

        public override async Task<Result<Employee>> Handle(GetEmployeeByIdQuery request,CancellationToken cancellationToken)
        {
            if (request.EmployeeId <= 0)
                return Result.Failure<Employee>(EmployeeErrors.InvalidId);

            var employee = await _unitOfWork.Repository<Employee>()
                .GetByIdAsync(request.EmployeeId);

            return employee != null
                ? Result.Success(employee)
                : Result.Failure<Employee>(EmployeeErrors.NotFound);
        }
    }
}
