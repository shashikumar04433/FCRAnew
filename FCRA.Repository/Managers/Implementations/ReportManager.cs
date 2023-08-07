using FCRA.Repository.Repositories;
using FCRA.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers.Implementations
{
    internal class ReportManager : IReportManager
    {
        private readonly IReportRepository _repository;

        public ReportManager(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InherentRisksSummaryViewModel>> GetInherentRisksSummary()
        {
            List<InherentRisksSummaryViewModel> model = new();
            var ds = _repository.GetInherentRisksSummary();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    model = ds.Tables[0].MapTo<InherentRisksSummaryViewModel>();
            }

            return await Task.FromResult(model);
        }

        public async Task<List<SummaryBaseViewModel>> GetRisksSummary(int riskTypeId)
        {
            List<SummaryBaseViewModel> model = new();
            var ds = _repository.GetRisksSummary(riskTypeId);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    model = ds.Tables[0].MapTo<SummaryBaseViewModel>();
            }

            return await Task.FromResult(model);
        }

        public async Task<List<RegisterCompletionViewModel>> RiskRegisterCompletionGet(int customerId, int categoryId, int categoryType)
        {
            List<RegisterCompletionViewModel> model = new();
            var ds = _repository.RiskRegisterCompletionGet(customerId, categoryId, categoryType);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    model = ds.Tables[0].MapTo<RegisterCompletionViewModel>();
            }
            return await Task.FromResult(model);
        }
    }
}
