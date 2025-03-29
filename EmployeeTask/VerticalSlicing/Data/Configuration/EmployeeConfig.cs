using EmployeeTask.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.VerticalSlicing.Data.Configuration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder.ToTable("Employees");
            //builder.HasBaseType<User>();

            builder.Property(e => e.EmployeeCode)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.HasIndex(e => e.EmployeeCode)
                   .IsUnique();

            builder.Property(e => e.Salary)
                   .HasColumnType("decimal(12,2)");
        }
    }

}
