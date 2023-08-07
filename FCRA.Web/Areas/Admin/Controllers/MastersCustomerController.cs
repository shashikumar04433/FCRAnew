﻿using FCRA.Common;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using FCRA.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FCRA.Web.Areas.Admin.Controllers
{
    public class MastersCustomerController<TModel> : BaseController
        where TModel : BaseMasterCustomerViewModel, new()
    {
        protected readonly IMasterManagerCustomer<TModel> _manager;
        protected readonly string _masterListType;
        protected readonly string _masterType;
        protected readonly string[]? _masterGetInclude;
        protected readonly string[]? _masterEditInclude;
        protected string _globalErrorField = "Name";
        public MastersCustomerController(IMasterManagerCustomer<TModel> manager, string masterListType, string masterType,
            string[]? masterGetInclude = null, string[]? masterEditInclude = null)
        {
            _manager = manager;
            _masterListType = masterListType;
            _masterType = masterType;
            _masterGetInclude = masterGetInclude;
            _masterEditInclude = masterEditInclude;
        }
        public virtual async Task<IActionResult> Index()
        {
            ViewData["Title"] = _masterListType;
            var list = await _manager.GetAsync(GetUserCustomerId(), _masterGetInclude);
            await SetIndexDropdownViewBag();
            return View(list);
        }

        public virtual async Task<IActionResult> Item(string iId)
        {
            ViewData["Title"] = _masterType;
            if (iId == null)
            {
                TModel model = new();
                SetProperties(ref model!);
                await SetDropdownViewBag(model);
                return View(model);
            }
            iId = iId.Decrypt(true);
            if (string.IsNullOrWhiteSpace(iId))
                return NotFound();
            var idInt = Convert.ToInt32(iId);
            var item = await _manager.GetAsync(GetUserCustomerId(), idInt, _masterEditInclude);
            SetProperties(ref item!);
            await SetDropdownViewBag(item!);
            return View(item);
        }
        [HttpPost]
        public virtual async Task<IActionResult> Item(TModel model)
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
            var result = await _manager.AddUpdateAsync(GetUserCustomerId(), model, this.GetUserId());
            if (result)
            {
                SetApplicationResult(true, $"{_masterType} {(model.Id == 0 ? "added" : "updated")} successfully");
                return Redirect("Index");
            }
            ModelState.AddModelError(_globalErrorField, "Something went worng, please contact support");
            await SetDropdownViewBag(model);
            return View(model);
        }

        protected virtual void RemoveFromModelState()
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("CreatedByUser");
            ModelState.Remove("CreatedByUser");
            if (ModelState.Keys.Contains("ScaleType"))
                ModelState.Remove("ScaleType");
        }

        /// <summary>
        /// Used to set additional navigational properties
        /// </summary>
        /// <param name="model"></param>
        protected virtual void SetProperties(ref TModel model)
        {
            //do not delete this
            //this is overried in implemented controllers
        }

        /// <summary>
        /// Used to change in existing properties
        /// </summary>
        /// <param name="model"></param>
        protected virtual void SetEditProperties(ref TModel model)
        {
            //do not delete this
            //this is overried in implemented controllers
        }

        protected virtual async Task<bool> ValidateModel(TModel model)
        {
            var retult = !await _manager.IsValidName(GetUserCustomerId(), model);
            if (retult)
                ModelState.AddModelError("Name", "Name already in use");
            return retult;
        }

        protected virtual async Task SetDropdownViewBag(TModel model)
        {
            await Task.FromResult(true);
        }
        protected virtual async Task SetIndexDropdownViewBag()
        {
            await Task.FromResult(true);
        }

    }
}
