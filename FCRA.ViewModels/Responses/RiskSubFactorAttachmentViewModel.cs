using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Common;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using Microsoft.AspNetCore.Http;

namespace FCRA.ViewModels.Responses
{
    public class RiskSubFactorAttachmentViewModel : BaseCustomerViewModel
    {
        public int RiskSubFactorId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public virtual RiskSubFactorViewModel? SubFactor { get; set; }
        [NotMapped, Display(Name = "File")]
        public List<IFormFile>? FormFile { get; set; }
        public int? ApprovalId { get; set; }
    }
}
