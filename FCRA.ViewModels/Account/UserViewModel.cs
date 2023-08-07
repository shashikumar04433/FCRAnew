using FCRA.Common;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Customers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.ViewModels.Account
{
    public class UserViewModel : BaseMasterViewModel
    {
        [MapToDTO]
        [Required]
        [Display(Name = "Name")]
        public new string? Name { get; set; }
        [MapToDTO]
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [MapToDTO]
        [Display(Name = "Role")]
        public int? RoleId { get; set; }
        [MapToDTO]
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }
        public virtual RoleMasterViewModel? Role { get; set; }
        public virtual CustomerViewModel? Customer { get; set; }
        public string RoleName { get { return Role == null || string.IsNullOrWhiteSpace(Role.Name) ? string.Empty : Role.Name + (Role.IsActive ? "" : " (Inactive)"); } }
        public virtual List<UserRolesViewModel> UserRoles { get; set; } = new();
    }
}
