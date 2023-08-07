using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Models.Base;

namespace FCRA.Models.Masters
{
    [Table(nameof(ProductService))]
    public class ProductService : BaseMasterCustomerModel
    {
        [Required, Column(Order = 1), StringLength(-1)]
        public override string? Name { get; set; }

        [Column(Order = 2), StringLength(-1)]
        public override string? Description { get; set; }
    }
}
