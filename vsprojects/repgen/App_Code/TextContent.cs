using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
//using System.Text.RegularExpressions
using DocumentFormat.OpenXml.Packaging;
using RSMTenon.Data;

namespace RSMTenon.ReportGenerator
{
    class TextContent
    {
        private enum TimeHorizon
        {
            one = 1,
            two,
            three,
            four,
            five,
            six,
            seven,
            eight,
            nine,
            ten,
            eleven,
            twelve,
            thirteen,
            fourteen,
            fifteen,
            sixteen,
            seventeen,
            eighteen,
            nineteen,
            twenty
        }

        public Client Client { get; set; }
        public void GenerateTextContent(MainDocumentPart mainPart, Client client, string contentFileName, Stream tempContentFile)
        {
            Client = client;
            GetContent(client, contentFileName, tempContentFile);

            StreamReader sr = new StreamReader(tempContentFile, Encoding.UTF8);
            string customXml = sr.ReadToEnd();
            sr.Close();

            replaceCustomXML(mainPart, customXml);
            mainPart.Document.Save();
        }

        protected void GetContent(Client client, string sourceXml, Stream outputXml)
        {
            // get content from database
            string category = (client.ExistingAssets ? "existing-assets" : "no-existing-assets");
            var contents = RSMTenon.Data.Content.GetContents(client.StrategyID, category).ToDictionary(c => c.ContentID);

            XmlDocument doc = new XmlDocument();
            outputXml.Position = 0;
            doc.Load(outputXml);

            // Create an XmlNamespaceManager to resolve the default namespace.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("wmr", @"http://rmtenon.com/2010/wealth-management-report");

            XmlNode xmlnode;
            XmlElement root = doc.DocumentElement;

            // Client
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client", nsmgr);

            XmlAttribute xmlattr;

            // existing-assets?
            xmlattr = doc.CreateAttribute("existing-assets");
            xmlattr.Value = client.ExistingAssets ? "true" : "false";
            xmlnode.Attributes.Append(xmlattr);

            // client.name
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:name", nsmgr);
            xmlnode.InnerText = client.Name;

            // client.meeting-date
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:meeting-date", nsmgr);
            xmlnode.InnerText = client.MeetingDate.ToString("dd MMMM yyyy");

            // client.time-horizon
            int clientHorizon = client.TimeHorizon;
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:time-horizon", nsmgr);
            xmlnode.InnerText = Enum.GetName(typeof(TimeHorizon), clientHorizon);

            // client.reporting-frequency
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:reporting-frequency", nsmgr);
            xmlnode.InnerText = client.ReportingFrequency;

            // client.initial-fee
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:initial-fee", nsmgr);
            xmlnode.InnerText = (client.InitialFee / 100).ToString("0.00%");

            // Strategy
            // get strategy object
            Strategy strategy = client.Strategy;

            // Strategy properties
            // strategy.name-lower
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-lower", nsmgr);
            xmlnode.InnerText = strategy.Name.ToLower(); ;

            // strategy.name-proper
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-proper", nsmgr);
            xmlnode.InnerText = strategy.Name; ;

            // strategy.performance.return-over-base
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return-over-base", nsmgr);
            xmlnode.InnerText = ((double)strategy.ReturnOverBase / 100).ToString("0%");

            // strategy.time-horizon
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:time-horizon", nsmgr);
            xmlnode.InnerText = Enum.GetName(typeof(TimeHorizon), strategy.TimeHorizon);

            // strategy.performance.rolling-return
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:rolling-return", nsmgr);
            xmlnode.InnerText = (strategy.RollingReturn / 100).ToString("0.0%");

            // strategy.cost
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:cost", nsmgr);
            xmlnode.InnerText = strategy.Cost.ToString("£#,##0");

            // Look up
            // strategy.aim
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:aim", nsmgr);
            //xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.aim").Text;
            setTextNode(root, "/wmr:repgen/wmr:strategy/wmr:aim", nsmgr, "strategy.aim", contents);

            // strategy.asset-classes
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:asset-classes", nsmgr);
            //xmlnode.InnerText = contents.Single(c => c.ContentID == "strategy.asset-classes").Text;
            setTextNode(root, "/wmr:repgen/wmr:strategy/wmr:asset-classes", nsmgr,"strategy.asset-classes", contents);

            // strategy.investor-focus
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:investor-focus", nsmgr);
            //xmlnode.InnerText = contents.Single(c => c.ContentID == "strategy.investor-focus").Text;
            setTextNode(root, "/wmr:repgen/wmr:strategy/wmr:investor-focus", nsmgr, "strategy.investor-focus", contents);

            // strategy.comparison-chart-header
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:comparison/wmr:header", nsmgr);
            //xmlnode.InnerText = contents.Single(c => c.ContentID == "strategy.comparison-chart-header").Text;
            setTextNode(root, "/wmr:repgen/wmr:charts/wmr:comparison/wmr:header", nsmgr, "strategy.comparison-chart-header", contents);

            // strategy.income-note1
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note1", nsmgr);
            //xmlnode.InnerText = contents.Single(c => c.ContentID == "strategy.income-note1").Text;
            setTextNode(root, "/wmr:repgen/wmr:strategy/wmr:income-note1", nsmgr, "strategy.income-note1", contents);


            // strategy.income-note2
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note2", nsmgr);
            //xmlnode.InnerText = contents.Single(c => c.ContentID == "strategy.income-note2").Text;
            setTextNode(root, "/wmr:repgen/wmr:strategy/wmr:income-note2", nsmgr, "strategy.income-note2", contents);

            // Cash or Assets
            string allocationHeader;
            string allocationCaption;
            string weightingText = null;
            string stressCrashHeader = null;
            string stressCrashText = null;
            string stressRiseText = null;

            if (client.ExistingAssets) {
                allocationHeader = contents["charts.allocation.header"].Text;
                allocationCaption = contents["charts.allocation.caption"].Text;
                weightingText = contents["charts.allocation.weighting-text"].Text;
                stressCrashHeader = contents["charts.stress-crash.header"].Text;
                stressCrashText = contents["charts.stress-crash.text"].Text;
                stressRiseText = contents["charts.stress-rise.text"].Text;
            } else {
                allocationHeader = contents["charts.allocation.header"].Text;
                allocationCaption = contents["charts.allocation.caption"].Text;
                stressRiseText = contents["charts.stress-rise.text"].Text;
            }

            // charts.allocation.header [BOTH]
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:header", nsmgr);
            //xmlnode.InnerText = allocationHeader;
            setTextNode(root, "/wmr:repgen/wmr:charts/wmr:allocation/wmr:header", nsmgr, allocationHeader);

            // charts.allocation.caption [BOTH]
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:caption", nsmgr);
            //xmlnode.InnerText = allocationCaption;
            setTextNode(root, "/wmr:repgen/wmr:charts/wmr:allocation/wmr:caption", nsmgr, allocationCaption);

            // charts.allocation.weighting-text [ASSETS]
            if (weightingText != null) {
                //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:weighting-text", nsmgr);
                //xmlnode.InnerText = weightingText;
                setTextNode(root, "/wmr:repgen/wmr:charts/wmr:allocation/wmr:weighting-text", nsmgr, weightingText);
            }

            // charts.stress-crash.header [ASSETS]
            if (stressCrashHeader != null) {
                //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:header", nsmgr);
                //xmlnode.InnerText = stressCrashHeader;
                setTextNode(root, "/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:header", nsmgr, stressCrashHeader);
            }

            // charts.stress-crash.text [ASSETS]
            if (stressCrashText != null) {
                //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:text", nsmgr);
                //xmlnode.InnerText = stressCrashText;
                setTextNode(root, "/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:text", nsmgr, stressCrashText);
            }

            // charts.stress-rise.text [ASSETS]
            if (stressRiseText != null) {
                //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-rise/wmr:text", nsmgr);
                //xmlnode.InnerText = stressRiseText;
                setTextNode(root, "/wmr:repgen/wmr:charts/wmr:stress-rise/wmr:text", nsmgr, stressRiseText);
            }

            // charts.drawdown.text
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:drawdown/wmr:text", nsmgr);
            //xmlnode.InnerText = contents.Single(c => c.ContentID == "charts.drawdown.text").Text;
            setTextNode(root, "/wmr:repgen/wmr:charts/wmr:drawdown/wmr:text", nsmgr, "charts.drawdown.text", contents);

            // Calculation
            // strategy.performance.return
            double modelReturn = calculateModelReturn(client.Strategy);
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return", nsmgr);
            xmlnode.InnerText = modelReturn.ToString("0.0%");

            outputXml.Position = 0;
            StreamWriter sw = new StreamWriter(outputXml, Encoding.UTF8);

            doc.Save(sw);
            outputXml.Position = 0;
        }

        private void replaceCustomXML(MainDocumentPart mainPart, string customXML)
        {
                mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                //Add a new customXML part and then add content
                CustomXmlPart customXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);

                //copy the XML into the new part...
                using (StreamWriter ts = new StreamWriter(customXmlPart.GetStream())) {
                    ts.Write(customXML);
            }
        }

        private double calculateModelReturn(Strategy strategy)
        {
            var prices = strategy.GetStrategyReturn();

            double endPrice = prices.Last().Value.Value;
            double startPrice = prices.ElementAt(prices.Count - 121).Value.Value;

            double rtrn = Math.Log(endPrice / startPrice);

            return rtrn;
        }

        private void setTextNode(XmlNode root, string contentPath, XmlNamespaceManager nsmgr, string contentId, Dictionary<string,Content> contents)
        {
            XmlNode xmlnode = root.SelectSingleNode(contentPath, nsmgr);
            //var item = contents.Single(c => c.ContentID == contentId);
            Content item = null;
            contents.TryGetValue(contentId, out item);
            if (item != null)
                xmlnode.InnerText = item.Text.Replace("[Strategy]", Client.Strategy.Name);
        }

        private void setTextNode(XmlNode root, string contentPath, XmlNamespaceManager nsmgr, string content)
        {
            XmlNode xmlnode = root.SelectSingleNode(contentPath, nsmgr);
            xmlnode.InnerText = content.Replace("[Strategy]", Client.Strategy.Name);
        }

    }
}
