using EmployeeTask.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.VerticalSlicing.Data.Repository.Specification.EmployeeSpec
{
    public class EmployeeSpecification:BaseSpecification<Employee>
    {
        public EmployeeSpecification(int id)
        : base(r => r.Id == id)
        {
            Includes.Add(p => p.Include(p => p.SalaryDetails));

        }
        public EmployeeSpecification(SpecParams spec)
        {
            Includes.Add(p => p.Include(p => p.SalaryDetails));


            if (!string.IsNullOrEmpty(spec.SearchByName))
            {
                Criteria = e => e.FullName.ToLower().Contains(spec.SearchByName.ToLower());
            }
            if (!string.IsNullOrEmpty(spec.SearchByJobTitle))
            {
                Criteria = e => e.JobTitle.ToLower().Contains(spec.SearchByJobTitle.ToLower());
            }


            if (spec.MinSalary.HasValue && spec.MaxSalary.HasValue)
            {
                Criteria = e => e.Salary >= spec.MinSalary && e.Salary <= spec.MaxSalary;
            }

            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort.ToLower())
                {
                    case "name":
                        AddOrderBy(e => e.FullName);
                        break;
                    case "salary":
                        AddOrderBy(e => e.Salary);
                        break;
                    default:
                        AddOrderBy(e => e.JobTitle);
                        break;
                }
            }

            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
        }
    }
}
