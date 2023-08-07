using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models
{
    [Table(nameof(ExitRemarks))]
    public class ExitRemarks : BaseCustomerModel
    {
        [Required]
        public string? Remarks { get; set; }
    }
}
