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
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FCRA.Repository.Managers.Implementations
{
    internal class ApprovalMatrixManager : IApprovalMatrixManager
    {
        private readonly IApprovalMatrixRepository _repository;
        private readonly IMapper _mapper;
        public ApprovalMatrixManager(IApprovalMatrixRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ApprovalMatrixViewModel>> GetApprovalMatrixAccess(int customerId, int userId)
        {
            var ApprovalMatrix = await _repository.GetApprovalMatrixAccess(customerId, userId);
            return _mapper.Map<List<ApprovalMatrixViewModel>>(ApprovalMatrix);
        }

        public async Task<List<ApprovalMatrixViewModel>> GetApprovalMatrixViewModelAsync(RiskFactorViewModel model)
        {
            var ApprovalMatrix = await _repository.GetApprovalMatrixViewModelAsync(model);
            return _mapper.Map< List<ApprovalMatrixViewModel>>(ApprovalMatrix);
        }

        public DataSet GetApprovalStatus(int customerId, string status)
        {
            var approvalStatus = _repository.GetApprovalStatus(customerId, status);
            return approvalStatus;
        }

        public async Task<bool> SaveApprovalMatrix(List<ApprovalMatrixViewModel> model, int customerId, int userId)
        {
            var result = false;
            foreach (var response in model)
            {
                result = await _repository.SaveApprovalMatrix(_mapper.Map<ApprovalMatrix>(response), customerId, userId);
            }
            return result;
        }
    }
}
