using FCRA.Models.Masters;
using FCRA.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class ReportRepository : IReportRepository
    {
        private readonly IDBHelper _dBHelper;

        public ReportRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        public DataSet GetInherentRisksSummary()
        {
            return _dBHelper.ExecuteProc("GetInherentRisksSummary");
        }

        public DataSet GetRisksSummary(int riskTypeId)
        {
            return _dBHelper.ExecuteProc("GetRisksSummary"
                , new SqlParameter("@RiskTypeId", riskTypeId));
        }

        public DataSet RiskRegisterCompletionGet(int customerId, int categoryId, int categoryType)
        {
            return _dBHelper.ExecuteProc("RiskRegisterCompletionGet"
               , new SqlParameter("@CustomerId", customerId)
               , new SqlParameter("@CategoryType", categoryType)
               , new SqlParameter("@CategoryId", categoryId));
        }
    }
}
