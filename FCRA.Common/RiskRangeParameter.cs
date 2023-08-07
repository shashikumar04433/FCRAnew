using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCRA.Common
{
    public enum RiskRangeParameter : int
    {
        [Display(Name = "% Range")]
        PercentRange = 1,
        [Display(Name = "Pre Defined Parameters")]
        PreDefinedParameters = 2,
        [Display(Name = "Descriptive")]
        Descriptive = 3,
        [Display(Name = "Volume")]
        Volume = 4,
        [Display(Name = "Scale")]
        Scale = 5,
        [Display(Name = "Number Range")]
        NumberRange = 6,
    }
}
