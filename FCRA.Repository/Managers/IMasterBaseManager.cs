using FCRA.Models;
using FCRA.ViewModels;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IMasterBaseManager<TViewModel>
        where TViewModel : BaseViewModel
    {
        Task<List<TViewModel>> GetAsync(string[]? includes = null, System.Linq.Expressions.Expression<Func<TViewModel, bool>>? predicate = null);
        Task<TViewModel?> GetAsync(int id, string[]? includes = null);
        Task<bool> AddUpdateAsync(TViewModel model, int userId);
        Task<bool> CheckExpression(System.Linq.Expressions.Expression<Func<TViewModel, bool>> predicate);
        Task<ModelResultViewModel<TViewModel>> AddUpdateResultAsync(TViewModel model, int userId);
    }
}
