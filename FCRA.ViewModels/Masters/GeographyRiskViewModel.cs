using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCRA.ViewModels.Masters
{
    public class GeographyRiskViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Display(Name = "Country")]
        public int CountryId { get; set; }
        [MapToDTO, Display(Name = "Risk Rating")]
        public RiskRating RiskRating { get; set; } = RiskRating.Low;//1=Low, 2=Medium, 3=High


        [NotMapped]
        public override string? Name { get; set; }
        [NotMapped]
        public override string? Description { get; set; }
        [NotMapped]
        public CountryViewModel? Country { get; set; }
    }
}
