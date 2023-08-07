using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IAccountRepository
    {
        DataSet GetUserMenu(string userId);
        DataSet GetAllRolePermissionMapping();
        DataSet UpdateRoleAccessMapping(DataTable mappings);
        DataSet GetRolePermissionsById(int roleId);
        DataSet UpdateRolePermissions(DataTable permissions, int roleId);
        DataSet GetAllUserTypes();
        DataSet RolesByCompanyId(int companyId, string userId);
        DataSet GetFormControlPermissionByUser(int formId, string userId);
        DataSet GetFormControlPermissionByRole(int formId, int roleId);
        DataSet UpdateFormControlPermissionByRole(DataTable permissions, int roleId, string userId);
        DataSet CompanyLogoGetByUser(string userId);
        DataSet GetCustomerConfiguration();
        DataSet UpdateCustomerConfiguration(DataTable configuration);
    }
}
