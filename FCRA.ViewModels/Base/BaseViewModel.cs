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
    public class BaseViewModel
    {
        public virtual int Id { get; set; }
        [Display(Name = "Is Active")]
        [MapToDTO]
        public virtual bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual UserViewModel? CreatedByUser { get; set; }
        public virtual UserViewModel? UpdatedByUser { get; set; }

        public virtual string? LastActionBy
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(UpdatedByName))
                    return UpdatedByName;
                return CreatedByName;
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
        public int? CreatedBy { get; set; }
        public string CreatedByName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CreatedByNameDB))
                    return CreatedByNameDB;
                return CreatedByUser == null || string.IsNullOrWhiteSpace(CreatedByUser.Name) ? string.Empty : CreatedByUser.Name;
            }
        }
        public string UpdatedByName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(UpdatedByNameDB))
                    return UpdatedByNameDB;
                return UpdatedByUser == null || string.IsNullOrWhiteSpace(UpdatedByUser.Name) ? string.Empty : UpdatedByUser.Name;
            }
        }
        public string CreatedOnOrder { get { return CreatedOn.ToString("yyyyMMddHHmmssfff"); } }
        public string CreatedOnStr
        {
            get
            {
                return CreatedOn.UTCToIST().ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        public string? CreatedByNameDB { get; set; }
        public string? UpdatedByNameDB { get; set; }
    }
}
