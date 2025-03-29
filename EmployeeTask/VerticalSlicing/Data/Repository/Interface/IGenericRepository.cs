using System.Linq.Expressions;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Data.Repository.Specification;

namespace EmployeeTask.VerticalSlicing.Data.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);
        Task<int> SaveChangesAsync();
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAsyncToInclude(Expression<Func<T, bool>> expression);
        Task<int> GetCountWithSpecAsync(ISpecification<T> Spec);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
    }
}
