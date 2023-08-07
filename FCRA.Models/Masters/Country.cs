using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Models.Base;

namespace FCRA.Models.Masters
{
    [Table(nameof(Country))]
    public class Country : BaseMasterModel
    {
        [NotMapped]
        public override string? Description { get; set; }
    }
}
