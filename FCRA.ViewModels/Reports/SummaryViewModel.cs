using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Reports
{
    public class SummaryViewModel
    {
        public List<InherentRisksSummaryViewModel> InherentRisksSummary { get; set; } = new();
        public List<SummaryBaseViewModel> ControlSummary { get; set; } = new();
    }
}
