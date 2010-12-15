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

        static void Main(string[] args)
        {
            Program This = new Program();

            // create test Client and Report
            Client client = new Client() { Name = "Joe Mayo", ExistingAssets = false, MeetingDate = new DateTime(2010, 12, 14), StrategyId = "CO", TimeHorizon = 5, Investment = 1000000 };
            Report report = new Report() { Client = client };

            //This.PriceTest();
            //This.RollingReturnTest();
            //This.DrawdownTest();
            //Thread.Sleep(500000);
            //return;

            string template = path + "chart_template.docx";
            string generated = path + "chart_test.docx";

            // copy template to generated
            File.Copy(template, generated, true);

            // open Word document
            WordprocessingDocument myWordDoc = WordprocessingDocument.Open(generated, true);
            MainDocumentPart mainPart = myWordDoc.MainDocumentPart;

            This.InsertAllocationPie(mainPart);
            This.InsertRRChartIntoWord(mainPart, report);

            myWordDoc.Close();
        }

        private void RollingReturnTest()
        {
            var ctx = new RepGenDataContext();
            ReturnCalculation calc = new ReturnCalculation();

            // calculate prices
            ctx.ModelPrice("CO");
            var data = ctx.ModelPrice("CO");

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

        private void PriceTest()
        {

            var ctx = new RepGenDataContext();
            ctx.ModelPrice("CO");
            var data = ctx.ModelPrice("CO");

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

        private double calculateDrawdown(Drawdown drawdown)
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

        public void InsertAllocationPie(MainDocumentPart mainPart)
        {
            // variables from client details
            bool assets = false;
            string strategyId = "CO";
            string strategyName = Strategy.GetStrategyNameFromId(strategyId);

            Paragraph para = findAndRemoveContent(mainPart, "AllocationPieChart");

            // generate new ChartPart and ChartSpace
            ChartPart chartPart = mainPart.AddNewPart<ChartPart>();
            string relId = mainPart.GetIdOfPart(chartPart);
            C.ChartSpace chartSpace = GraphSpace.GenerateChartSpace(chartPart);

            // generate Pie Chart and add to ChartSpace
            string title = strategyName + " Allocation";
            IQueryable<ModelAllocation> allocation;

            if (assets) {
                allocation = Model.GetModelAllocation("CO");
            } else {
                allocation = Model.GetModelAllocation("CO");
            }
            AllocationPieChart pie = new AllocationPieChart();
            C.Chart chart = pie.GenerateChart(title, allocation.ToList());
            chartSpace.Append(chart);

            // set ChartPart ChartSpace
            chartPart.ChartSpace = chartSpace;

            // generate a new Wordprocessing Drawing, add to a new Run,
            // and relate to new ChartPart
            Run run = new Run();
            Drawing drawing = GraphDrawing.GenerateDrawing(relId, title, 2U, AllocationPieChart.Cx, AllocationPieChart.Cy);
            para.Append(run);
            run.Append(drawing);

            // save and close document
            mainPart.Document.Save();
        }

        public void InsertRRChartIntoWord(MainDocumentPart mainPart, Report report)
        {
            // variables from client details
            //string strategyName = Strategy.GetStrategyNameFromId(report.Client.StrategyId);
            //string title = "1yr Rolling return";
            //string contentControlName = "RollingReturnOneYear";
            ChartItem chartItem = report.RollingReturnChart(3);
            string ccn = chartItem.CustomControlName;

            // open Word documant and remove existing content from control
            Paragraph para = findAndRemoveContent(mainPart, ccn);

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
            Drawing drawing = GraphDrawing.GenerateDrawing(relId, ccn, 3U, RollingReturnLineChart.Cx, RollingReturnLineChart.Cy);
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

    }
}
