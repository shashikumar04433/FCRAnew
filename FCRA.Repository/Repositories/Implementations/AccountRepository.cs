using FCRA.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories.Implementations
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly IDBHelper _dBHelper;
        public AccountRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        public DataSet GetUserMenu(string userId)
        {
            return _dBHelper.ExecuteProc("GetUserMenu",
                 new SqlParameter("UserId", userId));
        }

        public DataSet GetAllRolePermissionMapping()
        {
            return _dBHelper.ExecuteProc("GetAllRolePermissionMapping");
        }

        public DataSet UpdateRoleAccessMapping(DataTable mappings)
        {
            return _dBHelper.ExecuteProc("UpdateRoleAccessMapping",
                new SqlParameter("@Mappings", mappings));
        }

        public DataSet GetRolePermissionsById(int roleId)
        {
            return _dBHelper.ExecuteProc("GetRolePermissionsById",
                new SqlParameter("@RoleId", roleId));
        }

        public DataSet UpdateRolePermissions(DataTable permissions, int roleId)
        {
            return _dBHelper.ExecuteProc("UpdateRolePermissions",
                new SqlParameter("@RoleId", roleId),
                new SqlParameter("@Permissions", permissions));
        }

        public DataSet GetAllUserTypes()
        {
            return _dBHelper.ExecuteProc("GetAllUserTypes");
        }

        public DataSet RolesByCompanyId(int companyId, string userId)
        {
            return _dBHelper.ExecuteProc("RolesByCompanyId",
                new SqlParameter("@UserId", userId));
        }

        public DataSet GetFormControlPermissionByUser(int formId, string userId)
        {
            return _dBHelper.ExecuteProc("FormControlPermissionByUser",
                new SqlParameter("@FormId", formId),
                new SqlParameter("@UserId", userId));
        }

        public DataSet GetFormControlPermissionByRole(int formId, int roleId)
        {
            return _dBHelper.ExecuteProc("FormControlPermissionByRole",
               new SqlParameter("@FormId", formId),
               new SqlParameter("@RoleId", roleId));
        }

        public DataSet UpdateFormControlPermissionByRole(DataTable permissions, int roleId, string userId)
        {
            return _dBHelper.ExecuteProc("FormControlPermissionUpdate",
               new SqlParameter("@Permissions", permissions),
               new SqlParameter("@RoleId", roleId),
               new SqlParameter("@UserId", userId));
        }

        public DataSet CompanyLogoGetByUser(string userId)
        {
            return _dBHelper.ExecuteProc("CompanyLogoGetByUser",
                new SqlParameter("@UserId", userId));
        }
        public DataSet GetCustomerConfiguration()
        {
            return _dBHelper.ExecuteProc("GetCustomerConfiguration");
        }

        public DataSet UpdateCustomerConfiguration(DataTable configuration)
        {
            return _dBHelper.ExecuteProc("UpdateCustomerConfiguration",
                new SqlParameter("@configuration", configuration));
        }
    }
}
