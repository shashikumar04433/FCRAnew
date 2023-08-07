using FCRA.Common;
using FCRA.Models.Base;
using FCRA.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Responses
{
    [Table(nameof(RiskSubFactorAttachment))]
    public class RiskSubFactorAttachment : BaseCustomerModel
    {
        public int RiskSubFactorId { get; set; }
        [MaxLength(200)]
        public string? FileName { get; set; }
        [MaxLength(200)]
        public string? FilePath { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public virtual RiskSubFactor? SubFactor { get; set; }
    }
}
