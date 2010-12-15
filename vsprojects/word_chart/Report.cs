using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.Linq;
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
        //private string specPath = "chart-spec2";
        private XElement specs = null;
        private static string SPEC_FILE = @"C:\Documents and Settings\garsiden\My Documents\svn\repgen\vsprojects\word_chart\chart-spec2.xml";
        private List<AssetClass> assetClasses = null;

        public ChartItem RollingReturnChart(int years)
        {
            // get chart specs
            XElement sp = getChartSpecs();
            string id = null;
            switch (years) {
                case 1: id = "rolling-return-1yr"; break;
                case 2: id = "rolling-return-2yr"; break;
                case 3: id = "rolling-return-3yr"; break;
            }

            var rrc = (from rep in sp.Descendants("report")
                       where rep.Attribute("id").Value == id
                       select rep).Single();

            // set title
            string title = rrc.Element("title").Value;

            // get asset classes to plot (defalult in UKGB and GLEQ)
            var assets = getAssetClasses();

            var ctx = new RepGenDataContext();

            // first asset class
            var data1 = ctx.RollingReturn(years, assetClasses[0].ID);
            string dataKey1 = assetClasses[0].Name;

            // create chart (requires one intital data series)
            RollingReturnLineChart lc = new RollingReturnLineChart();
            C.Chart chart = lc.GenerateChart(title, data1.ToList(), dataKey1);

            // add second data series
            var data2 = ctx.RollingReturn(years, assetClasses[1].ID);
            string dataKey2 = assetClasses[1].Name;
            lc.AddLineChartSeries(chart, data2.ToList(), dataKey2);

            // add appropriate strategy data if cash
            ReturnCalculation calc = new ReturnCalculation();

            if (!Client.ExistingAssets) {
                var rr = getModelRollingReturn(Client.StrategyId, years);
                string strategyName = Strategy.GetStrategyNameFromId(Client.StrategyId);
                lc.AddLineChartSeries(chart, rr, strategyName + " Strategy");
            }

            string ccn = rrc.Element("control-name").Value;
            ChartItem chartItem = new ChartItem { Chart = chart, Title = title, CustomControlName = ccn };

            return chartItem;
        }

        private List<ReturnData> getModelRollingReturn(string strategyId, int years)
        {
            var ctx = new RepGenDataContext();
            ReturnCalculation calc = new ReturnCalculation();

            // model calculate prices
            ctx.ModelPrice("CO");
            var data = ctx.ModelPrice(strategyId);

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

        private XElement getChartSpecs()
        {
            if (specs == null) {
                specs = XElement.Load(SPEC_FILE);
            }

            return specs;
        }

        private List<AssetClass> getAssetClasses()
        {
            if (assetClasses == null) {
                var ctx = new RepGenDataContext();
                var allClasses = ctx.AssetClasses.ToDictionary(a => a.ID);
                var assets = from spec in getChartSpecs().Descendants("asset-class")
                             select spec;

                var classes = new List<AssetClass>();
                foreach (var a in assets) {
                    classes.Add(new AssetClass() { ID = a.Value, Name = allClasses[a.Value].Name });
                }
                assetClasses = classes;
            }
            return assetClasses;

        }
    }
}
