using FCRA.Models.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class QuestionRiskCriteriaMappingRepository : BaseModelCustomerRepository<QuestionsRiskCriteriaMapping>, IQuestionRiskCriteriaMappingRepository
    {
        public QuestionRiskCriteriaMappingRepository(ApplicationDBContext context) : base(context)
        {
        }
        public override async Task<bool> UpdateAsync(QuestionsRiskCriteriaMapping entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.RiskFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.RiskSubFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.ProductId).IsModified = false;
            _context.Entry(entity).Property(t => t.RiskCriteriaId).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }
        public override async Task<bool> DeleteAsync(QuestionsRiskCriteriaMapping entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }
    }
}
