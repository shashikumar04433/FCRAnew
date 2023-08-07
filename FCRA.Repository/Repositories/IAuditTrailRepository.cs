using FCRA.Models;
using FCRA.Models.Account;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Masters;

namespace FCRA.Repository.Repositories
{
    internal interface IAuditTrailRepository
    {
        Task<List<DataAuditTrail>> GetAuditTrail(int objectId, string objectname);
        Task<List<UserMaster>> GetUserList();
        Task<List<DataAuditTrail>> GetObjectAuditTrail(string Objectname);
    }
}
