
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Data.Repository.Interface;

namespace EmployeeTask.VerticalSlicing.Common.AuditService
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LogAsync(int userId, string action, string entity, string details)
        {
            var auditLog = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityAffected = entity,
                Details = details
            };

            await _unitOfWork.Repository<AuditLog>().AddAsync(auditLog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
    
}
