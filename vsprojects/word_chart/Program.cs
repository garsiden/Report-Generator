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
using System.Threading;

using RSMTenon.Graphing;
using RSMTenon.Data;

namespace RSMTenon.ReportGenerator
{
    class Program
    {
        static string path = @"C:\Documents and Settings\garsiden\My Documents\Projects\RepGen\test\chart\";
        static string generated = path + "ChartDoc.docx";
        private double previousDD = 1;
        private uint docPrId = 0U;

        static void Main(string[] args)
        {
            Program This = new Program();

            // create test Client and Report
            //Client client = new Client() { Name = "Joe Mayo", ExistingAssets = false, MeetingDate = new DateTime(2010, 12, 14), StrategyID = "CO", TimeHorizon = 5, InvestmentAmount = 1000000 };
            //Guid guid = new Guid("636c8103-e06d-4575-aafc-574474c2d7f8");
            //Guid guid = new Guid("979de312-8e99-49d3-9d41-54ecae0cad5c");
            //Guid guid = new Guid("979de312-8e99-49d3-9d41-54ecae0cad5c");
            Guid guid = new Guid("1426a508-fc66-4e2d-b3cc-b3e1e1240a0e");
            //client.GUID = guid;
            Client client = Client.GetClientByGUID(guid);
            Report report = new Report() { Client = client };

            //This.TenYearTest();
            //This.RollingReturnTest();
            //This.DrawdownTest();
            //Thread.Sleep(return);
            //This.StressTest();
            //return;
            //500000;

            string template = path + "chart_template.docx";
            string generated = path + "chart_test.docx";

            // copy template to generated
            File.Copy(template, generated, true);

            // open Word document
            WordprocessingDocument myWordDoc = WordprocessingDocument.Open(generated, true);
            MainDocumentPart mainPart = myWordDoc.MainDocumentPart;

            //This.InsertAllocationPie(mainPart);
            //This.InsertRRChartIntoWord(mainPart, report);
            This.CreateReport(mainPart, report);

            myWordDoc.Close();
        }


        public void CreateReport(MainDocumentPart mainPart, Report report)
        {
            ChartItem chartItem = null;
            string controlName = null;
            
            // Stress Test Market Rise Bar Chart
            chartItem = report.StressTestMarketRise();
            controlName = chartItem.CustomControlName;
            AddChartToDoc(mainPart, chartItem, controlName);

            // Allocation Pie Chart
            chartItem = report.AllocationPieChart();
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

        }
        private void RollingReturnTest()
        {
            var ctx = new RepGenDataContext();
            ReturnCalculation calc = new ReturnCalculation();

            // calculate prices
            ctx.ModelReturn("CO");
            var data = ctx.ModelReturn("CO");

            var prices = from d in data
                         select new ReturnData {
                             Value = calc.calculatePrice(d),
                             Date = d.Date
                         };

            var p1 = prices.ToList();
            p1.Insert(0, new ReturnData() { Value = 100D });
            var p2 = p1.Skip(12 * 3);
            var p3 = new List<ReturnData>();
            int i = 0;

            foreach (var item in p2) {
                var rd = new ReturnData {
                    Value = calc.calculateRollingReturn(item, p1.ElementAt(i++)),
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
            var data = ctx.ModelReturn( "CO", new DateTime(1999, 9, 30));
//            var data = ctx.AssetClassReturn(new DateTime(1999, 9, 30), "GLEQ");

            ReturnCalculation calc = new ReturnCalculation();
            var match = from d in data
                        select new ReturnData {
                            Value = calc.rebaseReturn(d),
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
            Thread.Sleep(500000);
        }

        private void PriceTest()
        {

            var ctx = new RepGenDataContext();
            ctx.ModelReturn("CO");
            var data = ctx.ModelReturn("CO");

            ReturnCalculation calc = new ReturnCalculation();
            var match = from d in data
                        select new ReturnData {
                            Value = calc.calculatePrice(d),
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
            Thread.Sleep(500000);

        }

        private void DrawdownTest()
        {

            var ctx = new RepGenDataContext();
            var data = ctx.Drawdown("UKGB");

            var match = from d in data
                        select new ReturnData {
                            Value = calculateDrawdown(d),
                            Date = d.Date
                        };

            foreach (var item in match) {
                Console.WriteLine("{0}\t{1}", item.Date, item.Value);
            }
            Thread.Sleep(500000);

        }

        private double calculateDrawdown(DrawdownData drawdown)
        {
            if ((drawdown.Value / drawdown.PreviousValue > 1) && previousDD == 1) {
                return 1D;
            } else {
                double retval = Math.Min(1D, previousDD * (1 + Math.Log(drawdown.Value / drawdown.PreviousValue)));
                this.previousDD = retval;
                return retval;
            }
        }

        public void InsertBarChartIntoWord(MainDocumentPart mainPart, string title)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, "Chart1");

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chartPart);

            // generate Pie Chart and add to ChartSpace
            //StressBarChart bc = new StressBarChart();
            AllocationBarChart bc = new AllocationBarChart();
            C.Chart chart = bc.GenerateChart(title);
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

        private void AddChartToDoc(MainDocumentPart mainPart, ChartItem chartItem, string controlName)
        {
            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, controlName);

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chartPart);
            chartSpace.Append(chartItem.Chart);

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

    }
}
