using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Masters;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Masters;
using System.Data;

namespace FCRA.Repository.Repositories
{
    internal interface IApprovalMatrixRepository
    {
        Task<List<ApprovalMatrix>> GetApprovalMatrixViewModelAsync(RiskFactorViewModel model);
        Task<bool> SaveApprovalMatrix(ApprovalMatrix model, int customerId, int userId);
        Task<List<ApprovalMatrix>> GetApprovalMatrixAccess(int customerId, int userId);
        DataSet GetApprovalStatus(int customerId, string status);
    }
}
