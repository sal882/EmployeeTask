using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Features.AditLogs.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.VerticalSlicing.Features.AditLogs
{
    public class AuditLogsController : BaseController
    {
        public AuditLogsController(ControllerParameters controllerParameters) : base(controllerParameters) { }
        [HttpGet("GetAuditLogs")]
        public async Task<IActionResult> GetAuditLogs()
        {
            var logsResult = await _mediator.Send(new GetAuditLogsQuery());

            if (!logsResult.IsSuccess)
                return NotFound(logsResult.Error);

            return Ok(logsResult.Data);
        }
    }
}
