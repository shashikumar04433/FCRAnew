using FCRA.Repository.Managers;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Customers;
using FCRA.Web.Controllers;
using FCRA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Role)]
    [Area("Admin")]
    public class RolesController : MastersController<RoleMasterViewModel>
    {
        private readonly IAccountManager _accountManager;

        public RolesController(IMasterManager<RoleMasterViewModel> manager, IAccountManager accountManager
            )
            : base(manager, "Roles", "Role", new[] { "UserType" })
        {
            _accountManager = accountManager;
        }
        protected override async Task SetDropdownViewBag(RoleMasterViewModel model)
        {
            //State binding
            var list = await _accountManager.GetAllUserTypes();
            var userType = GetUserType();
            if (userType != 1)
            {
                list = list.Where(x => x.Id != 1).ToList();
            }
            ViewBag.UserTypeId = list.GetSelectList(model.UserTypeId);
        }
    }
}
