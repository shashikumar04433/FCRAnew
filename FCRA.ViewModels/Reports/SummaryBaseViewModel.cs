using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Reports
{
    public class SummaryBaseViewModel
    {
        public string? Category { get; set; }
        public decimal WeightPercentage { get; set; }
        public decimal? AggregateRiskScore { get; set; }
        public decimal? WeightedScore { get; set; }
        public decimal? LowRiskRange { get; set; }
        public decimal? MediumRiskMinRange { get; set; }
        public decimal? MediumRiskMaxRange { get; set; }
        public decimal? HighRiskRange { get; set; }
    }
}
