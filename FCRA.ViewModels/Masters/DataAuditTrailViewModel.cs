using FCRA.Common;
using FCRA.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class DataAuditTrailViewModel
    {
        public virtual int Id { get; set; }
        public string? DataObject { get; set; }
        public int DataObjectId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? ActionType { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastActionOnStr
        {
            get
            {
                return CreatedOn.UTCToIST().ToString("dd/MM/yyyy hh:mm tt");
            }
        }
        public string? ClassName { get; set; }
    }
}
