using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Models.Base;
using FCRA.Models.Masters;

namespace FCRA.Models.Customers
{
    [Table(nameof(CustomerLocation))]
    public class CustomerLocation 
    {
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public int CountryId { get; set; }
        [Required]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Required]
        [StringLength(200)]
        public string? Address1 { get; set; }
        [StringLength(200)]
        public string? Address2 { get; set; }
        [StringLength(200)]
        public string? Address3 { get; set; }
        [StringLength(200)]
        public string? Address4 { get; set; }


        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country? Country { get; set; }
    }
}
