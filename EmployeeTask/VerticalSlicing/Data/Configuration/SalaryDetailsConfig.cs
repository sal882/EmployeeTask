using EmployeeTask.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.VerticalSlicing.Data.Configuration
{
    public class SalaryDetailsConfiguration : IEntityTypeConfiguration<SalaryDetails>
    {
        public void Configure(EntityTypeBuilder<SalaryDetails> builder)
        {
            builder.Property(s => s.BasicSalary)
                   .HasColumnType("decimal(12,2)");

            builder.Property(s => s.Allowances)
                   .HasColumnType("decimal(12,2)");

            builder.Property(s => s.Deductions)
                   .HasColumnType("decimal(12,2)");
        }
    }
}
