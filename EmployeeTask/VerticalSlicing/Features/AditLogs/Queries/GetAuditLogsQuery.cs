using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Data.Repository.Interface;
using EmployeeTask.VerticalSlicing.Features.UserProfile;
using MediatR;

namespace EmployeeTask.VerticalSlicing.Features.AditLogs.Queries
{
    public record GetAuditLogsQuery() : IRequest<Result<List<AuditLog>>>;
    public class GetAuditLogsQueryHandler : BaseRequestHandler<GetAuditLogsQuery, Result<List<AuditLog>>>
    {
        public GetAuditLogsQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<List<AuditLog>>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _unitOfWork.Repository<AuditLog>().GetAllAsync();

            if (logs == null || !logs.Any())
                return Result.Failure<List<AuditLog>>(UserErrors.NoAuditFound);

            return Result.Success(logs.ToList());
        }
    }

}
