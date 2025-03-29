using EmployeeTask.API.VerticalSlicing.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.VerticalSlicing.Data.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public int? PhotoId { get; set; }
        public Attachments Photo { get; set; }
        public Gender  Gender { get; set; }
        public string? VerificationOTP { get; set; }
        public DateTime? VerificationOTPExpiration { get; set; }
        public string? PasswordResetOTP { get; set; }
        public DateTime? PasswordResetOTPExpiration { get; set; }
        public bool IsPasswordResetOTPVerified { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<AuditLog> AuditLogs { get; set; }

    }
}
