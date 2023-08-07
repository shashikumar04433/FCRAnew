using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCRA.Common
{
    public enum RiskRating : int
    {
        [Display(Name = "Low")]
        Low = 1,
        [Display(Name = "Medium")]
        Medium = 2,
        [Display(Name = "High")]
        High = 3,
        [Display(Name = "Higher")]
        Higher = 4,
        [Display(Name = "Critical")]
        Extreme = 5
    }
}
