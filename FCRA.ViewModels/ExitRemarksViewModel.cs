using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels
{
    public class ExitRemarksViewModel : BaseCustomerViewModel
    {
        [Required]
        [MapToDTO]
        public string? Remarks { get; set; }
    }
}
