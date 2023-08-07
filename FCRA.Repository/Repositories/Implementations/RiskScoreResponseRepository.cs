using FCRA.Models.Customers;
using FCRA.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class RiskScoreResponseRepository : BaseModelCustomerRepository<RiskScoreResponse>, IRiskScoreResponseRepository

    {
        public RiskScoreResponseRepository(ApplicationDBContext context) : base(context)
        {

        }
        public override async Task<bool> UpdateAsync(RiskScoreResponse entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.RiskFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.ProductId).IsModified = false;
            _context.Entry(entity).Property(t => t.RiskCriteriaId).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }

        public override async Task<bool> DeleteAsync(RiskScoreResponse entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }

        public async Task<List<RiskScoreResponse>> GetRiskScoreResponse(int customerId, List<int> riskFactorIds)
        {
            return await _context.RiskScoreResponses
               .Include(t => t.RiskFactor)
               .Include(t => t.ProductService)
               .Include(t => t.RiskCriteria)
               .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }
        public async Task<List<ApprovedRiskScoreResponse>> GetApprovedRiskScoreResponse(int customerId, int VersionId, List<int> riskFactorIds)
        {
            return await _context.ApprovedRiskScoreResponses
               .Include(t => t.RiskFactor)
               .Include(t => t.ProductService)
               .Include(t => t.RiskCriteria)
               .Where(t => t.CustomerId == customerId && t.ApprovalId == VersionId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }

    }
}
