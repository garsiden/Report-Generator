using System;
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
    #region Helper Classes
    public class ChartItem
    {
        public C.Chart Chart { get; set; }
        public string Title { get; set; }
        public string CustomControlName { get; set; }
        public GraphData GraphData { get; set; }
        public decimal SizeX { get; set; }
        public decimal SizeY { get; set; }
        public long Cx { get { return (long)(SizeX * Graph.EMUS_PER_CENTIMETRE); } }
        public long Cy { get { return (long)(SizeY * Graph.EMUS_PER_CENTIMETRE); } }
    }

    public class StressTest
    {
        public string Id;
        public DateTime FromDate;
        public DateTime ToDate;
        private string pointName;
        private string format = "yyyy-MM-dd";
        public bool PreDate { get; set; }

        public StressTest(string id, XElement reportSpec)
        {
            var test1 = reportSpec.Element("categories").Elements().Single(t => t.Attribute("id").Value.Equals(id));
            PointName = test1.Element("point-name").Value;
            var fromAttr = test1.Attribute("from");
            var toAttr = test1.Attribute("to");
            PreDate = (bool?)test1.Attribute("pre-date") ?? false;

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
    #endregion Helper Classes

    public class Report
    {
        public Client Client { get; set; }
        private RepGenDataContext context;
        private XElement reportSpec = null;
        public string SpecFile { get; set; }
        public decimal DefaultSizeX { get; set; }
        public decimal DefaultSizeY { get; set; }

        // Return variables
        private List<ReturnData> modelReturn = null;
        private List<ReturnData> clientAssetReturn = null;
        private Dictionary<string, List<ReturnData>> assetClassPricesDictionary = new Dictionary<string, List<ReturnData>>();

        // Price variables
        private Dictionary<int, ReturnData> modelPrices = null;
        private Dictionary<string, List<ReturnData>> assetClassReturnDictionary = new Dictionary<string, List<ReturnData>>();

        private List<AssetClass> assetClasses = null;
        private string strategyName;

        // Colours
        private string strategyColourHex = "0066CC";
        private string clientColourHex = "98CC00";
        private string benchmarkColourHex = "C0C0C0";

        public Report(string specFile)
        {
            SpecFile = specFile;
            setDefaultChartSize();
        }

        private void setDefaultChartSize()
        {
            var spec = ReportSpec.Element("chart-spec");
            DefaultSizeX = (decimal?)spec.Attribute("size-x") ?? Graph.DEFAULT_SIZE_X;
            DefaultSizeY = (decimal?)spec.Attribute("size-y") ?? Graph.DEFAULT_SIZE_Y;
        }

        public string GetContentControlNameForChart(string chartId)
        {
            XElement rpt = chartSpec(chartId);

            if (rpt == null)
                return null;
            else
                return rpt.Element("control-name").Value;
        }

        #region Text


        #endregion

        # region Model Table
        public Table ModelTable()
        {
            ModelTable modelTable = new ModelTable();

            // get table specifications
            var tspec = tableSpec("model");
            uint rowHeight = (uint)tspec.Attribute("row-height");
            var cols = tspec.Element("columns").Elements("column").ToList();
            int ncols = cols.Count;
            var colwidths = new List<string>();
            var formats = new List<string>();
            var colHeaders = new List<string>();

            foreach (var c in cols) {
                colwidths.Add(c.Attribute("width").Value);
                formats.Add((string)c.Attribute("format") ?? String.Empty);
                colHeaders.Add(c.Value);
            }

            // get tactical model investments, by asset group
            var model = getTacticalModelTableData(Client.StrategyID, Client.StatusName).ToList();

            // build table
            Table table1 = modelTable.GenerateTable(colwidths, colHeaders);

            decimal amount = Client.InvestmentAmount;
            decimal totalIncome = 0;
            decimal income = 0;

            foreach (var g in model) {
                // Asset Group sub-headings
                var headerCells = new List<CellProps>();
                headerCells.Add(new CellProps { text = g.AssetGroupName, align = JustificationValues.Left });
                headerCells.Add(new CellProps { text = g.Weighting.ToString(formats[1]) });
                for (int i = 2; i < ncols; i++) {
                    headerCells.Add(new CellProps());
                }
                var header = modelTable.GenerateTableHeaderRow(headerCells, rowHeight);
                table1.Append(header);

                // investment rows for each asset class
                foreach (var inv in g.Investments) {
                    decimal weighting = Client.HighNetWorth ? inv.WeightingHNW : inv.WeightingAffluent;
                    income = amount * weighting * inv.ExpectedYield;
                    totalIncome += income;
                    var contentCells = new List<CellProps>();
                    contentCells.Add(new CellProps { span = 2, text = inv.InvestmentName, align = JustificationValues.Left });
                    contentCells.Add(new CellProps() { span = 0 });
                    contentCells.Add(new CellProps() { text = weighting.ToString(formats[2]) });
                    contentCells.Add(new CellProps() { text = (amount * weighting).ToString(formats[3]) });
                    contentCells.Add(new CellProps() { text = inv.ExpectedYield.ToString(formats[4]) });
                    contentCells.Add(new CellProps() { text = income.ToString(formats[5]) });
                    TableRow row = modelTable.GenerateTableRow(contentCells, rowHeight);
                    table1.Append(row);
                }
                // empty row after last investment
                var emptyCells = new List<CellProps>();
                for (int i = 0; i < ncols; i++)
                    emptyCells.Add(new CellProps() { text = String.Empty });
                TableRow emptyRow = modelTable.GenerateTableRow(emptyCells, rowHeight);
                table1.Append(emptyRow);
            }

            // create a footer row with totals for asset group weighting, investment amount and total income
            var footerCells = new List<CellProps>();

            footerCells.Add(new CellProps());
            footerCells.Add(new CellProps() { text = model.Sum(m => m.Weighting).ToString(formats[1]) });
            footerCells.Add(new CellProps());
            footerCells.Add(new CellProps() { text = amount.ToString(formats[3]), boxed = true });
            footerCells.Add(new CellProps());
            footerCells.Add(new CellProps() { text = totalIncome.ToString(formats[5]), boxed = true });

            //decimal totalWeighting = model.Aggregate(0M, (accum, each) => accum + each.Weighting);

            TableRow footer = modelTable.GenerateTableFooterRow(footerCells, rowHeight);
            table1.Append(footer);

            return table1;
        }
        #endregion model table

        #region Charts
        public ChartItem Allocation()
        {
            XElement rpt = chartSpec("allocation");
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;
            string title = null;

            // set title
            if (Client.ExistingAssets) {
                title = rpt.Element("title").Element("existing-assets").Value;
            } else {
                title = String.Format(rpt.Element("title").Element("cash").Value, StrategyName);
            }

            List<AssetWeighting> data;

            if (Client.ExistingAssets) {
                data = ClientAssetClass.GetClientAssetWeighting(Client.GUID).Where(a => a.Weighting > 0).ToList();
            } else {
                data = StrategicModel.GetAssetWeighting(Client.StrategyID).Where(a => a.Weighting > 0).ToList();
            }

            AllocationPieChart pie = new AllocationPieChart();
            C.Chart chart = pie.GenerateChart(title, data);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = pie.GraphData
            };

            return chartItem;
        }

        public ChartItem AllocationComparison()
        {
            // get chart specs
            XElement rpt = chartSpec("allocation-comparison");
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;

            // set title
            string title = String.Format(rpt.Element("title").Value, StrategyName);

            var comp = DataContext.ClientWeightingComparison(Client.GUID, Client.StrategyID);
            var diff = from c in comp
                       select new AssetWeighting { AssetGroup = c.AssetGroupName, Weighting = c.WeightingDifference };
            var data = diff.ToList();

            AllocationComparisonBarChart bc = new AllocationComparisonBarChart();
            C.Chart chart = bc.GenerateChart(title, data);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = bc.GraphData
            };

            return chartItem;
        }

        public ChartItem Drawdown()
        {
            // get chart specs
            XElement rpt = chartSpec("drawdown");
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();

            // create chart
            DrawdownLineChart lc = new DrawdownLineChart();
            C.Chart chart = lc.GenerateChart(title);

            // client assets
            if (Client.ExistingAssets) {
                var data1 = calculateClientAssetDrawdown();
                lc.AddLineChartSeries(chart, data1, "Current", clientColourHex);
            }

            // first asset class
            var data2 = calculateAssetClassDrawdown(assets[0].ID);
            lc.AddLineChartSeries(chart, data2, assets[0].Name, assets[0].ColourHex);

            // second asset class
            var data3 = calculateAssetClassDrawdown(assets[1].ID);
            lc.AddLineChartSeries(chart, data3, assets[1].Name, assets[1].ColourHex);

            // model drawdown
            var data4 = calculateStrategicModelDrawdown(Client.StrategyID, Client.StatusName);
            lc.AddLineChartSeries(chart, data4, StrategyName + " Strategy", strategyColourHex);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = lc.GraphData
            };

            return chartItem;
        }

        public ChartItem StressTestMarketRise()
        {
            // get chart specs
            XElement rpt = chartSpec("stress-test-market-rise");
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();
            var ctx = DataContext;

            // create chart
            StressTestBarChart bc = new StressTestBarChart();
            C.Chart chart = bc.GenerateChart(title);

            if (Client.ExistingAssets) {
                var prices1 = calculateClientAssetPrices();
                var series1 = stressTestMarketRiseSeries(prices1, "Current", clientColourHex, rpt);
                bc.AddBarChartSeries(chart, series1);
            }

            // first asset class (Note: GLEQ)
            var prices2 = getAssetClassPrices(assets[1].ID).ToDictionary(d => d.Date);
            var series2 = stressTestMarketRiseSeries(prices2, assets[1].Name, assets[1].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series2);

            // second asset class
            var prices3 = getAssetClassPrices(assets[0].ID).ToDictionary(d => d.Date);
            var series3 = stressTestMarketRiseSeries(prices3, assets[0].Name, assets[0].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series3);

            // strategy
            var prices4 = calculateStrategicModelPrices();
            var series4 = stressTestMarketRiseSeries(prices4, Client.Strategy.Name, strategyColourHex, rpt);
            bc.AddBarChartSeries(chart, series4);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = bc.GraphData
            };

            return chartItem;
        }

        public ChartItem StressTestMarketCrash()
        {
            // get chart specs
            XElement rpt = chartSpec("stress-test-market-crash");
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses();
            var ctx = DataContext;

            // create chart
            StressTestBarChart bc = new StressTestBarChart();
            C.Chart chart = bc.GenerateChart(title);

            if (Client.ExistingAssets) {
                var returns1 = calculateClientAssetPrices();
                var series1 = stressTestMarketCrashSeries(returns1, "Current", clientColourHex, rpt);
                bc.AddBarChartSeries(chart, series1);
            }

            // first asset class (Note: GLEQ)
            var returns2 = getAssetClassPrices(assets[1].ID).ToDictionary(d => d.Date);
            var series2 = stressTestMarketCrashSeries(returns2, assets[1].Name, assets[1].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series2);

            // second asset class
            var returns3 = getAssetClassPrices(assets[0].ID).ToDictionary(d => d.Date);
            var series3 = stressTestMarketCrashSeries(returns3, assets[0].Name, assets[0].ColourHex, rpt);
            bc.AddBarChartSeries(chart, series3);

            // strategy
            var returns4 = calculateStrategicModelPrices();
            var series4 = stressTestMarketCrashSeries(returns4, Client.Strategy.Name, strategyColourHex, rpt);
            bc.AddBarChartSeries(chart, series4);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = bc.GraphData
            };

            return chartItem;
        }

        public ChartItem TenYearReturn()
        {
            // get chart specs
            XElement rpt = chartSpec("ten-year-return");
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;

            // set title
            string title = rpt.Element("title").Value;

            // get asset classes to plot (default is UKGB and GLEQ)
            var assets = getAssetClasses(rpt);
            var ctx = DataContext;

            // create chart
            TenYearLineChart lc = new TenYearLineChart();
            C.Chart chart = lc.GenerateChart(null);

            if (Client.ExistingAssets) {
                var rtrn1 = getClientAssetReturn();
                var data1 = calculateTenYearReturn(rtrn1);
                string dataKey1 = "Current";
                lc.AddLineChartSeries(chart, data1, dataKey1, clientColourHex);
            }

            // first asset class
            var rtrn2 = getAssetClassReturn(assetClasses[0].ID);
            var data2 = calculateTenYearReturn(rtrn2);
            string dataKey2 = assetClasses[0].Name;
            lc.AddLineChartSeries(chart, data2, dataKey2, assetClasses[0].ColourHex);

            // second asset class
            var rtrn3 = getAssetClassReturn(assetClasses[1].ID);
            var data3 = calculateTenYearReturn(rtrn3);
            string dataKey3 = assetClasses[1].Name;
            lc.AddLineChartSeries(chart, data3, dataKey3, assetClasses[1].ColourHex);

            // IMA Benchmark
            var rtrn5 = DataContext.BenchmarkPrice(Client.Strategy.BenchmarkID).Where(b => b.Value > 0).ToList();
            var data5 = calculateTenYearBenchmarkReturn(rtrn5);
            string dataKey5 = Client.Strategy.Benchmark.Name;
            lc.AddLineChartSeries(chart, data5, dataKey5, benchmarkColourHex);

            // strategy
            var rtrn4 = getStrategicModelReturn();
            var data4 = calculateTenYearReturn(rtrn4);
            string dataKey4 = StrategyName;
            lc.AddLineChartSeries(chart, data4, dataKey4, strategyColourHex);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = lc.GraphData
            };

            return chartItem;
        }

        public ChartItem RollingReturnChart(int years)
        {
            string id = null;

            switch (years) {
                case 1:
                    id = "rolling-return-1yr";
                    break;
                case 3:
                    id = "rolling-return-3yr";
                    break;
                case 5:
                    id = "rolling-return-5yr";
                    break;
                default:
                    throw new ArgumentException("Illegal argument: 1, 3 or 5 only are valid");
            }

            // get chart specs
            XElement rpt = chartSpec(id);
            decimal x = (decimal?)rpt.Attribute("size-x") ?? DefaultSizeX;
            decimal y = (decimal?)rpt.Attribute("size-y") ?? DefaultSizeY;

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
                var data1 = getClientAssetReturn();
                var rrex = calulateRollingReturn(data1, years);
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
            var data4 = getStrategicModelReturn();
            string dataKey4 = StrategyName + " Strategy";
            var rr = calulateRollingReturn(data4, years);
            lc.AddLineChartSeries(chart, rr, dataKey4, strategyColourHex);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem
            {
                Chart = chart,
                Title = title,
                CustomControlName = ccn,
                SizeX = x,
                SizeY = y,
                GraphData = lc.GraphData
            };

            return chartItem;
        }

        #endregion Charts

        #region Database Access

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

        private List<ReturnData> getAssetClassPrices(string assetClassId)
        {
            List<ReturnData> prices = null;
            assetClassPricesDictionary.TryGetValue(assetClassId, out prices);
            if (prices == null) {
                prices = DataContext.HistoricPrice(assetClassId).ToList();
                assetClassPricesDictionary[assetClassId] = prices;
            }

            return prices;
        }

        private List<ReturnData> getAssetClassReturn(string assetClassId)
        {
            List<ReturnData> returns = null;
            assetClassReturnDictionary.TryGetValue(assetClassId, out returns);
            if (returns == null) {
                returns = DataContext.AssetClassReturn(assetClassId).ToList();
                assetClassReturnDictionary[assetClassId] = returns;
            }

            return returns;
        }

        private List<ReturnData> getClientAssetReturn()
        {
            if (clientAssetReturn == null)
                clientAssetReturn = DataContext.ClientAssetReturn(Client.GUID).ToList();
            return clientAssetReturn;
        }

        private List<ReturnData> getStrategicModelReturn()
        {
            if (modelReturn == null)
                modelReturn = DataContext.StrategicModelReturn(Client.StrategyID).ToList();
            return modelReturn;
        }

        private IQueryable<TacticalModelTableData> getTacticalModelTableData(string strategyId, string status)
        {
            var models = DataContext.TacticalModels;

            var modelData = from m in models
                            where m.StrategyID == strategyId
                            group m by m.AssetGroupID
                                into g
                                let weight = (status == "HNW" ? g.Sum(m => m.WeightingHNW) : g.Sum(m => m.WeightingAffluent))
                                select new TacticalModelTableData
                                {
                                    AssetGroupId = g.Key,
                                    AssetGroupName = g.First().AssetGroup.Name,
                                    Investments = g,
                                    Weighting = weight
                                };

            return modelData;
        }

        private List<AssetClass> getAssetClasses(XElement rpt)
        {
            var ctx = DataContext;
            var allClasses = ctx.AssetClasses.ToDictionary(a => a.ID);
            var assets = from spec in rpt.Descendants("asset-class")
                         select spec;

            var classes = new List<AssetClass>();
            foreach (var a in assets) {
                classes.Add(new AssetClass()
                {
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
                var assets = from spec in ReportSpec.Descendants("asset-class")
                             select spec;

                var classes = new List<AssetClass>();
                foreach (var a in assets) {
                    classes.Add(new AssetClass()
                    {
                        ID = a.Attribute("id").Value,
                        Name = allClasses[a.Attribute("id").Value].Name,
                        ColourHex = a.Attribute("colour-hex").Value
                    });
                }
                assetClasses = classes;
            }

            return assetClasses;
        }

        #endregion Database Access

        #region Calculations
        private Dictionary<int, ReturnData> calculateClientAssetPrices()
        {
            var returns = getClientAssetReturn();

            var calc = new ReturnCalculation();
            var prices = from p in returns
                         select new ReturnData
                         {
                             Date = p.Date,
                             Value = calc.Price(p)
                         };

            return prices.ToDictionary(p => p.Date);
        }

        private Dictionary<int, ReturnData> calculateStrategicModelPrices()
        {
            if (modelPrices == null) {
                var returns = getStrategicModelReturn();
                var calc = new ReturnCalculation();
                var prices = from p in returns
                             select new ReturnData
                             {
                                 Date = p.Date,
                                 Value = calc.Price(p)
                             };

                modelPrices = prices.ToDictionary(p => p.Date);
            }

            return modelPrices;
        }

        private List<ReturnData> calculateTenYearBenchmarkReturn(List<ReturnData> prices)
        {
            int months = prices.Count;
            //int startIndex = months - Math.Min(121, months);
            int startDate = prices[months - 121].Date;

            ReturnCalculation cr = new ReturnCalculation();
            ReturnCalculation cb = new ReturnCalculation();

            var rtrn = from p in prices.Skip(months - 120)
                       let r = cr.Return(p)
                       select new ReturnData
                       {
                           Value = cb.RebaseReturn(r),
                           Date = p.Date
                       };

            var tyr = rtrn.ToList();
            var newItem = new ReturnData() { Date = startDate, Value = 0 };
            tyr.Insert(0, newItem);

            return tyr;
        }

        private List<ReturnData> calculateTenYearReturn(List<ReturnData> data)
        {
            int months = data.Count;
            int startDate = data[months - 121].Date;

            ReturnCalculation rc = new ReturnCalculation();

            var rtrn = from d in data.Skip(months - 120)
                       select new ReturnData
                       {
                           Value = rc.RebaseReturn(d),
                           Date = d.Date
                       };

            var tyr = rtrn.ToList();
            var newItem = new ReturnData() { Date = startDate, Value = 0 };
            tyr.Insert(0, newItem);

            return tyr;
        }

        public double CalculateModelReturn()
        {
            var prices = calculateStrategicModelPrices();
            double endPrice = prices.Last().Value.Value;
            double startPrice = prices.ElementAt(prices.Count - 121).Value.Value;

            double rtrn = Math.Log(endPrice / startPrice);

            return rtrn;
        }

        private List<ReturnData> calculateStrategicModelDrawdown(string strategyId, string status)
        {
            var returns = getStrategicModelReturn();

            ReturnCalculation cp = new ReturnCalculation();
            ReturnCalculation cd = new ReturnCalculation();

            var dd = from r in returns
                     let price = cp.Price(r)
                     select new ReturnData
                     {
                         Value = cd.Drawdown(price, r.Value) - 1,
                         Date = r.Date
                     };

            return dd.ToList();
        }

        private List<ReturnData> calculateClientAssetDrawdown()
        {
            var returns = getClientAssetReturn();

            ReturnCalculation cp = new ReturnCalculation();
            ReturnCalculation cd = new ReturnCalculation();

            var dd = from r in returns
                     let price = cp.Price(r)
                     select new ReturnData
                     {
                         Value = cd.Drawdown(price, r.Value) - 1,
                         Date = r.Date
                     };

            return dd.ToList();
        }

        private List<ReturnData> calculateAssetClassDrawdown(string assetclassId)
        {
            var returns = getAssetClassReturn(assetclassId);

            ReturnCalculation cp = new ReturnCalculation();
            ReturnCalculation cd = new ReturnCalculation();

            var dd = from r in returns
                     let price = cp.Price(r)
                     select new ReturnData
                     {
                         Value = cd.Drawdown(price, r.Value) - 1,
                         Date = r.Date
                     };

            return dd.ToList();
        }
        #endregion

        #region Chart Series Calculations
        private List<ReturnData> calulateRollingReturn(List<ReturnData> data, int years)
        {
            ReturnCalculation calc = new ReturnCalculation();

            var prices = from d in data
                         select new ReturnData
                         {
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
                var rd = new ReturnData
                {
                    Value = calc.RollingReturn(item, from.ElementAt(i++), years),
                    Date = item.Date
                };
                rv.Add(rd);
            }

            return rv;
        }

        private BarGraphSeries stressTestMarketCrashSeries(Dictionary<int, ReturnData> prices, string seriesName, string colourHex, XElement rpt)
        {
            //new StressTest("russian-debt-crisis", rpt);

            var tests = new StressTest[]  {
                new StressTest("economic-slowdown", rpt),
                new StressTest("technology-bubble-burst", rpt),
                new StressTest("zeros-collapse", rpt),
                new StressTest("credit-crunch", rpt)
            };

            var pointNames = new string[tests.Length];
            var values = new double[tests.Length];

            for (int i = 0; i < tests.Length; i++) {
                var t = tests[i];
                var start = t.PreDate ? prices.Last(p => p.Key < t.FromIntegerDate).Value : prices[t.FromIntegerDate];
                var end = prices[t.ToIntegerDate];
                double rtrn = (end.Value - start.Value) / start.Value;
                pointNames[i] = t.PointName;
                values[i] = rtrn;
            }

            var series = new BarGraphSeries
            {
                Name = seriesName,
                ColourHex = colourHex,
                PointNames = pointNames,
                Values = values
            };

            return series;
        }

        private BarGraphSeries stressTestMarketRiseSeries(Dictionary<int, ReturnData> prices, string seriesName, string colourHex, XElement rpt)
        {
            var test1 = new StressTest("bull", rpt);

            // Bull
            var start1 = test1.PreDate ? prices.Last(p => p.Key < test1.FromIntegerDate).Value : prices[test1.FromIntegerDate];
            var end1 = prices[test1.ToIntegerDate];
            double return1 = (end1.Value - start1.Value) / start1.Value;

            // Bear
            var test3 = new StressTest("bear", rpt);
            var start3 = test3.PreDate ? prices.Last(p => p.Key < test3.FromIntegerDate).Value : prices[test3.FromIntegerDate];
            var end3 = prices[test3.ToIntegerDate];
            double return3 = (end3.Value - start3.Value) / start3.Value;

            // Ten Year Return
            var test2 = new StressTest("ten-year", rpt);
            var start2 = prices.ElementAt(prices.Count() - 121);
            var end2 = prices.Last();

            double return2 = Math.Log(end2.Value.Value / start2.Value.Value);
            test2.FromDate = start2.Value.DateFromInteger;
            test2.ToDate = end2.Value.DateFromInteger;

            var series = new BarGraphSeries
            {
                Name = seriesName,
                ColourHex = colourHex,
                PointNames = new string[] { test1.PointName, test3.PointName, test2.PointName },
                Values = new double[] { return1, return3, return2 }
            };

            return series;
        }

        #endregion Chart Series Calculations

        #region XML File Access

        private XElement ReportSpec
        {
            get
            {
                if (reportSpec == null) {
                    reportSpec = XElement.Load(SpecFile);
                }
                return reportSpec;
            }
        }

        private XElement tableSpec(string id)
        {
            var tspec = (from t in ReportSpec.Descendants("table")
                         where t.Attribute("id").Value == id
                         select t).Single();


            return tspec;

        }

        private XElement chartSpec(string id)
        {
            var cspec = (from c in ReportSpec.Descendants("chart")
                         where c.Attribute("id").Value == id
                         select c).Single();

            return cspec;
        }

        #endregion XML file access

        #region Properties

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

        #endregion Properties
    }
}