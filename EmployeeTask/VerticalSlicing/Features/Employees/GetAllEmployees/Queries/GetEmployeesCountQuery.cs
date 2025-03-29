using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Data.Repository.Interface;
using EmployeeTask.VerticalSlicing.Data.Repository.Specification;
using EmployeeTask.VerticalSlicing.Data.Repository.Specification.EmployeeSpec;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.GetAllEmployees.Queries
{
    public record GetEmployeesCountQuery(SpecParams SpecParams) : IRequest<Result<int>>;

    public class GetEmployeesCountQueryHandler : IRequestHandler<GetEmployeesCountQuery, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeesCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(GetEmployeesCountQuery request, CancellationToken cancellationToken)
        {
            var EmployeeSpec = new CountEmployeeWithSpec(request.SpecParams);
            var count = await _unitOfWork.Repository<Employee>().GetCountWithSpecAsync(EmployeeSpec);

            return Result.Success(count);
        }
    }
 
}
