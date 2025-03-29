using EmployeeTask.VerticalSlicing.Data.Entities;

namespace EmployeeTask.VerticalSlicing.Data.Repository.Specification.EmployeeSpec
{
    public class CountEmployeeWithSpec : BaseSpecification<Employee>
    {
        public CountEmployeeWithSpec(SpecParams specParams)
        : base(p => !p.IsDeleted)
        {

        }
    }
    
}
