using Microsoft.EntityFrameworkCore;
using System.Reflection;
using EmployeeTask.VerticalSlicing.Data.Entities;
using System.Net.Mail;

namespace EmployeeTask.VerticalSlicing.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SalaryDetails> SalaryDetails { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserRoleChangeLog> UserRoleChangeLogs { get; set; }


    }
}
