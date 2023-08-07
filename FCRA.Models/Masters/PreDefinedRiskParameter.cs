using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Models.Base;

namespace FCRA.Models.Masters
{
    [Table(nameof(PreDefinedRiskParameter))]
    public class PreDefinedRiskParameter : BaseMasterCustomerModel
    {
        [Required, Column(Order = 1), StringLength(-1)]
        public override string? Name { get; set; }

        [Column(Order = 2), StringLength(-1)]
        public override string? Description { get; set; }
    }
}
