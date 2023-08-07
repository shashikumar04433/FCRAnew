using FCRA.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.Models.Account
{
    [Table(nameof(FormMaster))]
    public class FormMaster : BaseIdModel
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        [StringLength(50)]
        public string? Area { get; set; }
        [Required]
        [StringLength(50)]
        public string? Controller { get; set; }
        [Required]
        [StringLength(50)]
        public string? Action { get; set; }
        [StringLength(50)]
        public string? IconClass { get; set; }
        public int Sequence { get; set; }
        public int? MenuId { get; set; }
        public bool IsAdmin { get; set; } = false;

        [ForeignKey(nameof(MenuId))]
        public virtual MenuMaster? MenuMaster { get; set; }
    }
}
