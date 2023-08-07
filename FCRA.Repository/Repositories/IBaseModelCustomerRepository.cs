using FCRA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IBaseModelCustomerRepository<T> where T : class
    {
        IQueryable<T> GetAsync(string[]? includes = null);
        Task<T?> GetAsync(int customerId, int id, string[]? includes = null);
        Task<bool> AddAsync(T entity);
        Task<bool> AddAsync(List<T> entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> Find(int customerId, Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
        Task<bool> AuditTrail(DataAuditTrail model);
    }
}
