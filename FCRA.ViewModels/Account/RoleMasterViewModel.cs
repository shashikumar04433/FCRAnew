using FCRA.Common;
using FCRA.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace FCRA.ViewModels.Account
{
    public class RoleMasterViewModel : BaseMasterViewModel
    {
        [MapToDTO]
        [Display(Name = "User Type")]
        public int? UserTypeId { get; set; }
        public virtual UserTypeViewModel? UserType { get; set; }
        public string UserTypeName { get { return UserType == null || string.IsNullOrEmpty(UserType.Name) ? string.Empty : UserType.Name; } }
    }
}