using System.Data;

namespace FCRA.Repository.Repositories
{
    internal interface IReportRepository
    {
        DataSet GetInherentRisksSummary();
        DataSet GetRisksSummary(int riskTypeId);
        DataSet RiskRegisterCompletionGet(int customerId, int categoryId, int categoryType);
    }
}
