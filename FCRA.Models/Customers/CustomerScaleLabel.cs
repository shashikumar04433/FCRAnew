using FCRA.Models.Defaults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Customers
{
    public class CustomerScaleLabel
    {
        public int CustomerId { get; set; }
        public int ScaleId { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        [ForeignKey(nameof(ScaleId))]
        public virtual DefaultScale? DefaultScale { get; set; }
    }
}
