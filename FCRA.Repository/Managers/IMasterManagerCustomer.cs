using FCRA.Models;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IMasterManagerCustomer<TViewModel>
        where TViewModel : BaseMasterCustomerViewModel
    {
        Task<List<TViewModel>> GetAsync(int customerId, string[]? includes = null, System.Linq.Expressions.Expression<Func<TViewModel, bool>>? predicate = null);
        Task<List<TViewModel>> GetWithoutOrderAsync(int customerId, string[]? includes = null, System.Linq.Expressions.Expression<Func<TViewModel, bool>>? predicate = null);
        Task<TViewModel?> GetAsync(int customerId, int id, string[]? includes = null);
        Task<bool> AddUpdateAsync(int customerId, TViewModel model, int userId);
        Task<bool> IsValidName(int customerId, TViewModel model);
        Task<bool> CheckExpression(int customerId, System.Linq.Expressions.Expression<Func<TViewModel, bool>> predicate);
        Task<bool> AddUpdateRangeAsync(int customerId, List<TViewModel> model, int userId);
        Task<List<TViewModel>> GetAuditTrailAsync(int objectId, string objectname);
    }
}
