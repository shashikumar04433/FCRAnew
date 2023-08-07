using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Common;
using System.Xml.Linq;
using FCRA.ViewModels.Base;

namespace FCRA.ViewModels.Masters
{
    public class RiskCriteriaViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Display(Name = "Display Order")]
        public int Sequence { get; set; }
    }
}
