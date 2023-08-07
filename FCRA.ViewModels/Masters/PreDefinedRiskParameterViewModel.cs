using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Common;
using FCRA.ViewModels.Base;

namespace FCRA.ViewModels.Masters
{
    public class PreDefinedRiskParameterViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Required, MaxLength(int.MaxValue)]
        public override string? Name { get; set; }
        [MapToDTO, MaxLength(int.MaxValue)]
        public override string? Description { get; set; }
    }
}
