using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Masters
{
    public class CountryViewModel : BaseMasterViewModel
    {
        [MapToDTO, Required, MaxLength(100), Display(Name = "Country")]
        public override string? Name { get; set; }       
    }
}
