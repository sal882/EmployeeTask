namespace EmployeeTask.VerticalSlicing.Data.Entities
{
    public class AuditLog:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Action { get; set; }
        public string EntityAffected { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
