using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Data.Repository.Interface;

namespace EmployeeTask.VerticalSlicing.Data.Repository.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
