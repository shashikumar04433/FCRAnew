using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using FCRA.Common;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;

namespace FCRA.ViewModels.Mappings
{
    public class RiskFactorProductServiceMappingViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }
        public int ScaleRange2 { get; set; } = ScaleConstantsInt.ScaleRange2;
        public int ScaleRange3 { get; set; } = ScaleConstantsInt.ScaleRange3;
        public int? ScaleRange4 { get; set; } = ScaleConstantsInt.ScaleRange4;
        public int? ScaleRange5 { get; set; } = ScaleConstantsInt.ScaleRange5;
        public virtual RiskFactorViewModel? RiskFactor { get; set; }
        public RiskSubFactorViewModel? RiskSubFactor { get; set; }
        public virtual ProductServiceViewModel? ProductService { get; set; }


        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public string? ProductServiceName { get; set; }

        [NotMapped]
        public List<ProductRiskCriteriaMappingViewModel> RiskCriteriaMappings { get; set; } = new();

        [NotMapped]
        public decimal? Volume { get; set; }
        [NotMapped]
        public int? TotalScore { get; set; }
        [NotMapped]
        public int? FinalScore { get; set; }
        [NotMapped]
        public int RiskRating { get; set; }//1=Low, 2=Medium, 3=High
        [NotMapped]
        public ScaleType? ScaleType { get; set; }
        [NotMapped]
        public decimal? Value { get; set; }
    }
}
