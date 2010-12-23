using System;
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

        private string sourceXml = @"../../App_Data/content.xml";
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

        public void  GenerateTextContent(Client client)
        {
            string tempDocFile = createTempDocFile();

            GetContent(client);
            string customXml = File.ReadAllText(destinationXml);
            replaceCustomXML(tempDocFile, customXml);


            //Delete the temp file
            //File.Delete(tempFile);
            //File.Delete(destinationXml);
        }

        protected void GetContent(Client client)
        {
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
            xmlnode.InnerText = client.InitialFee.ToString("£#,##0");

            // Strategy
            // get strategy object
            Strategy strategy = client.Strategy;

            // strategy.name-lower
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-lower", nsmgr);
            xmlnode.InnerText = strategy.Name.ToLower(); ;

            // strategy.name-proper
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-proper", nsmgr);
            xmlnode.InnerText = strategy.Name; ;

            // strategy.aim
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:aim", nsmgr);
            xmlnode.InnerText = strategy.Aim;

            // strategy.time-horizon-text
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:time-horizon-text", nsmgr);
            xmlnode.InnerText = strategy.TimeHorizonText;

            // strategy.asset-classes
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:asset-classes", nsmgr);
            xmlnode.InnerText = strategy.AssetClasses;

            // strategy.investor-focus
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:investor-focus", nsmgr);
            xmlnode.InnerText = strategy.InvestorFocus;

            // strategy.performance.return-over-base
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return-over-base", nsmgr);
            //xmlnode.InnerText = strategy.ReturnOverBase;

            // strategy.time-horizon
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:time-horizon", nsmgr);
            xmlnode.InnerText = Enum.GetName(typeof(TimeHorizon), strategy.TimeHorizon);

            // strategy.comparison-chart-header
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:comparison-chart/wmr:header", nsmgr);
            //xmlnode.InnerText = strategy.ComparisonChartHeader;

            // strategy.performance.return -- TO BE CALCULATED FROM DATABASE
            Decimal portReturn = 0.0M;

            switch (client.StrategyID) {
                case "CA": portReturn = 53.1M; break;
                case "CO": portReturn = 46.0M; break;
                case "BA": portReturn = 39.1M; break;
                case "GR": portReturn = 31.8M; break;
                case "AC": portReturn = 29.2M; break;
            }

            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return", nsmgr);
            xmlnode.InnerText = String.Format("{0}", Decimal.Round(portReturn, 1));

            // strategy.performance.rolling-return
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:rolling-return", nsmgr);
            //xmlnode.InnerText = strategy.RollingReturn;

            // strategy.cost
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:cost", nsmgr);
            //xmlnode.InnerText = strategy.Cost;

            // for income strategies only -- GET FROM XML FILE?
            // strategy.income-note1
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note1", nsmgr);
            xmlnode.InnerText = "To ensure that we do not ‘run out’ of cash to make the client’s income payments we will hold one years income requirement as cash either at the wrap provider level (SIPP or OIB) or at James Brearley for non wrap accounts.";

            // strategy.income-note2
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note2", nsmgr);
            xmlnode.InnerText = "YOU MUST EXPLAIN AND DOCUMENT THIS IN THE SUITABILITY LETTER.";


            // Cash or Assets
            string allocationHeader;
            string allocationCaption;
            string compareCaption = null;
            string weightingText = null;
            string stressCrashHeader = null;
            string stressCrashText = null;
            string stressRiseText = null;

            if (client.ExistingAssets) {
                allocationHeader = "The following chart shows how your portfolio is allocated across the major asset classes";
                allocationCaption = "How does this compare to our recommendations and what are the main differences?  The following chart shows where your current portfolio is over or under weight in each asset class compared to that which we would recommend for a Defensive investor.";
                weightingText = "The main issues are your significant overweight position in UK &amp; Global equities and your underweight position in the more cautious asset classes of bonds, hedge funds and commercial property.";
                stressCrashHeader = "How does the risk compare?";
                stressCrashText = "How does this risk impact on a portfolio?  Well, one way of looking at risk is what happens if markets suffer a shock or go through a prolonged downturn.";
                stressCrashText += String.Format(" The following chart shows how the mix of assets you currently have would have performed during a number of these market downturns compared to our {0} Strategy, Global Equity Markets and Government Bonds.", strategy.Name);
                stressRiseText = "RETRIEVE FROM XML FILE";
            } else {
                allocationHeader = String.Format("The following chart shows the current asset allocation mix of our {0} Model", strategy.Name);
                allocationCaption = "One way of understanding the beneficial impact of good asset allocation is to look at what might happen if markets were to suffer a shock or go through a prolonged downturn.   This can best be undertaken by looking at extreme historic events.";
                allocationCaption += String.Format(" The following chart compares the return that would have been delivered by the mix of assets which represents our {0} Strategy and compares it to the returns of UK Government Bonds and Global Equities during a number of these market downturns.", strategy.Name);
            }

            // charts.allocation.header [BOTH]
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:header", nsmgr);
            xmlnode.InnerText = allocationHeader;

            // charts.allocation.caption [BOTH]
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:caption", nsmgr);
            xmlnode.InnerText = allocationCaption;

            if (compareCaption != null) {
                xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:compare-chart/wmr:caption", nsmgr);
                xmlnode.InnerText = compareCaption;
            }

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


            doc.Save(destinationXml);

        }
        private void replaceCustomXML(string fileName, string customXML)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true)) {
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                //Add a new customXML part and then add content
                CustomXmlPart customXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);

                //copy the XML into the new part...
                using (StreamWriter ts = new StreamWriter(customXmlPart.GetStream()))
                    ts.Write(customXML);
            }

        }

        private string createTempDocFile()
        {
            string tempDir = Environment.GetEnvironmentVariable("temp");
            string sourceFile = @"../../App_Data/template1.dotx";
            string tempFile = String.Format("{0}\\{1}.dotx", tempDir, Guid.NewGuid().ToString());
            copyFile(sourceFile, tempFile);

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

        private void createTempXmlFile(string source, string destination)
        {
            copyFile(source, destination);
        }
    }
}
