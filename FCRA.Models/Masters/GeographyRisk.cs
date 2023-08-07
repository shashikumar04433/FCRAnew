using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Common;
using FCRA.Models.Base;

namespace FCRA.Models.Masters
{
    [Table(nameof(GeographyRisk))]
    public class GeographyRisk : BaseMasterCustomerModel
    {
        [NotMapped]
        public override string? Name { get; set; }
        [NotMapped]
        public override string? Description { get; set; }
        public RiskRating RiskRating { get; set; } = RiskRating.Low;//1=Low, 2=Medium, 3=High
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }
    }
}
