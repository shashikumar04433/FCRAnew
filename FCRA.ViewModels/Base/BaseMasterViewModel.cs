using FCRA.Common;
using FCRA.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Base
{
    public class BaseMasterViewModel
    {
        public int Id { get; set; }
        [MapToDTO, Required, MaxLength(100)]
        public virtual string? Name { get; set; }
        [MapToDTO, MaxLength(200)]
        public virtual string? Description { get; set; }
        [Display(Name = "Is Active")]
        [MapToDTO]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public UserViewModel? CreatedByUser { get; set; }
        public UserViewModel? UpdatedByUser { get; set; }
        public string? LastActionBy
        {
            get
            {
                if (UpdatedByUser != null)
                    return UpdatedByUser.Name;
                if (CreatedByUser != null)
                    return CreatedByUser.Name;
                return null;
            }
        }
        public DateTime LastActionOn { get { return UpdatedOn ?? CreatedOn; } }
        public string LastActionOnOrder { get { return LastActionOn.ToString("yyyyMMddHHmmssfff"); } }
        public string LastActionOnStr
        {
            get
            {
                return LastActionOn.UTCToIST().ToString("dd/MM/yyyy hh:mm tt");
            }
        }
    }
}
