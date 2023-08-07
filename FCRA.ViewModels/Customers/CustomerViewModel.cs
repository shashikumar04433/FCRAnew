using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCRA.ViewModels.Customers
{
    public class CustomerViewModel : BaseMasterViewModel
    {
        [NotMapped]
        public override string? Description { get; set; }
        [MapToDTO, Display(Name = "Scale Type")]
        public ScaleType ScaleType { get; set; }
        public virtual List<CustomerLocationViewModel> Locations { get; set; } = new();
        //public virtual List<CustomerCountryViewModel> Countries { get; set; } = new();
        public virtual List<CustomerScaleLabelViewModel> Scales { get; set; } = new();
        public virtual List<CustomerFormViewModel> Forms { get; set; } = new();

    }
}
