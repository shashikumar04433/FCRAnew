﻿using FCRA.Models;
using FCRA.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class BaseModelCustomerRepository<T> : IBaseModelCustomerRepository<T> where T : BaseCustomerModel
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseModelCustomerRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IQueryable<T> GetAsync(string[]? includes = null)
        {
            var query = _dbSet.Include(t => t.CreatedByUser).Include(t => t.UpdatedByUser).AsNoTracking();
            if (includes != null && includes.Length > 0)
            {
                foreach (var icludeItem in includes)
                {
                    query = query.Include(icludeItem);
                }
            }
            return query;
        }
        public Task<T?> GetAsync(int customerId, int id, string[]? includes = null)
        {
            var query = _dbSet.AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var icludeItem in includes)
                {
                    query = query.Include(icludeItem);
                }
            }
            return query.FirstOrDefaultAsync(t => t.Id == id && t.CustomerId == customerId);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> AddAsync(List<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.Id).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<T>> Find(int customerId, Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(t => t.CustomerId == customerId).Where(predicate).AsNoTracking().ToListAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AuditTrail(DataAuditTrail model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return true; 
        }
    }
}
