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
    internal class AuditTrailManager : IAuditTrailManager
    {
        private readonly IAuditTrailRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<UserMaster> _userrepository;
        public AuditTrailManager(IAuditTrailRepository repository, IMapper mapper, IRepository<UserMaster> userrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userrepository = userrepository;
        }

        public async Task<List<DataAuditTrailViewModel>> GetAuditTrail(int objectId, string objectname)
        {
            var list = await _repository.GetAuditTrail(objectId, objectname);
            return _mapper.Map<List<DataAuditTrailViewModel>>(list);
        }

        public async Task<List<DataAuditTrailViewModel>> GetObjectAuditTrail(string Objectname)
        {
            var list = await _repository.GetObjectAuditTrail(Objectname);
            return _mapper.Map<List<DataAuditTrailViewModel>>(list);
        }

        public async Task<List<UserViewModel>> GetUserList()
        {
            var list = await _repository.GetUserList();
            return _mapper.Map<List<UserViewModel>>(list);
        }
    }
}
