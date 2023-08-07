using FCRA.Models.Base;
using FCRA.Repository.Repositories;
using FCRA.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers.Implementations.Masters
{
    internal class ModelManagerCustomer<TViewModel, TMasterModel> : IModelManagerCustomer<TViewModel>
        where TViewModel : BaseCustomerViewModel, new()
        where TMasterModel : BaseCustomerModel, new()
    {
        protected readonly IBaseModelCustomerRepository<TMasterModel> _repository;
        protected readonly AutoMapper.IMapper _mapper;
        public ModelManagerCustomer(IBaseModelCustomerRepository<TMasterModel> repository, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<List<TViewModel>> GetAsync(int customerId, string[]? includes = null, Expression<Func<TViewModel, bool>>? predicate = null)
        {
            var query = _repository.GetAsync(includes).Where(t => t.CustomerId == customerId);
            if (predicate != null)
            {
                var masterExpression = predicate.Convert<TViewModel, TMasterModel>(true);
                query = query.Where(masterExpression);
            }
            var list = await query.ToListAsync();
            var finalList = _mapper.Map<List<TViewModel>>(list);
            return finalList;
        }

        public virtual async Task<TViewModel?> GetAsync(int customerId, int id, string[]? includes = null)
        {
            var model = await _repository.GetAsync(customerId, id, includes);
            return _mapper.Map<TViewModel>(model);
        }
        public virtual async Task<bool> AddUpdateAsync(int customerId, TViewModel model, int userId)
        {
            //Add mode
            if (model.Id == 0)
            {
                var createModel = model.Map<TViewModel, TMasterModel>();
                createModel.IsActive = true;
                createModel.CreatedOn = DateTime.Now;
                createModel.CreatedBy = userId;
                var result = await _repository.AddAsync(createModel);
                if (!result)
                    return false;
            }
            else
            {  //Edit mode
                var oldModel = await _repository.GetAsync(customerId, model.Id);
                var updatedModel = model.MapToDTO(oldModel!);
                updatedModel.UpdatedOn = DateTime.Now;
                updatedModel.UpdatedBy = userId;
                var editResult = await _repository.UpdateAsync(updatedModel);
                if (!editResult)
                    return false;
            }
            await _repository.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> CheckExpression(int customerId, Expression<Func<TViewModel, bool>> expression)
        {
            var masterExpression = expression.Convert<TViewModel, TMasterModel>(true);
            return (await _repository.Find(customerId, masterExpression)).Any();
        }
    }
}
