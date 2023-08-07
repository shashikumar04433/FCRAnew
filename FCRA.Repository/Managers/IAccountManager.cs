using FCRA.ViewModels;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IAccountManager
    {
        Task<UserViewModel?> GetUserByEmail(string email);
        Task<UserViewModel?> Login(string email, string pasword);
        Task<UserPermissions?> GetUserPermissions(string userId);
        Task<List<RolePermissionsViewModel>> GetRolePermissionsById(int roleId);
        Task<int> UpdateRolePermissions(List<RolePermissionsViewModel> permissions, int roleId, int userId);
        Task<List<UserTypeViewModel>> GetAllUserTypes();
        Task<List<SelectViewModel>> GetRolesByCompanyId(int companyId, string userId);
        Task<FormControlListViewModel> GetFormControlPermissionByUser(int formId, string userId);
        Task<List<FormControlViewModel>> GetFormControlPermissionByRole(int formId, int roleId);
        Task<int> UpdateFormControlPermissionByRole(List<FormControlViewModel> permissions, int roleId, string userId);
        Task<List<CustomerConfigurationViewModel>> GetCustomerConfiguration();
        Task<int> UpdateCustomerConfiguration(List<CustomerConfigurationViewModel> configuration);
    }
}
