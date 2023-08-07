using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class RiskFactorResponseRepository : BaseModelCustomerRepository<RiskFactorResponse>, IRiskFactorResponseRepository
    {
        private readonly IDBHelper _dBHelper;
        public RiskFactorResponseRepository(ApplicationDBContext context, IDBHelper dBHelper) : base(context)
        {
            _dBHelper = dBHelper;
        }

        public override async Task<bool> UpdateAsync(RiskFactorResponse entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(t => t.RiskFactorId).IsModified = false;
            _context.Entry(entity).Property(t => t.CustomerId).IsModified = false;
            return await Task.FromResult(true);
        }

        public override async Task<bool> DeleteAsync(RiskFactorResponse entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }

        public async Task<List<RiskFactorResponse>> GetRiskFactorResponse(int customerId, List<int> riskFactorIds)
        {
            return await _context.RiskFactorResponses
                 .Include(t => t.RiskFactor)
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }
        public Task<bool> SubmitApprovedRiskFactorResponses(int customerId, int approvalId, List<int> riskFactorIds, List<int> risksubFactorIds)
        {
            var dataSaved = false;
            var riskFactorResponses = _context.RiskFactorResponses
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToList();
            List<ApprovedRiskFactorResponse> approvedRiskFactorResponses = new List<ApprovedRiskFactorResponse>();
            if (riskFactorResponses != null)
            {
                foreach (var item in riskFactorResponses)
                {
                    ApprovedRiskFactorResponse approvedRiskFactorResponse = new ApprovedRiskFactorResponse
                    {
                        RiskFactorId = item.RiskFactorId,
                        TotalWeightedScore = item.TotalWeightedScore,
                        CustomerId = item.CustomerId,
                        IsActive = item.IsActive,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn,
                        ApprovalId = approvalId
                    };
                    approvedRiskFactorResponses.Add(approvedRiskFactorResponse);
                }
                
            }
            _context.AddRange(approvedRiskFactorResponses);


            var risksubFactorResponses = _context.RiskSubFactorResponses
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToList();
            List<ApprovedRiskSubFactorResponse> approvedRiskSubFactorResponses = new List<ApprovedRiskSubFactorResponse>();
            if (risksubFactorResponses != null)
            {
                foreach (var item in risksubFactorResponses)
                {
                    ApprovedRiskSubFactorResponse approvedRiskSubFactorResponse = new ApprovedRiskSubFactorResponse
                    {
                        RiskFactorId = item.RiskFactorId,
                        RiskSubFactorId = item.RiskSubFactorId,
                        Score = item.Score,
                        Assumptions = item.Assumptions,
                        Response = item.Response,
                        PreDefinedParameterId = item.PreDefinedParameterId,
                        ResponseDescription = item.ResponseDescription,
                        ScaleResponse = item.ScaleResponse,
                        CustomerId = item.CustomerId,
                        IsActive = item.IsActive,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn,
                        ApprovalId = approvalId
                    };
                    approvedRiskSubFactorResponses.Add(approvedRiskSubFactorResponse);
                }

            }
            _context.AddRange(approvedRiskSubFactorResponses);

            var riskScoreProductVolumRatingResponses = _context.RiskScoreProductVolumRatingResponses
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToList();
            List<ApprovedRiskScoreProductVolumRatingResponse> approvedRiskScoreProductVolumRatingResponses = new List<ApprovedRiskScoreProductVolumRatingResponse>();
            if (riskScoreProductVolumRatingResponses != null)
            {
                foreach (var item in riskScoreProductVolumRatingResponses)
                {
                    ApprovedRiskScoreProductVolumRatingResponse approvedRiskScoreProductVolumRatingResponse = new ApprovedRiskScoreProductVolumRatingResponse
                    {
                        RiskFactorId = item.RiskFactorId,
                        RiskSubFactorId = item.RiskSubFactorId,
                        ProductId = item.ProductId,
                        TotalScore = item.TotalScore,
                        Volume = item.Volume,
                        FinalScore = item.FinalScore,
                        RiskRating = item.RiskRating,
                        Values = item.Values,
                        Value = item.Value,
                        CustomerId = item.CustomerId,
                        IsActive = item.IsActive,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn,
                        ApprovalId = approvalId
                    };
                    approvedRiskScoreProductVolumRatingResponses.Add(approvedRiskScoreProductVolumRatingResponse);
                }

            }
            _context.AddRange(approvedRiskScoreProductVolumRatingResponses);

            var riskScoreResponses = _context.RiskScoreResponses
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToList();
            List<ApprovedRiskScoreResponse> approvedRiskScoreResponses = new List<ApprovedRiskScoreResponse>();
            if (riskScoreResponses != null)
            {
                foreach (var item in riskScoreResponses)
                {
                    ApprovedRiskScoreResponse approvedRiskScoreResponse = new ApprovedRiskScoreResponse
                    {
                        RiskFactorId = item.RiskFactorId,
                        RiskSubFactorId = item.RiskSubFactorId,
                        ProductId = item.ProductId,
                        RiskCriteriaId = item.RiskCriteriaId,
                        Score = item.Score,
                        QuestionIds = item.QuestionIds,
                        Answers = item.Answers,
                        CustomerId = item.CustomerId,
                        IsActive = item.IsActive,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn,
                        ApprovalId = approvalId
                    };
                    approvedRiskScoreResponses.Add(approvedRiskScoreResponse);
                }

            }
            _context.AddRange(approvedRiskScoreResponses);

            var riskSubFactorVolumeResponses = _context.RiskSubFactorVolumeResponses
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToList();
            List<ApprovedRiskSubFactorVolumeResponse> approvedRiskSubFactorVolumeResponses = new List<ApprovedRiskSubFactorVolumeResponse>();
            if (riskSubFactorVolumeResponses != null)
            {
                foreach (var item in riskSubFactorVolumeResponses)
                {
                    ApprovedRiskSubFactorVolumeResponse approvedRiskSubFactorVolumeResponse = new ApprovedRiskSubFactorVolumeResponse
                    {
                        RiskFactorId = item.RiskFactorId,
                        RiskSubFactorId = item.RiskSubFactorId,
                        Score1 = item.Score1,
                        Score2 = item.Score2,
                        Score3 = item.Score3,
                        Score4 = item.Score4,
                        Score5 = item.Score5,
                        Volume1 = item.Volume1,
                        Volume2 = item.Volume2,
                        Volume3 = item.Volume3,
                        Volume4 = item.Volume4,
                        Volume5 = item.Volume5,
                        Weight1 = item.Weight1,
                        Weight2 = item.Weight2,
                        Weight3 = item.Weight3,
                        Weight4 = item.Weight4,
                        Weight5 = item.Weight5,
                        WeightedScore1 = item.WeightedScore1,
                        WeightedScore2 = item.WeightedScore2,
                        WeightedScore3 = item.WeightedScore3,
                        WeightedScore4 = item.WeightedScore4,
                        WeightedScore5 = item.WeightedScore5,
                        Countries = item.Countries,
                        CountryWiseRating = item.CountryWiseRating,
                        CountryWiseVolume = item.CountryWiseVolume,
                        CustomerId = item.CustomerId,
                        IsActive = item.IsActive,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn,
                        ApprovalId = approvalId
                    };
                    approvedRiskSubFactorVolumeResponses.Add(approvedRiskSubFactorVolumeResponse);
                }

            }
            _context.AddRange(approvedRiskSubFactorVolumeResponses);

            var riskSubFactorAttachment = _context.RiskSubFactorAttachments
                 .Where(t => t.CustomerId == customerId && risksubFactorIds.Contains(t.RiskSubFactorId)).ToList();
            List<ApprovedRiskSubFactorAttachment> approvedRiskSubFactorAttachments = new List<ApprovedRiskSubFactorAttachment>();
            if (riskSubFactorAttachment != null)
            {
                foreach (var item in riskSubFactorAttachment)
                {
                    ApprovedRiskSubFactorAttachment approvedRiskSubFactorAttachment = new ApprovedRiskSubFactorAttachment
                    {
                        RiskSubFactorId = item.RiskSubFactorId,
                        FileName = item.FileName,
                        FilePath = item.FilePath,
                        CustomerId = item.CustomerId,
                        IsActive = item.IsActive,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn,
                        ApprovalId = approvalId
                    };
                    approvedRiskSubFactorAttachments.Add(approvedRiskSubFactorAttachment);
                }

            }
            _context.AddRange(approvedRiskSubFactorAttachments);
            _context.SaveChanges();

            dataSaved = true;
            return Task.FromResult(dataSaved);
        }

        public async Task<List<ApprovedRiskFactorResponse>> GetVersionRiskFactorResponse(int customerId, List<int> riskFactorIds, int VersionId)
        {
            return await _context.ApprovedRiskFactorResponses
                 .Include(t => t.RiskFactor)
                 .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId) && t.ApprovalId == VersionId).ToListAsync();
        }
        public DataSet GetApprovalCompletion(int customerId)
        {
            return _dBHelper.ExecuteProc("GetApprovalCompletion",
                 new SqlParameter("CustomerId", customerId));
        }

        public int SubmitVersionMaster(CustomerVersionMaster model)
        {
            _context.CustomerVersionMasters.Add(model);
            _context.SaveChanges();
            return model.Id;
        }
        public async Task<List<ApprovedRiskFactorResponse>> GetApprovedRiskFactorResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            return await _context.ApprovedRiskFactorResponses
                 .Include(t => t.RiskFactor)
                 .Where(t => t.CustomerId == customerId && t.ApprovalId == versionId  && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }
    }
}
