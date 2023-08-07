using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class QuestionsViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required]
        [MapToDTO, MaxLength(int.MaxValue)]
        [Display(Name = "Question")]
        public override string? Name { get; set; }
        [MapToDTO, MaxLength(int.MaxValue)]
        public override string? Description { get; set; }
        [MapToDTO, Display(Name = "Country")]
        public int CountryId { get; set; }
        [MapToDTO, Display(Name = "Scale Value 1")]
        public int Scale1Value { get; set; } = 1;
        [MapToDTO,  Display(Name = "Scale Value 2")]
        public int Scale2Value { get; set; } = 2;
        [MapToDTO,  Display(Name = "Scale Value 3")]
        public int Scale3Value { get; set; } = 3;
        [MapToDTO,  RequiredIf($"{nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5"), Display(Name = "Scale Value 4")]
        public int? Scale4Value { get; set; } = 4;
        [MapToDTO, DecimalNumber, RequiredIf($"{nameof(ScaleType)} == 5"), Display(Name = "Scale Value 5")]
        public int? Scale5Value { get; set; } = 5;
        public ProductServiceViewModel? Product { get; set; }

        [NotMapped]
        public ScaleType ScaleType { get; set; }
    }
}
