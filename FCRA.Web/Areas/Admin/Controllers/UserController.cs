using FCRA.Repository.Managers;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.User)]
    [Area("Admin")]
    public class UserController : MastersController<UserViewModel>
    {
        private readonly IAccountManager _accountManager;
        private readonly IMasterManager<CustomerViewModel> _customerManager;

        public UserController(IMasterManager<UserViewModel> manager, IAccountManager accountManager
            , IMasterManager<CustomerViewModel> customerManager)
            : base(manager, "Users", "User", new[] { "Role", "Customer" }, new[] { "UserRoles", "Customer" })
        {
            _accountManager = accountManager;
            _customerManager = customerManager;
        }

        [HttpPost]
        public override async Task<IActionResult> Item(UserViewModel model)
        {
            RemoveFromModelState();

            ViewData["Title"] = _masterType;
            if (!ModelState.IsValid)
            {
                await SetDropdownViewBag(model);
                return View(model);
            }
            if (await ValidateModel(model))
            {
                await SetDropdownViewBag(model);
                return View(model);
            }
            SetEditProperties(ref model);
            //model.UserRoles = null;
            var result = await _manager.AddUpdateAsync(model, this.GetUserId());
            if (result)
            {
                SetApplicationResult(true, $"{_masterType} {(model.Id == 0 ? "added" : "updated")} successfully");
                return Redirect("Index");
            }
            else
                ModelState.AddModelError(_globalErrorField, "Something went worng, please contact support");
            await SetDropdownViewBag(model);
            return View(model);
        }

        protected override void SetProperties(ref UserViewModel model)
        {
            if (model.Id == 0 && GetUserType() != 1)
                model.CustomerId = GetUserCustomerId();
        }

        protected override async Task SetIndexDropdownViewBag()
        {
            ViewBag.UserType = GetUserType();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> GetRolesOptions(int companyId)
        {
            var list = await _accountManager.GetRolesByCompanyId(companyId, String.Empty);
            List<SelectListItem> selectList = new SelectList(list, "Id", "Name").ToList();
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", selectList);
        }

        protected override void RemoveFromModelState()
        {
            base.RemoveFromModelState();
            ModelState.Remove("Role");
            ModelState.Remove("Password");
            ModelState.Remove("UserId");
        }

        protected override void SetEditProperties(ref UserViewModel model)
        {
            if (model.Id == 0)
                model.Password = model.Password?.Encrypt(false);
        }

        protected override async Task<bool> ValidateModel(UserViewModel model)
        {
            if (model.Id == 0 && string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("Password", "Password is required");
                return await Task.FromResult(true);
            }
            var resultEmail = await _manager.CheckExpression(t => (model.Id == 0 || t.Id != model.Id)
                    && t.Email == model.Email);
            if (resultEmail)
            {
                ModelState.AddModelError("Email", "Email already in use");
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
        protected override async Task SetDropdownViewBag(UserViewModel model)
        {

            ViewBag.RoleId = await _accountManager.GetRolesByCompanyId(0, string.Empty);
            var userType = GetUserType();
            if (userType == 1)
                ViewBag.Customers = (await _customerManager.GetAsync()).GetSelectList();
            ViewBag.UserType = userType;
        }
    }
}
