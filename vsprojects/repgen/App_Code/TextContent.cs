using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RSMTenon.Data;

namespace RSMTenon.ReportGenerator
{
    class TextContent
    {
        private static readonly string nspace = "/wmr:";
        private static readonly string nsprefix = "/wmr:repgen/wmr:";
        private static readonly string dateFormat = "d MMMM yyyy";

        public Client Client { get; set; }
        public Report Report { get; set; }

        public TextContent(Report report)
        {
            Report = report;
            Client = Report.Client;
        }

        public void GenerateTextContent(MainDocumentPart mainPart, string contentFileName, Stream tempContentFile, bool fillControls)
        {
            var contents = Data.Content.GetContents(Client.StrategyID, Client.ExistingAssets).ToList();

            if (fillControls) {
                fillContentControlsText(mainPart, contents);
                fillContentControlsClientData(mainPart);
            } else {
                GetContent(Client, contentFileName, tempContentFile, contents);
                StreamReader sr = new StreamReader(tempContentFile, Encoding.UTF8);
                string customXml = sr.ReadToEnd();
                sr.Close();
                replaceCustomXML(mainPart, customXml);
            }

            // Remove unused text Content Controls
            var all = Data.Content.GetAllContentIDs(Client.StrategyID);
            var unused = all.Except(contents.Select(c => c.ContentID));
            removeUnusedContentControls(mainPart, unused);

            // Save doc
            mainPart.Document.Save();
        }

        public void RemoveContentControlByAlias(MainDocumentPart mainPart, string controlAlias)
        {
            // remove Structured Document Tags
            var sdts = mainPart.Document.Descendants<SdtElement>();

            foreach (var sdt in sdts) {
                var alias = sdt.Descendants<SdtAlias>().FirstOrDefault();
                if ((alias != null) && (alias.Val != null) &&
                  (alias.Val.HasValue) && (alias.Val.Value == controlAlias)) {
                    sdt.Remove();
                }
            }
        }

        private void removeUnusedContentControls(MainDocumentPart mainPart, IEnumerable<string> unused)
        {
            foreach (var id in unused) {
                RemoveContentControlByAlias(mainPart, id);
            }
        }

        #region Content Controls
        private void fillContentControlsText(MainDocumentPart mainPart, List<Data.Content> contents)
        {
            foreach (var c in contents) {
                List<SdtElement> sdtList = findSdtByTag(mainPart, c.ContentID);
                replaceSdtText(sdtList, c.Text.Replace("[Strategy]", Client.Strategy.Name));
            }
        }

        private void fillContentControlsClientData(MainDocumentPart mainPart)
        {
            List<SdtElement> sdtList;

            // client.date-issued
            sdtList = findSdtByTag(mainPart, "client.date-issued");
            replaceSdtText(sdtList, Client.DateIssued.ToString(dateFormat));

            // client.name
            sdtList = findSdtByTag(mainPart, "client.name");
            replaceSdtText(sdtList, Client.Name);

            // client.time-horizon
            sdtList = findSdtByTag(mainPart, "client.time-horizon");
            replaceSdtText(sdtList, Enum.GetName(typeof(TimeHorizon), Client.TimeHorizon));

            // client.reporting-frequency
            sdtList = findSdtByTag(mainPart, "client.reporting-frequency");
            replaceSdtText(sdtList, Client.ReportingFrequency);

            // Strategy
            // get strategy object
            Strategy strategy = Client.Strategy;

            // Strategy properties
            // strategy.name-lower
            sdtList = findSdtByTag(mainPart, "client.strategy.name-lower");
            replaceSdtText(sdtList, strategy.Name.ToLower());

            // client.strategy.name-proper
            sdtList = findSdtByTag(mainPart, "client.strategy.name-proper");
            replaceSdtText(sdtList, strategy.Name);

            // strategy.performance.return-over-base
            sdtList = findSdtByTag(mainPart, "strategy.performance.return-over-base");
            replaceSdtText(sdtList, strategy.ReturnOverBase.ToString("0\\%"));

            // strategy.time-horizon
            sdtList = findSdtByTag(mainPart, "strategy.time-horizon");
            replaceSdtText(sdtList, Enum.GetName(typeof(TimeHorizon), strategy.TimeHorizon));

            // strategy.performance.rolling-return
            sdtList = findSdtByTag(mainPart, "strategy.performance.rolling-return");
            replaceSdtText(sdtList, strategy.RollingReturn.ToString("0.0\\%"));

            // strategy.aggregate-charge
            sdtList = findSdtByTag(mainPart, "strategy.aggregate-charge");
            replaceSdtText(sdtList, strategy.AggregateCharge.ToString("0.00\\%"));

            // strategy.cost NOT USED
            //sdtList = findSdtByTag(mainPart, "strategy.cost");
            //decimal cost = Model.GetModelCost(strategy.ID, Client.HighNetWorth);
            //replaceSdtText(sdtList, cost.ToString("C0"));

            // Calculation
            // strategy.performance.return NOT USED
            //sdtList = findSdtByTag(mainPart, "strategy.performance.return");
            //double modelReturn = Report.CalculateModelReturn();
            //replaceSdtText(sdtList, modelReturn.ToString("0.0%"));

        }

        private void replaceSdtText(List<SdtElement> sdtList, string text)
        {
            foreach (var sdt in sdtList) {
                Text t = sdt.Descendants<Text>().First();
                t.Text = text;
            }
        }

        private List<SdtElement> findSdtByTag(MainDocumentPart mainPart, string tagName)
        {
            // find all Structured Document Tags (Block or Run)
            List<SdtElement> sdtList = mainPart.Document.Descendants<SdtElement>()
                    .Where(s => tagName
                    .Contains
                    (s.SdtProperties.GetFirstChild<Tag>().Val.Value)).ToList();

            return sdtList;
        }

        private void replaceSdtWithText(List<SdtElement> sdtList, string text)
        {
            foreach (var sdt in sdtList) {
                OpenXmlElement elem = null;
                var parent = sdt.Parent;

                if (sdt.GetType() == typeof(SdtBlock)) {
                    elem = new Paragraph();
                    Run run = elem.AppendChild(new Run());
                    run.AppendChild(new Text(text));
                } else {
                    elem = new Run();
                    elem.AppendChild(new Text(text));
                }

                parent.InsertAfter(elem, sdt);
                sdt.Remove();
            }
        }

        #endregion Content Controls

        #region Custom XML

        protected void GetContent(Client client, string sourceXml, Stream outputXml, List<Content> contents)
        {
            string category = (client.ExistingAssets ? "existing-assets" : "no-existing-assets");

            XmlDocument doc = new XmlDocument();
            outputXml.Position = 0;
            doc.Load(outputXml);

            // Create an XmlNamespaceManager to resolve the default namespace.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("wmr", @"http://rmstenon.com/2010/wealth-management-report");

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

            // client.date-issued
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:date-issued", nsmgr);
            xmlnode.InnerText = client.DateIssued.ToString(dateFormat);

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
            xmlnode.InnerText = strategy.Name.ToLower();

            // strategy.name-proper
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-proper", nsmgr);
            xmlnode.InnerText = strategy.Name;

            // strategy.performance.return-over-base
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return-over-base", nsmgr);
            xmlnode.InnerText = strategy.ReturnOverBase.ToString("0\\%");

            // strategy.time-horizon
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:time-horizon", nsmgr);
            xmlnode.InnerText = Enum.GetName(typeof(TimeHorizon), strategy.TimeHorizon);

            // strategy.performance.rolling-return
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:rolling-return", nsmgr);
            xmlnode.InnerText = strategy.RollingReturn.ToString("0.0\\%");

            // strategy.aggregate-charge
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:aggregate-charge", nsmgr);
            xmlnode.InnerText = strategy.AggregateCharge.ToString("0.00\\%");

            // strategy.cost
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:cost", nsmgr);
            decimal cost = TacticalModel.GetCost(strategy.ID, Client.HighNetWorth);
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

        #endregion Custom XML

        private double calculateStrategicModelReturn(Strategy strategy)
        {
            var prices = strategy.GetStrategyPrices();

            double endPrice = prices.Last().Value.Value;
            double startPrice = prices.ElementAt(prices.Count - 121).Value.Value;
            double rtrn = Math.Log(endPrice / startPrice);

            return rtrn;
        }

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
    }
}
