using FCRA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IRepositoryCustomer<T> where T : class
    {
        IQueryable<T> GetAsync(string[]? includes = null);
        IQueryable<T> GetWithoutOrderAsync(string[]? includes = null);
        Task<T?> GetAsync(int customerId, int id, string[]? includes = null);
        Task<bool> AddAsync(T entity);
        Task<bool> AddAsync(List<T> entity);
        Task<bool> UpdateAsync(T entity);
        Task<IEnumerable<T>> Find(int customerId, Expression<Func<T, bool>> predicate, string[]? includes = null);
        Task SaveChangesAsync();
        Task<bool> AuditTrail(DataAuditTrail model);
        IQueryable<T> GetAuditTrailAsync(int objectId, string objectname);
    }
}
