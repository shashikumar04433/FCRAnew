using FCRA.Models.Base;
using FCRA.Repository.Repositories;
using FCRA.ViewModels;
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
    internal class MasterBaseManager<TViewModel, TMasterModel> : IMasterBaseManager<TViewModel>
        where TViewModel : BaseViewModel, new()
        where TMasterModel : BaseModel, new()
    {
        protected readonly IBaseModelRepository<TMasterModel> _repository;
        protected readonly AutoMapper.IMapper _mapper;
        public MasterBaseManager(IBaseModelRepository<TMasterModel> repository, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<List<TViewModel>> GetAsync(string[]? includes = null, Expression<Func<TViewModel, bool>>? predicate = null)
        {
            var query = _repository.GetAsync(includes);
            if (predicate != null)
            {
                var masterExpression = predicate.Convert<TViewModel, TMasterModel>();
                query = query.Where(masterExpression);
            }
            var list = await query.ToListAsync();
            var finalList = _mapper.Map<List<TViewModel>>(list);
            return finalList;
        }

        public virtual async Task<TViewModel?> GetAsync(int id, string[]? includes = null)
        {
            var model = await _repository.GetAsync(id, includes);
            return _mapper.Map<TViewModel>(model);
        }
        public virtual async Task<bool> AddUpdateAsync(TViewModel model, int userId)
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
                var oldModel = await _repository.GetAsync(model.Id);
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


        public virtual async Task<bool> CheckExpression(Expression<Func<TViewModel, bool>> expression)
        {
            var masterExpression = expression.Convert<TViewModel, TMasterModel>();
            return (await _repository.Find(masterExpression)).Any();
        }

        public virtual async Task<ModelResultViewModel<TViewModel>> AddUpdateResultAsync(TViewModel model, int userId)
        {
            var createModel = model.Map<TViewModel, TMasterModel>();
            //Add mode
            if (model.Id == 0)
            {
                createModel.IsActive = true;
                createModel.CreatedOn = DateTime.Now;
                createModel.CreatedBy = userId;
                var result = await _repository.AddAsync(createModel);
                if (!result)
                    new ModelResultViewModel<TViewModel>() { Result = false };
            }
            else
            {  //Edit mode
                var oldModel = await _repository.GetAsync(model.Id);
                var updatedModel = model.MapToDTO(oldModel!);
                updatedModel.UpdatedOn = DateTime.Now;
                updatedModel.UpdatedBy = userId;
                var editResult = await _repository.UpdateAsync(updatedModel);
                if (!editResult)
                    new ModelResultViewModel<TViewModel>() { Result = false };
            }
            await _repository.SaveChangesAsync();
            if (model.Id == 0)
            {
                model = createModel.Map<TMasterModel, TViewModel>();
            }
            return new ModelResultViewModel<TViewModel>() { Result = true, Model = model }; ;
        }
    }
}
