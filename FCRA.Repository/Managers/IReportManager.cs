using FCRA.ViewModels.Reports;

namespace FCRA.Repository.Managers
{
    public interface IReportManager
    {
        Task<List<InherentRisksSummaryViewModel>> GetInherentRisksSummary();
        Task<List<SummaryBaseViewModel>> GetRisksSummary(int riskTypeId);
        Task<List<RegisterCompletionViewModel>> RiskRegisterCompletionGet(int customerId, int categoryId, int categoryType);
    }
}
