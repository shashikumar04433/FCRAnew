﻿using FCRA.Models.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class ProductRiskCriteriaMappingRepository : BaseModelCustomerRepository<ProductRiskCriteriaMapping>, IProductRiskCriteriaMappingRepository
    {
        public ProductRiskCriteriaMappingRepository(ApplicationDBContext context) : base(context)
        {
        }
        public override async Task<bool> UpdateAsync(ProductRiskCriteriaMapping entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.RiskFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.ProductId).IsModified = false;
            _context.Entry(entity).Property(t => t.RiskCriteriaId).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }
    }
}
