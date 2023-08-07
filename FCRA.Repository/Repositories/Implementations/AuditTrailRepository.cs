using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.Helpers;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace FCRA.Repository.Repositories.Implementations
{
    internal class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly ApplicationDBContext _context;
        public AuditTrailRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<DataAuditTrail>> GetAuditTrail(int objectId, string objectname)
        {
            if (objectId != 0 && string.IsNullOrEmpty(objectname))
            {
                return await _context.DataAuditTrails.Where(t => t.DataObjectId == objectId && t.DataObject.Contains("Response")).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(objectname) && objectId == 0)
            {
                return await _context.DataAuditTrails.Where(t => t.DataObject == objectname).ToListAsync();
            }
            else
            {
                return await _context.DataAuditTrails.Where(t => t.DataObject == objectname && t.DataObjectId == objectId).ToListAsync();
            }
        }

        public async Task<List<DataAuditTrail>> GetObjectAuditTrail(string Objectname)
        {
            return await _context.DataAuditTrails.Where(t => t.DataObject == Objectname).ToListAsync();
        }

        public async Task<List<UserMaster>> GetUserList()
        {
            return await _context.UserMasters.ToListAsync();
        }
    }
}
