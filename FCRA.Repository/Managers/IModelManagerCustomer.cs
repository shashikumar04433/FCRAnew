using FCRA.Models;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IModelManagerCustomer<TViewModel>
        where TViewModel : BaseCustomerViewModel
    {
        Task<List<TViewModel>> GetAsync(int customerId, string[]? includes = null, System.Linq.Expressions.Expression<Func<TViewModel, bool>>? predicate = null);
        Task<TViewModel?> GetAsync(int customerId, int id, string[]? includes = null);
        Task<bool> AddUpdateAsync(int customerId, TViewModel model, int userId);
        Task<bool> CheckExpression(int customerId, System.Linq.Expressions.Expression<Func<TViewModel, bool>> predicate);
    }
}
