using FCRA.Models;
using FCRA.Models.Base;
using FCRA.Repository.Repositories;
using FCRA.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers.Implementations.Masters
{
    internal class MasterManager<TViewModel, TMasterModel> : IMasterManager<TViewModel>
        where TViewModel : BaseMasterViewModel, new()
        where TMasterModel : BaseMasterModel, new()
    {
        protected readonly IRepository<TMasterModel> _repository;
        protected readonly AutoMapper.IMapper _mapper;
        public MasterManager(IRepository<TMasterModel> repository, AutoMapper.IMapper mapper)
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
                //var createModel = model.Map<TViewModel, TMasterModel>();
                var createModel = _mapper.Map<TMasterModel>(model);
                var NewdatajsonStr = JsonSerializer.Serialize(createModel);
                createModel.IsActive = true;
                createModel.CreatedOn = DateTime.Now;
                createModel.CreatedBy = userId;
                var result = await _repository.AddAsync(createModel);
                if (!result)
                    return false;
                else
                {
                    DataAuditTrail dataAudit = new DataAuditTrail();
                    dataAudit.DataObject = model.GetType().Name;
                    dataAudit.ActionType = "Create";
                    dataAudit.NewValue = NewdatajsonStr;
                    dataAudit.CreatedBy = userId;
                    dataAudit.CreatedOn = DateTime.Now;
                    await _repository.AuditTrail(dataAudit);
                }
            }
            else
            {  //Edit mode
                var oldModel = await _repository.GetAsync(model.Id);
                var OlddatajsonStr = JsonSerializer.Serialize(oldModel);
                var updatedModel = model.MapToDTO(oldModel!);
                var NewdatajsonStr = JsonSerializer.Serialize(updatedModel);
                updatedModel.UpdatedOn = DateTime.Now;
                updatedModel.UpdatedBy = userId;
                var editResult = await _repository.UpdateAsync(updatedModel);
                if (!editResult)
                    return false;
                else
                {
                    DataAuditTrail dataAudit = new DataAuditTrail();
                    dataAudit.DataObject = model.GetType().Name;
                    dataAudit.DataObjectId = model.Id;
                    dataAudit.ActionType = "Update";
                    dataAudit.OldValue = OlddatajsonStr;
                    dataAudit.NewValue = NewdatajsonStr;
                    dataAudit.CreatedBy = userId;
                    dataAudit.CreatedOn = DateTime.Now;
                    await _repository.AuditTrail(dataAudit);
                }
            }
            await _repository.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> IsValidName(TViewModel model)
        {
            return !(await _repository.Find(t => (model.Id == 0 || t.Id != model.Id) && t.Name == model.Name)).Any();
        }

        public virtual async Task<bool> CheckExpression(Expression<Func<TViewModel, bool>> expression)
        {
            var masterExpression = expression.Convert<TViewModel, TMasterModel>();
            return (await _repository.Find(masterExpression)).Any();
        }
    }
}
