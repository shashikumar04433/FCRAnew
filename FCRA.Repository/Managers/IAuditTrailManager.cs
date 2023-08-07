using FCRA.Common;
using FCRA.Models;
using FCRA.Models.Account;
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
    public interface IAuditTrailManager
    {
        Task<List<DataAuditTrailViewModel>> GetAuditTrail(int objectId, string objectname);
        Task<List<UserViewModel>> GetUserList();
        Task<List<DataAuditTrailViewModel>> GetObjectAuditTrail(string Objectname);

    }
}
