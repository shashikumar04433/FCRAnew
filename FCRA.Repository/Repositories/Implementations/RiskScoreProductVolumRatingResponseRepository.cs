using FCRA.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class RiskScoreProductVolumRatingResponseRepository : BaseModelCustomerRepository<RiskScoreProductVolumRatingResponse>, IRiskScoreProductVolumRatingResponseRepository
    {
        public RiskScoreProductVolumRatingResponseRepository(ApplicationDBContext context) : base(context)
        {

        }

        public override async Task<bool> UpdateAsync(RiskScoreProductVolumRatingResponse entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.RiskFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.RiskSubFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.ProductId).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }

        public override async Task<bool> DeleteAsync(RiskScoreProductVolumRatingResponse entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }

        public async Task<List<RiskScoreProductVolumRatingResponse>> GetRiskScoreProductVolumRatingResponse(int customerId, List<int> riskFactorIds)
        {
            return await _context.RiskScoreProductVolumRatingResponses
                 .Include(t => t.RiskFactor)
                 .Include(t => t.RiskSubFactor)
                 .Include(t => t.ProductService)
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }

        public async Task<List<ApprovedRiskScoreProductVolumRatingResponse>> GetApprovedRiskScoreProductVolumRatingResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            return await _context.ApprovedRiskScoreProductVolumRatingResponses
                  .Include(t => t.RiskFactor)
                  .Include(t => t.RiskSubFactor)
                  .Include(t => t.ProductService)
                  .Where(t => t.CustomerId == customerId && t.ApprovalId == versionId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }
    }
}
