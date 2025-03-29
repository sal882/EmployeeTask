using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword.Commands;
using EmployeeTask.VerticalSlicing.Features.Auth.ForgetPassword;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee;
using EmployeeTask.VerticalSlicing.Features.Employees.AddNewEmpployee.Commands;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Queries;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee.Commands;
using EmployeeTask.VerticalSlicing.Features.Employees.UpdateEmployee;
using EmployeeTask.VerticalSlicing.Data.Repository.Specification;
using EmployeeTask.VerticalSlicing.Features.Employees.GetAllEmployees;
using EmployeeTask.VerticalSlicing.Features.Employees.GetAllEmployees.Queries;
using EmployeeTask.VerticalSlicing.Features.Employees.DeleteEmployee.Commands;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTask.VerticalSlicing.Features.Employees
{
 
    public class EmployeeController : BaseController
    {
        public EmployeeController(ControllerParameters controllerParameters) : base(controllerParameters) { }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddNewEmployee")]
        public async Task<IActionResult> AddNewEmployee(AddNewEmployeeRequest request)
        {
            var command = request.Map<AddNewEmployeeCommand>();

            var response = await _mediator.Send(command);

            return response.IsSuccess
                ? Ok(response.Data)
                : NotFound(response.Error);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var query = new GetEmployeeByIdQuery(employeeId);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Data)
                : NotFound(result.Error);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest request)
        {

            var command = request.Map<UpdateEmployeeCommand>();
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("ListEmployees")]
        public async Task<Result<Pagination<GetAllEmployeesResponse>>> GetAllEmployees([FromQuery] SpecParams spec)
        {
            var result = await _mediator.Send(new GetEmployeesQuery(spec));
            if (!result.IsSuccess)
            {
                return Result.Failure<Pagination<GetAllEmployeesResponse>>(result.Error);
            }

            var EmployeesCount = await _mediator.Send(new GetEmployeesCountQuery(spec));
            var paginationResult = new Pagination<GetAllEmployeesResponse>(spec.PageSize, spec.PageIndex, EmployeesCount.Data, result.Data);

            return Result.Success(paginationResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteEmployee")]
        public async Task<Result<bool>> DeleteRecipe(int EmployeeId)
        {
            var command = new DeleteEmployeeCommand(EmployeeId);
            var response = await _mediator.Send(command);
            return response;
        }

    }
}
