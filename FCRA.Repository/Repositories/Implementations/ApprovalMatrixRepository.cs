using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.Helpers;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class ApprovalMatrixRepository : IApprovalMatrixRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IDBHelper _dBHelper;
        public ApprovalMatrixRepository(ApplicationDBContext context, IDBHelper dBHelper)
        {
            _context = context;
            _dBHelper = dBHelper;
        }

        public async Task<List<ApprovalMatrix>> GetApprovalMatrixAccess(int customerId, int userId)
        {
            return await _context.ApprovalMatrixs.Where(t => t.CustomerId == customerId && t.UserId == userId).ToListAsync();
        }

        public async Task<List<ApprovalMatrix>> GetApprovalMatrixViewModelAsync(RiskFactorViewModel model)
        {
            return await _context.ApprovalMatrixs.Where(t => t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId
            && t.GeographicPresenceId == model.GeographicPresenceId && t.BusinessSegmentId == model.BusinessSegmentId && t.CustomerSegmentId == model.CustomerSegmentId).ToListAsync();
        }

        public DataSet GetApprovalStatus(int customerId, string status)
        {
            var result = new DataSet();
            result = _dBHelper.ExecuteProc("GetApprovalStatus",
               new SqlParameter("CustomerId", customerId),
               new SqlParameter("Status", status));
            return result;
        }

        public Task<bool> SaveApprovalMatrix(ApprovalMatrix model, int customerId, int userId)
        {
            var entity = _context.ApprovalMatrixs.Where(t => t.CustomerId == customerId &&
            t.StageId == model.StageId && t.RiskTypeId == model.RiskTypeId && t.GeographicPresenceId == model.GeographicPresenceId &&
            t.CustomerSegmentId == model.CustomerSegmentId && t.BusinessSegmentId == model.BusinessSegmentId && t.SequenceNo == model.SequenceNo).OrderByDescending(t => t.Id).FirstOrDefault();
            if (entity != null)
            {
                entity.CustomerId = customerId;
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = userId;
                entity.SequenceNo = model.SequenceNo;
                entity.UserId = model.UserId;
                _context.ApprovalMatrixs.Update(entity);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                model.CustomerId = customerId;
                model.CreatedOn = DateTime.Now;
                model.CreatedBy = userId;
                _context.ApprovalMatrixs.Add(model);
                _context.SaveChanges();
                return Task.FromResult(true);
            }

        }
    }
}
