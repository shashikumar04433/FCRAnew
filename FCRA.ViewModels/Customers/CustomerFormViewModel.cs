using FCRA.Common;
using FCRA.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Customers
{
    public class CustomerFormViewModel
    {
        [MapToDTO]
        public int CustomerId { get; set; }
        [MapToDTO]
        public int FormId { get; set; }
        [MapToDTO, Required, MaxLength(50), Display(Name = "Custom Form Name")]
        public string? FormName { get; set; }
        public string? DefaultFormName { get; set; }
    }
}
