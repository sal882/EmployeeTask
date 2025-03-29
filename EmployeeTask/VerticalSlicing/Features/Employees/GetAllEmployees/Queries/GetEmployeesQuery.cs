using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Data.Repository.Specification;
using EmployeeTask.VerticalSlicing.Data.Repository.Specification.EmployeeSpec;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.Employees.GetAllEmployees.Queries
{
    public record GetEmployeesQuery(SpecParams SpecParams) : IRequest<Result<List<GetAllEmployeesResponse>>>;

    public class GetRecipesQueryHandler : BaseRequestHandler<GetEmployeesQuery, Result<List<GetAllEmployeesResponse>>>
    {
        public GetRecipesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<List<GetAllEmployeesResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var spec = new EmployeeSpecification(request.SpecParams);
            var employee = await _unitOfWork.Repository<Employee>().GetAllWithSpecAsync(spec);


            var response = employee.Select(e => new GetAllEmployeesResponse
            {
                Id=e.Id,
                EmployeeCode = e.EmployeeCode,
                FullName = e.FullName,
                JobTitle = e.JobTitle,
                Salary = e.Salary,
                IsActive=e.IsActive,
                SalaryDetails = request.SpecParams.IncludeSalaryDetails ? new SalaryDetails
                {
                    BasicSalary = e.SalaryDetails?.BasicSalary ?? 0,
                    Allowances = e.SalaryDetails?.Allowances ?? 0,
                    Deductions = e.SalaryDetails?.Deductions ?? 0
                } : null
            }).ToList();


            return Result.Success(response);
        }
    }
}
