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
    internal class RiskSubFactorVolumeResponseRepository : BaseModelCustomerRepository<RiskSubFactorVolumeResponse>, IRiskSubFactorVolumeResponseRepository
    {
        public RiskSubFactorVolumeResponseRepository(ApplicationDBContext context) : base(context)
        {

        }

        public override async Task<bool> UpdateAsync(RiskSubFactorVolumeResponse entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.RiskFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.RiskSubFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }

        public override async Task<bool> DeleteAsync(RiskSubFactorVolumeResponse entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }

        public async Task<List<RiskSubFactorVolumeResponse>> GetRiskSubFactorVolumeResponse(int customerId, List<int> riskFactorIds)
        {
            return await _context.RiskSubFactorVolumeResponses
                 .Include(t => t.RiskFactor)
                 .Include(t => t.RiskSubFactor)
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }
        public async Task<List<ApprovedRiskSubFactorVolumeResponse>> GetApprovedRiskSubFactorVolumeResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            return await _context.ApprovedRiskSubFactorVolumeResponses
                 .Include(t => t.RiskFactor)
                 .Include(t => t.RiskSubFactor)
                 .Where(t => t.CustomerId == customerId && t.ApprovalId == versionId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }

    }
}
