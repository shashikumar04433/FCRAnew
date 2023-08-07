using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ExcelSupport;
using ExcelSupport.Style;
using ExcelSupport.Table;
using FCRA.Common;
using FCRA.Repository.Managers;
using FCRA.Utilities;
using FCRA.ViewModels;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Reports;
using FCRA.ViewModels.Responses;
using FCRA.Web.Controllers;
using FCRA.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskAssessmentVersion)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskAssessmentVersionController : BaseController
    {
        private readonly IRiskAssessmentManager _riskAssessmentManager;
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentManager;
        private readonly IMasterManagerCustomer<RiskFactorViewModel> _riskFactorManager;
        private readonly IProductServiceMappingManager _productServiceMappingManager;
        private readonly IProductRiskCriteriaMappingManager _riskCriteriaMappingManager;
        private readonly IMasterManagerCustomer<GeographyRiskViewModel> _geographyRiskManager;
        private readonly IWebHostEnvironment _environment;

        public static StorageSettings StorageSettings { get; set; } = new();

        public RiskAssessmentVersionController(IRiskAssessmentManager riskAssessmentManager, IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager
            , IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager
            , IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentManager
            , IMasterManagerCustomer<RiskFactorViewModel> riskFactorManager
            , IProductServiceMappingManager productServiceMappingManager, IProductRiskCriteriaMappingManager riskCriteriaMappingManager
            , IMasterManagerCustomer<GeographyRiskViewModel> geographyRiskManager, IWebHostEnvironment environment)
        {
            _riskAssessmentManager = riskAssessmentManager;
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentManager = businessSegmentManager;
            _riskFactorManager = riskFactorManager;
            _productServiceMappingManager = productServiceMappingManager;
            _riskCriteriaMappingManager = riskCriteriaMappingManager;
            _geographyRiskManager = geographyRiskManager;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var versionList = await _riskAssessmentManager.GetApprovedVersion(GetUserCustomerId());
            return View(versionList);
        }

        public IActionResult Download(string? versionId)
        {
            if (!int.TryParse(versionId?.Decrypt(), out int vId) || vId <= 0)
                return NotFound();

            IDictionary<string, List<int>> hiddenColumns = new Dictionary<string, List<int>>();

            var ds = GetRiskExcelTemplateFactorSummary("Summary", vId);
            foreach (DataTable item in ds.Tables)
            {
                hiddenColumns.Add(item.TableName, new() { 1, 2 });
            }


            var scaleType = (ScaleType)GetUserCustomerScale();
            var ApprovedfactorList = new List<DataSet>();
            var GetApprovedRiskCombination = _riskAssessmentManager.GetApprovedRiskCombination(vId);
            if (GetApprovedRiskCombination != null && GetApprovedRiskCombination.Tables.Count > 0)
            {
                foreach (DataRow dr in GetApprovedRiskCombination.Tables[0].Rows)
                {
                    int sId = 0;
                    int? tId, gId, cId, bId;
                    tId = gId = cId = bId = null;
                    if (int.TryParse(dr["StageId"].ToString(), out int sId1))
                        sId = sId1;
                    if (int.TryParse(dr["RiskTypeId"].ToString(), out int tId1))
                        tId = tId1;
                    if (int.TryParse(dr["GeographicPresenceId"].ToString(), out int gId1))
                        gId = gId1;
                    if (int.TryParse(dr["CustomerSegmentId"].ToString(), out int cId1))
                        cId = cId1;
                    if (int.TryParse(dr["BusinessSegmentId"].ToString(), out int bId1))
                        bId = bId1;
                    var model = GetRiskResponseModelsPills(sId, tId, gId, cId, bId);
                    DataSet dsRiskAssessment = new();
                    var fileName = GetExcelFileName(model.Result);
                    var dt = GetRiskExcelTemplateFactor(model.Result, fileName, out List<int> lockedRows);
                    dsRiskAssessment.Tables.Add(dt);

                    if (model.Result.ProductServiceMappings.Any())
                    {
                        var dt2 = GetRiskExcelTemplateProductSubFactor(model.Result);
                        if (dt2 != null)
                        {
                            dsRiskAssessment.Tables.Add(dt2);
                        }
                    }

                    if (model.Result.RiskFactors.Any(t => t.RiskRangeParameter == RiskRangeParameter.Volume))
                    {
                        var dt1 = GetRiskExcelTemplateVolumeSubFactor(model.Result, scaleType);
                        foreach (var tbl in dt1)
                        {
                            dsRiskAssessment.Tables.Add(tbl);
                        }

                    }
                    ApprovedfactorList.Add(dsRiskAssessment);
                }
            }


            var stream = ProcessExcel(ds, ApprovedfactorList, 0, false, false, hiddenColumns);
            return File(stream, "application/vnd.ms-excel", $"{"Risk Assessment"} {DateTime.UtcNow.UTCToIST():yyyy-MM-dd_HHmm}.xlsx");
        }

        private static MemoryStream ProcessExcel(DataSet ds, List<DataSet> datasets, int tableStyle, bool removeAcentChars, bool showFilters, IDictionary<string, List<int>> hideColumns = null)
        {
            MemoryStream stream = new MemoryStream();
            ExcelPackage package = new ExcelPackage(stream);
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("Summary");

            var iCount = 2;
            var tableCount = 0;
            List<int> imageColumsAll = new List<int>();


            foreach (DataTable dataTable in ds.Tables)
            {
                var totalRows = dataTable.Rows.Count;
                var totalCols = dataTable.Columns.Count;
                if (tableStyle > 0 || showFilters)
                    ws.Cells[iCount, 1].LoadFromDataTable(dataTable, true, (ExcelSupport.Table.TableStyles)tableStyle, removeAcentChars);
                else if (tableCount == ds.Tables.Count - 1)
                    ws.Cells[iCount + 1, 1].LoadFromDataTable(dataTable, false, removeAcentChars);
                else
                    ws.Cells[iCount, 1].LoadFromDataTable(dataTable, true, removeAcentChars);
                //Set header style
                if (tableStyle == 0)
                    using (var headerCells = ws.Cells[iCount, 1, iCount, totalCols])
                    {
                        var headerFont = headerCells.Style.Font;
                        headerFont.Bold = true;
                    }

                //Setting Title and last row property
                ws.Row(iCount - 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(iCount - 1).Style.Font.Bold = true;
                ws.Row(iCount + totalRows).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(iCount + totalRows).Style.Font.Bold = true;
                ws.Row(iCount - 1).Style.Font.Color.SetColor(System.Drawing.Color.White);
                ws.Cells[iCount - 1, 1, iCount - 1, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[iCount - 1, 1, iCount - 1, totalCols].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Black);

                ws.Cells[iCount - 1, 1].Value = dataTable.Rows[0]["Stage"];
                ws.Cells[iCount - 1, 1, iCount - 1, totalCols].Merge = true;

                if (tableCount < ds.Tables.Count - 1)
                {
                    var hasImage = dataTable.Columns.Cast<DataColumn>().Any(t => t.ColumnName.Contains("Image", StringComparison.InvariantCultureIgnoreCase));
                    ws.Cells[iCount + totalRows, 1].Value = "Aggregate " + dataTable.Rows[0]["Stage"] + " Rating";
                    ws.Cells[iCount + totalRows, 1, iCount + totalRows, totalCols - 1].Merge = true;

                    var row = iCount + 1;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        using (ExcelRange Rng = ws.Cells[row, totalCols - 1, row, totalCols - 1])
                        {
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor((System.Drawing.Color)dr["Color"]);
                        }
                        if (totalRows == row - iCount)
                        {
                            ws.Cells[row, totalCols, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[row, totalCols, row, totalCols].Style.Fill.BackgroundColor.SetColor((System.Drawing.Color)dr["Color"]);
                        }
                        row++;
                    }

                    //Set all cells border
                    using (var allCells = ws.Cells[iCount, 1, iCount + totalRows, totalCols])
                    {
                        if (tableStyle == 0)
                        {
                            var border = allCells.Style.Border;
                            border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        var dateColumns = GetDateColumns(dataTable);
                        //format date field 
                        dateColumns.ForEach(item => allCells[iCount, item, iCount + totalRows, item].Style.Numberformat.Format = "yyyy/MM/dd HH:mm");
                        //Format hyperlinks
                        FormatHyperLink(dataTable, allCells, iCount + 1, iCount + totalRows);
                        if (hasImage)
                        {
                            ImplementImage(ws, dataTable, allCells, iCount + 1, iCount + totalRows, out List<int> imageColums);
                            imageColumsAll.AddRange(imageColums);
                        }
                    }
                    if (tableStyle > 0 || showFilters)
                    {
                        ExcelTable table = ws.Tables[tableCount];
                        table.ShowFilter = showFilters;
                    }
                    ws.Cells.AutoFitColumns();

                    if (hideColumns != null && hideColumns.Count > 0)
                    {
                        if (hideColumns.ContainsKey(dataTable.TableName))
                        {
                            var dic = hideColumns.Where(t => t.Key == dataTable.TableName).First();
                            foreach (var item in dic.Value)
                            {
                                ws.Column(item).Hidden = true;
                            }
                        }
                    }


                    if (hasImage)
                    {
                        var rowHeight = PixelHeightToExcel(75);
                        var colWidth = PixelWidthToExcel(100);
                        for (int i = iCount + 1; i <= iCount + totalRows; i++)
                        {
                            ws.Row(i).Height = rowHeight;
                        }
                        foreach (var columnIndex in imageColumsAll)
                        {
                            ws.Column(columnIndex).Width = colWidth;
                        }
                    }
                }
                else if (tableCount == ds.Tables.Count - 1)
                {

                    ws.Cells[iCount + totalRows, 1].Value = "Residual AML Risk Rating";
                    //ws.Cells[iCount + totalRows, 1, iCount + totalRows, totalCols - 1].Merge = true;

                    var row = iCount + 1;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        using (ExcelRange Rng = ws.Cells[row, totalCols - 3, row, totalCols - 1])
                        {
                            Rng.Merge = true;
                        }
                        using (ExcelRange Rng = ws.Cells[row, totalCols, row, totalCols])
                        {
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor((System.Drawing.Color)dr["Color"]);
                        }
                        row++;
                    }

                    //Set all cells border
                    using (var allCells = ws.Cells[iCount, 1, iCount + totalRows, totalCols])
                    {
                        if (tableStyle == 0)
                        {
                            var border = allCells.Style.Border;
                            border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        var dateColumns = GetDateColumns(dataTable);
                        //format date field 
                        dateColumns.ForEach(item => allCells[iCount, item, iCount + totalRows, item].Style.Numberformat.Format = "yyyy/MM/dd HH:mm");
                        //Format hyperlinks
                        FormatHyperLink(dataTable, allCells, iCount + 1, iCount + totalRows);
                    }
                    if (tableStyle > 0 || showFilters)
                    {
                        ExcelTable table = ws.Tables[tableCount];
                        table.ShowFilter = showFilters;
                    }
                    ws.Cells.AutoFitColumns();

                    if (hideColumns != null && hideColumns.Count > 0)
                    {
                        if (hideColumns.ContainsKey(dataTable.TableName))
                        {
                            var dic = hideColumns.Where(t => t.Key == dataTable.TableName).First();
                            foreach (var item in dic.Value)
                            {
                                ws.Column(item).Hidden = true;
                            }
                        }
                    }
                }

                iCount += dataTable.Rows.Count + 3;
                tableCount += 1;
            }

            var prefix = 1;
            foreach (var dts in datasets)
            {
                var prefixcount = 1;
                foreach (DataTable dataTable in dts.Tables)
                {
                    ws = package.Workbook.Worksheets.Add(prefix + "." + prefixcount + " " + dataTable.TableName);

                    iCount = 1;
                    var hasImage = dataTable.Columns.Cast<DataColumn>().Any(t => t.ColumnName.Contains("Image", StringComparison.InvariantCultureIgnoreCase));
                    var totalRows = dataTable.Rows.Count;
                    var totalCols = dataTable.Columns.Count;
                    if (tableStyle > 0 || showFilters)
                        ws.Cells[iCount, 1].LoadFromDataTable(dataTable, true, (ExcelSupport.Table.TableStyles)tableStyle, removeAcentChars);
                    else
                        ws.Cells[iCount, 1].LoadFromDataTable(dataTable, true, removeAcentChars);
                    //Set header style
                    if (tableStyle == 0)
                        using (var headerCells = ws.Cells[iCount, 1, iCount, totalCols])
                        {
                            var headerFont = headerCells.Style.Font;
                            headerFont.Bold = true;
                        }

                    //Set all cells border
                    using (var allCells = ws.Cells[iCount, 1, iCount + totalRows, totalCols])
                    {
                        if (tableStyle == 0)
                        {
                            var border = allCells.Style.Border;
                            border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        var dateColumns = GetDateColumns(dataTable);
                        //format date field 
                        dateColumns.ForEach(item => allCells[iCount, item, iCount + totalRows, item].Style.Numberformat.Format = "yyyy/MM/dd HH:mm");
                        //Format hyperlinks
                        FormatHyperLink(dataTable, allCells, iCount + 1, iCount + totalRows);
                        if (hasImage)
                        {
                            ImplementImage(ws, dataTable, allCells, iCount + 1, iCount + totalRows, out List<int> imageColums);
                            imageColumsAll.AddRange(imageColums);
                        }
                    }
                    if (tableStyle > 0 || showFilters)
                    {
                        ExcelTable table = ws.Tables[tableCount];
                        table.ShowFilter = showFilters;
                    }
                    ws.Cells.AutoFitColumns();
                    if (hideColumns != null && hideColumns.Count > 0)
                    {
                        if (hideColumns.ContainsKey(dataTable.TableName))
                        {
                            var dic = hideColumns.Where(t => t.Key == dataTable.TableName).First();
                            foreach (var item in dic.Value)
                            {
                                ws.Column(item).Hidden = true;
                            }
                        }
                    }
                    if (hasImage)
                    {
                        var rowHeight = PixelHeightToExcel(75);
                        var colWidth = PixelWidthToExcel(100);
                        for (int i = iCount + 1; i <= iCount + totalRows; i++)
                        {
                            ws.Row(i).Height = rowHeight;
                        }
                        foreach (var columnIndex in imageColumsAll)
                        {
                            ws.Column(columnIndex).Width = colWidth;
                        }
                    }
                    prefixcount++;
                }
                prefix++;
            }

            return new MemoryStream(package.GetAsByteArray());
        }


        private static List<int> GetDateColumns(DataTable dataTable)
        {
            List<int> dateColumns = new List<int>();
            //get the first indexer
            int datecolumn = 1;
            //loop through the object and get the list of datecolumns
            foreach (DataColumn column in dataTable.Columns)
            {
                //check if property is of DateTime type or nullable DateTime type
                if (column.DataType.Name.Contains("Date"))
                {
                    dateColumns.Add(datecolumn);
                }
                datecolumn++;
            }
            return dateColumns;
        }
        private static void FormatHyperLink(DataTable dataTable, ExcelRange allCells, int rowFrom, int totalRows)
        {
            List<int> hyperLinkColumns = new List<int>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow dr = dataTable.Rows[0];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var value = Convert.ToString(dr[i]);
                    if (!string.IsNullOrEmpty(value) && value.ToLower().Contains("hyperlink"))
                    {
                        hyperLinkColumns.Add(i + 1);
                    }
                }
            }

            if (hyperLinkColumns.Count > 0)
            {
                foreach (var item in hyperLinkColumns)
                {
                    for (int i = rowFrom; i <= totalRows; i++)
                    {
                        var value = Convert.ToString(allCells[i, item, i, item].Value);
                        if (!string.IsNullOrEmpty(value))
                        {
                            value = value.Replace("\"\"", "\"");
                            allCells[i, item, i, item].Formula = value;
                        }
                    }
                }
            }
        }
        private static void ImplementImage(ExcelWorksheet ws, DataTable dataTable, ExcelRange allCells, int rowFrom, int totalRows, out List<int> imageColums)
        {
            imageColums = new List<int>();
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                if (dataTable.Columns[j].ColumnName.Contains("image", StringComparison.InvariantCultureIgnoreCase))
                {
                    var columnIndex = j + 1;
                    for (int i = rowFrom; i <= totalRows; i++)
                    {
                        var value = Convert.ToString(allCells[i, columnIndex, i, columnIndex].Value);
                        if (!string.IsNullOrEmpty(value))
                        {
                            try
                            {
                                var imageStream = AzureOperations.GetFileStreamAzure(value, StorageSettings, true);
                                if (imageStream != null)
                                {
                                    var newImageStream = ImageUtility.ResizeImage(imageStream, 75, 100);
                                    var picture = ws.Drawings.AddPicture($"{value}_{columnIndex}_{i}", newImageStream, ExcelSupport.Drawing.ePictureType.Jpg);
                                    picture.SetPosition(i - 1, 0, columnIndex - 1, 0);
                                    picture.SetSize(100, 75);
                                }
                            }
                            catch (Exception) { }
                            allCells[i, columnIndex, i, columnIndex].Value = "";
                        }
                    }
                    imageColums.Add(columnIndex);
                }
            }
        }
        private static double PixelWidthToExcel(int pixels)
        {
            var tempWidth = pixels * 0.14099;
            var correction = (tempWidth / 100) * -1.30;
            return tempWidth - correction;
        }
        private static double PixelHeightToExcel(int pixels)
        {
            return pixels * 0.75;
        }

        private DataSet GetRiskExcelTemplateFactorSummary(string Name, int versionId)
        {
            DataSet ds = new();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add(new DataColumn("Stage", typeof(string)));
            dt.Columns.Add(new DataColumn("Color", typeof(System.Drawing.Color)));
            dt.Columns.Add(new DataColumn("Risk Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Category Weights", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Risk Score", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Weighted Score", typeof(string)));

            var data = GetSummaryFactors(versionId).Result;

            var stages = data?.Select(t => t.Stage).DistinctBy(t => t!.Id).OrderBy(t => t.Sequence).ToList();
            var totWeightages = 0.00M;

            foreach (var stage in stages!)
            {
                var uniquedata = data?.Where(t => t.StageName == stage!.Name);
                var totWeightage = 0.00M;
                List<int> lockedRows = new();
                //Subfactor
                DataTable dtFactor = new(Name + " for " + stage!.Name);
                dtFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
                dtFactor.Columns.Add(new DataColumn("Color", typeof(System.Drawing.Color)));
                dtFactor.Columns.Add(new DataColumn("Risk Category", typeof(string)));
                dtFactor.Columns.Add(new DataColumn("Category Weights", typeof(decimal)));
                dtFactor.Columns.Add(new DataColumn("Risk Score", typeof(decimal)));
                dtFactor.Columns.Add(new DataColumn("Weighted Score", typeof(decimal)));
                foreach (var factor in uniquedata!)
                {
                    var weightedScore = factor.WeightedScore * factor.WeightPercentage / 100;
                    DataRow dr = dtFactor.NewRow();
                    dr["Stage"] = factor!.StageName;
                    dr["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(factor.WeightedScore, stage.ScaleRange2, stage.ScaleRange3, stage.ScaleRange4, stage.ScaleRange5));
                    dr["Risk Category"] = factor!.Category;
                    dr["Category Weights"] = factor!.WeightPercentage;
                    dr["Risk Score"] = factor!.WeightedScore.ToTwoDecimal();
                    dr["Weighted Score"] = weightedScore.ToTwoDecimal();
                    dtFactor.Rows.Add(dr);
                    totWeightage = totWeightage + weightedScore;
                }

                DataRow drs = dtFactor.NewRow();
                drs["Stage"] = "";
                drs["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(totWeightage, stage.ScaleRange2, stage.ScaleRange3, stage.ScaleRange4, stage.ScaleRange5));
                drs["Risk Category"] = "Aggregate " + stage.Name + " Rating";
                drs["Category Weights"] = 0;
                drs["Risk Score"] = 0;
                drs["Weighted Score"] = totWeightage;
                dtFactor.Rows.Add(drs);

                DataRow dr1 = dt.NewRow();
                dr1["Stage"] = "Residual Risk Rating";
                dr1["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(totWeightage, stage.ScaleRange2, stage.ScaleRange3, stage.ScaleRange4, stage.ScaleRange5));
                dr1["Risk Category"] = "Aggregate " + stage.Name + " Rating";
                dr1["Category Weights"] = 0;
                dr1["Risk Score"] = 0;
                dr1["Weighted Score"] = Convert.ToString(totWeightage.ToTwoDecimal());
                dt.Rows.Add(dr1);

                totWeightages = totWeightages + totWeightage;

                ds.Tables.Add(dtFactor);
            }

            if (stages.Count > 1)
            {
                var rating = Utilitiy.GetScoreRating(totWeightages, stages[0]!.ScaleRange2, stages[0]!.ScaleRange3, stages[0]!.ScaleRange4, stages[0]!.ScaleRange5);
                var scaleType = (ScaleType)GetUserCustomerScale();

                DataRow dr2 = dt.NewRow();
                dr2["Stage"] = "Residual Risk Rating";
                dr2["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(totWeightages, stages[0]!.ScaleRange2, stages[0]!.ScaleRange3, stages[0]!.ScaleRange4, stages[0]!.ScaleRange5));
                dr2["Risk Category"] = "Residual AML Risk Rating";
                dr2["Category Weights"] = 0;
                dr2["Risk Score"] = 0;
                dr2["Weighted Score"] = @Utilitiy.GetRatingText(scaleType, rating);
                dt.Rows.Add(dr2);

                ds.Tables.Add(dt);
            }

            return ds;
        }

        private async Task<List<SummaryWeightViewModel>> GetSummaryFactors(int versionId)
        {
            var customerId = GetUserCustomerId();
            List<RiskFactorViewModel> riskFactors = new List<RiskFactorViewModel>();
            riskFactors = (await _riskFactorManager.GetAsync(customerId
                , new[] { "Stage", "RiskType", "GeographicPresence", "GeographicPresence.Country", "CustomerSegment", "BusinessSegment" }
                , t => t.IsActive))
                .OrderBy(t => t.Stage!.Sequence).ThenBy(t => t.Stage!.Name)
                .OrderBy(t => t.RiskType!.Sequence).ThenBy(t => t.RiskType!.Name)
                .OrderBy(t => t.GeographicPresence?.Sequence).ThenBy(t => t.GeographicPresence?.CountryName)
                .OrderBy(t => t.CustomerSegment?.Sequence).ThenBy(t => t.CustomerSegment?.Name)
                .OrderBy(t => t.BusinessSegment?.Sequence).ThenBy(t => t.BusinessSegment?.Name)
                .ThenBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            List<int> riskFactorIds = new();
            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetApprovedRiskFactorResponse(customerId, riskFactorIds, versionId);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in riskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }
            }
            List<SummaryWeightViewModel> factorList = new();
            var stages = riskFactors?.Select(t => t.Stage).DistinctBy(t => t!.Id).OrderBy(t => t.Sequence).ToList();
            foreach (var stage in stages!)
            {
                var uniqueFactors = riskFactors?.Where(t => t.StageId == stage!.Id).Select(t => t.Name).Distinct();

                var totalScore = 0.00M;
                foreach (var factorName in uniqueFactors!)
                {
                    var factorScores = riskFactors?.Where(t => t.StageId == stage!.Id && t.Name == factorName);
                    var weigthPercent = 0.00M;
                    var avgScore = 0.00M;
                    if (factorScores!.Any())
                    {
                        weigthPercent = factorScores!.First().WeightPercentage;
                        avgScore = factorScores!.Average(t => t.TotalWeightedScore);
                    }
                    factorList.Add(new()
                    {
                        Category = factorName,
                        WeightPercentage = weigthPercent,
                        WeightedScore = avgScore,
                        StageName = stage!.Name,
                        Stage = riskFactors?.Select(t => t.Stage).Where(t => t!.Id == factorScores!.FirstOrDefault()!.StageId).FirstOrDefault()
                    });
                }
                totalScore = factorList.Sum(t => t.WeightedScore);
            }

            return factorList;
        }


        private async Task<AssessmentPillsViewModel> GetRiskResponseModelsPills(int stageId, int? riskTypeId
            , int? geoPresenceId, int? customerSegmentId, int? businessSegmentId)
        {
            int customerId = GetUserCustomerId();
            AssessmentPillsViewModel model = new();

            if (stageId > 0)
                model.Stage = await _stageManager.GetAsync(customerId, stageId);
            if (riskTypeId.HasValue && riskTypeId.Value > 0)
                model.RiskType = await _riskTypeManager.GetAsync(customerId, riskTypeId.Value);
            if (geoPresenceId.HasValue && geoPresenceId.Value > 0)
                model.GeographicPresence = await _geographicPresenceManager.GetAsync(customerId, geoPresenceId.Value, new[] { nameof(GeographicPresenceViewModel.Country) });
            if (customerSegmentId.HasValue && customerSegmentId.Value > 0)
                model.CustomerSegment = await _customerSegmentManager.GetAsync(customerId, customerSegmentId.Value);
            if (businessSegmentId.HasValue && businessSegmentId.Value > 0)
                model.BusinessSegment = await _businessSegmentManager.GetAsync(customerId, businessSegmentId.Value);

            List<int> riskFactorIds = new();
            List<RiskFactorViewModel> riskFactors = (await _riskFactorManager.GetAsync(customerId, null
                , t => t.StageId == stageId && ((!riskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == riskTypeId))
                && ((!geoPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == geoPresenceId))
                && ((!customerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == customerSegmentId))
                && ((!businessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == businessSegmentId))))
                .OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            if (riskFactorIds.Any())
                riskFactors = riskFactors.Where(t => riskFactorIds.Contains(t.Id)).ToList();
            if (riskFactors != null && riskFactors.Any())
            {
                model.RiskFactors = riskFactors;
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(customerId, riskFactorIds);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in model.RiskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }

                //Sub Factor responses
                var subFactors = (await _riskAssessmentManager.GetRiskSubFactorsByRiskType(customerId, riskFactorIds))
                     .OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
                if (subFactors != null && subFactors.Any())
                {
                    model.RiskSubFactors = subFactors;
                    var subFactorResponses = await _riskAssessmentManager.GetRiskSubFactorResponse(customerId, riskFactorIds);
                    foreach (var response in subFactorResponses)
                    {
                        var itemSubFactor = model.RiskSubFactors.Where(t => t.RiskFactorId == response.RiskFactorId && t.Id == response.RiskSubFactorId).FirstOrDefault();
                        if (itemSubFactor != null)
                        {
                            itemSubFactor.Score = response.Score;
                            itemSubFactor.Assumptions = response.Assumptions;
                            if (itemSubFactor.RiskFactor.RiskRangeParameter == RiskRangeParameter.Scale)
                            {
                                if (response.Response.HasValue && response.Response.Value > 0)
                                    itemSubFactor.Response = Convert.ToInt32(response.Response);
                            }
                            else
                                itemSubFactor.Response = response.Response;
                            itemSubFactor.PreDefinedParameterId = response.PreDefinedParameterId;
                            itemSubFactor.ResponseDescription = response.ResponseDescription;
                            itemSubFactor.RiskRangeParameter = itemSubFactor.RiskFactor!.RiskRangeParameter;
                        }
                    }
                }
                //Sub Factor Volume mapping
                model.VolumeMappings = await _riskAssessmentManager.GetRiskSubFactorVolumeResponse(customerId, riskFactorIds);
                foreach (var subFactor in model.RiskSubFactors)
                {
                    if (subFactor.RiskFactor!.RiskRangeParameter == Common.RiskRangeParameter.Volume && !model.VolumeMappings.Any(t => t.RiskFactorId == subFactor.RiskFactorId && t.RiskSubFactorId == subFactor.Id))
                    {
                        model.VolumeMappings.Add(new()
                        {
                            RiskFactorId = subFactor.RiskFactorId,
                            RiskSubFactorId = subFactor.Id,
                            Score1 = subFactor.RiskVolume1,
                            Score2 = subFactor.RiskVolume2,
                            Score3 = subFactor.RiskVolume3,
                            Score4 = subFactor.RiskVolume4,
                            Score5 = subFactor.RiskVolume5
                        });
                    }
                }
            }
            var productServiceMapping = await _productServiceMappingManager.GetAsync(customerId, new[] { "ProductService" }, t => t.IsActive);
            if (productServiceMapping != null && productServiceMapping.Any())
            {
                model.ProductServiceMappings = productServiceMapping;
                var productVolumResponse = await _riskAssessmentManager.GetRiskScoreProductVolumRatingResponse(customerId, riskFactorIds);
                if (productVolumResponse != null && productVolumResponse.Any())
                {
                    foreach (var service in model.ProductServiceMappings)
                    {
                        var response = productVolumResponse.Where(t => t.RiskFactorId == service.RiskFactorId && t.RiskSubFactorId == service.RiskSubFactorId && t.ProductId == service.ProductId).FirstOrDefault();
                        if (response != null)
                        {
                            service.Volume = response.Volume;
                            service.TotalScore = response.TotalScore;
                            service.FinalScore = response.FinalScore;
                            service.RiskRating = response.RiskRating;
                            service.Value = response.Value;
                        }
                    }
                }
            }
            var riskCriteria = await _riskCriteriaMappingManager.GetAsync(customerId, new[] { "ProductService", "RiskCriteria" }, t => t.IsActive);
            if (riskCriteria != null && riskCriteria.Any())
            {
                var scores = await _riskAssessmentManager.GetRiskScoreResponse(customerId, riskFactorIds);
                foreach (var criteria in riskCriteria)
                {
                    var score = scores.Where(t => t.RiskFactorId == criteria.RiskFactorId && t.ProductId == criteria.ProductId && t.RiskCriteriaId == criteria.RiskCriteriaId).FirstOrDefault();
                    RiskScoreResponseViewModel tempScore = new()
                    {
                        RiskFactorId = criteria.RiskFactorId,
                        RiskSubFactorId = criteria.RiskSubFactorId,
                        ProductId = criteria.ProductId,
                        RiskCriteriaId = criteria.RiskCriteriaId,
                        RiskFactor = criteria.RiskFactor,
                        ProductService = criteria.ProductService,
                        RiskCriteria = criteria.RiskCriteria
                    };
                    if (score != null)
                    {
                        tempScore.Score = score.Score;
                        tempScore.QuestionIds = score.QuestionIds;
                        tempScore.Answers = score.Answers;
                    }
                    model.RiskCriteriaMappings.Add(tempScore);
                }
            }
            //Approval status
            var approvalStatus = (await _riskAssessmentManager.GetApprovalStatusData(customerId, stageId, riskTypeId, geoPresenceId, customerSegmentId, businessSegmentId)).OrderByDescending(t => t.Id).ToList();
            if (approvalStatus != null && approvalStatus.Any())
            {
                model.ApprovalRequests = approvalStatus;
            }
            var approverList = (await _riskAssessmentManager.GetApproverList(customerId));
            if (approverList != null && approverList.Any())
            {
                model.UserViewModels = approverList;
            }

            return model;
        }

        private string GetExcelFileName(AssessmentPillsViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.BusinessSegment?.Name))
                return model.BusinessSegment.Name;
            if (!string.IsNullOrWhiteSpace(model.CustomerSegment?.Name))
                return model.CustomerSegment.Name;
            if (!string.IsNullOrWhiteSpace(model.GeographicPresence?.CountryName))
                return model.GeographicPresence.CountryName;
            if (!string.IsNullOrWhiteSpace(model.RiskType?.Name))
                return model.RiskType.Name;
            if (!string.IsNullOrWhiteSpace(model.Stage?.Name))
                return model.Stage.Name;
            return string.Empty;
        }

        private DataTable GetRiskExcelTemplateFactor(AssessmentPillsViewModel model, string Name, out List<int> lockedRows)
        {
            lockedRows = new();
            //Subfactor
            DataTable dtSubFactor = new(Name);
            dtSubFactor.Columns.Add(new DataColumn("SL.No.", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("RegisterType", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("StageId", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("RiskTypeId", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("GeographicPresenceId", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("CustomerSegmentId", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("BusinessSegmentId", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("FactorId", typeof(int)));
            //dtSubFactor.Columns.Add(new DataColumn("SubFactorId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Risk Type", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Geographic Presence", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Business Segment", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Sub Unit", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Factor", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Sub Factor", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Description", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Response", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Comments", typeof(string)));
            var iIndex = 1;
            foreach (var factor in model.RiskFactors)
            {
                var hasProduct = model.ProductServiceMappings.Any(x => x.RiskFactorId == factor.Id);
                var hasVolume = factor.RiskRangeParameter == RiskRangeParameter.Volume;

                foreach (var subFactor in model.RiskSubFactors.Where(t => t.RiskFactorId == factor.Id))
                {
                    DataRow dr = dtSubFactor.NewRow();
                    dr["SL.No."] = iIndex;
                    //dr["RegisterType"] = 1;
                    //dr["StageId"] = factor!.StageId;
                    //dr["RiskTypeId"] = factor.RiskTypeId.HasValue ? factor?.RiskTypeId.Value : DBNull.Value;
                    //dr["GeographicPresenceId"] = factor.GeographicPresenceId.HasValue ? factor?.GeographicPresenceId.Value : DBNull.Value;
                    //dr["CustomerSegmentId"] = factor.CustomerSegmentId.HasValue ? factor?.CustomerSegmentId.Value : DBNull.Value;
                    //dr["BusinessSegmentId"] = factor.BusinessSegmentId.HasValue ? factor?.BusinessSegmentId.Value : DBNull.Value;
                    //dr["FactorId"] = subFactor.RiskFactorId;
                    //dr["SubFactorId"] = subFactor.Id;
                    dr["Stage"] = model.Stage?.Name;
                    dr["Risk Type"] = model.RiskType?.Name;
                    dr["Geographic Presence"] = model.GeographicPresence?.CountryName;
                    dr["Business Segment"] = model.CustomerSegment?.Name;
                    dr["Sub Unit"] = model.BusinessSegment?.Name;
                    dr["Factor"] = factor?.Name;
                    dr["Sub Factor"] = subFactor.Name;
                    dr["Description"] = subFactor.Description;
                    if (hasProduct || hasVolume || subFactor.IsExcludedInRisk)
                    {
                        lockedRows.Add(iIndex);
                        dr["Response"] = "NA";
                    }
                    else
                    {
                        if (factor.RiskRangeParameter == RiskRangeParameter.PercentRange)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.PreDefinedParameters)
                        {
                            dr["Response"] = subFactor.PreDefinedParameterId;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.Descriptive)
                        {
                            dr["Response"] = subFactor.ResponseDescription;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.NumberRange)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.Volume)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.Scale)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                    }
                    dr["Comments"] = subFactor.Assumptions;
                    dtSubFactor.Rows.Add(dr);
                    iIndex += 1;
                }
            }
            return dtSubFactor;
        }

        private DataTable? GetRiskExcelTemplateProductSubFactor(AssessmentPillsViewModel model)
        {
            List<int> riskFactorIds = model.RiskFactors.Select(t => t.Id).ToList();

            if (!model.ProductServiceMappings.Any(x => riskFactorIds.Contains(x.RiskFactorId)))
            {
                return null;
            }
            var questionList = _riskAssessmentManager.GetQuestionRiskCriteriaMapping(GetUserCustomerId(), riskFactorIds).Result;
            DataTable dtProduct = new() { TableName = $"Appendix 1 - PRA" };
            dtProduct.Columns.Add(new DataColumn("SL.No.", typeof(string)));
            //dtProduct.Columns.Add(new DataColumn("RegisterType", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("StageId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("RiskTypeId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("GeographicPresenceId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("CustomerSegmentId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("BusinessSegmentId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("FactorId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("SubFactorId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("ProductId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("CriteriaId", typeof(int)));
            //dtProduct.Columns.Add(new DataColumn("QuestionId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("Stage", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Risk Type", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Geographic Presence", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Business Segment", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Sub Unit", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Factor", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Sub Factor", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Product/ServiceCategory", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Value", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Criteria", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Question", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Purpose", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Answer", typeof(string)));
            var iIndex = 1;
            foreach (var factor in model.RiskFactors)
            {

                foreach (var product in model.ProductServiceMappings.Where(x => x.RiskFactorId == factor?.Id))
                {
                    foreach (var criteria in model.RiskCriteriaMappings.Where(t => t.RiskFactorId == product?.RiskFactorId && t.ProductId == product.ProductId))
                    {
                        List<int> answerQuestions = new();
                        List<int> answerAnswers = new();
                        if (!string.IsNullOrWhiteSpace(criteria.QuestionIds) && !string.IsNullOrWhiteSpace(criteria.Answers))
                        {
                            answerQuestions = criteria.QuestionIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x => Convert.ToInt32(x.Trim())).ToList();
                            answerAnswers = criteria.Answers.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x => Convert.ToInt32(x.Trim())).ToList();
                        }
                        foreach (var question in questionList.Where(t => t.RiskFactorId == factor.Id && t.RiskSubFactorId == product.RiskSubFactorId
                         && t.ProductId == product.ProductId && t.RiskCriteriaId == criteria.RiskCriteriaId))
                        {
                            DataRow dr = dtProduct.NewRow();
                            dr["SL.No."] = iIndex;
                            //dr["RegisterType"] = 3;
                            //dr["StageId"] = factor!.StageId;
                            //dr["RiskTypeId"] = factor.RiskTypeId.HasValue ? factor?.RiskTypeId.Value : DBNull.Value;
                            //dr["GeographicPresenceId"] = factor.GeographicPresenceId.HasValue ? factor?.GeographicPresenceId.Value : DBNull.Value;
                            //dr["CustomerSegmentId"] = factor.CustomerSegmentId.HasValue ? factor?.CustomerSegmentId.Value : DBNull.Value;
                            //dr["BusinessSegmentId"] = factor.BusinessSegmentId.HasValue ? factor?.BusinessSegmentId.Value : DBNull.Value;
                            //dr["FactorId"] = product.RiskFactorId;
                            //dr["SubFactorId"] = product.RiskSubFactorId;
                            //dr["ProductId"] = product?.ProductId;
                            //dr["CriteriaId"] = criteria.RiskCriteriaId;
                            //dr["QuestionId"] = question.QuestionId;
                            dr["Stage"] = model.Stage?.Name;
                            dr["Risk Type"] = model.RiskType?.Name;
                            dr["Geographic Presence"] = model.GeographicPresence?.CountryName;
                            dr["Business Segment"] = model.CustomerSegment?.Name;
                            dr["Sub Unit"] = model.BusinessSegment?.Name;
                            dr["Factor"] = factor?.Name;
                            dr["Sub Factor"] = model?.RiskSubFactors?.Where(x => x.Id == product.RiskSubFactorId).FirstOrDefault()?.Name;
                            dr["Product/ServiceCategory"] = product?.ProductServiceName;
                            dr["Value"] = product?.Volume;
                            dr["Criteria"] = criteria?.RiskCriteria?.Name;
                            dr["Question"] = question.Questions?.Name;
                            dr["Purpose"] = question.Questions?.Description;
                            dr["Answer"] = answerQuestions.Contains(question.QuestionId) ? answerAnswers[answerQuestions.IndexOf(question.QuestionId)] : DBNull.Value;
                            dtProduct.Rows.Add(dr);
                            iIndex += 1;
                        }
                    }


                }
            }
            return dtProduct;
        }

        private List<DataTable> GetRiskExcelTemplateVolumeSubFactor(AssessmentPillsViewModel model, ScaleType scaleType)
        {
            List<DataTable> dtList = new();
            var list = (_geographyRiskManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.IsActive)).Result.OrderBy(t => t.Country!.Name);
            foreach (var factor in model.RiskFactors)
            {
                var sheetIndex = 1;
                if (factor.RiskRangeParameter == RiskRangeParameter.Volume)
                {
                    foreach (var subFactor in model.RiskSubFactors.Where(x => x.RiskFactorId == factor?.Id))
                    {
                        var iIndex = 1;
                        DataTable dtSubFactor = new();
                        dtSubFactor.TableName = "Appendix 2- GRA" + Convert.ToString(sheetIndex);
                        dtSubFactor.Columns.Add(new DataColumn("SL.No.", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("RegisterType", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("StageId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("RiskTypeId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("GeographicPresenceId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("CustomerSegmentId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("BusinessSegmentId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("FactorId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("SubFactorId", typeof(int)));
                        //dtSubFactor.Columns.Add(new DataColumn("CountryId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Risk Type", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Geographic Presence", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Business Segment", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Sub Unit", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Factor", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Sub Factor", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Country", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Country Rating", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Volume", typeof(decimal)));

                        List<CountryVolumeResponseViewModel> countryList = new();
                        foreach (var vol in list)
                        {
                            countryList.Add(new()
                            {
                                Id = vol.Id,
                                Name = vol.Country?.Name,
                                RiskRating = vol.RiskRating
                            });
                        }
                        var volResp = model.VolumeMappings.Where(x => x.RiskFactorId == subFactor.RiskFactorId && x.RiskSubFactorId == subFactor.Id).FirstOrDefault();
                        if (volResp != null && !string.IsNullOrWhiteSpace(volResp?.Countries) && !string.IsNullOrWhiteSpace(volResp?.CountryWiseRating) && !string.IsNullOrWhiteSpace(volResp?.CountryWiseVolume))
                        {
                            List<int> cList = volResp.Countries.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                            List<int> ratingList = volResp.CountryWiseRating.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                            List<decimal> volumeList = volResp.CountryWiseVolume.Split(',').Select(t => Convert.ToDecimal(t)).ToList();
                            if (cList.Count == ratingList.Count && ratingList.Count == volumeList.Count)
                            {
                                for (int i = 0; i < cList.Count; i++)
                                {
                                    var country = countryList.Where(t => t.Id == cList[i]).FirstOrDefault();
                                    if (country != null)
                                    {
                                        country.RiskRating = (RiskRating)ratingList[i];
                                        country.Volume = volumeList[i];
                                    }
                                }
                            }
                        }
                        foreach (var cL in countryList)
                        {
                            DataRow dr = dtSubFactor.NewRow();
                            dr["SL.No."] = iIndex;
                            //dr["RegisterType"] = 2;
                            //dr["StageId"] = factor!.StageId;
                            //dr["RiskTypeId"] = factor.RiskTypeId.HasValue ? factor?.RiskTypeId.Value : DBNull.Value;
                            //dr["GeographicPresenceId"] = factor.GeographicPresenceId.HasValue ? factor?.GeographicPresenceId.Value : DBNull.Value;
                            //dr["CustomerSegmentId"] = factor.CustomerSegmentId.HasValue ? factor?.CustomerSegmentId.Value : DBNull.Value;
                            //dr["BusinessSegmentId"] = factor.BusinessSegmentId.HasValue ? factor?.BusinessSegmentId.Value : DBNull.Value;
                            //dr["FactorId"] = subFactor.RiskFactorId;
                            //dr["SubFactorId"] = subFactor.Id;
                            //dr["CountryId"] = cL?.Id;
                            dr["Stage"] = model.Stage?.Name;
                            dr["Risk Type"] = model.RiskType?.Name;
                            dr["Geographic Presence"] = model.GeographicPresence?.CountryName;
                            dr["Business Segment"] = model.CustomerSegment?.Name;
                            dr["Sub Unit"] = model.BusinessSegment?.Name;
                            dr["Factor"] = factor?.Name;
                            dr["Sub Factor"] = subFactor.Name;
                            dr["Country"] = cL.Name;
                            dr["Country Rating"] = Utilitiy.GetRatingText(scaleType, (int)cL.RiskRating);
                            dr["Volume"] = cL.Volume.HasValue ? cL.Volume : DBNull.Value;
                            dtSubFactor.Rows.Add(dr);
                            iIndex += 1;
                        }
                        sheetIndex += 1;
                        dtList.Add(dtSubFactor);
                    }
                    return dtList;
                }
            }
            return dtList;
        }

        public async Task SummaryVersionReports(string? versionId)
        {
            var vId = Convert.ToInt32(versionId?.Decrypt());
            var summaryReport = new List<SummaryWeightViewModel>();
            summaryReport = await GetSummaryFactors(vId);
            var ds = GetRiskExcelTemplateFactorSummary(summaryReport);
            var document = Path.Combine(_environment.WebRootPath, "WordTemplates\\Sample FCRA Report.docx");
            byte[] byteArray = System.IO.File.ReadAllBytes(document);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);
                var fileNameResult = Path.Combine(_environment.WebRootPath, "WordTemplates\\Risk Assessment Version.docx");
                await System.IO.File.WriteAllBytesAsync(fileNameResult, stream.ToArray());
                using (WordprocessingDocument doc = WordprocessingDocument.Open(fileNameResult, true))
                {
                    var tableCount = 1;
                    foreach (System.Data.DataTable tbl in ds.Tables)
                    {

                        var index = 1;
                        DocumentFormat.OpenXml.Wordprocessing.Table table = new DocumentFormat.OpenXml.Wordprocessing.Table();

                        TableProperties tblProp = new TableProperties(
                            new TableBorders(
                                new DocumentFormat.OpenXml.Wordprocessing.TopBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new DocumentFormat.OpenXml.Wordprocessing.BottomBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new DocumentFormat.OpenXml.Wordprocessing.LeftBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new DocumentFormat.OpenXml.Wordprocessing.RightBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new InsideHorizontalBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 10
                                },
                                new InsideVerticalBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 10
                                }
                            )
                        );

                        table.AppendChild<TableProperties>(tblProp);

                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            TableRow trHeader = new TableRow();
                            TableCell tcHeader1 = new TableCell();
                            TableCell tcHeader2 = new TableCell();
                            TableCell tcHeader3 = new TableCell();
                            TableCell tcHeader4 = new TableCell();
                            TableCellProperties tcProperties = new TableCellProperties();
                            tcProperties.Append(new HorizontalMerge() { Val = MergedCellValues.Restart });
                            TableCellProperties tcProperties1 = new TableCellProperties();
                            tcProperties1.Append(new HorizontalMerge() { });
                            TableCellProperties tcProperties2 = new TableCellProperties();
                            tcProperties2.Append(new HorizontalMerge() { });
                            TableCellProperties tcProperties3 = new TableCellProperties();
                            tcProperties3.Append(new HorizontalMerge() { });
                            tcHeader1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3600" }));
                            tcHeader1.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Bold(), new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(tbl.Rows[0]["Stage"]))))));
                            tcHeader2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcHeader2.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Text("")))));
                            tcHeader3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1800" }));
                            tcHeader3.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Text("")))));
                            tcHeader4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcHeader4.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Text("")))));
                            var shading1 = new Shading()
                            {
                                Color = "black",
                                Fill = "ABCDEF",
                                Val = ShadingPatternValues.Solid
                            };


                            tcProperties.Append(new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center });
                            tcProperties.Append(shading1);
                            tcHeader1.Append(tcProperties);
                            tcHeader2.Append(tcProperties1);
                            tcHeader3.Append(tcProperties2);
                            tcHeader4.Append(tcProperties3);
                            trHeader.Append(tcHeader1);
                            if (tableCount < ds.Tables.Count || ds.Tables.Count == 1)
                            {
                                trHeader.Append(tcHeader2);
                                trHeader.Append(tcHeader3);
                            }
                            trHeader.Append(tcHeader4);
                            table.Append(trHeader);
                        }

                        if (tableCount < ds.Tables.Count || ds.Tables.Count == 1)
                        {
                            TableRow tr = new TableRow();
                            TableCell tcH = new TableCell();
                            tcH.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3600" }));
                            tcH.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Bold(), new DocumentFormat.OpenXml.Wordprocessing.Text("Risk Category")))));
                            tr.Append(tcH);

                            TableCell tcH2 = new TableCell();
                            tcH2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcH2.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Bold(), new DocumentFormat.OpenXml.Wordprocessing.Text("Category Weights")))));
                            tr.Append(tcH2);

                            TableCell tcH3 = new TableCell();
                            tcH3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1800" }));
                            tcH3.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Bold(), new DocumentFormat.OpenXml.Wordprocessing.Text("Risk Score")))));
                            tr.Append(tcH3);

                            TableCell tcH4 = new TableCell();
                            tcH4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcH4.AppendChild(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.RunProperties(new DocumentFormat.OpenXml.Wordprocessing.Bold(), new DocumentFormat.OpenXml.Wordprocessing.Text("Weighted Score")))));
                            tr.Append(tcH4);

                            table.Append(tr);
                        }

                        if (tbl != null && tbl.Rows.Count > 0 && (tableCount < ds.Tables.Count || ds.Tables.Count == 1))
                        {
                            foreach (DataRow item in tbl.Rows)
                            {
                                TableRow tr2 = new TableRow();

                                TableCell tc2 = new TableCell();
                                tc2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3600" }));
                                tc2.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Risk Category"])))));


                                TableCell tc3 = new TableCell();
                                tc3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                                tc3.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Category Weights"])))));


                                TableCell tc4 = new TableCell();
                                tc4.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Risk Score"])))));
                                var tcp = new TableCellProperties(new TableCellWidth()
                                {
                                    Type = TableWidthUnitValues.Dxa,
                                    Width = "1800",
                                });
                                // Add cell shading.
                                var shading = new Shading()
                                {
                                    Color = Convert.ToString(item["Color"]),
                                    Fill = "ABCDEF",
                                    Val = ShadingPatternValues.Solid
                                };
                                tcp.Append(shading);
                                tc4.Append(tcp);

                                TableCell tc5 = new TableCell();
                                if (index == tbl.Rows.Count)
                                {
                                    TableCellProperties tcProp = new TableCellProperties();
                                    tcProp.Append(new HorizontalMerge() { Val = MergedCellValues.Restart });
                                    tc2.Append(tcProp);
                                    TableCellProperties tcProp1 = new TableCellProperties();
                                    tcProp1.Append(new HorizontalMerge() { });
                                    tc3.Append(tcProp1);
                                    TableCellProperties tcProp2 = new TableCellProperties();
                                    tcProp2.Append(new HorizontalMerge() { });
                                    tc4.Append(tcProp2);


                                    tc5.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Weighted Score"])))));
                                    var tcpro = new TableCellProperties(new TableCellWidth()
                                    {
                                        Type = TableWidthUnitValues.Dxa,
                                        Width = "1800",
                                    });
                                    var shading1 = new Shading()
                                    {
                                        Color = Convert.ToString(item["Color"]),
                                        Fill = "ABCDEF",
                                        Val = ShadingPatternValues.Solid
                                    };
                                    tcpro.Append(shading1);
                                    tc5.Append(tcpro);
                                    //tr2.Append(tc5);
                                }
                                else
                                {
                                    tc5.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                                    tc5.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Weighted Score"])))));
                                    //tr2.Append(tc5);
                                }
                                tr2.Append(tc2);
                                tr2.Append(tc3);
                                tr2.Append(tc4);
                                tr2.Append(tc5);

                                table.Append(tr2);
                                index++;
                            }
                        }
                        else if (tbl != null && tbl.Rows.Count > 0 && tableCount == ds.Tables.Count)
                        {
                            foreach (DataRow item in tbl.Rows)
                            {
                                TableRow tr2 = new TableRow();

                                TableCell tc2 = new TableCell();
                                tc2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "7800" }));
                                tc2.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Risk Category"])))));


                                TableCell tc3 = new TableCell();
                                tc3.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(Convert.ToString(item["Weighted Score"])))));
                                var tcpro = new TableCellProperties(new TableCellWidth()
                                {
                                    Type = TableWidthUnitValues.Dxa,
                                    Width = "2400",
                                });
                                var shading1 = new Shading()
                                {
                                    Color = Convert.ToString(item["Color"]),
                                    Fill = "ABCDEF",
                                    Val = ShadingPatternValues.Solid
                                };
                                tcpro.Append(shading1);
                                tc3.Append(tcpro);

                                tr2.Append(tc2);
                                tr2.Append(tc3);
                                table.Append(tr2);
                            }

                        }

                        var tblStr = table.ToString();
                        string tablePlaceholder = "{{summary}}";
                        DocumentFormat.OpenXml.Wordprocessing.Text tablePl = doc.MainDocumentPart.Document.Body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>().Where(x => x.Text.Contains(tablePlaceholder)).First();

                        if (tablePl != null)
                        {
                            var parent = tablePl.Parent.Parent.Parent;
                            parent.InsertAfter(table, tablePl.Parent.Parent);
                            tablePl.Text = tablePl.Text.Replace(tablePlaceholder, "");

                        }
                        tableCount++;
                    }

                    var body = doc.MainDocumentPart.Document.Body;
                    var paras = body.Elements<Paragraph>();

                    foreach (var para in paras)
                    {
                        foreach (var run in para.Elements<DocumentFormat.OpenXml.Wordprocessing.Run>())
                        {
                            foreach (var text in run.Elements<DocumentFormat.OpenXml.Wordprocessing.Text>())
                            {
                                if (text.Text.Contains("{{summary}}"))
                                {
                                    text.Text = text.Text.Replace("{{summary}}", "");
                                }
                            }
                        }
                    }
                    //doc.MainDocumentPart.Document.Save();
                    //doc.Close();

                }

            }
            await Task.CompletedTask;
        }


        private DataSet GetRiskExcelTemplateFactorSummary(List<SummaryWeightViewModel> data)
        {
            DataSet ds = new();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add(new DataColumn("Stage", typeof(string)));
            dt.Columns.Add(new DataColumn("Color", typeof(string)));
            dt.Columns.Add(new DataColumn("Risk Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Category Weights", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Risk Score", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Weighted Score", typeof(string)));

            var stages = data?.Select(t => t.Stage).DistinctBy(t => t!.Id).OrderBy(t => t.Sequence).ToList();
            var totWeightages = 0.00M;
            foreach (var stage in stages!)
            {
                var uniquedata = data?.Where(t => t.StageName == stage!.Name);
                var totWeightage = 0.00M;
                List<int> lockedRows = new();
                System.Data.DataTable dtFactor = new(stage!.Name);
                dtFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
                dtFactor.Columns.Add(new DataColumn("Color", typeof(string)));
                dtFactor.Columns.Add(new DataColumn("Risk Category", typeof(string)));
                dtFactor.Columns.Add(new DataColumn("Category Weights", typeof(decimal)));
                dtFactor.Columns.Add(new DataColumn("Risk Score", typeof(decimal)));
                dtFactor.Columns.Add(new DataColumn("Weighted Score", typeof(decimal)));
                foreach (var factor in uniquedata!)
                {
                    var weightedScore = factor.WeightedScore * factor.WeightPercentage / 100;
                    DataRow dr = dtFactor.NewRow();
                    dr["Stage"] = factor!.StageName;
                    dr["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(factor.WeightedScore, stage.ScaleRange2, stage.ScaleRange3, stage.ScaleRange4, stage.ScaleRange5)).Name;
                    dr["Risk Category"] = factor!.Category;
                    dr["Category Weights"] = factor!.WeightPercentage;
                    dr["Risk Score"] = factor!.WeightedScore.ToTwoDecimal();
                    dr["Weighted Score"] = weightedScore.ToTwoDecimal();
                    dtFactor.Rows.Add(dr);
                    totWeightage = totWeightage + weightedScore;
                }

                DataRow drs = dtFactor.NewRow();
                drs["Stage"] = "";
                drs["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(totWeightage, stage.ScaleRange2, stage.ScaleRange3, stage.ScaleRange4, stage.ScaleRange5)).Name;
                drs["Risk Category"] = "Aggregate " + stage.Name + " Rating";
                drs["Category Weights"] = 0;
                drs["Risk Score"] = 0;
                drs["Weighted Score"] = totWeightage.ToTwoDecimal();
                dtFactor.Rows.Add(drs);

                DataRow dr1 = dt.NewRow();
                dr1["Stage"] = "Residual Risk Rating";
                dr1["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(totWeightage, stage.ScaleRange2, stage.ScaleRange3, stage.ScaleRange4, stage.ScaleRange5)).Name;
                dr1["Risk Category"] = "Aggregate " + stage.Name + " Rating";
                dr1["Category Weights"] = 0;
                dr1["Risk Score"] = 0;
                dr1["Weighted Score"] = Convert.ToString(totWeightage.ToTwoDecimal());
                dt.Rows.Add(dr1);

                totWeightages = totWeightages + totWeightage;


                ds.Tables.Add(dtFactor);
            }
            if (stages.Count > 1)
            {
                var rating = Utilitiy.GetScoreRating(totWeightages, stages[0]!.ScaleRange2, stages[0]!.ScaleRange3, stages[0]!.ScaleRange4, stages[0]!.ScaleRange5);
                var scaleType = (ScaleType)GetUserCustomerScale();

                DataRow dr2 = dt.NewRow();
                dr2["Stage"] = "Residual Risk Rating";
                dr2["Color"] = Utilitiy.GetRatingColor(Utilitiy.GetScoreRating(totWeightages, stages[0]!.ScaleRange2, stages[0]!.ScaleRange3, stages[0]!.ScaleRange4, stages[0]!.ScaleRange5)).Name;
                dr2["Risk Category"] = "Residual AML Risk Rating";
                dr2["Category Weights"] = 0;
                dr2["Risk Score"] = 0;
                dr2["Weighted Score"] = @Utilitiy.GetRatingText(scaleType, rating);
                dt.Rows.Add(dr2);

                ds.Tables.Add(dt);
            }
            return ds;
        }

    }
}
