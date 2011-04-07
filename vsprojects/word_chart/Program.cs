using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using S = DocumentFormat.OpenXml.Spreadsheet;

using System.Threading;

using RSMTenon.Graphing;
using RSMTenon.Data;

namespace RSMTenon.ReportGenerator
{
    class Program
    {
        static string path = @"C:\Documents and Settings\garsiden\My Documents\Projects\RepGen\test\chart\excel_data\";
        static string generated = path + "ChartDoc.docx";
        private static string sourceFile = path + "template.docx";
        private uint docPrId = 0U;
        private string contentSourceXml = @"../../App_Data/content.xml";


        static void Main(string[] args)
        {
            Program This = new Program();

            File.Copy(sourceFile, generated, true);
            Report report = new Report();
            WordprocessingDocument myWordDoc = WordprocessingDocument.Open(generated, true);
            MainDocumentPart mainPart = myWordDoc.MainDocumentPart;

            string ssTest = path + "GraphData.xlsx";
            MemoryStream ms = new MemoryStream();
            SpreadsheetDocument spreadsheetDoc = GraphData.GenerateDataSpreadsheet(ms);
            GraphData gd = new GraphData();
            gd.Add(spreadsheetDoc);


            spreadsheetDoc.Close();
            //copyStream(ms, ssTest);
            This.CreateReportTest(mainPart, report, ms);



            myWordDoc.Close();
            return;

            // create test Client and Report
            //Guid guid = new Guid("636c8103-e06d-4575-aafc-574474c2d7f8");
            //Client client = Client.GetClientByGUID(guid);
            //Report report = new Report() { Client = client };

            //// copy template to generated
            ////File.Copy(template, generated, true);
            //string tempDoc = createTempDocFile(sourceFile);

            //// open Word document
            //WordprocessingDocument myWordDoc = WordprocessingDocument.Open(tempDoc, true);
            //MainDocumentPart mainPart = myWordDoc.MainDocumentPart;

            ////This.InsertAllocationPie(mainPart);
            //This.CreateReport(mainPart, report);

            //myWordDoc.Close();
        }


        public void CreateReportTest(MainDocumentPart mainPart, Report report, Stream ms)
        {
            ChartItem chartItem = null;
            string controlName = null;

            // Test Graph
            chartItem = report.ExcelChart();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName, ms);

            // save and close document
            mainPart.Document.Save();
        }

        public void CreateReport(MainDocumentPart mainPart, Report report)
        {
            TextContent tc = new TextContent();
            tc.GenerateTextContent(mainPart, report.Client, contentSourceXml);

            ChartItem chartItem = null;
            string controlName = null;

            // Model Table
            Table table = report.ModelTable();
            controlName = "ModelTable";
            AddTableToDoc(mainPart, table, controlName);

            // Drawdown
            chartItem = report.Drawdown();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Comparison Chart
            if (report.Client.ExistingAssets) {
                chartItem = report.AllocationComparison();
                controlName = chartItem.CustomControlName;
                AddChartToDoc(mainPart, chartItem, controlName);
            }

            // Stress Test Market Rise Bar Chart
            chartItem = report.StressTestMarketRise();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Stress Test Market Crash Bar Chart
            chartItem = report.StressTestMarketCrash();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Allocation Pie Chart
            chartItem = report.Allocation();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Rolling Return 1 yr
            chartItem = report.RollingReturnChart(1);
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Rolling Return 3 yr
            chartItem = report.RollingReturnChart(3);
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Rolling Return 5 yr
            chartItem = report.RollingReturnChart(5);
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Ten Year Return Chart
            chartItem = report.TenYearReturn();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // save and close document
            mainPart.Document.Save();
        }

        private void TableDataTest()
        {

            //decimal amount = 0;

            var ctx = new RepGenDataContext();
            var models = ctx.Models;

            var model = from m in models
                        where m.StrategyID == "CO"
                        group m by m.AssetClassID
                            into g
                            select new //ModelTableData
                            {
                                AssetClassId = g.Key,
                                AssetClassName = g.First().AssetClass.Name,
                                Investments = g,
                                WeightingHNW = g.Sum(m => m.WeightingHNW)
                            };

            foreach (var g in model) {
                Console.WriteLine("{0}\t{1}", g.AssetClassId, g.WeightingHNW);
                foreach (var m in g.Investments) {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", m.AssetClass.Name, m.InvestmentName, m.WeightingHNW, m.ExpectedYield);
                }
            }

        }

        private void StressTest()
        {
            var ctx = new RepGenDataContext();

            var hd = ctx.HistoricPrice("UKGB").ToDictionary(d => d.Date);
            var last = hd.Last();

            // Ten Year Return
            var frm = hd.ElementAt(hd.Count() - 121);
            double retval = Math.Log(last.Value.Value / frm.Value.Value);

            // Bull Run Mar-03 to Mar-06

            int fromDate = (int)(new DateTime(2003, 3, 31).ToOADate());
            int toDate = (int)(new DateTime(2006, 3, 31).ToOADate());

            //var start = hd.Single(h => h.  .Date.Equals(fromDate-2));
            //var end = hd.Single(h => h.Date.Equals(toDate -2));
            var start = hd[fromDate - 2];
            var end = hd[toDate - 2];
            double bull = (end.Value - start.Value) / start.Value;

            // Model
            var returns = ctx.ModelReturn("CO", "HNW");
            var calc = new ReturnCalculation();
            int rn = 0;
            var prices = from p in returns
                         select new RankedReturnData {
                             RankNumber = rn++,
                             Date = p.Date,
                             Value = calc.Price(p)
                         };

            // Ten Year Return
            var pd = prices.ToDictionary(p => p.Date);
            var lastPrice = pd.Last();
            var fromMod = pd.ElementAt(pd.Count() - 121);
            double tyrMod = Math.Log(lastPrice.Value.Value / fromMod.Value.Value);

            // Bull
            int fromInt = ReturnData.IntegerDate(new DateTime(2003, 3, 31));
            int toInt = ReturnData.IntegerDate(new DateTime(2006, 3, 31));
            var startMod = pd[fromInt];
            var endMod = pd[toInt];
            double bullMod = (endMod.Value - startMod.Value) / startMod.Value;


        }
        private void RollingReturnTest()
        {
            var ctx = new RepGenDataContext();
            ReturnCalculation calc = new ReturnCalculation();

            // calculate prices
            //ctx.ModelReturn("CO");
            var data = ctx.ModelReturn("CO", "HNW");

            var prices = from d in data
                         select new ReturnData {
                             Value = calc.Price(d),
                             Date = d.Date
                         };

            var p1 = prices.ToList();
            p1.Insert(0, new ReturnData() { Value = 100D });
            var p2 = p1.Skip(12 * 3);
            var p3 = new List<ReturnData>();
            int i = 0;

            foreach (var item in p2) {
                var rd = new ReturnData {
                    Value = calc.RollingReturn(item, p1.ElementAt(i++)),
                    Date = item.Date
                };
                p3.Add(rd);
            }

            foreach (var item in p3) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
        }

        private void TenYearTest()
        {
            var ctx = new RepGenDataContext();
            var data = ctx.ModelReturn("CO", "HNW");
            //            var data = ctx.AssetClassReturn(new DateTime(1999, 9, 30), "GLEQ");

            ReturnCalculation calc = new ReturnCalculation();
            var match = from d in data
                        select new ReturnData {
                            Value = calc.RebaseReturn(d),
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
            Thread.Sleep(500000);
        }

        private void TenYearBenchTest()
        {
            var ctx = new RepGenDataContext();
            var prices = ctx.BenchmarkPrice("CAMA");

            ReturnCalculation cr = new ReturnCalculation();
            ReturnCalculation cp = new ReturnCalculation();

            var tyr = from p in prices
                      let rtrn = cr.Return(p)
                      select new ReturnData {
                          Value = cp.RebaseReturn(rtrn),
                          Date = p.Date
                      };
            foreach (var item in tyr) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }


        }
        private void PriceTest()
        {

            var ctx = new RepGenDataContext();
            //ctx.ModelReturn("CO");
            var data = ctx.ModelReturn("CO", "HNW");

            ReturnCalculation calc = new ReturnCalculation();
            var match = from d in data
                        select new ReturnData {
                            Value = calc.Price(d),
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
            Thread.Sleep(500000);

        }

        private void DrawdownTest3()
        {

            var ctx = new RepGenDataContext();
            var data = ctx.ModelReturn("CO", "HNW");

            ReturnCalculation calcPrice = new ReturnCalculation();
            ReturnCalculation calcDrawdown = new ReturnCalculation();

            var match = from d in data
                        let price = calcPrice.Price(d)
                        select new ReturnData {
                            Value = calcDrawdown.Drawdown(price, d.Value) - 1,
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
        }

        private void DrawdownTest2()
        {

            var ctx = new RepGenDataContext();
            var data = ctx.HistoricPrice("UKGB");

            ReturnCalculation calc = new ReturnCalculation();

            var match = from d in data
                        select new ReturnData {
                            Value = calc.Drawdown(d) - 1,
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
            //Thread.Sleep(500000);

        }
        public void InsertBarChartIntoWord(MainDocumentPart mainPart, string title)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, "Chart1");

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);

            AllocationComparisonBarChart bc = new AllocationComparisonBarChart();
            C.Chart chart = bc.GenerateChart(title, null);
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate Pie Chart and add to ChartSpace
            //StressBarChart bc = new StressBarChart();
            chartSpace.Append(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GraphDrawing.GenerateDrawing(relId, "Chart 1", 2U, Graph.Cx, Graph.Cy);
            para.Append(run);
            run.Append(drawing);

            // save and close document
            mainPart.Document.Save();

        }


        private Paragraph findAndRemoveContent(MainDocumentPart main, string blockName)
        {
            SdtBlock sdt = main.Document.Descendants<SdtBlock>().Where(
                 s => s.SdtProperties.GetFirstChild<SdtAlias>().Val.Value.Equals(blockName)).First();

            if (sdt != null) {
                Paragraph para = sdt.SdtContentBlock.GetFirstChild<Paragraph>();
                para.RemoveAllChildren();
                return para;
            }
            return null;
        }



        private void AddChartToDoc(MainDocumentPart mainPart, ChartItem chartItem, string controlName) { }


        private void AddChartToDoc(MainDocumentPart mainPart, ChartItem chartItem, string controlName, Stream ms)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, controlName);

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);
            string embeddedDataId = "rIdExcel1";
            ExcelGraph.AddEmbeddedToChartPart(chartPart, embeddedDataId, ms);

            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpaceWithData(chartItem.Chart, embeddedDataId);
            //chartSpace.Append(chartItem.Chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GraphDrawing.GenerateDrawing(relId, controlName, docPrId, Graph.Cx, Graph.Cy);
            docPrId++;
            para.Append(run);
            run.Append(drawing);
        }

        private void AddTableToDoc(MainDocumentPart mainPart, Table table, string controlName)
        {
            // open document and create table
            //WordprocessingDocument myDoc = WordprocessingDocument.Open(docName, true);
            //MainDocumentPart mainPart = myDoc.MainDocumentPart;
            Document doc = mainPart.Document;


            // create table to hold strategy details
            // add table to doc and save
            List<SdtBlock> stdList =
                mainPart.Document.Descendants<SdtBlock>()
                .Where(s => controlName
                .Contains
                (s.SdtProperties.GetFirstChild<SdtAlias>().Val.Value)).ToList();

            if (stdList.Count != 0) {
                SdtBlock sdt = stdList.First<SdtBlock>();
                OpenXmlElement parent = sdt.Parent;
                parent.InsertAfter(table, sdt);
                //sdt.Remove();
            }

            doc.Save();
        }

        private static string createTempDocFile(string sourceFile)
        {
            string tempDir = Environment.GetEnvironmentVariable("temp");

            string tempFile = String.Format("{0}\\{1}.dotx", tempDir, Guid.NewGuid().ToString());
            copyFile(sourceFile, tempFile);

            return tempFile;
        }

        public static void copyStream(Stream input, string destination)
        {
            using (input) {
                input.Position = 0;
                using (FileStream output = new FileStream(destination, FileMode.Create, FileAccess.Write)) {
                    byte[] buffer = new byte[16384];
                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0) {
                        output.Write(buffer, 0, len);
                    }
                }
            }
        }

        private static void copyFile(string source, string destination)
        {
            using (FileStream input = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                using (FileStream output = new FileStream(destination, FileMode.Create, FileAccess.Write)) {
                    byte[] buffer = new byte[16384];
                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0) {
                        output.Write(buffer, 0, len);
                    }
                }
            }
        }
    }
}
