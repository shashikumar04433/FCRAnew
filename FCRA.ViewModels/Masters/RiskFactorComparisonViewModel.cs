using FCRA.Common;
using FCRA.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class RiskFactorComparisonViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int StageId { get; set; }
        public string? StageName { get; set; }
        public decimal? WeightPercentage1 { get; set; }
        public decimal? WeightPercentage2 { get; set; }
        public decimal? TotalWeightedScore1 { get; set; }
        public decimal? TotalWeightedScore2 { get; set; }
        public decimal? TotalPercentage1 { get; set; }
        public decimal? TotalPercentage2 { get; set; }
    }
}
