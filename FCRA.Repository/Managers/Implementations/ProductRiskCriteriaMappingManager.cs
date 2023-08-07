using AutoMapper;
using FCRA.Models;
using FCRA.Models.Mappings;
using FCRA.Repository.Managers.Implementations.Masters;
using FCRA.Repository.Repositories;
using FCRA.ViewModels.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers.Implementations
{
    internal class ProductRiskCriteriaMappingManager : ModelManagerCustomer<ProductRiskCriteriaMappingViewModel, ProductRiskCriteriaMapping>, IProductRiskCriteriaMappingManager
    {
        private readonly IQuestionRiskCriteriaMappingRepository _questionRiskCriteriaMappingRepository;

        public ProductRiskCriteriaMappingManager(IProductRiskCriteriaMappingRepository repository, IQuestionRiskCriteriaMappingRepository questionRiskCriteriaMappingRepository, IMapper mapper) : base((IBaseModelCustomerRepository<ProductRiskCriteriaMapping>)repository, mapper)
        {
            _questionRiskCriteriaMappingRepository = questionRiskCriteriaMappingRepository;
        }

        public async Task<bool> UpdateProductRiskCriteriaMappings(int customerId, List<ProductRiskCriteriaMappingViewModel> mappings, int riskFactorId, int riskSubFactorId, int userId)
        {
            var list = _repository.GetAsync().Where(t => t.CustomerId == customerId && t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
            var itemsToBeUpdated = false;
            var itemsToBeUpdatedQuestion = false;
            var result = false;
            //Set Inactive/Update if existed and not selected
            foreach (var item in list)
            {
                var mapping = mappings.Where(t => t.RiskFactorId == item.RiskFactorId && t.RiskSubFactorId == item.RiskSubFactorId && t.ProductId == item.ProductId && t.RiskCriteriaId == item.RiskCriteriaId).FirstOrDefault();
                var olddatajsonstr = JsonSerializer.Serialize(item);
                if (mapping != null)
                {
                    if (item.IsActive != mapping.IsSelected)
                    {
                        item.IsActive = mapping.IsSelected;
                        item.UpdatedBy = userId;
                        item.UpdatedOn = DateTime.Now;
                        itemsToBeUpdated = true;
                        result = await _repository.UpdateAsync(item);
                    }
                }
                else
                {
                    if (item.IsActive)
                    {
                        item.IsActive = false;
                        item.UpdatedBy = userId;
                        item.UpdatedOn = DateTime.Now;
                        itemsToBeUpdated = true;
                        result = await _repository.UpdateAsync(item);
                    }
                }
                var newdatajsonstr = JsonSerializer.Serialize(item);
                if (result)
                {
                    DataAuditTrail dataAudit = new DataAuditTrail();
                    dataAudit.DataObject = item.GetType().Name;
                    dataAudit.ActionType = "Update";
                    dataAudit.NewValue = newdatajsonstr;
                    dataAudit.OldValue = olddatajsonstr;
                    dataAudit.CreatedBy = userId;
                    dataAudit.CreatedOn = DateTime.Now;
                    await _repository.AuditTrail(dataAudit);
                }
            }
            //Add new mappings
            foreach (var mapping in mappings)
            {
                if (!list.Any(t => t.RiskFactorId == mapping.RiskFactorId && t.RiskSubFactorId == mapping.RiskSubFactorId && t.ProductId == mapping.ProductId && t.RiskCriteriaId == mapping.RiskCriteriaId))
                {
                    ProductRiskCriteriaMapping item = new()
                    {
                        CustomerId = customerId,
                        RiskFactorId = mapping.RiskFactorId,
                        RiskSubFactorId = mapping.RiskSubFactorId,
                        ProductId = mapping.ProductId,
                        RiskCriteriaId = mapping.RiskCriteriaId,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                        IsActive = true,

                    };
                    itemsToBeUpdated = true;
                    var NewdatajsonStr = JsonSerializer.Serialize(item);
                    result = await _repository.AddAsync(item);

                    if (result)
                    {
                        DataAuditTrail dataAudit = new DataAuditTrail();
                        dataAudit.DataObject = item.GetType().Name;
                        dataAudit.ActionType = "Create";
                        dataAudit.NewValue = NewdatajsonStr;
                        dataAudit.CreatedBy = userId;
                        dataAudit.CreatedOn = DateTime.Now;
                        await _repository.AuditTrail(dataAudit);
                    }
                }
            }

            //question
            var questionList = _questionRiskCriteriaMappingRepository.GetAsync().Where(t => t.CustomerId == customerId && t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
            List<QuestionsRiskCriteriaMapping> addedQuestion = new();
            foreach (var mapping in mappings)
            {
                if (string.IsNullOrWhiteSpace(mapping.QuestionIds))
                    continue;
                List<int> ids = mapping.QuestionIds.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                foreach (int id in ids)
                {
                    QuestionsRiskCriteriaMapping item = new()
                    {
                        CustomerId = customerId,
                        RiskFactorId = mapping.RiskFactorId,
                        RiskSubFactorId = mapping.RiskSubFactorId,
                        ProductId = mapping.ProductId,
                        RiskCriteriaId = mapping.RiskCriteriaId,
                        QuestionId = id,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                    };
                    addedQuestion.Add(item);
                    if (!questionList.Any(t => t.RiskFactorId == mapping.RiskFactorId && t.RiskSubFactorId == mapping.RiskSubFactorId
                    && t.ProductId == mapping.ProductId && t.RiskCriteriaId == mapping.RiskCriteriaId && t.QuestionId == id))
                    {
                        itemsToBeUpdatedQuestion = true;
                        await _questionRiskCriteriaMappingRepository.AddAsync(item);
                    }
                }
            }
            //remove extra
            foreach (var item in questionList)
            {
                if (!addedQuestion.Any(t => t.RiskFactorId == item.RiskFactorId && t.RiskSubFactorId == item.RiskSubFactorId
                    && t.ProductId == item.ProductId && t.RiskCriteriaId == item.RiskCriteriaId && t.QuestionId == item.QuestionId))
                {
                    itemsToBeUpdatedQuestion = true;
                    await _questionRiskCriteriaMappingRepository.DeleteAsync(item);
                }
            }



            try
            {
                if (itemsToBeUpdated)
                {
                    await _repository.SaveChangesAsync();
                }
                if (itemsToBeUpdatedQuestion)
                {
                    await _questionRiskCriteriaMappingRepository.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<QuestionsRiskCriteriaMappingViewModel>> GetQuestionsRiskCriteriaMappings(int customerId, int riskFactorId, int riskSubFactorId)
        {
            var list = await _questionRiskCriteriaMappingRepository.GetAsync()
                .Where(t => t.CustomerId == customerId && t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId)
                .ToListAsync();
            return _mapper.Map<List<QuestionsRiskCriteriaMappingViewModel>>(list);
        }
    }
}
