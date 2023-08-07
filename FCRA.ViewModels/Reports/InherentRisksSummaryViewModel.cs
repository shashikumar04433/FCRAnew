using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Reports
{
    public class InherentRisksSummaryViewModel : SummaryBaseViewModel
    {

        public decimal? RetailScore { get; set; }
        public decimal? CorporateScore { get; set; }
    }
}
