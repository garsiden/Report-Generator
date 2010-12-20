﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;

using DocumentFormat.OpenXml.Wordprocessing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using RSMTenon.Data;
using RSMTenon.Graphing;

namespace RSMTenon.ReportGenerator
{
    public class ChartItem
    {
        public C.Chart Chart { get; set; }
        public string Title { get; set; }
        public string CustomControlName { get; set; }
    }

    public class StressTest
    {
        public string Id;
        public DateTime FromDate;
        public DateTime ToDate;
        private string pointName;
        private string format = "yyyy-MM-dd";

        public StressTest(string id, XElement reportSpec)
        {
            var test1 = reportSpec.Element("categories").Elements().Single(t => t.Attribute("id").Value.Equals(id));
            PointName = test1.Element("point-name").Value;
            var fromAttr = test1.Attribute("from");
            var toAttr = test1.Attribute("to");
            if (fromAttr != null) {
                FromDate = DateTime.ParseExact(fromAttr.Value, format, new DateTimeFormatInfo());
            }
            if (toAttr != null) {
                ToDate = DateTime.ParseExact(toAttr.Value, format, new DateTimeFormatInfo());
            }
        }

        public string PointName
        {
            get { return String.Format(pointName, FromDate.ToString("MMM yy"), ToDate.ToString("MMM yy")); }
            set { pointName = value; }
        }

        public int FromIntegerDate
        {
            get { return ReturnData.IntegerDate(FromDate); }
        }
        public int ToIntegerDate
        {
            get { return ReturnData.IntegerDate(ToDate); }
        }

    }

    public class Report
    {
        public Client Client { get; set; }
        private RepGenDataContext context;
        private XElement specs = null;
        private static string SPEC_FILE = @"C:\Documents and Settings\garsiden\My Documents\svn\repgen\vsprojects\word_chart\chart-spec.xml";
        private List<AssetClass> assetClasses = null;
        private string strategyName;
        // "C0C0C0", "808080", "0066CC", "98CC00"
        private string strategyColourHex = "0066CC";
        private string clientColourHex = "98CC00";
        private DateTime tenYearStart = new DateTime(1999, 9, 30);

        public Table ModelTable()
        {
            ModelTable modelTable = new ModelTable();
            string[] colwidths = { "3700", "980", "1180", "1120", "1480", "1120", "1160" };
            string[] headerText = {null, null, "Weighting", "Amount", "Expected Yield*", "Projected Income" };
 
            Table table1 = modelTable.GenerateTable(colwidths, headerText);

            //  create an asset class section
            var cellProps = new CellProps[] {
            new CellProps() { text = "Cash", align = JustificationValues.Left },
            new CellProps() { text = "1.5%" },
            new CellProps(),
            new CellProps(),
            new CellProps(),
            new CellProps()

            };
            var header = modelTable.GenerateTableHeaderRow(cellProps, 255U);
            table1.Append(header);

            // create a content row
            cellProps = new CellProps[] {
            new CellProps() { span = 2, text = "Aviva Emerging Market Local Currency Bond Fund", align = JustificationValues.Left },
            new CellProps() { span = 0 },
            new CellProps() { text = "1.50%" },
            new CellProps() { text = "£15,000" },
            new CellProps() { text = "0.10%" },
            new CellProps() { text = "£15.00" }
            };

            TableRow row = modelTable.GenerateTableRow(cellProps, 255U);
            table1.Append(row);

            // create a footer row
            cellProps = new CellProps[] {
            new CellProps(),
            new CellProps() { text= "100.0%" },
            new CellProps(),
            new CellProps() { text = "£,1000,000", boxed = true },
            new CellProps(),
            new CellProps() { text = "£24,779", boxed = true }
            };

            TableRow footer = modelTable.GenerateTableFooterRow(cellProps, 255U);
            table1.Append(footer);

            return table1;
        }

        public ChartItem Allocation()
        {
            XElement rpt = getChartSpec("allocation");
            string title = null;

            // set title
            if (Client.ExistingAssets) {
                title = rpt.Element("title").Element("existing-assets").Value;
            } else {
                title = String.Format(rpt.Element("title").Element("cash").Value, StrategyName);
            }

            IQueryable<AssetWeighting> data;

            if (Client.ExistingAssets) {
                data = ClientAssetClass.GetClientAssetClass(Client.GUID);
            } else {
                data = Model.GetModelAllocation(Client.StrategyID);
            }

            AllocationPieChart pie = new AllocationPieChart();
            C.Chart chart = pie.GenerateChart(title, data.ToList());

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;
        }

        public ChartItem AllocationComparison()
        {
            // get chart specs
            XElement rpt = getChartSpec("allocation-comparison");

            // set title
            string title = String.Format(rpt.Element("title").Value, StrategyName);

            var comp = DataContext.ClientWeightingComparison(Client.GUID, Client.StrategyID);
            var data = comp.ToList();

            AllocationComparisonBarChart bc = new AllocationComparisonBarChart();
            C.Chart chart = bc.GenerateChart(title, data);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;
        }

        public ChartItem Drawdown()
        {
            // get chart specs
            XElement rpt = getChartSpec("drawdown");

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();

            // create chart
            DrawdownLineChart lc = new DrawdownLineChart();
            C.Chart chart = lc.GenerateChart(title);

            // client assets
            if (Client.ExistingAssets) {
                var data1 = getClientAssetDrawdown(Client.GUID);
                lc.AddLineChartSeries(chart, data1, "Current", clientColourHex);

            }

            // first asset class
            var data2 = getAssetClassDrawdown(assets[0].ID);
            lc.AddLineChartSeries(chart, data2, assets[0].Name, assets[0].ColourHex);

            // second asset class
            var data3 = getAssetClassDrawdown(assets[1].ID);
            lc.AddLineChartSeries(chart, data3, assets[1].Name, assets[1].ColourHex);

            // model drawdown
            var data4 = getModelDrawdown(Client.StrategyID);
            lc.AddLineChartSeries(chart, data4, StrategyName + " Strategy", strategyColourHex);

            string ccn = rpt.Element("control-name").Value;

            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;
        }

        private List<ReturnData> getAssetClassDrawdown(string assetclassId)
        {
            var prices = DataContext.HistoricPrice(assetclassId);

            ReturnCalculation calc = new ReturnCalculation();

            var dd = from p in prices
                     select new ReturnData {
                         Value = calc.Drawdown(p) - 1,
                         Date = p.Date
                     };

            return dd.ToList();
        }

        private List<ReturnData> getModelDrawdown(string strategyId)
        {
            var returns = DataContext.ModelReturn(strategyId);

            ReturnCalculation cp = new ReturnCalculation();
            ReturnCalculation cd = new ReturnCalculation();

            var dd = from r in returns
                        let price = cp.Price(r)
                        select new ReturnData {
                            Value = cd.Drawdown(price, r.Value) - 1,
                            Date = r.Date
                        };

            return dd.ToList();
        }

        private List<ReturnData> getClientAssetDrawdown(Guid clientGuid)
        {
            var returns = DataContext.ClientAssetReturn(clientGuid);

            ReturnCalculation cp = new ReturnCalculation();
            ReturnCalculation cd = new ReturnCalculation();

            var dd = from r in returns
                     let price = cp.Price(r)
                     select new ReturnData {
                         Value = cd.Drawdown(price, r.Value) - 1,
                         Date = r.Date
                     };

            return dd.ToList();
        }

        public ChartItem StressTestMarketRise()
        {
            // get chart specs
            XElement rpt = getChartSpec("stress-test-market-rise");

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();
            var ctx = DataContext;

            // create chart
            StressTestBarChart bc = new StressTestBarChart();
            C.Chart chart = bc.GenerateChart(title);

            if (Client.ExistingAssets) {
                var returns1 = getStressTestClientAssetReturn(Client.GUID);
                var series1 = stressTestMarketRiseSeries(returns1, "Current", clientColourHex, rpt);
                bc.AddBarChartSeries(chart, series1);
            }

            // first asset class (Note: GLEQ)
            var returns2 = getStressTestAssetClassReturn(assets[1].ID);
            var series2 = stressTestMarketRiseSeries(returns2, assets[1].Name, assets[1].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series2);

            // second asset class
            var returns3 = getStressTestAssetClassReturn(assets[0].ID);
            var series3 = stressTestMarketRiseSeries(returns3, assets[0].Name, assets[0].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series3);

            // strategy
            var returns4 = getStressTestModelReturn(Client.StrategyID);
            var series4 = stressTestMarketRiseSeries(returns4, Client.Strategy.Name, strategyColourHex, rpt);
            bc.AddBarChartSeries(chart, series4);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;

        }

        public ChartItem StressTestMarketCrash()
        {
            // get chart specs
            XElement rpt = getChartSpec("stress-test-market-crash");

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();
            var ctx = DataContext;

            // create chart
            StressTestBarChart bc = new StressTestBarChart();
            C.Chart chart = bc.GenerateChart(title);

            if (Client.ExistingAssets) {
                var returns1 = getStressTestClientAssetReturn(Client.GUID);
                var series1 = stressTestMarketCrashSeries(returns1, "Current", clientColourHex, rpt);
                bc.AddBarChartSeries(chart, series1);
            }

            // first asset class (Note: GLEQ)
            var returns2 = getStressTestAssetClassReturn(assets[1].ID);
            var series2 = stressTestMarketCrashSeries(returns2, assets[1].Name, assets[1].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series2);

            // second asset class
            var returns3 = getStressTestAssetClassReturn(assets[0].ID);
            var series3 = stressTestMarketCrashSeries(returns3, assets[0].Name, assets[0].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series3);

            // strategy
            var returns4 = getStressTestModelReturn(Client.StrategyID);
            var series4 = stressTestMarketCrashSeries(returns4, Client.Strategy.Name, strategyColourHex, rpt);
            bc.AddBarChartSeries(chart, series4);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;

        }

        public ChartItem TenYearReturn()
        {
            // get chart specs
            XElement rpt = getChartSpec("ten-year-return");

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses(rpt);
            var ctx = DataContext;

            // create chart
            TenYearLineChart lc = new TenYearLineChart();
            C.Chart chart = lc.GenerateChart(null);

            // first asset class
            var data2 = tenYearAssetClassReturn(assetClasses[0].ID, tenYearStart);
            string dataKey2 = assetClasses[0].Name;
            lc.AddLineChartSeries(chart, data2.ToList(), dataKey2, assetClasses[0].ColourHex);

            // second asset class
            var data3 = tenYearAssetClassReturn(assetClasses[1].ID, tenYearStart);
            string dataKey3 = assetClasses[1].Name;
            lc.AddLineChartSeries(chart, data3.ToList(), dataKey3, assetClasses[1].ColourHex);

            // strategy
            var data4 = tenYearModelReturn(Client.StrategyID, tenYearStart);
            string dataKey4 = StrategyName;
            lc.AddLineChartSeries(chart, data4, dataKey4, strategyColourHex);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;

        }

        private Dictionary<int, ReturnData> getStressTestClientAssetReturn(Guid clientGuid)
        {
            var returns = DataContext.ClientAssetReturn(clientGuid);
            var calc = new ReturnCalculation();
            var prices = from p in returns
                         select new ReturnData {
                             Date = p.Date,
                             Value = calc.Price(p)
                         };

            return prices.ToDictionary(p => p.Date);
        }

        private Dictionary<int, ReturnData> getStressTestAssetClassReturn(string assetClassId)
        {
            return DataContext.HistoricPrice(assetClassId).ToDictionary(d => d.Date);
        }

        private Dictionary<int, ReturnData> getStressTestModelReturn(string strategyId)
        {
            var returns = DataContext.ModelReturn(strategyId);
            var calc = new ReturnCalculation();
            var prices = from p in returns
                         select new ReturnData {
                             Date = p.Date,
                             Value = calc.Price(p)
                         };

            return prices.ToDictionary(p => p.Date);
        }

        private BarGraphSeries stressTestMarketCrashSeries(Dictionary<int, ReturnData> pd, string seriesName, string colourHex, XElement rpt)
        {
            var test1 = new StressTest("russian-debt-crisis", rpt);

            // Russian Debt
            var start1 = pd.TakeWhile(p => p.Key < test1.FromIntegerDate).Last();
            var end1 = pd[test1.ToIntegerDate];
            double return1 = (end1.Value - start1.Value.Value) / start1.Value.Value;

            // Economic Slowdown
            var test2 = new StressTest("economic-slowdown", rpt);
            var start2 = pd[test2.FromIntegerDate];
            var end2 = pd[test2.ToIntegerDate];
            double return2 = (end2.Value - start2.Value) / start2.Value;

            // Technology Bubble
            var test3 = new StressTest("technology-bubble-burst", rpt);
            var start3 = pd[test3.FromIntegerDate];
            var end3 = pd[test3.ToIntegerDate];
            double return3 = (end3.Value - start3.Value) / start3.Value;

            var series = new BarGraphSeries {
                Name = seriesName,
                ColourHex = colourHex,
                PointNames = new string[] { test1.PointName, test2.PointName, test3.PointName },
                Values = new double[] { return1, return2, return3 }
            };

            return series;
        }

        private BarGraphSeries stressTestMarketRiseSeries(Dictionary<int, ReturnData> pd, string seriesName, string colourHex, XElement rpt)
        {
            var test1 = new StressTest("bull", rpt);

            // Bull
            var start1 = pd[test1.FromIntegerDate];
            var end1 = pd[test1.ToIntegerDate];
            double return1 = (end1.Value - start1.Value) / start1.Value;

            // Ten Year Return
            var test2 = new StressTest("ten-year", rpt);
            var start2 = pd.ElementAt(pd.Count() - 121);
            var end2 = pd.Last();

            double return2 = Math.Log(end2.Value.Value / start2.Value.Value);
            test2.FromDate = start2.Value.DateFromInteger;
            test2.ToDate = end2.Value.DateFromInteger;

            var series = new BarGraphSeries {
                Name = seriesName,
                ColourHex = colourHex,
                PointNames = new string[] { test1.PointName, test2.PointName },
                Values = new double[] { return1, return2 }
            };

            return series;
        }



        private List<ReturnData> tenYearModelReturn(string strategId, DateTime tenYearStart)
        {
            var data = DataContext.ModelReturn(strategId, tenYearStart);

            ReturnCalculation calc = new ReturnCalculation();
            var tyr = from d in data
                      select new ReturnData {
                          Value = calc.RebaseReturn(d),
                          Date = d.Date
                      };

            return tyr.ToList();
        }

        public ChartItem RollingReturnChart(int years)
        {
            string id = null;

            switch (years) {
                case 1: id = "rolling-return-1yr"; break;
                case 3: id = "rolling-return-3yr"; break;
                case 5: id = "rolling-return-5yr"; break;
                default: throw new ArgumentException("Illegal argument: 1, 3 or 5 only are valid");
            }

            // get chart specs
            XElement rpt = getChartSpec(id);

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();
            var ctx = DataContext;

            // create chart
            RollingReturnLineChart lc = new RollingReturnLineChart();
            C.Chart chart = lc.GenerateChart(title);

            if (Client.ExistingAssets) {
                string dataKey1 = "Current";
                var data1 = ctx.ClientAssetReturn(Client.GUID);
                var rrex = getRollingReturn(data1, years);
                lc.AddLineChartSeries(chart, rrex, dataKey1, clientColourHex);
            }

            // first asset class
            var data2 = ctx.RollingReturn(years, assetClasses[0].ID);
            string dataKey2 = assetClasses[0].Name;
            lc.AddLineChartSeries(chart, data2.ToList(), dataKey2, assetClasses[0].ColourHex);

            // add second data series
            var data3 = ctx.RollingReturn(years, assetClasses[1].ID);
            string dataKey3 = assetClasses[1].Name;
            lc.AddLineChartSeries(chart, data3.ToList(), dataKey3, assetClasses[1].ColourHex);

            // add appropriate strategy data
            var data4 = ctx.ModelReturn(Client.StrategyID);
            string dataKey4 = StrategyName + " Strategy";
            var rr = getRollingReturn(data4, years);
            lc.AddLineChartSeries(chart, rr, dataKey4, strategyColourHex);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;
        }

        private List<ReturnData> tenYearAssetClassReturn(string assetClassId, DateTime startDate)
        {
            var data = DataContext.AssetClassReturn(startDate, assetClassId);

            ReturnCalculation calc = new ReturnCalculation();
            var tyr = from d in data
                      select new ReturnData {
                          Value = calc.RebaseReturn(d),
                          Date = d.Date
                      };

            return tyr.ToList(); ;
        }

        private List<ReturnData> getRollingReturn(ISingleResult<ReturnData> data, int years)
        {
            ReturnCalculation calc = new ReturnCalculation();

            var prices = from d in data
                         select new ReturnData {
                             Value = calc.Price(d),
                             Date = d.Date
                         };

            // create the 'From' list, starting with index value of 100
            var from = prices.ToList();
            from.Insert(0, new ReturnData() { Value = 100D });

            // create the 'To' list, starting with a years * 12 offset
            var to = from.Skip(12 * years);

            // new list for return values
            var rv = new List<ReturnData>();
            int i = 0;

            foreach (var item in to) {
                var rd = new ReturnData {
                    Value = calc.RollingReturn(item, from.ElementAt(i++), years),
                    Date = item.Date
                };
                rv.Add(rd);
            }

            return rv;
        }


        private List<AssetClass> getAssetClasses(XElement rpt)
        {
            var ctx = DataContext;
            var allClasses = ctx.AssetClasses.ToDictionary(a => a.ID);
            var assets = from spec in rpt.Descendants("asset-class")
                         select spec;

            var classes = new List<AssetClass>();
            foreach (var a in assets) {
                classes.Add(new AssetClass() {
                    ID = a.Attribute("id").Value,
                    Name = allClasses[a.Attribute("id").Value].Name,
                    ColourHex = a.Attribute("colour-hex").Value
                });
            }
            assetClasses = classes;

            return assetClasses;
        }

        private List<AssetClass> getAssetClasses()
        {
            if (assetClasses == null) {
                var ctx = DataContext;
                var allClasses = ctx.AssetClasses.ToDictionary(a => a.ID);
                var assets = from spec in ChartSpecs.Descendants("asset-class")
                             select spec;

                var classes = new List<AssetClass>();
                foreach (var a in assets) {
                    classes.Add(new AssetClass() {
                        ID = a.Attribute("id").Value,
                        Name = allClasses[a.Attribute("id").Value].Name,
                        ColourHex = a.Attribute("colour-hex").Value
                    });
                }
                assetClasses = classes;
            }

            return assetClasses;
        }

        private XElement getChartSpec(string id)
        {
            var spec = (from rep in ChartSpecs.Descendants("chart")
                        where rep.Attribute("id").Value == id
                        select rep).Single();

            return spec;
        }

        private RepGenDataContext DataContext
        {
            get
            {
                if (context == null) {
                    context = new RepGenDataContext();
                }

                return context;
            }
        }

        private string StrategyName
        {
            get
            {
                if (strategyName == null) {
                    strategyName = Strategy.GetStrategyNameFromId(Client.StrategyID);
                }
                return strategyName;
            }
        }

        private XElement ChartSpecs
        {
            get
            {
                if (specs == null) {
                    specs = XElement.Load(SPEC_FILE);
                }
                return specs;
            }
        }
    }
}
