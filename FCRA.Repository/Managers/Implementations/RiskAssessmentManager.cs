using AutoMapper;
using FCRA.Common;
using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.Repositories;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Reports;
using FCRA.ViewModels.Responses;
using FCRA.ViewModels.Responses.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FCRA.Repository.Managers.Implementations
{
    internal class RiskAssessmentManager : IRiskAssessmentManager
    {
        private readonly IRiskAssessmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRiskScoreResponseRepository _riskScoreResponseRepository;
        private readonly IRiskFactorResponseRepository _riskFactorResponseRepository;
        private readonly IRiskSubFactorResponseRepository _riskSubFactorResponseRepository;
        private readonly IRiskScoreProductVolumRatingResponseRepository _riskScoreProductVolumRatingResponseRepository;
        private readonly IRiskSubFactorVolumeResponseRepository _riskSubFactorVolumeResponseRepository;
        private readonly IQuestionRiskCriteriaMappingRepository _questionRiskCriteriaMappingRepository;

        public RiskAssessmentManager(IRiskAssessmentRepository repository, IMapper mapper
            , IRiskScoreResponseRepository riskScoreResponseRepository, IRiskFactorResponseRepository riskFactorResponseRepository
            , IRiskSubFactorResponseRepository riskSubFactorResponseRepository, IRiskScoreProductVolumRatingResponseRepository riskScoreProductVolumRatingResponseRepository
            , IRiskSubFactorVolumeResponseRepository riskSubFactorVolumeResponseRepository, IQuestionRiskCriteriaMappingRepository questionRiskCriteriaMappingRepository
            , IRepository<UserMaster> UserMasterRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _riskScoreResponseRepository = riskScoreResponseRepository;
            _riskFactorResponseRepository = riskFactorResponseRepository;
            _riskSubFactorResponseRepository = riskSubFactorResponseRepository;
            _riskScoreProductVolumRatingResponseRepository = riskScoreProductVolumRatingResponseRepository;
            _riskSubFactorVolumeResponseRepository = riskSubFactorVolumeResponseRepository;
            _questionRiskCriteriaMappingRepository = questionRiskCriteriaMappingRepository;
        }


        public async Task<List<RiskSubFactorViewModel>> GetRiskSubFactorsByRiskType(int customerId, List<int> riskFactorIds)
        {
            var list = await _repository.GetRiskSubFactorsByRiskType(customerId, riskFactorIds);
            return _mapper.Map<List<RiskSubFactorViewModel>>(list);
        }
        public async Task<List<RiskScoreResponseViewModel>> GetRiskScoreResponse(int customerId, List<int> riskFactorIds)
        {
            var list = await _riskScoreResponseRepository.GetRiskScoreResponse(customerId, riskFactorIds);
            return _mapper.Map<List<RiskScoreResponseViewModel>>(list);
        }

        public async Task<List<RiskFactorResponseViewModel>> GetRiskFactorResponse(int customerId, List<int> riskFactorIds)
        {
            var list = await _riskFactorResponseRepository.GetRiskFactorResponse(customerId, riskFactorIds);
            return _mapper.Map<List<RiskFactorResponseViewModel>>(list);
        }
        public async Task<List<RiskSubFactorResponseViewModel>> GetRiskSubFactorResponse(int customerId, List<int> riskFactorIds)
        {
            var list = await _riskSubFactorResponseRepository.GetRiskSubFactorResponse(customerId, riskFactorIds);
            return _mapper.Map<List<RiskSubFactorResponseViewModel>>(list);
        }
        public async Task<List<RiskSubFactorVolumeResponseViewModel>> GetRiskSubFactorVolumeResponse(int customerId, List<int> riskFactorIds)
        {
            var list = await _riskSubFactorVolumeResponseRepository.GetRiskSubFactorVolumeResponse(customerId, riskFactorIds);
            return _mapper.Map<List<RiskSubFactorVolumeResponseViewModel>>(list);
        }
        public async Task<List<RiskScoreProductVolumRatingResponse>> GetRiskScoreProductVolumRatingResponse(int customerId, List<int> riskFactorIds)
        {
            var list = await _riskScoreProductVolumRatingResponseRepository.GetRiskScoreProductVolumRatingResponse(customerId, riskFactorIds);
            return _mapper.Map<List<RiskScoreProductVolumRatingResponse>>(list);
        }
        public async Task<List<QuestionsRiskCriteriaMappingViewModel>> GetQuestionRiskCriteriaMapping(int customerId, int riskFactorId, int riskSubFactorId, int productId, int riskCriteriaId)
        {
            var list = await _questionRiskCriteriaMappingRepository.GetAsync(new[] { "Questions" }).Where(t => t.CustomerId == customerId && t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId
                 && t.ProductId == productId && t.RiskCriteriaId == riskCriteriaId).ToListAsync();
            return _mapper.Map<List<QuestionsRiskCriteriaMappingViewModel>>(list);
        }
        public async Task<List<QuestionsRiskCriteriaMappingViewModel>> GetQuestionRiskCriteriaMapping(int customerId, List<int> riskFactorIds)
        {
            var list = await _questionRiskCriteriaMappingRepository.GetAsync(new[] { "Questions" }).Where(t => t.CustomerId == customerId && riskFactorIds.Contains(t.RiskFactorId)).ToListAsync();
            return _mapper.Map<List<QuestionsRiskCriteriaMappingViewModel>>(list);
        }
        public async Task<bool> UpdateAssessmentResponse(int customerId, ResponseSaveViewModel responses, List<int> riskFactorIds, int userId)
        {
            JsonSerializerOptions jsonSerializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            var itemsToBeUpdatedFactor = false;
            var itemsToBeUpdatedSF = false;
            var itemsToBeUpdatedProduct = false;
            var itemsToBeUpdatedScore = false;
            var itemsToBeUpdatedSFVolume = false;

            //Factor resposes start
            var scoreListFactor = await _riskFactorResponseRepository.GetRiskFactorResponse(customerId, riskFactorIds);
            //Add/update
            foreach (var response in responses.RiskFactorResponses)
            {
                var result = false;
                var score = scoreListFactor.Where(t => t.RiskFactorId == response.RiskFactorId).FirstOrDefault();
                if (score != null)
                {
                    var OldFactordatajsonStr = JsonSerializer.Serialize(score);
                    if (response.TotalWeightedScore > 0 && response.TotalWeightedScore != score.TotalWeightedScore)
                    {
                        score.TotalWeightedScore = response.TotalWeightedScore;
                        score.UpdatedBy = userId;
                        score.UpdatedOn = DateTime.Now;
                        itemsToBeUpdatedFactor = true;
                        result = await _riskFactorResponseRepository.UpdateAsync(score);
                    }
                    if (result)
                    {
                        var NewFactordatajsonStr = JsonSerializer.Serialize(score);
                        DataAuditTrail dataAudit = new DataAuditTrail();
                        dataAudit.DataObject = score.GetType().Name;
                        dataAudit.DataObjectId = customerId;
                        dataAudit.ActionType = "Update";
                        dataAudit.OldValue = OldFactordatajsonStr;
                        dataAudit.NewValue = NewFactordatajsonStr;
                        dataAudit.CreatedBy = userId;
                        dataAudit.CreatedOn = DateTime.Now;
                        await _riskFactorResponseRepository.AuditTrail(dataAudit);
                    }
                }
                else
                {
                    if (response.TotalWeightedScore > 0)
                    {
                        RiskFactorResponse item = new()
                        {
                            CustomerId = customerId,
                            RiskFactorId = response.RiskFactorId,
                            TotalWeightedScore = response.TotalWeightedScore,
                            IsActive = true,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        };
                        itemsToBeUpdatedFactor = true;
                        result = await _riskFactorResponseRepository.AddAsync(item);

                        if (result)
                        {
                            var NewFactordatajsonStr = JsonSerializer.Serialize(score);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = item.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Add";
                            dataAudit.NewValue = NewFactordatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }

                }
            }
            //Remove existing
            foreach (var score in scoreListFactor)
            {
                if (!responses.RiskFactorResponses.Any(t => t.RiskFactorId == score.RiskFactorId))
                {
                    itemsToBeUpdatedFactor = true;
                    await _riskFactorResponseRepository.DeleteAsync(score);
                }
            }
            //Factor resposes end


            //Sub Factor responses start
            var scoreListSF = await _riskSubFactorResponseRepository.GetRiskSubFactorResponse(customerId, riskFactorIds);
            //Add/Update
            foreach (var response in responses.RiskSubFactorResponses)
            {
                var result = false;
                var score = scoreListSF.Where(t => t.RiskFactorId == response.RiskFactorId && t.RiskSubFactorId == response.RiskSubFactorId).FirstOrDefault();
                if (score != null)
                {
                    var OldSFactordatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                    if (response.Score.HasValue && (response.Score != score.Score || response.Assumptions != score.Assumptions || response.Response != score.Response))
                    {
                        score.Score = response.Score.Value;
                        score.Assumptions = response.Assumptions;
                        score.Response = response.Response;
                        score.PreDefinedParameterId = response.PreDefinedParameterId;
                        score.ResponseDescription = response.ResponseDescription;
                        score.UpdatedBy = userId;
                        score.UpdatedOn = DateTime.Now;
                        itemsToBeUpdatedSF = true;
                        result = await _riskSubFactorResponseRepository.UpdateAsync(score);

                        if (result)
                        {
                            var NewSFactordatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = score.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Update";
                            dataAudit.OldValue = OldSFactordatajsonStr;
                            dataAudit.NewValue = NewSFactordatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
                else
                {
                    if (response.Score.HasValue)
                    {
                        RiskSubFactorResponse item = new()
                        {
                            CustomerId = customerId,
                            RiskFactorId = response.RiskFactorId,
                            RiskSubFactorId = response.RiskSubFactorId,
                            Score = response.Score.Value,
                            Assumptions = response.Assumptions,
                            Response = response.Response,
                            PreDefinedParameterId = response.PreDefinedParameterId,
                            ResponseDescription = response.ResponseDescription,
                            IsActive = true,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        };
                        itemsToBeUpdatedSF = true;
                        result = await _riskSubFactorResponseRepository.AddAsync(item);

                        if (result)
                        {
                            var NewSFactordatajsonStr = JsonSerializer.Serialize(item, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = item.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Create";
                            dataAudit.NewValue = NewSFactordatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
            }
            //Remove existing
            foreach (var score in scoreListSF)
            {
                if (!responses.RiskSubFactorResponses.Any(t => t.RiskFactorId == score.RiskFactorId && t.RiskSubFactorId == score.RiskSubFactorId))
                {
                    itemsToBeUpdatedSF = true;
                    await _riskSubFactorResponseRepository.DeleteAsync(score);
                }
            }
            //Sub Factor responses end

            //Product score start
            var scoreListProduct = await _riskScoreProductVolumRatingResponseRepository.GetRiskScoreProductVolumRatingResponse(customerId, riskFactorIds);
            //Add/update
            foreach (var response in responses.RiskScoreProductVolumRatingResponses)
            {
                var result = false;
                var score = scoreListProduct.Where(t => t.RiskFactorId == response.RiskFactorId && t.RiskSubFactorId == response.RiskSubFactorId && t.ProductId == response.ProductId).FirstOrDefault();
                if (score != null)
                {
                    var OldProductdatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                    if (response.Volume != score.Volume || response.TotalScore != score.TotalScore || response.FinalScore != score.FinalScore
                        || response.RiskRating != score.RiskRating || response.Value != score.Value)
                    {
                        score.Volume = response.Volume;
                        score.TotalScore = response.TotalScore;
                        score.FinalScore = response.FinalScore;
                        score.RiskRating = response.RiskRating;
                        score.UpdatedBy = userId;
                        score.UpdatedOn = DateTime.Now;
                        itemsToBeUpdatedProduct = true;
                        score.Value = response.Value;
                        result = await _riskScoreProductVolumRatingResponseRepository.UpdateAsync(score);
                        if (result)
                        {
                            var NewProductdatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = score.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Update";
                            dataAudit.OldValue = OldProductdatajsonStr;
                            dataAudit.NewValue = NewProductdatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
                else
                {
                    if ((response.Volume.HasValue && response.Volume.Value > 0) || (response.TotalScore.HasValue && response.TotalScore.Value > 0))
                    {
                        RiskScoreProductVolumRatingResponse item = new()
                        {
                            CustomerId = customerId,
                            RiskFactorId = response.RiskFactorId,
                            RiskSubFactorId = response.RiskSubFactorId,
                            ProductId = response.ProductId,
                            Volume = response.Volume,
                            TotalScore = response.TotalScore,
                            FinalScore = response.FinalScore,
                            RiskRating = response.RiskRating,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now,
                            Value = response.Value
                        };
                        itemsToBeUpdatedProduct = true;
                        result = await _riskScoreProductVolumRatingResponseRepository.AddAsync(item);

                        if (result)
                        {
                            var NewProductdatajsonStr = JsonSerializer.Serialize(item, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = item.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Create";
                            dataAudit.NewValue = NewProductdatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
            }
            //Remove existing
            foreach (var score in scoreListProduct)
            {
                if (!responses.RiskScoreProductVolumRatingResponses.Any(t => t.RiskFactorId == score.RiskFactorId && t.RiskSubFactorId == score.RiskSubFactorId && t.ProductId == score.ProductId))
                {
                    itemsToBeUpdatedProduct = true;
                    await _riskScoreProductVolumRatingResponseRepository.DeleteAsync(score);
                }
            }
            //Product score end


            //score responses start
            var scoreList = await _riskScoreResponseRepository.GetRiskScoreResponse(customerId, riskFactorIds);
            //Add/Update
            foreach (var response in responses.RiskScoreResponses)
            {
                var result = false;
                var score = scoreList.Where(t => t.RiskFactorId == response.RiskFactorId && t.RiskSubFactorId == response.RiskSubFactorId
                && t.ProductId == response.ProductId && t.RiskCriteriaId == response.RiskCriteriaId).FirstOrDefault();
                if (score != null)
                {
                    var OldscoredatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                    if (response.Score.HasValue && (response.Score != score.Score || response.QuestionIds != score.QuestionIds || response.Answers != score.Answers))
                    {
                        score.Score = response.Score.Value;
                        score.QuestionIds = response.QuestionIds;
                        score.Answers = response.Answers;
                        score.UpdatedBy = userId;
                        score.UpdatedOn = DateTime.Now;
                        itemsToBeUpdatedScore = true;
                        result = await _riskScoreResponseRepository.UpdateAsync(score);

                        if (result)
                        {
                            var NewSFactordatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = score.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Update";
                            dataAudit.OldValue = OldscoredatajsonStr;
                            dataAudit.NewValue = NewSFactordatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
                else
                {
                    if (response.Score.HasValue)
                    {
                        RiskScoreResponse item = new()
                        {
                            CustomerId = customerId,
                            RiskFactorId = response.RiskFactorId,
                            RiskSubFactorId = response.RiskSubFactorId,
                            ProductId = response.ProductId,
                            RiskCriteriaId = response.RiskCriteriaId,
                            Score = response.Score.Value,
                            QuestionIds = response.QuestionIds,
                            Answers = response.Answers,
                            IsActive = true,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        };
                        itemsToBeUpdatedScore = true;
                        await _riskScoreResponseRepository.AddAsync(item);

                        if (result)
                        {
                            var NewscoredatajsonStr = JsonSerializer.Serialize(score, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = item.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Update";
                            dataAudit.NewValue = NewscoredatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
            }
            //Remove existing
            foreach (var score in scoreList)
            {
                if (!responses.RiskScoreResponses.Any(t => t.RiskFactorId == score.RiskFactorId && t.RiskSubFactorId == score.RiskSubFactorId
                && t.ProductId == score.ProductId && t.RiskCriteriaId == score.RiskCriteriaId))
                {
                    itemsToBeUpdatedScore = true;
                    await _riskScoreResponseRepository.DeleteAsync(score);
                }
            }
            //score responses end


            //Sub Factor Volume start
            var volumesList = await _riskSubFactorVolumeResponseRepository.GetRiskSubFactorVolumeResponse(customerId, riskFactorIds);
            //Add/Update
            foreach (var response in responses.VolumeMappings)
            {
                var result = false;
                var volume = volumesList.Where(t => t.RiskFactorId == response.RiskFactorId && t.RiskSubFactorId == response.RiskSubFactorId).FirstOrDefault();
                if (volume != null)
                {
                    var OldVolumedatajsonStr = JsonSerializer.Serialize(volume, options: jsonSerializerOptions);
                    if (response.Volume1 != volume.Volume1 || response.Score2 != volume.Score2
                        || response.Volume3 != volume.Volume3 || response.Volume4 != volume.Volume4
                        || response.Volume5 != volume.Volume5)
                    {
                        volume.Score1 = response.Score1;
                        volume.Volume1 = response.Volume1;
                        volume.Weight1 = response.Weight1;
                        volume.WeightedScore1 = response.WeightedScore1;
                        volume.Score2 = response.Score2;
                        volume.Volume2 = response.Volume2;
                        volume.Weight2 = response.Weight2;
                        volume.WeightedScore2 = response.WeightedScore2;
                        volume.Score3 = response.Score3;
                        volume.Volume3 = response.Volume3;
                        volume.Weight3 = response.Weight3;
                        volume.WeightedScore3 = response.WeightedScore3;
                        volume.Score4 = response.Score4;
                        volume.Volume4 = response.Volume4;
                        volume.Weight4 = response.Weight4;
                        volume.WeightedScore4 = response.WeightedScore4;
                        volume.Score5 = response.Score5;
                        volume.Volume5 = response.Volume5;
                        volume.Weight5 = response.Weight5;
                        volume.WeightedScore5 = response.WeightedScore5;
                        volume.Countries = response.Countries;
                        volume.CountryWiseRating = response.CountryWiseRating;
                        volume.CountryWiseVolume = response.CountryWiseVolume;
                        itemsToBeUpdatedSFVolume = true;
                        result = await _riskSubFactorVolumeResponseRepository.UpdateAsync(volume);

                        if (result)
                        {
                            var NewVolumedatajsonStr = JsonSerializer.Serialize(volume, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = volume.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Update";
                            dataAudit.OldValue = OldVolumedatajsonStr;
                            dataAudit.NewValue = NewVolumedatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
                else
                {
                    if (response.Volume1.HasValue || response.Volume2.HasValue || response.Volume3.HasValue
                        || response.Volume4.HasValue || response.Volume5.HasValue)
                    {
                        RiskSubFactorVolumeResponse item = new()
                        {
                            CustomerId = customerId,
                            RiskFactorId = response.RiskFactorId,
                            RiskSubFactorId = response.RiskSubFactorId,
                            Score1 = response.Score1,
                            Volume1 = response.Volume1,
                            Weight1 = response.Weight1,
                            WeightedScore1 = response.WeightedScore1,
                            Score2 = response.Score2,
                            Volume2 = response.Volume2,
                            Weight2 = response.Weight2,
                            WeightedScore2 = response.WeightedScore2,
                            Score3 = response.Score3,
                            Volume3 = response.Volume3,
                            Weight3 = response.Weight3,
                            WeightedScore3 = response.WeightedScore3,
                            Score4 = response.Score4,
                            Volume4 = response.Volume4,
                            Weight4 = response.Weight4,
                            WeightedScore4 = response.WeightedScore4,
                            Score5 = response.Score5,
                            Volume5 = response.Volume5,
                            Weight5 = response.Weight5,
                            WeightedScore5 = response.WeightedScore5,
                            Countries = response.Countries,
                            CountryWiseRating = response.CountryWiseRating,
                            CountryWiseVolume = response.CountryWiseVolume,
                            IsActive = true,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        };
                        itemsToBeUpdatedSFVolume = true;
                        result = await _riskSubFactorVolumeResponseRepository.AddAsync(item);

                        if (result)
                        {
                            var NewVolumedatajsonStr = JsonSerializer.Serialize(item, options: jsonSerializerOptions);
                            DataAuditTrail dataAudit = new DataAuditTrail();
                            dataAudit.DataObject = item.GetType().Name;
                            dataAudit.DataObjectId = customerId;
                            dataAudit.ActionType = "Create";
                            dataAudit.NewValue = NewVolumedatajsonStr;
                            dataAudit.CreatedBy = userId;
                            dataAudit.CreatedOn = DateTime.Now;
                            await _riskFactorResponseRepository.AuditTrail(dataAudit);
                        }
                    }
                }
            }
            //Remove existing
            foreach (var item in volumesList)
            {
                if (!responses.VolumeMappings.Any(t => t.RiskFactorId == item.RiskFactorId && t.RiskSubFactorId == item.RiskSubFactorId))
                {
                    itemsToBeUpdatedSFVolume = true;
                    await _riskSubFactorVolumeResponseRepository.DeleteAsync(item);
                }
            }
            //Sub Factor Volume end

            try
            {
                if (itemsToBeUpdatedFactor)
                {
                    await _riskFactorResponseRepository.SaveChangesAsync();
                }
                if (itemsToBeUpdatedSF)
                {
                    await _riskSubFactorResponseRepository.SaveChangesAsync();
                }
                if (itemsToBeUpdatedProduct)
                {
                    await _riskScoreProductVolumRatingResponseRepository.SaveChangesAsync();
                }
                if (itemsToBeUpdatedScore)
                {
                    await _riskScoreResponseRepository.SaveChangesAsync();
                }
                if (itemsToBeUpdatedSFVolume)
                {
                    await _riskSubFactorVolumeResponseRepository.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> ProcessRiskRegisterExcel(int customerId, ScaleType scaleType, DataSet ds
            , List<GeographyRiskViewModel> riskCountries, List<RiskFactorProductServiceMappingViewModel> productMappings
            , List<RiskSubFactorViewModel> riskSubFactors, List<RiskFactorViewModel> riskFactors, int userId)
        {
            ExcelResponseViewModel request = new();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var recordType = Convert.ToInt32(dt.Rows[0]["RegisterType"]);
                        if (recordType == 1)
                        {
                            var list = dt.MapTo<ExcelRiskSubFactorResponse>();
                            request.SubFactorResponses.AddRange(list);
                        }
                        if (recordType == 2)
                        {
                            var list = dt.MapTo<ExcelRiskVolumeResponse>();
                            request.VolumeResponses.AddRange(list);
                        }
                        if (recordType == 3)
                        {
                            var list = dt.MapTo<ExcelRiskProductResponse>();
                            request.ProductResponses.AddRange(list);
                        }
                    }
                }
            }
            if (!request.SubFactorResponses.Any() && !request.VolumeResponses.Any() && !request.ProductResponses.Any())
                return -2;

            ResponseSaveViewModel responses = new();

            #region Volume transformation
            var uniqueFactors = request.VolumeResponses.Select(t => t.FactorId).Distinct().ToList();
            var uniqueVolumeSubFactors = request.VolumeResponses.Select(t => new { t.FactorId, t.SubFactorId }).Distinct().ToList();
            List<int> rank1Countries = riskCountries.Where(t => t.RiskRating == RiskRating.Low).Select(t => t.Id).ToList();
            List<int> rank2Countries = riskCountries.Where(t => t.RiskRating == RiskRating.Medium).Select(t => t.Id).ToList();
            List<int> rank3Countries = riskCountries.Where(t => t.RiskRating == RiskRating.High).Select(t => t.Id).ToList();
            List<int> rank4Countries = riskCountries.Where(t => t.RiskRating == RiskRating.Higher).Select(t => t.Id).ToList();
            List<int> rank5Countries = riskCountries.Where(t => t.RiskRating == RiskRating.Extreme).Select(t => t.Id).ToList();
            foreach (var item in uniqueVolumeSubFactors)
            {
                var volume1 = 0.00M;
                var volume2 = 0.00M;
                var volume3 = 0.00M;
                var volume4 = 0.00M;
                var volume5 = 0.00M;
                List<int> selectedListCountries = new();
                var responseVolumeContries = request.VolumeResponses.Where(t => t.FactorId == item.FactorId && t.SubFactorId == item.SubFactorId
                    && t.Volume.HasValue);
                var list1 = responseVolumeContries.Where(t => rank1Countries.Contains(t.CountryId));
                if (list1.Any())
                {
                    if (list1.Any(t => t.Volume.HasValue))
                        volume1 = list1.Sum(t => t.Volume!.Value);
                    selectedListCountries.AddRange(list1.Select(t => t.CountryId).ToList());
                }
                var list2 = responseVolumeContries.Where(t => rank2Countries.Contains(t.CountryId));
                if (list2.Any())
                {
                    if (list2.Any(t => t.Volume.HasValue))
                        volume2 = list2.Sum(t => t.Volume!.Value);
                    selectedListCountries.AddRange(list2.Select(t => t.CountryId).ToList());
                }
                var list3 = responseVolumeContries.Where(t => rank3Countries.Contains(t.CountryId));
                if (list3.Any())
                {
                    if (list3.Any(t => t.Volume.HasValue))
                        volume3 = list3.Sum(t => t.Volume!.Value);
                    selectedListCountries.AddRange(list3.Select(t => t.CountryId).ToList());
                }
                if (scaleType == ScaleType.FourPoint || scaleType == ScaleType.FivePoint)
                {
                    var list4 = responseVolumeContries.Where(t => rank4Countries.Contains(t.CountryId));
                    if (list4.Any())
                    {
                        if (list4.Any(t => t.Volume.HasValue))
                            volume4 = list4.Sum(t => t.Volume!.Value);
                        selectedListCountries.AddRange(list4.Select(t => t.CountryId).ToList());
                    }
                }
                if (scaleType == ScaleType.FivePoint)
                {
                    var list5 = responseVolumeContries.Where(t => rank5Countries.Contains(t.CountryId));
                    if (list5.Any())
                    {
                        if (list5.Any(t => t.Volume.HasValue))
                            volume5 = list5.Sum(t => t.Volume!.Value);
                        selectedListCountries.AddRange(list5.Select(t => t.CountryId).ToList());
                    }
                }
                var totalVolume = volume1 + volume2 + volume3 + volume4 + volume5;
                var totalWeightedScore = 0.00M;
                if (totalVolume > 0)
                {
                    RiskSubFactorVolumeResponseViewModel volumeModel = new()
                    {
                        RiskFactorId = item.FactorId,
                        RiskSubFactorId = item.SubFactorId,
                        Score1 = 1,
                        Score2 = 2,
                        Score3 = 3
                    };
                    if (scaleType == ScaleType.FourPoint || scaleType == ScaleType.FivePoint)
                    {
                        volumeModel.Score4 = 4;
                        volumeModel.Volume4 = 0;
                    }
                    if (scaleType == ScaleType.FivePoint)
                    {
                        volumeModel.Score5 = 5;
                        volumeModel.Volume5 = 0;
                    }

                    volumeModel.Volume1 = volume1;
                    volumeModel.Weight1 = 0;
                    volumeModel.WeightedScore1 = 0;
                    if (volume1 > 0)
                    {
                        volumeModel.Weight1 = volume1 * 100 / totalVolume;
                        volumeModel.WeightedScore1 = 1 * (volumeModel.Weight1 / 100);
                        totalWeightedScore += volumeModel.WeightedScore1.Value;
                    }
                    volumeModel.Volume2 = volume2;
                    volumeModel.Weight2 = 0;
                    volumeModel.WeightedScore2 = 0;
                    if (volume2 > 0)
                    {
                        volumeModel.Weight2 = volume2 * 100 / totalVolume;
                        volumeModel.WeightedScore2 = 2 * (volumeModel.Weight2 / 100);
                        totalWeightedScore += volumeModel.WeightedScore2.Value;
                    }
                    volumeModel.Volume3 = volume3;
                    volumeModel.Weight3 = 0;
                    volumeModel.WeightedScore3 = 0;
                    if (volume3 > 0)
                    {
                        volumeModel.Weight3 = volume3 * 100 / totalVolume;
                        volumeModel.WeightedScore3 = 1 * (volumeModel.Weight3 / 100);
                        totalWeightedScore += volumeModel.WeightedScore3.Value;
                    }
                    if (volume4 > 0)
                    {
                        volumeModel.Volume4 = volume4;
                        volumeModel.Score4 = 4;
                        volumeModel.Weight4 = volume4 * 100 / totalVolume;
                        volumeModel.WeightedScore4 = 4 * (volumeModel.Weight4 / 100);
                        totalWeightedScore += volumeModel.WeightedScore4.Value;
                    }
                    if (volume5 > 0)
                    {
                        volumeModel.Volume5 = volume5;
                        volumeModel.Score5 = 5;
                        volumeModel.Weight5 = volume5 * 100 / totalVolume;
                        volumeModel.WeightedScore5 = 5 * (volumeModel.Weight5 / 100);
                        totalWeightedScore += volumeModel.WeightedScore5.Value;
                    }
                    foreach (var rc in responseVolumeContries)
                    {
                        var oc = riskCountries.Where(t => t.Id == rc.CountryId).FirstOrDefault();
                        if (oc != null)
                        {
                            rc.RiskRating = oc.RiskRating;
                        }
                    }

                    volumeModel.Countries = string.Join(",", responseVolumeContries.Where(t => selectedListCountries.Contains(t.CountryId))
                        .Select(t => t.CountryId).ToList());
                    volumeModel.CountryWiseVolume = string.Join(",", responseVolumeContries.Where(t => selectedListCountries.Contains(t.CountryId))
                        .Select(t => t.Volume).ToList());
                    volumeModel.CountryWiseRating = string.Join(",", responseVolumeContries.Where(t => selectedListCountries.Contains(t.CountryId))
                       .Select(t => (int)t.RiskRating).ToList());
                    responses.VolumeMappings.Add(volumeModel);

                    var subFactor = riskSubFactors.Where(t => t.RiskFactorId == item.FactorId && t.Id == item.SubFactorId).FirstOrDefault();
                    if (subFactor != null)
                    {
                        RiskSubFactorResponseViewModel subFactorResponse = new()
                        {
                            RiskFactorId = item.FactorId,
                            RiskSubFactorId = item.SubFactorId,
                            Response = totalWeightedScore,
                            ResponseDescription = request.SubFactorResponses.Where(t => t.FactorId == item.FactorId && t.SubFactorId == item.SubFactorId).FirstOrDefault()?.Comments,
                            Score = GetScoreRating(scaleType, subFactor, Convert.ToString(totalWeightedScore))
                        };
                        responses.RiskSubFactorResponses.Add(subFactorResponse);
                    }
                }
            }
            #endregion

            #region Product transformation
            var uniqueFactorsProducts = request.ProductResponses.Select(t => t.FactorId).Distinct().ToList();
            var uniqueProductSubFactors = request.ProductResponses.Select(t => new { t.FactorId, t.SubFactorId, t.ProductId }).Distinct().ToList();
            foreach (var product in uniqueProductSubFactors)
            {
                var productMapping = productMappings.Where(t => t.RiskFactorId == product.FactorId && t.RiskSubFactorId == product.SubFactorId && t.ProductId == product.ProductId).FirstOrDefault();
                //Product Criteria question score
                var scoreRsponseList = request.ProductResponses.Where(t => t.FactorId == product.FactorId && t.SubFactorId == product.SubFactorId && t.ProductId == product.ProductId && t.Answer.HasValue);
                if (scoreRsponseList.Any())
                {
                    var scoreResponseSum = scoreRsponseList.Sum(t => t.Answer);
                    var ratingResponse = new RiskScoreProductVolumRatingResponseViewModel()
                    {
                        RiskFactorId = product.FactorId,
                        RiskSubFactorId = product.SubFactorId,
                        ProductId = product.ProductId,
                        Volume = scoreRsponseList.Where(t => t.Value.HasValue)?.Select(x => x.Value).FirstOrDefault(),
                        TotalScore = scoreResponseSum
                    };
                    if (productMapping != null && scoreResponseSum > 0)
                    {
                        if (ratingResponse.TotalScore < productMapping.ScaleRange2)
                        {
                            ratingResponse.FinalScore = 10;
                            ratingResponse.RiskRating = 1;
                        }
                        else if (ratingResponse.TotalScore < productMapping.ScaleRange3)
                        {
                            ratingResponse.FinalScore = 20;
                            ratingResponse.RiskRating = 2;
                        }
                        else if (scaleType == ScaleType.ThreePoint)
                        {
                            if (ratingResponse.TotalScore >= productMapping.ScaleRange3)
                            {
                                ratingResponse.FinalScore = 30;
                                ratingResponse.RiskRating = 3;
                            }
                            else
                            {
                                ratingResponse.FinalScore = 20;
                                ratingResponse.RiskRating = 2;
                            }
                        }
                        else if ((scaleType == ScaleType.FourPoint || scaleType == ScaleType.FivePoint) && ratingResponse.TotalScore < productMapping.ScaleRange4)
                        {
                            ratingResponse.FinalScore = 30;
                            ratingResponse.RiskRating = 3;
                        }
                        else if (scaleType == ScaleType.FourPoint)
                        {
                            if (ratingResponse.TotalScore >= productMapping.ScaleRange4)
                            {
                                ratingResponse.FinalScore = 40;
                                ratingResponse.RiskRating = 4;
                            }
                            else
                            {
                                ratingResponse.FinalScore = 30;
                                ratingResponse.RiskRating = 3;
                            }
                        }
                        else if (scaleType == ScaleType.FivePoint && ratingResponse.TotalScore < productMapping.ScaleRange5)
                        {
                            ratingResponse.FinalScore = 40;
                            ratingResponse.RiskRating = 4;
                        }
                        else if (scaleType == ScaleType.FivePoint && ratingResponse.TotalScore >= productMapping.ScaleRange5)
                        {
                            ratingResponse.FinalScore = 50;
                            ratingResponse.RiskRating = 5;
                        }
                    }
                    responses.RiskScoreProductVolumRatingResponses.Add(ratingResponse);

                    var uniqueCriterias = scoreRsponseList.Select(t => new { t.FactorId, t.SubFactorId, t.ProductId, t.CriteriaId }).ToList();
                    foreach (var criteria in uniqueCriterias)
                    {
                        var criteriaResponseSum = scoreRsponseList.Where(t => t.CriteriaId == criteria.CriteriaId)?.Sum(t => t.Answer);
                        if (criteriaResponseSum > 0)
                            responses.RiskScoreResponses.Add(new()
                            {
                                RiskFactorId = criteria.FactorId,
                                RiskSubFactorId = criteria.SubFactorId,
                                ProductId = criteria.ProductId,
                                RiskCriteriaId = criteria.CriteriaId,
                                Score = criteriaResponseSum,
                                QuestionIds = string.Join(",", scoreRsponseList.Where(t => t.CriteriaId == criteria.CriteriaId).Select(t => t.QuestionId).ToList()),
                                Answers = string.Join(",", scoreRsponseList.Where(t => t.CriteriaId == criteria.CriteriaId).Select(t => t.Answer).ToList()),
                            });
                    }
                }
            }
            var uniqueSubFactorsProductRating = uniqueProductSubFactors.Select(t => new { t.FactorId, t.SubFactorId }).Distinct();
            foreach (var subFactorRating in uniqueSubFactorsProductRating)
            {
                var subFactor = riskSubFactors.Where(t => t.RiskFactorId == subFactorRating.FactorId && t.Id == subFactorRating.SubFactorId).FirstOrDefault();
                if (subFactor != null)
                {
                    decimal totalProductVolume = responses.RiskScoreProductVolumRatingResponses.Where(t => t.RiskFactorId == subFactorRating.FactorId && t.Id == subFactorRating.SubFactorId && t.Volume.HasValue)
                        .Sum(x => x.Volume!.Value);
                    decimal totalHighVolume = 0.00M;
                    if (scaleType == ScaleType.ThreePoint)
                    {
                        totalHighVolume = responses.RiskScoreProductVolumRatingResponses.Where(t => t.RiskFactorId == subFactorRating.FactorId && t.Id == subFactorRating.SubFactorId && t.Volume.HasValue && t.RiskRating == 3)
                             .Sum(x => x.Volume!.Value);
                    }
                    else if (scaleType == ScaleType.FourPoint)
                    {
                        totalHighVolume = responses.RiskScoreProductVolumRatingResponses.Where(t => t.RiskFactorId == subFactorRating.FactorId && t.Id == subFactorRating.SubFactorId && t.Volume.HasValue && t.RiskRating == 4)
                             .Sum(x => x.Volume!.Value);
                    }
                    else if (scaleType == ScaleType.FivePoint)
                    {
                        totalHighVolume = responses.RiskScoreProductVolumRatingResponses.Where(t => t.RiskFactorId == subFactorRating.FactorId && t.Id == subFactorRating.SubFactorId && t.Volume.HasValue && t.RiskRating == 5)
                             .Sum(x => x.Volume!.Value);
                    }

                    decimal totalWeightedScore = totalProductVolume == 0 ? 0 : (totalHighVolume * 100 / totalProductVolume);
                    if (totalWeightedScore > 0)
                    {
                        RiskSubFactorResponseViewModel subFactorResponse = new()
                        {

                            RiskFactorId = subFactorRating.FactorId,
                            RiskSubFactorId = subFactorRating.SubFactorId,
                            Response = totalWeightedScore,
                            ResponseDescription = request.SubFactorResponses.Where(t => t.FactorId == subFactorRating.FactorId && t.SubFactorId == subFactorRating.SubFactorId).FirstOrDefault()?.Comments,
                            Score = GetScoreRating(scaleType, subFactor, Convert.ToString(totalWeightedScore))
                        };
                        responses.RiskSubFactorResponses.Add(subFactorResponse);
                    }
                }
            }
            #endregion
            //Other subfactor response
            foreach (var subFactorResponse in request.SubFactorResponses)
            {
                if (string.IsNullOrWhiteSpace(subFactorResponse.Response) && string.IsNullOrWhiteSpace(subFactorResponse.Comments))
                    continue;
                if (responses.RiskSubFactorResponses.Any(t => t.RiskFactorId == subFactorResponse.FactorId && t.RiskSubFactorId == subFactorResponse.SubFactorId))
                    continue;
                var subFactor = riskSubFactors.Where(t => t.RiskFactorId == subFactorResponse.FactorId && t.Id == subFactorResponse.SubFactorId).FirstOrDefault();
                if (subFactor == null)
                    continue;
                if (!string.IsNullOrWhiteSpace(subFactorResponse.Response))
                {
                    RiskSubFactorResponseViewModel subFactorResponseItem = new()
                    {

                        RiskFactorId = subFactorResponse.FactorId,
                        RiskSubFactorId = subFactorResponse.SubFactorId,
                        ResponseDescription = subFactorResponse.Comments,
                        Score = GetScoreRating(scaleType, subFactor, subFactorResponse.Response)
                    };
                    if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.PreDefinedParameters)
                    {
                        subFactorResponseItem.PreDefinedParameterId = Convert.ToInt32(subFactorResponse.Response);
                    }
                    else if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.Descriptive)
                    {
                        subFactorResponseItem.ResponseDescription = subFactorResponse.Response;
                    }
                    else if (decimal.TryParse(subFactorResponse.Response, out decimal dResponse))
                    {
                        subFactorResponseItem.Response = dResponse;
                    }
                    responses.RiskSubFactorResponses.Add(subFactorResponseItem);
                }
            }

            var uniqueFactorResponses = responses.RiskSubFactorResponses.Select(t => t.RiskFactorId).Distinct().ToList();
            foreach (var factorResponse in uniqueFactorResponses)
            {
                var factor = riskFactors.Where(t => t.Id == factorResponse).FirstOrDefault();
                if (factor == null)
                    continue;
                var totalWeight = responses.RiskSubFactorResponses.Where(t => t.RiskFactorId == factorResponse && t.Score.HasValue)
                    .Sum(x => x.Score!.Value);
                if (totalWeight <= 0) continue;
                responses.RiskFactorResponses.Add(new()
                {
                    RiskFactorId = factorResponse,
                    TotalWeightedScore = totalWeight
                });
            }

            if (!responses.RiskFactorResponses.Any() && !responses.RiskSubFactorResponses.Any()
                && !responses.RiskScoreProductVolumRatingResponses.Any() && !responses.RiskScoreResponses.Any()
                && !responses.VolumeMappings.Any())
                return -2;

            var updateResult = await UpdateAssessmentResponse(customerId, responses, riskFactors.Select(t => t.Id).ToList(), userId);
            if (updateResult)
                return 1;
            return 0;
        }

        private int GetScoreRating(ScaleType scaleType, RiskSubFactorViewModel subFactor, string? response)
        {
            if (string.IsNullOrWhiteSpace(response))
                return 1;
            decimal value = 0.00M;
            decimal s2 = 1000.00M;
            decimal s3 = 1000.00M;
            decimal s4 = 1000.00M;
            decimal s5 = 1000.00M;
            if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.PercentRange)
            {
                if (!decimal.TryParse(response, out value))
                    return 1;
                s2 = subFactor.Percentage2 ?? s2;
                s3 = subFactor.Percentage3 ?? s3;
                s4 = subFactor.Percentage4 ?? s4;
                s5 = subFactor.Percentage5 ?? s5;
            }
            else if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.PreDefinedParameters)
            {
                if (Convert.ToString(subFactor.PreDefinedParameter1Id) == response)
                    return 1;
                if (Convert.ToString(subFactor.PreDefinedParameter2Id) == response)
                    return 2;
                if (Convert.ToString(subFactor.PreDefinedParameter3Id) == response)
                    return 3;
                if (scaleType == ScaleType.FourPoint || scaleType == ScaleType.FivePoint)
                    if (Convert.ToString(subFactor.PreDefinedParameter4Id) == response)
                        return 4;
                if (scaleType == ScaleType.FivePoint)
                    if (Convert.ToString(subFactor.PreDefinedParameter5Id) == response)
                        return 5;
                return 1;
            }
            else if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.Descriptive)
            {
                if (subFactor.RiskDescription1 == response)
                    return 1;
                if (subFactor.RiskDescription2 == response)
                    return 2;
                if (subFactor.RiskDescription3 == response)
                    return 3;
                if (scaleType == ScaleType.FourPoint || scaleType == ScaleType.FivePoint)
                    if (subFactor.RiskDescription4 == response)
                        return 4;
                if (scaleType == ScaleType.FivePoint)
                    if (subFactor.RiskDescription5 == response)
                        return 5;
                return 1;
            }
            else if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.Scale)
            {
                if (!decimal.TryParse(response, out value))
                    return 1;
                s2 = 2;
                s3 = 3;
                s4 = 4;
                s5 = 5;
            }
            else if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.Volume)
            {
                if (!decimal.TryParse(response, out value))
                    return 1;
                s2 = subFactor.RiskVolume2 ?? s2;
                s3 = subFactor.RiskVolume3 ?? s3;
                s4 = subFactor.RiskVolume4 ?? s4;
                s5 = subFactor.RiskVolume5 ?? s5;
            }
            else if (subFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.NumberRange)
            {
                if (!decimal.TryParse(response, out value))
                    return 1;
                s2 = subFactor.Number2 ?? s2;
                s3 = subFactor.Number3 ?? s3;
                s4 = subFactor.Number4 ?? s4;
                s5 = subFactor.Number5 ?? s5;
            }

            if (value < s2)
                return 1;
            else if (value < s3)
                return 2;
            else if (scaleType == ScaleType.ThreePoint)
            {
                if (value >= s3)
                    return 3;
                else
                    return 2;
            }
            else if ((scaleType == ScaleType.FourPoint || scaleType == ScaleType.FivePoint) && value < s4)
            {
                return 3;
            }
            else if (scaleType == ScaleType.FourPoint)
            {
                if (value >= s4)
                    return 4;
                else
                    return 3;
            }
            else if (scaleType == ScaleType.FivePoint && value < s5)
            {
                return 4;
            }
            else if (scaleType == ScaleType.FivePoint && value >= s5)
            {
                return 5;
            }
            return 1;
        }

        public void SubFactorTempFileAdd(RiskSubFactorAttachmentViewModel model)
        {
            _repository.SubFactorTempFileAdd(_mapper.Map<RiskSubFactorAttachment>(model));
        }

        public Task<bool> SubmitApprovalRequest(ApprovalRequestViewModel modeldata, int userId)
        {
            var itemsToBeUpdated = false;

            var returnVal = _repository.SubmitApprovalRequest(_mapper.Map<ApprovalRequest>(modeldata));
            foreach (var response in modeldata.ApprovalHistory)
            {
                response.ApprovalId = returnVal.Id;
                response.Sequence = returnVal.Sequence;
                response.PendingWithUser = returnVal.PendingWithUser;
                _repository.SubmitApprovalRemark(_mapper.Map<ApprovalHistory>(response));
                itemsToBeUpdated = true;
            }
            return Task.FromResult(itemsToBeUpdated);

        }

        public async Task<List<ApprovalRequestViewModel>> GetApprovalStatusData(int customerId, int stageId, int? riskTypeId, int? geoPresenceId, int? customerSegmentId, int? businessSegmentId)
        {
            var list = await _repository.GetApprovalStatusData(customerId, stageId, riskTypeId, geoPresenceId, customerSegmentId, businessSegmentId);
            return _mapper.Map<List<ApprovalRequestViewModel>>(list);
        }

        public async Task<bool> SaveApprovedResponse(int customerId, List<int> riskFactorIds, List<int> risksubFactorIds, CustomerVersionMaster versionMaster)
        {
            var approvalId = _riskFactorResponseRepository.SubmitVersionMaster(versionMaster);
            var result = await _riskFactorResponseRepository.SubmitApprovedRiskFactorResponses(customerId, approvalId, riskFactorIds, risksubFactorIds);
            return result;
        }

        public async Task<List<CustomerVersionMasterViewModel>> GetApprovedVersion(int customerid)
        {
            var list = await _repository.GetApprovedVersion(customerid);
            return _mapper.Map<List<CustomerVersionMasterViewModel>>(list);
        }
        public async Task<List<UserViewModel>> GetApproverList(int customerid)
        {
            var list = await _repository.GetApproverList(customerid);
            return _mapper.Map<List<UserViewModel>>(list);
        }

        public async Task<List<RiskFactorResponseViewModel>> GetApprovedRiskFactorResponse(int customerId, List<int> riskFactorIds, int VersionId)
        {
            var list = await _riskFactorResponseRepository.GetVersionRiskFactorResponse(customerId, riskFactorIds, VersionId);
            return _mapper.Map<List<RiskFactorResponseViewModel>>(list);
        }
        public bool GetApprovalCompletion(int customerId)
        {
            var ds =  _riskFactorResponseRepository.GetApprovalCompletion(customerId);
            var isCompleted = false;
            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var result = ds.Tables[0].Rows[0]["Result"].ToString();
                if(result == "True")
                    isCompleted = true;
                else
                    isCompleted = false;
            }
            return isCompleted;
        }

        public async Task<List<RiskScoreResponseViewModel>> GetApprovedRiskScoreResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            var list = await _riskScoreResponseRepository.GetApprovedRiskScoreResponse(customerId, versionId, riskFactorIds);
            return _mapper.Map<List<RiskScoreResponseViewModel>>(list);
        }

        public async Task<List<RiskFactorResponseViewModel>> GetApprovedRiskFactorResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            var list = await _riskFactorResponseRepository.GetApprovedRiskFactorResponse(customerId, versionId, riskFactorIds);
            return _mapper.Map<List<RiskFactorResponseViewModel>>(list);
        }

        public async Task<List<RiskSubFactorResponseViewModel>> GetApprovedRiskSubFactorResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            var list = await _riskSubFactorResponseRepository.GetApprovedRiskSubFactorResponse(customerId, versionId, riskFactorIds);
            return _mapper.Map<List<RiskSubFactorResponseViewModel>>(list);
        }

        public async Task<List<RiskSubFactorVolumeResponseViewModel>> GetApprovedRiskSubFactorVolumeResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            var list = await _riskSubFactorVolumeResponseRepository.GetApprovedRiskSubFactorVolumeResponse(customerId, versionId, riskFactorIds);
            return _mapper.Map<List<RiskSubFactorVolumeResponseViewModel>>(list);
        }

        public async Task<List<RiskScoreProductVolumRatingResponseViewModel>> GetApprovedRiskScoreProductVolumRatingResponse(int customerId, int versionId, List<int> riskFactorIds)
        {
            var list = await _riskScoreProductVolumRatingResponseRepository.GetApprovedRiskScoreProductVolumRatingResponse(customerId, versionId, riskFactorIds);
            return _mapper.Map<List<RiskScoreProductVolumRatingResponseViewModel>>(list);
        }
        public DataSet GetApprovedRiskCombination(int versionId)
        {
            return _repository.GetApprovedRiskCombination(versionId);
        }

    }
}
