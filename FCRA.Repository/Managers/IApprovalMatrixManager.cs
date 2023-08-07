using FCRA.Common;
using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using FCRA.ViewModels.Responses.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IApprovalMatrixManager
    {
        Task<List<ApprovalMatrixViewModel>> GetApprovalMatrixViewModelAsync(RiskFactorViewModel model);
        Task<bool> SaveApprovalMatrix(List<ApprovalMatrixViewModel> model, int customerId, int userId);
        Task<List<ApprovalMatrixViewModel>> GetApprovalMatrixAccess(int customerId, int userId);
        DataSet GetApprovalStatus(int customerId, string status);
    }
}
