﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using RSMTenon.Data;

namespace RSMTenon.ReportGenerator
{
    class TextContent
    {
        private string destinationXml = @"../../App_Data/content_temp.xml";

        public enum TimeHorizon
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

        public void GenerateTextContent(MainDocumentPart mainPart, Client client, string contentFile)
        {
            GetContent(client, contentFile);
            string customXml = File.ReadAllText(destinationXml);
            replaceCustomXML(mainPart, customXml);
            mainPart.Document.Save();

            //Delete the temp files
            //File.Delete(tempFile);
            //File.Delete(destinationXml);
        }

        protected void GetContent(Client client, string sourceXml)
        {
            // get content from database
            string assets = (client.ExistingAssets ? "existing-assets" : "no-existing-assets");
            var ctx = new RepGenDataContext();
            var contents = ctx.Contents;
            var match = from c in contents
                        where (c.StrategyID.Equals(client.StrategyID) || c.StrategyID.Equals(null)) &&
                          (c.Category.Equals(assets) || c.Category.Equals(null))
                        select c;
            var content = match.ToList();

            // create temp content file
            createTempXmlFile(sourceXml, destinationXml);

            XmlDocument doc = new XmlDocument();
            doc.Load(destinationXml);

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
            xmlnode.InnerText = client.DateIssued.ToString("dd MMMM yyyy");

            // client.time-horizon
            int clientHorizon = client.TimeHorizon;
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:time-horizon", nsmgr);
            xmlnode.InnerText = Enum.GetName(typeof(TimeHorizon), clientHorizon);

            // client.reporting-frequency
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:reporting-frequency", nsmgr);
            xmlnode.InnerText = client.ReportingFrequency;

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
            //xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:cost", nsmgr);
            //xmlnode.InnerText = strategy.Cost.ToString("£#,##0");

            // Look up
            // strategy.aim
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:aim", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.aim").Text;

            // strategy.asset-classes
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:asset-classes", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.asset-classes").Text;

            // strategy.investor-focus
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:investor-focus", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.investor-focus").Text;

            // strategy.comparison-chart-header
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:comparison/wmr:header", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.comparison-chart-header").Text;

            // strategy.income-note1
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note1", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.income-note1").Text;

            // strategy.income-note2
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note2", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "strategy.income-note2").Text;

            // Cash or Assets
            string allocationHeader;
            string allocationCaption;
            string weightingText = null;
            string stressCrashHeader = null;
            string stressCrashText = null;
            string stressRiseText = null;

            if (client.ExistingAssets) {
                allocationHeader = content.Single(c => c.ContentID == "charts.allocation.header").Text;
                allocationCaption = content.Single(c => c.ContentID == "charts.allocation.caption").Text;
                weightingText = content.Single(c => c.ContentID == "charts.allocation.weighting-text").Text;
                stressCrashHeader = content.Single(c => c.ContentID == "charts.stress-crash.header").Text;
                stressCrashText = String.Format(
content.Single(c => c.ContentID == "charts.stress-crash.text" && c.StrategyID == strategy.ID).Text, strategy.Name);
                // TODO: add to text database
                stressRiseText = content.Single(c => c.ContentID == "charts.stress-rise.text").Text;
            } else {
                allocationHeader = String.Format(content.Single(c => c.ContentID == "charts.allocation.header").Text, strategy.Name);
                allocationCaption = String.Format(content.Single(c => c.ContentID == "charts.allocation.caption").Text, strategy.Name);
            }

            // charts.allocation.header [BOTH]
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:header", nsmgr);
            xmlnode.InnerText = allocationHeader;

            // charts.allocation.caption [BOTH]
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:caption", nsmgr);
            xmlnode.InnerText = allocationCaption;

            // charts.allocation.weighting-text [ASSETS]
            if (weightingText != null) {
                xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:weighting-text", nsmgr);
                xmlnode.InnerText = weightingText;
            }

            // charts.stress-crash.header [ASSETS]
            if (stressCrashHeader != null) {
                xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:header", nsmgr);
                xmlnode.InnerText = stressCrashHeader;
            }

            // charts.stress-crash.text [ASSETS]
            if (stressCrashText != null) {
                xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:text", nsmgr);
                xmlnode.InnerText = stressCrashText;
            }

            // charts.stress-rise.text [ASSETS]
            if (stressRiseText != null) {
                xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-rise/wmr:text", nsmgr);
                xmlnode.InnerText = stressRiseText;
            }

            // charts.drawdown.text
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:drawdown/wmr:text", nsmgr);
            xmlnode.InnerText = content.Single(c => c.ContentID == "charts.drawdown.text").Text;

            // Calculation
            // strategy.performance.return
            double modelReturn = calculateModelReturn(client.Strategy);
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return", nsmgr);
            xmlnode.InnerText = modelReturn.ToString("0.0%");

            doc.Save(destinationXml);

        }
        private void replaceCustomXML(MainDocumentPart mainPart, string customXML)
        {
            //using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true)) {
            //    MainDocumentPart mainPart = wordDoc.MainDocumentPart;

            mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

            //Add a new customXML part and then add content
            CustomXmlPart customXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);

            //copy the XML into the new part...
            using (StreamWriter ts = new StreamWriter(customXmlPart.GetStream())) {
                ts.Write(customXML);
            }

        }

        private string createTempDocFile()
        {
            string tempDir = Environment.GetEnvironmentVariable("temp");
            string sourceFile = @"../../App_Data/template1.dotx";
            string tempFile = String.Format("{0}\\{1}.dotx", tempDir, Guid.NewGuid().ToString());
            copyFile2(sourceFile, tempFile);

            return tempFile;
        }

        private void copyFile(string source, string destination)
        {
            // copy file allowing sharing
            FileStream fr = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read);
            int len = (int)fr.Length;
            byte[] data = new byte[len];
            fr.Read(data, 0, len);
            fr.Close();

            FileStream fw = new FileStream(destination, FileMode.Create, FileAccess.Write);
            fw.Write(data, 0, len);
            fw.Close();
        }

        private void copyFile2(string source, string destination)
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

        private void createTempXmlFile(string source, string destination)
        {
            copyFile2(source, destination);
        }

        private double calculateModelReturn(Strategy strategy)
        {
            //var prices = strategy.GetStrategyReturn();

            //double endPrice = prices.Last().Value.Value;
            //double startPrice = prices.ElementAt(prices.Count - 121).Value.Value;

            double rtrn = 0;// = Math.Log(endPrice / startPrice);

            return rtrn;
        }
    }
}
