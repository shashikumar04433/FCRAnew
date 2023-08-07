using AutoMapper;
using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Repository.Repositories;
using FCRA.ViewModels;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Customers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FCRA.Repository.Managers.Implementations
{
    internal class AccountManager : IAccountManager
    {
        private readonly IRepository<UserMaster> _repository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountManager(IRepository<UserMaster> repository, IAccountRepository accountRepository, IMapper mapper)
        {
            _repository = repository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<UserViewModel?> GetUserByEmail(string email)
        {
            var list = await _repository.Find(t => t.Email == email);
            UserViewModel? model = list.FirstOrDefault()?.Map<UserMaster, UserViewModel>();
            return model;
        }

        public async Task<UserViewModel?> Login(string email, string pasword)
        {
            var list = await _repository.Find(t => t.Email == email && EF.Functions.Collate(t.Password, "SQL_Latin1_General_CP1_CS_AS") == EncriptorUtility.Encrypt(pasword, false), new[] { "Role", "Customer" });
            if (list == null || !list.Any())
                return null;
            return _mapper.Map<UserViewModel>(list.First());
        }

        public async Task<UserPermissions?> GetUserPermissions(string userId)
        {
            UserPermissions model = new();
            if (string.IsNullOrWhiteSpace(userId))
                return null;
            var dataSet = _accountRepository.GetUserMenu(userId);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                model.Menus = dataSet.Tables[0].MapTo<MenuViewModel>();
                if (dataSet.Tables.Count > 1)
                    model.Forms = dataSet.Tables[1].MapTo<FormViewModel>();
            }
            return await Task.FromResult(model);
        }


        public async Task<List<RolePermissionsViewModel>> GetRolePermissionsById(int roleId)
        {
            List<RolePermissionsViewModel> model = new();
            var ds = _accountRepository.GetRolePermissionsById(roleId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model = ds.Tables[0].MapTo<RolePermissionsViewModel>();
            return await Task.FromResult(model);
        }

        public async Task<int> UpdateRolePermissions(List<RolePermissionsViewModel> permissions, int roleId, int userId)
        {
            var oldData = GetRolePermissionsById(roleId);
            var oldDatajsonStr = JsonSerializer.Serialize(oldData);
            var newDatajsonStr = JsonSerializer.Serialize(permissions);
            var model = permissions.Select(t => new { t.RoleId, t.FormId, FormView = t.View, FormAdd = t.Add, FormEdit = t.Edit });
            var result = 0;
            var ds = _accountRepository.UpdateRolePermissions(model.ToDataTable(), roleId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            if (result == 1)
            {
                DataAuditTrail dataAudit = new DataAuditTrail();
                dataAudit.DataObject = permissions[0].GetType().Name;
                dataAudit.ActionType = "Update";
                dataAudit.NewValue = newDatajsonStr;
                dataAudit.OldValue = oldDatajsonStr;
                dataAudit.CreatedBy = userId;
                dataAudit.CreatedOn = DateTime.Now;
                await _repository.AuditTrail(dataAudit);
            }
            return await Task.FromResult(result);
        }

        public async Task<List<UserTypeViewModel>> GetAllUserTypes()
        {
            List<UserTypeViewModel> model = new();
            var ds = _accountRepository.GetAllUserTypes();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model = ds.Tables[0].MapTo<UserTypeViewModel>();
            return await Task.FromResult(model);
        }

        public async Task<List<SelectViewModel>> GetRolesByCompanyId(int companyId, string userId)
        {
            List<SelectViewModel> model = new();
            var ds = _accountRepository.RolesByCompanyId(companyId, userId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model = ds.Tables[0].MapTo<SelectViewModel>();
            return await Task.FromResult(model);
        }

        public async Task<FormControlListViewModel> GetFormControlPermissionByUser(int formId, string userId)
        {
            FormControlListViewModel model = new();
            var ds = _accountRepository.GetFormControlPermissionByUser(formId, userId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model.FormControls = ds.Tables[0].MapTo<FormControlViewModel>();
            return await Task.FromResult(model);
        }

        public async Task<List<FormControlViewModel>> GetFormControlPermissionByRole(int formId, int roleId)
        {
            List<FormControlViewModel> model = new();
            var ds = _accountRepository.GetFormControlPermissionByRole(formId, roleId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model = ds.Tables[0].MapTo<FormControlViewModel>();
            return await Task.FromResult(model);
        }

        public async Task<int> UpdateFormControlPermissionByRole(List<FormControlViewModel> permissions, int roleId, string userId)
        {
            var result = 0;
            var ds = _accountRepository.UpdateFormControlPermissionByRole(permissions.ToDataTable(), roleId, userId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return await Task.FromResult(result);
        }

        public async Task<List<CustomerConfigurationViewModel>> GetCustomerConfiguration()
        {
            List<CustomerConfigurationViewModel> model = new();
            var ds = _accountRepository.GetCustomerConfiguration();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                model = ds.Tables[0].MapTo<CustomerConfigurationViewModel>();
            return await Task.FromResult(model);
        }

        public async Task<int> UpdateCustomerConfiguration(List<CustomerConfigurationViewModel> configuration)
        {
            var model = configuration.Select(t => new { t.FieldId, t.Visible });
            var result = 0;
            var ds = _accountRepository.UpdateCustomerConfiguration(model.ToDataTable());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return await Task.FromResult(result);
        }
    }
}
