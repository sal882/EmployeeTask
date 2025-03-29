using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Orchestrator;
using EmployeeTask.VerticalSlicing.Features.Auth.SignUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeTask.VerticalSlicing.Features.Employees.DeleteEmployee.Commands;
using EmployeeTask.VerticalSlicing.Features.Roles.ModifyUserRole.Commands;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTask.VerticalSlicing.Features.Roles
{
    public class RolesController : BaseController
    {
        public RolesController(ControllerParameters controllerParameters) : base(controllerParameters) { }
        [Authorize(Roles = "Admin")]
        [HttpPut("ModifyUserRole")]
        public async Task<Result<bool>> ModifyUserRole(int userId, string roleName)
        {
            var command = new ModifyUserRoleCommand(userId,roleName);
            var response = await _mediator.Send(command);
            return response;
        }
    }
}
