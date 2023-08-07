using FCRA.ViewModels.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Reports
{
    public class SummaryWeightViewModel
    {
        public string? Category { get; set; }
        public decimal WeightPercentage { get; set; }
        public decimal WeightedScore { get; set; }
        public decimal? ScaleRange1 { get; set; }
        public decimal? ScaleRange2 { get; set; }
        public decimal? ScaleRange3 { get; set; }
        public decimal? ScaleRange4 { get; set; }
        public decimal? ScaleRange5 { get; set; }
        public decimal? toWeightedScore { get; set; }
        public string? StageName { get; set; }
        [NotMapped]
        public virtual StageViewModel? Stage { get; set; }
        public List<int>? RiskFactorIdList { get; set; }
    }
}
