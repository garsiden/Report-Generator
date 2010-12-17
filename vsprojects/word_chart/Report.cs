using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;

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

        public ChartItem AllocationPieChart()
        {
            XElement rpt = getChartSpec("allocation-pie");
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
                var data1 = ctx.ClientAssetPrice(Client.GUID);
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
            var data4 = ctx.ModelPrice(Client.StrategyID);
            string dataKey4 = StrategyName + " Strategy";
            var rr = getRollingReturn(data4, years);
            lc.AddLineChartSeries(chart, rr, dataKey4, strategyColourHex);

            string ccn = rpt.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;
        }

        private List<ReturnData> getRollingReturn(ISingleResult<ReturnData> data, int years)
        {
            ReturnCalculation calc = new ReturnCalculation();

            var prices = from d in data
                         select new ReturnData {
                             Value = calc.calculatePrice(d),
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
                    Value = calc.calculateRollingReturn(item, from.ElementAt(i++), years),
                    Date = item.Date
                };
                rv.Add(rd);
            }

            return rv;
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
            var spec = (from rep in ChartSpecs.Descendants("report")
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
