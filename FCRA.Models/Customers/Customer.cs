using FCRA.Common;
using FCRA.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.Models.Customers
{
    [Table(nameof(Customer))]
    public class Customer : BaseMasterModel
    {
        [NotMapped]
        public override string? Description { get; set; }
        public ScaleType ScaleType { get; set; }
        public virtual List<CustomerLocation> Locations { get; set; } = new();
        //public virtual List<CustomerCountry> Countries { get; set; } = new();
        public virtual List<CustomerScaleLabel> Scales { get; set; } = new();
        public virtual List<CustomerForm> Forms { get; set; } = new();
    }
}
