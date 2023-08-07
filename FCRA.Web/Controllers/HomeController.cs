using FCRA.Repository.Managers;
using FCRA.Utilities;
using FCRA.ViewModels;
using FCRA.ViewModels.Masters;
using FCRA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using System.Net;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using FCRA.ViewModels.Reports;
using NuGet.Versioning;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Charts;
using FCRA.Models.Masters;
using FCRA.Common;

namespace FCRA.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly StorageSettings _storageSettings;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IMasterManagerCustomer<RiskFactorViewModel> _riskFactorManager;
        IRiskAssessmentManager _riskAssessmentManager;

        public HomeController(
            //ILogger<HomeController> logger,
            IOptions<StorageSettings> options
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IWebHostEnvironment environment,
            IMasterManagerCustomer<RiskFactorViewModel> riskfactorManager,
            IRiskAssessmentManager riskAssessmentManager
            )
        {
            //_logger = logger;
            _storageSettings = options.Value;
            _riskTypeManager = riskTypeManager;
            _environment = environment;
            _riskFactorManager = riskfactorManager;
            _riskAssessmentManager = riskAssessmentManager;
        }

        public async Task<IActionResult> Index()
        {

            if (GetUserCustomerId() <= 0)
            {
                if (GetUserType() == 1)
                {
                    return Redirect("~/Auth/CustomerSelection");
                }
                return View("Index_Configuration_NotComplete");
            }
            var list = (await _riskTypeManager.GetAsync(GetUserCustomerId(), new[] { "Stage" }, t => t.IsActive))
                 .OrderBy(t => t.Stage!.Sequence).ThenBy(t => t.Stage!.Name).ThenBy(t => t.Sequence).ThenBy(t => t.Name);
            ViewBag.RiskTypes = list.Join(list.AsEnumerable()
                , a => a.Id
                , b => b.Id
                , (a, b) => new { a, b })
                .GroupBy(c => c.a.Name, d => d.b)
                .Select(e => new
               Select2Item
                {
                    id = string.Join(",", string.Join(",", list.Where(t => t.Name.Equals(e.Key, StringComparison.InvariantCultureIgnoreCase)).Select(f => Convert.ToString(f.Id))))
                ,
                    text = e.Key
                }).ToList();
            return View();
        }

        public async Task<IActionResult> DashboardUser()
        {
            await Task.CompletedTask;
            return PartialView("_UserDashboardPartial");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DownloadFile(string p, string fn, string i)
        {
            byte[] file;
            if (!string.IsNullOrWhiteSpace(i) && i.ToUpper() == "Y")
                file = AzureOperations.GetFileAzureProduct(p, _storageSettings);
            else
                file = AzureOperations.GetFileAzure(p, _storageSettings);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fn, out string? contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(file, contentType, fn);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult STF(string p, string fn, string i)
        {
            string filePath = p.Decrypt();
            string fileName = fn.Decrypt();
            string fileType = i.Decrypt();//Product container or not values Y/N
            if (string.IsNullOrWhiteSpace(filePath))
                return NotFound();
            if (string.IsNullOrWhiteSpace(fileName))
                return NotFound();

            byte[] file;
            if (!string.IsNullOrWhiteSpace(fileType) && fileType.ToUpper() == "Y")
                file = AzureOperations.GetFileAzureProduct(filePath, _storageSettings);
            else
                file = AzureOperations.GetFileAzure(filePath, _storageSettings);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string? contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(file, contentType, fileName);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UnauthorizedAccess()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ItemNotFound()
        {
            return View();
        }



        public async Task SummaryReports(string riskType, string group)
        {
            var summaryReport = new List<SummaryWeightViewModel>();
            summaryReport = await SummaryDetails(riskType, group);
            var ds = GetRiskExcelTemplateFactorSummary(summaryReport);
            var document = Path.Combine(_environment.WebRootPath, "WordTemplates\\Sample FCRA Report.docx");
            byte[] byteArray = System.IO.File.ReadAllBytes(document);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);
                var fileNameResult = Path.Combine(_environment.WebRootPath, "WordTemplates\\Sample FCRA Report Updated.docx");
                await System.IO.File.WriteAllBytesAsync(fileNameResult, stream.ToArray());
                using (WordprocessingDocument doc = WordprocessingDocument.Open(fileNameResult, true))
                {
                    var tableCount = 1;
                    foreach (System.Data.DataTable tbl in ds.Tables)
                    {

                        var index = 1;
                        Table table = new Table();

                        TableProperties tblProp = new TableProperties(
                            new TableBorders(
                                new TopBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new BottomBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new LeftBorder()
                                {
                                    Val =
                                    new EnumValue<BorderValues>(BorderValues.Single),
                                    Size = 15
                                },
                                new RightBorder()
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
                            tcHeader1.AppendChild(new Paragraph(new Run(new RunProperties(new Bold(), new Text(Convert.ToString(tbl.Rows[0]["Stage"]))))));
                            tcHeader2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcHeader2.AppendChild(new Paragraph(new Run(new RunProperties(new Text("")))));
                            tcHeader3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1800" }));
                            tcHeader3.AppendChild(new Paragraph(new Run(new RunProperties(new Text("")))));
                            tcHeader4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcHeader4.AppendChild(new Paragraph(new Run(new RunProperties(new Text("")))));
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
                            tcH.AppendChild(new Paragraph(new Run(new RunProperties(new Bold(), new Text("Risk Category")))));
                            tr.Append(tcH);

                            TableCell tcH2 = new TableCell();
                            tcH2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcH2.AppendChild(new Paragraph(new Run(new RunProperties(new Bold(), new Text("Category Weights")))));
                            tr.Append(tcH2);

                            TableCell tcH3 = new TableCell();
                            tcH3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1800" }));
                            tcH3.AppendChild(new Paragraph(new Run(new RunProperties(new Bold(), new Text("Risk Score")))));
                            tr.Append(tcH3);

                            TableCell tcH4 = new TableCell();
                            tcH4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tcH4.AppendChild(new Paragraph(new Run(new RunProperties(new Bold(), new Text("Weighted Score")))));
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
                                tc2.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Risk Category"])))));


                                TableCell tc3 = new TableCell();
                                tc3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                                tc3.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Category Weights"])))));


                                TableCell tc4 = new TableCell();
                                tc4.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Risk Score"])))));
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


                                    tc5.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Weighted Score"])))));
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
                                    tc5.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Weighted Score"])))));
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
                                tc2.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Risk Category"])))));


                                TableCell tc3 = new TableCell();
                                tc3.Append(new Paragraph(new Run(new Text(Convert.ToString(item["Weighted Score"])))));
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
                        Text tablePl = doc.MainDocumentPart.Document.Body.Descendants<Text>().Where(x => x.Text.Contains(tablePlaceholder)).First();

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
                        foreach (var run in para.Elements<Run>())
                        {
                            foreach (var text in run.Elements<Text>())
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


        public async Task<List<SummaryWeightViewModel>> SummaryDetails(string riskTpye, string group)
        {
            //ViewBag.ScaleType = GetUserCustomerScale();

            var riskTypeList = riskTpye.Decrypt().Split(',').Select(t => Convert.ToInt32(t)).ToList();
            var customerId = GetUserCustomerId();

            List<RiskFactorViewModel> riskFactors = (await _riskFactorManager.GetAsync(customerId
                , new[] { "Stage", "RiskType", "GeographicPresence", "GeographicPresence.Country", "CustomerSegment", "BusinessSegment" }
                , t => t.IsActive))
                .OrderBy(t => t.Stage!.Sequence).ThenBy(t => t.Stage!.Name)
                .OrderBy(t => t.RiskType!.Sequence).ThenBy(t => t.RiskType!.Name)
                .OrderBy(t => t.GeographicPresence?.Sequence).ThenBy(t => t.GeographicPresence?.CountryName)
                .OrderBy(t => t.CustomerSegment?.Sequence).ThenBy(t => t.CustomerSegment?.Name)
                .OrderBy(t => t.BusinessSegment?.Sequence).ThenBy(t => t.BusinessSegment?.Name)
                .ThenBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            riskFactors = riskFactors.Where(t => riskTypeList.Contains(t.RiskTypeId!.Value)).ToList();
            List<int> riskFactorIds = new();
            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(customerId, riskFactorIds);
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

            if (riskFactors != null && riskFactors.Any())
            {
                var stages = riskFactors.Select(t => t.Stage).DistinctBy(t => t!.Id).OrderBy(t => t.Sequence).ToList();
                if (stages != null && stages.Any())
                {
                    foreach (var stage in stages)
                    {
                        var uniqueFactors = riskFactors.Where(t => t.StageId == stage!.Id).Select(t => t.Name).Distinct();
                        var totalWeightedScoreIR = 0.00M;
                        var totalScore = 0.00M;
                        foreach (var factorName in uniqueFactors)
                        {
                            var factorScores = riskFactors.Where(t => t.StageId == stage.Id && t.Name == factorName);
                            var weigthPercent = 0.00M;
                            var avgScore = 0.00M;
                            var weightedScore = 0.00M;
                            if (factorScores.Any())
                            {
                                weigthPercent = factorScores.First().WeightPercentage;
                                avgScore = factorScores.Average(t => t.TotalWeightedScore);
                                weightedScore = avgScore * weigthPercent / 100;
                                totalWeightedScoreIR += weightedScore;
                            }
                            factorList.Add(new()
                            {
                                Category = factorName,
                                WeightPercentage = weigthPercent,
                                WeightedScore = avgScore,
                                ScaleRange1 = weightedScore,
                                StageName = stage!.Name,
                                Stage = riskFactors?.Select(t => t.Stage).Where(t => t!.Id == factorScores!.FirstOrDefault()!.StageId).FirstOrDefault()
                            });
                        }
                        totalScore = factorList.Sum(t => t.WeightedScore);
                    }
                }
            }
            return factorList;
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