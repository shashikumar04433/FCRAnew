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
    public class ProductServiceViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Required]
        public override string? Name { get; set; }
        [MapToDTO]
        public override string? Description { get; set; }
    }
}
