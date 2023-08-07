using FCRA.Models.Account;
using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Customers
{
    [Table(nameof(CustomerForm))]
    public class CustomerForm
    {
        public int CustomerId { get; set; }
        public int FormId { get; set; }
        [Required, StringLength(50)]
        public string? FormName { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        [ForeignKey(nameof(FormId))]
        public virtual FormMaster? FormMaster { get; set; }
    }
}
