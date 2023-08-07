using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.Helpers;
using FCRA.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    internal class RiskAssessmentRepository : IRiskAssessmentRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IDBHelper _dBHelper;

        public RiskAssessmentRepository(ApplicationDBContext context, IDBHelper dbHelper)
        {
            _context = context;
            _dBHelper = dbHelper;
        }

        public async Task<List<ApprovalRequest>> GetApprovalStatusData(int customerId, int stageId, int? riskTypeId, int? geoPresenceId, int? customerSegmentId, int? businessSegmentId)
        {
            return await _context.ApprovalRequests
                .Where(t => t.CustomerId == customerId && t.StageId == stageId && t.RiskTypeId == riskTypeId && t.GeographicPresenceId == geoPresenceId &&
                t.CustomerSegmentId == customerSegmentId && t.BusinessSegmentId == businessSegmentId).ToListAsync();
        }

        public async Task<List<CustomerVersionMaster>> GetApprovedVersion(int customerId)
        {
            return await _context.CustomerVersionMasters.Where(t => t.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<RiskSubFactor>> GetRiskSubFactorsByRiskType(int customerId, List<int> riskFactorIds)
        {
            return await _context.RiskSubFactors
                .Include(t => t.RiskFactor)
                .Include(t => t.PreDefinedRiskParameter1)
                .Include(t => t.PreDefinedRiskParameter2)
                .Include(t => t.PreDefinedRiskParameter3)
                .Include(t => t.PreDefinedRiskParameter4)
                .Include(t => t.PreDefinedRiskParameter5)
                .Include(t => t.riskSubFactorAttachment)
                .Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
        }


        public void SubFactorTempFileAdd(RiskSubFactorAttachment model)
        {
            _context.RiskSubFactorAttachments.Add(model);
            _context.SaveChanges();
        }

        public void SubmitApprovalRemark(ApprovalHistory model)
        {
            _context.ApprovalHistorys.Add(model);
            _context.SaveChanges();
        }

        public ApprovalRequest SubmitApprovalRequest(ApprovalRequest model)
        {
            var entity = _context.ApprovalRequests.Where(t => t.CustomerId == model.CustomerId &&
            t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId && t.GeographicPresenceId == model.GeographicPresenceId &&
            t.CustomerSegmentId == model.CustomerSegmentId && t.BusinessSegmentId == model.BusinessSegmentId).OrderByDescending(t => t.Id).FirstOrDefault();

            var ApproverDetail = _context.ApprovalMatrixs.Where(t => t.CustomerId == model.CustomerId &&
            t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId && t.GeographicPresenceId == model.GeographicPresenceId &&
            t.CustomerSegmentId == model.CustomerSegmentId && t.BusinessSegmentId == model.BusinessSegmentId).OrderBy(t => t.SequenceNo).FirstOrDefault();

            var currentUserMatrix = _context.ApprovalMatrixs.Where(t => t.CustomerId == model.CustomerId &&
            t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId && t.GeographicPresenceId == model.GeographicPresenceId &&
            t.CustomerSegmentId == model.CustomerSegmentId && t.BusinessSegmentId == model.BusinessSegmentId && t.UserId == model.CreatedBy).FirstOrDefault();

            var nextUserMatrix = new ApprovalMatrix();
            if (currentUserMatrix != null)
                nextUserMatrix = _context.ApprovalMatrixs.Where(t => t.CustomerId == model.CustomerId &&
                t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId && t.GeographicPresenceId == model.GeographicPresenceId &&
                t.CustomerSegmentId == model.CustomerSegmentId && t.BusinessSegmentId == model.BusinessSegmentId && t.SequenceNo == currentUserMatrix.SequenceNo + 1).FirstOrDefault();

            var previousUserMatrix = new ApprovalMatrix();
            if (currentUserMatrix != null)
                previousUserMatrix = _context.ApprovalMatrixs.Where(t => t.CustomerId == model.CustomerId &&
                t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId && t.GeographicPresenceId == model.GeographicPresenceId &&
                t.CustomerSegmentId == model.CustomerSegmentId && t.BusinessSegmentId == model.BusinessSegmentId && t.SequenceNo == currentUserMatrix.SequenceNo - 1).FirstOrDefault();

            var returnValue = new ApprovalRequest();
            if (entity == null || (entity != null && entity.FinalStatus == 5))
            {
                if (ApproverDetail != null)
                {
                    model.Sequence = ApproverDetail.SequenceNo;
                    model.PendingWithUser = ApproverDetail.UserId;
                }
                else
                {
                    model.PendingWithUser = 0;
                    model.FinalStatus = 5;
                }
                _context.ApprovalRequests.Add(model);
                _context.SaveChanges();
                returnValue.Id = model.Id;
                returnValue.Sequence = model.Sequence;
                returnValue.PendingWithUser = model.PendingWithUser;
                return returnValue;
            }
            else
            {
                entity.UpdatedBy = model.CreatedBy;
                entity.UpdatedOn = model.CreatedOn;
                if(nextUserMatrix != null && nextUserMatrix.SequenceNo > 0 && model.Status == 1)
                {
                    entity.Sequence = nextUserMatrix.SequenceNo;
                    entity.PendingWithUser = nextUserMatrix.UserId;
                }
                else if(previousUserMatrix != null && model.Status == 0)
                {
                    entity.Sequence = previousUserMatrix.SequenceNo;
                    entity.PendingWithUser = previousUserMatrix.UserId;
                }
                else if(previousUserMatrix == null && model.Status == 0)
                {
                    entity.Sequence = 0;
                    entity.PendingWithUser = 0;
                    entity.FinalStatus = 0;
                }
                else
                {
                    entity.Sequence = 0;
                    entity.PendingWithUser = 0;
                    entity.FinalStatus = 5;
                }

                _context.ApprovalRequests.Update(entity);
                _context.SaveChanges();
                returnValue.Id = entity.Id;
                returnValue.Sequence = entity.Sequence;
                returnValue.PendingWithUser = entity.PendingWithUser;

                return returnValue;
            }

        }

        public async Task<List<UserMaster>> GetApproverList(int customerid)
        {
            return await _context.UserMasters
                .Include(x => x.Role)
                .Where(t => t.CustomerId == customerid).ToListAsync();
        }

        public DataSet GetApprovedRiskCombination(int versionId)
        {
            var result = new DataSet();
            result = _dBHelper.ExecuteProc("GetApprovedRiskCombination",
               new SqlParameter("VersionId", versionId));
            return result;
        }
    }
}
