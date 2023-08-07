using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Customers
{
    [Table(nameof(CustomerConfiguration))]
    public class CustomerConfiguration
    {
        [Column(Order = 0)]
        public int Id { get; set; }
        public int FieldId { get; set; }
        [Required, MaxLength(50)]
        public string? FieldName { get; set; }
        public bool Visible { get; set; }
    }
}
