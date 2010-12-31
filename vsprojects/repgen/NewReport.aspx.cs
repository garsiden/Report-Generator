using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Xml;
using RSMTenon.Data;

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

    public IEnumerable<Strategy> GetStrategies()
    {
        return getContext().Strategies;
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

        

        //doc.Save(Server.MapPath(@"App_Data/content_temp.xml"));

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
    protected void sourceClient_Inserting(object sender, LinqDataSourceInsertEventArgs e)
    {
        Client client = (Client)e.NewObject;
        client.GUID = Guid.NewGuid();
    }
}
