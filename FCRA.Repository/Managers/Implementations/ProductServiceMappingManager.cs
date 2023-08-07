using AutoMapper;
using FCRA.Models;
using FCRA.Models.Mappings;
using FCRA.Repository.Managers.Implementations.Masters;
using FCRA.Repository.Repositories;
using FCRA.ViewModels.Mappings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers.Implementations
{
    internal class ProductServiceMappingManager : ModelManagerCustomer<RiskFactorProductServiceMappingViewModel, RiskFactorProductServiceMapping>, IProductServiceMappingManager
    {
        public ProductServiceMappingManager(IProductServiceMappingRepository repository, IMapper mapper) : base((IBaseModelCustomerRepository<RiskFactorProductServiceMapping>)repository, mapper)
        {
        }

        public async Task<bool> UpdateProductServiceMappings(int customerId, List<RiskFactorProductServiceMappingViewModel> mappings, int riskFactorId, int riskSubFactorId, int userId)
        {
            var list = _repository.GetAsync().Where(t => t.CustomerId == customerId && t.RiskFactorId == riskFactorId).ToList();
            var itemsToBeUpdated = false;
            //Set Inactive/Update if existed and not selected
            foreach (var item in list)
            {
                var mapping = mappings.Where(t => t.RiskFactorId == item.RiskFactorId && t.RiskSubFactorId == riskSubFactorId && t.ProductId == item.ProductId).FirstOrDefault();
                var result = false;
                var OlddatajsonStr = JsonSerializer.Serialize(item);
                if (mapping != null)
                {
                    if (item.IsActive != mapping.IsSelected
                        || !item.ScaleRange2.Equals(mapping.ScaleRange2)
                        || !item.ScaleRange3.Equals(mapping.ScaleRange3) || !item.ScaleRange4.Equals(mapping.ScaleRange4)
                        || !item.ScaleRange5.Equals(mapping.ScaleRange5))
                    {
                        item.IsActive = mapping.IsSelected;
                        item.ScaleRange2 = mapping.ScaleRange2;
                        item.ScaleRange3 = mapping.ScaleRange3;
                        item.ScaleRange4 = mapping.ScaleRange4;
                        item.ScaleRange5 = mapping.ScaleRange5;
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

                //Audit Trail part
                var NewdatajsonStr = JsonSerializer.Serialize(item);
                if (result)
                {
                    DataAuditTrail dataAudit = new DataAuditTrail();
                    dataAudit.DataObject = item.GetType().Name;
                    dataAudit.ActionType = "Update";
                    dataAudit.NewValue = NewdatajsonStr;
                    dataAudit.OldValue = OlddatajsonStr;
                    dataAudit.CreatedBy = userId;
                    dataAudit.CreatedOn = DateTime.Now;
                    await _repository.AuditTrail(dataAudit);
                }
            }
            //Add new mappings
            foreach (var mapping in mappings)
            {
                if (!list.Any(t => t.RiskFactorId == mapping.RiskFactorId && t.RiskSubFactorId == mapping.RiskSubFactorId && t.ProductId == mapping.ProductId))
                {
                    RiskFactorProductServiceMapping item = new()
                    {
                        CustomerId = customerId,
                        RiskFactorId = mapping.RiskFactorId,
                        RiskSubFactorId = mapping.RiskSubFactorId,
                        ProductId = mapping.ProductId,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                        ScaleRange2 = mapping.ScaleRange2,
                        ScaleRange3 = mapping.ScaleRange3,
                        ScaleRange4 = mapping.ScaleRange4,
                        ScaleRange5 = mapping.ScaleRange5
                    };
                    itemsToBeUpdated = true;
                    var NewdatajsonStr = JsonSerializer.Serialize(item);
                    var result = await _repository.AddAsync(item);

                    if(result)
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
            try
            {
                if (itemsToBeUpdated)
                {
                    await _repository.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
