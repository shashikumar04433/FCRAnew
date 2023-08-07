using FCRA.Models.Account;
using FCRA.Models.Customers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.Models
{
    [Table(nameof(DataAuditTrail))]
    public class DataAuditTrail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public virtual int Id { get; set; }
        public string? DataObject { get; set; }
        public int DataObjectId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? ActionType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
