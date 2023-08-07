using FCRA.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.Models.Masters
{
    [Table(nameof(Questions))]
    public class Questions : BaseMasterCustomerModel
    {
        [Required, Column(Order = 1), StringLength(-1)]
        public override string? Name { get; set; }

        [Column(Order = 2), StringLength(-1)]
        public override string? Description { get; set; }
        public int ProductId { get; set; }
        public int Scale1Value { get; set; }
        public int Scale2Value { get; set; }
        public int Scale3Value { get; set; }
        public int? Scale4Value { get; set; }
        public int? Scale5Value { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductService? Product { get; set; }
    }
}
