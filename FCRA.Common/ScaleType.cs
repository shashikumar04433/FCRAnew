using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCRA.Common
{
    public enum ScaleType : int
    {
        
        [Display(Name = "Three Point")]
        ThreePoint = 3,
        [Display(Name = "Four Point")]
        FourPoint = 4,
        [Display(Name = "Five Point")]
        FivePoint = 5
    }
}
