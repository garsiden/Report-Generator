using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
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
        private static string nspace = "/wmr:";
        private static string nsprefix = "/wmr:repgen/wmr:";

        public Client Client { get; set; }
        public Report Report { get; set; }

        public TextContent(Report report)
        {
            Report = report;
            Client = Report.Client;
        }

        public void GenerateTextContent(MainDocumentPart mainPart, string contentFileName, Stream tempContentFile)
        {
            var contents = Data.Content.GetContents(Client.StrategyID, Client.ExistingAssets).ToList();
            GetContent(Client, contentFileName, tempContentFile, contents);

            StreamReader sr = new StreamReader(tempContentFile, Encoding.UTF8);
            string customXml = sr.ReadToEnd();
            sr.Close();

            replaceCustomXML(mainPart, customXml);

            // Remove unused text Content Controls
            var all = Data.Content.GetAllContentIDs(Client.StrategyID);
            var unused = all.Except(contents.Select(c => c.ContentID));

            foreach (var id in unused) {
                // try to remove from a run
                if (!ReportGenerator.RemoveContentControlFromRun(mainPart, id))
                    //else try block
                    ReportGenerator.RemoveContentControlFromBlock(mainPart, id);
            }

            // Save doc
            mainPart.Document.Save();
        }

        protected void GetContent(Client client, string sourceXml, Stream outputXml, List<Content> contents)
        {
            // get content from database
            string category = (client.ExistingAssets ? "existing-assets" : "no-existing-assets");
            //var contents = RSMTenon.Data.Content.GetContents(client.StrategyID, category).ToList();

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
            xmlnode.InnerText = strategy.Name.ToLower();

            // strategy.name-proper
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-proper", nsmgr);
            xmlnode.InnerText = strategy.Name;

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
            decimal cost = Model.GetModelCost(strategy.ID, Client.HighNetWorth);
            xmlnode.InnerText = cost.ToString("C0");

            // Look up in tblContent
            foreach (var item in contents) {
                setTextNode(root, nsmgr, item);
            }

            // Calculation
            // strategy.performance.return
            double modelReturn = Report.CalculateModelReturn();
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return", nsmgr);
            xmlnode.InnerText = modelReturn.ToString("0.0%");

            // write file
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

        private double calculateModelReturn(Strategy strategy, string status)
        {
            var prices = strategy.GetStrategyPrices(status);

            double endPrice = prices.Last().Value.Value;
            double startPrice = prices.ElementAt(prices.Count - 121).Value.Value;
            double rtrn = Math.Log(endPrice / startPrice);

            return rtrn;
        }

        private void setTextNode(XmlNode root, XmlNamespaceManager nsmgr, string contentId, Dictionary<string, Content> contents)
        {
            string contentPath = nsprefix + contentId.Replace(".", nspace);
            XmlNode xmlnode = root.SelectSingleNode(contentPath, nsmgr);
            Content item = null;
            contents.TryGetValue(contentId, out item);
            if (item != null)
                xmlnode.InnerText = item.Text.Replace("[Strategy]", Client.Strategy.Name);
        }

        private void setTextNode(XmlNode root, XmlNamespaceManager nsmgr, Content content)
        {
            string contentPath = nsprefix + content.ContentID.Replace(".", nspace);

            XmlNode xmlnode = root.SelectSingleNode(contentPath, nsmgr);
            xmlnode.InnerText = content.Text.Replace("[Strategy]", Client.Strategy.Name);
        }

        private void setTextNode(XmlNode root, string contentPath, XmlNamespaceManager nsmgr, string content)
        {
            XmlNode xmlnode = root.SelectSingleNode(contentPath, nsmgr);
            xmlnode.InnerText = content.Replace("[Strategy]", Client.Strategy.Name);
        }
    }
}
