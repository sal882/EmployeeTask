namespace EmployeeTask.VerticalSlicing.Common.AuditService
{
    public interface IAuditService
    {
        Task LogAsync(int userId, string action, string entity, string details);
    }
}
