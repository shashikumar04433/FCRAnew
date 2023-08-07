using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Defaults;
using FCRA.Common;

namespace FCRA.ViewModels.Customers
{
    public class CustomerScaleLabelViewModel
    {
        [MapToDTO]
        public int CustomerId { get; set; }
        [MapToDTO]
        public int ScaleId { get; set; }
        [MapToDTO, Required, MaxLength(50), Display(Name ="Custom Name")]
        public string? Name { get; set; }
        public virtual CustomerViewModel? Customer { get; set; }
        public virtual DefaultScaleViewModel? DefaultScale { get; set; }
    }
}
