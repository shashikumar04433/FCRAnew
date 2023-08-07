using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace FCRA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class AuditTrailController : Controller
    {
        private readonly IAuditTrailManager _auditTrailManager;
        public AuditTrailController(IAuditTrailManager auditTrailManager)
        {
            _auditTrailManager = auditTrailManager;
        }
        [HttpPost]
        public async Task<IActionResult> GetAuditTrail(string objectname = "", int objectId = 0)
        {
            var list = new List<DataAuditTrailViewModel>();
            if (objectId != 0 && string.IsNullOrEmpty(objectname))
            {
                list = (await _auditTrailManager.GetAuditTrail(objectId, ""));
            }
            else if (!string.IsNullOrEmpty(objectname) && objectId == 0)
            {
                list = (await _auditTrailManager.GetAuditTrail(0, objectname));
            }
            else
            {
                list = (await _auditTrailManager.GetAuditTrail(objectId, objectname));
            }
            var userlist = (await _auditTrailManager.GetUserList());
            List<DataAuditTrailViewModel> auditTraillist = new();
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.NewValue) && !string.IsNullOrEmpty(item.OldValue))
                {
                    JObject newJson = JObject.Parse(item.NewValue!);
                    JObject oldJson = JObject.Parse(item.OldValue!);

                    var newProps = newJson.Properties().ToList();
                    var oldProps = oldJson.Properties().ToList();
                    var userlist1 = userlist.Where(x => x.Id == item.CreatedBy).FirstOrDefault();
                    var auditLog = (from existingProp in oldProps
                                    from modifiedProp in newProps
                                    where modifiedProp.Path.Equals(existingProp.Path) && modifiedProp.Path != "UpdatedOn" && modifiedProp.Path != "UpdatedBy"
                                    where !modifiedProp.Value.ToString().Equals(existingProp.Value.ToString())
                                    select new DataAuditTrailViewModel
                                    {
                                        DataObject = existingProp.Path,
                                        OldValue = existingProp.Value.ToString(),
                                        NewValue = modifiedProp.Value.ToString(),
                                        ActionType = item.ActionType,
                                        CreatedByName = userlist1!.Name,
                                        CreatedOn = item.CreatedOn
                                    }).ToList();
                    auditTraillist.AddRange(auditLog);
                }
            }
            return PartialView("_AuditTrailPartial", auditTraillist);
        }

        [HttpPost]
        public async Task<IActionResult> GetRiskAssessmentAuditTrail(int objectId)
        {
            var list = (await _auditTrailManager.GetAuditTrail(objectId, ""));
            var userlist = (await _auditTrailManager.GetUserList());
            List<DataAuditTrailViewModel> auditTraillist = new();
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.NewValue) && !string.IsNullOrEmpty(item.OldValue))
                {
                    JObject newJson = JObject.Parse(item.NewValue);
                    JObject oldJson = JObject.Parse(item.OldValue);

                    var newProps = newJson.Properties().ToList();
                    var oldProps = oldJson.Properties().ToList();
                    var userlist1 = userlist.Where(x => x.Id == item.CreatedBy).FirstOrDefault();
                    var auditLog = (from existingProp in oldProps
                                    from modifiedProp in newProps
                                    where modifiedProp.Path.Equals(existingProp.Path) && modifiedProp.Path != "UpdatedOn" && modifiedProp.Path != "UpdatedBy"
                                    where !modifiedProp.Value.ToString().Equals(existingProp.Value.ToString())
                                    select new DataAuditTrailViewModel
                                    {
                                        ClassName= item.DataObject,
                                        DataObject = existingProp.Path,
                                        OldValue = existingProp.Value.ToString(),
                                        NewValue = modifiedProp.Value.ToString(),
                                        ActionType = item.ActionType,
                                        CreatedByName = userlist1!.Name,
                                        CreatedOn = item.CreatedOn
                                    }).ToList();
                    auditTraillist.AddRange(auditLog);
                }
            }
            return PartialView("_RiskAssessmentAuditTrail", auditTraillist);
        }
    }
}
