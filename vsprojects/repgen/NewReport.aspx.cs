using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Xml;
using RSMTenon.ReportGenerator.DataLayer;

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

public partial class NewReport : System.Web.UI.Page
{
    protected RepGenDataContext context;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected RepGenDataContext getContext()
    {
        if (context == null)
            context = new RepGenDataContext();

        return context;
    }

    protected void createReportButton_Click(object sender, EventArgs e)
    {
        string strTemp = Environment.GetEnvironmentVariable("temp");
        string strFileName = String.Format("{0}\\{1}.dotx", strTemp, Guid.NewGuid().ToString());
        File.Copy(Server.MapPath(@"App_Data/suitable1.dotx"), strFileName);

        GetContent();
        string customXml = File.ReadAllText(Server.MapPath(@"App_Data/content_temp.xml"));
        ReplaceCustomXML(strFileName, customXml);

        //return it to the client - we know strFile is updated, so return it
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "attachment; filename=ClientSuitable.dotx");
        Response.AppendHeader("Content-Type", "application/msword");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.TransmitFile(strFileName);
        Response.Flush();
        Response.Close();

        //Delete the temp file
        File.Delete(strFileName);
        File.Delete(Server.MapPath(@"App_Data/content_temp.xml"));
    }

    protected void GetContent()
    {

        // create temp content file
        File.Copy(Server.MapPath(@"App_Data/content.xml"), Server.MapPath(@"App_Data/content_temp.xml"), true);

        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath(@"App_Data/content_temp.xml"));

        // Create an XmlNamespaceManager to resolve the default namespace.
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("wmr", @"http://rmtenon.com/2010/wealth-management-report");

        XmlNode xmlnode;
        XmlElement root = doc.DocumentElement;

        // Client
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client", nsmgr);

        // Add client attributes
        // suitable?
        XmlAttribute xmlattr;
        xmlattr = doc.CreateAttribute("suitable");
        xmlattr.Value = suitableCheck.Checked ? "true" : "false";
        xmlnode.Attributes.Append(xmlattr);

        // existing-assets?
        xmlattr = doc.CreateAttribute("existing-assets");
        xmlattr.Value = existingAssetsCheck.Checked ? "true" : "false";
        xmlnode.Attributes.Append(xmlattr);

        // client.name
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:name", nsmgr);
        xmlnode.InnerText = clientNameText.Text;

        // client.meeting-date
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:meeting-date", nsmgr);
        xmlnode.InnerText = meetingDateText.Text;

        // client.time-horizon
        int clientHorizon = Int32.Parse(timeHorizonText.Text);
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:time-horizon", nsmgr);
        xmlnode.InnerText = Enum.GetName(typeof(TimeHorizon), clientHorizon);

        // client.reporting-frequency
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:reporting-frequency", nsmgr);
        xmlnode.InnerText = reportingFrequencyList.SelectedItem.Text.ToLower();

        // client.initial-fee
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:initial-fee", nsmgr);
        xmlnode.InnerText = initialFeeText.Text;

        // client.investment-type
        string invtype = strategyRadio.SelectedItem.Text;
        if (strategyRadio.SelectedValue != "G")
            invtype += String.Format(" {0}", investmentTypeRadio.SelectedItem.Text);
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:client/wmr:investment-type", nsmgr);
        xmlnode.InnerText = invtype;

        // Strategy
        // get strategy object
        string strategyId = strategyRadio.SelectedValue;
        Strategy strategy = getContext().Strategies.Single(s => s.ID == strategyId);

        // strategy.name-lower
        string strategyName = strategyRadio.SelectedItem.Text;
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-lower", nsmgr);
        xmlnode.InnerText = strategyName.ToLower(); ;

        // strategy.name-proper
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:name-proper", nsmgr);
        xmlnode.InnerText = strategyName; ;

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

        switch (strategyId)
        {
            case "D": portReturn = 53.1M; break;
            case "C": portReturn = 46.0M; break;
            case "B": portReturn = 39.1M; break;
            case "G": portReturn = 31.8M; break;
            case "O": portReturn = 29.2M; break;
        }

        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:return", nsmgr);
        xmlnode.InnerText = String.Format("{0.0}", Decimal.Round(portReturn, 1));

        // strategy.performance.rolling-return
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:performance/wmr:rolling-return", nsmgr);
        //xmlnode.InnerText = strategy.RollingReturn;

        // strategy.cost
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:cost", nsmgr);
        //xmlnode.InnerText = strategy.Cost;

        // for income strategies only -- GET FROM XML FILE?
        if (investmentTypeRadio.SelectedValue == "I")
        {
            // strategy.income-note1
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note1", nsmgr);
            xmlnode.InnerText = "To ensure that we do not ‘run out’ of cash to make the client’s income payments we will hold one years income requirement as cash either at the wrap provider level (SIPP or OIB) or at James Brearley for non wrap accounts.";

            // strategy.income-note2
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:income-note2", nsmgr);
            xmlnode.InnerText = "YOU MUST EXPLAIN AND DOCUMENT THIS IN THE SUITABILITY LETTER.";
        }
        
        // Cash or Assets
        string allocationHeader;
        string allocationCaption;
        string compareCaption = null;
        string weightingText = null;
        string stressCrashHeader = null;
        string stressCrashText = null;
        string stressRiseText = null;

        if (existingAssetsCheck.Checked)
        {
            allocationHeader = "The following chart shows how your portfolio is allocated across the major asset classes";
            allocationCaption = "How does this compare to our recommendations and what are the main differences?  The following chart shows where your current portfolio is over or under weight in each asset class compared to that which we would recommend for a Defensive investor.";
            weightingText = "The main issues are your significant overweight position in UK &amp; Global equities and your underweight position in the more cautious asset classes of bonds, hedge funds and commercial property.";
            stressCrashHeader = "How does the risk compare?";
            stressCrashText = "How does this risk impact on a portfolio?  Well, one way of looking at risk is what happens if markets suffer a shock or go through a prolonged downturn.";
            stressCrashText += String.Format(" The following chart shows how the mix of assets you currently have would have performed during a number of these market downturns compared to our {0} Strategy, Global Equity Markets and Government Bonds.", strategyName);
            stressRiseText = "RETRIEVE FROM XML FILE";
        }
        else
        {
            allocationHeader = String.Format("The following chart shows the current asset allocation mix of our {0} Model", strategyName);
            allocationCaption = "One way of understanding the beneficial impact of good asset allocation is to look at what might happen if markets were to suffer a shock or go through a prolonged downturn.   This can best be undertaken by looking at extreme historic events.";
            allocationCaption += String.Format(" The following chart compares the return that would have been delivered by the mix of assets which represents our {0} Strategy and compares it to the returns of UK Government Bonds and Global Equities during a number of these market downturns.", strategyName);
        }

        // charts.allocation.header [BOTH]
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:header", nsmgr);
        xmlnode.InnerText = allocationHeader;

        // charts.allocation.caption [BOTH]
        xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:caption", nsmgr);
        xmlnode.InnerText = allocationCaption;

        if (compareCaption != null)
        {
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:strategy/wmr:compare-chart/wmr:caption", nsmgr);
            xmlnode.InnerText = compareCaption;
        }

        // charts.allocation.weighting-text [ASSETS]
        if (weightingText != null)
        {
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:allocation/wmr:weighting-text", nsmgr);
            xmlnode.InnerText = weightingText;
        }

        // charts.stress-crash.header [ASSETS]
        if (stressCrashHeader != null)
        {
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:header", nsmgr);
            xmlnode.InnerText = stressCrashHeader;
        }

        // charts.stress-crash.text [ASSETS]
        if (stressCrashText != null)
        {
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-crash/wmr:text", nsmgr);
            xmlnode.InnerText = stressCrashText;
        }

        // charts.stress-rise.text [ASSETS]
        if (stressRiseText != null)
        {
            xmlnode = root.SelectSingleNode("/wmr:repgen/wmr:charts/wmr:stress-rise/wmr:text", nsmgr);
            xmlnode.InnerText = stressRiseText;
        }            
        

        doc.Save(Server.MapPath(@"App_Data/content_temp.xml"));

    }
    protected void ReplaceCustomXML(string fileName, string customXML)
    {
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
        {
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;

            mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

            //Add a new customXML part and then add content
            CustomXmlPart customXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);

            //copy the XML into the new part...
            using (StreamWriter ts = new StreamWriter(customXmlPart.GetStream()))
                ts.Write(customXML);
        }

    }
}
