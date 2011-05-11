using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

using RSMTenon.Data;

public partial class Pages_StrategicModel_upload : RepGenPage
{
    private static string tbl = "tblStrategicModel";
    private Dictionary<string, AssetClass> assetClasses;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.listModel.DataBind();
        }
        lblStatus.Text = String.Empty;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        var dt = new DataUpload.StrategicModelDataTable();
        string strategyId = this.listModel.SelectedValue;
        assetClasses = AssetClass.GetAssetClasses().ToDictionary(a => a.Name);

        if (uploader.PostedFile.ContentLength != 0) {
            try {
                if (listModel.SelectedValue == String.Empty)
                    throw new ArgumentException("Please select a model from the list.");
                if (uploader.PostedFile.ContentLength > 100000) {
                    lblStatus.Text = "File is too large for upload";
                } else {
                    using (StreamReader sr = new StreamReader(uploader.PostedFile.InputStream)) {
                        string line = null;
                        string[] split = null;
                        char[] sep = { ',' };

                        while ((line = sr.ReadLine()) != null) {
                            if (line.ToLower().StartsWith("asset")) {
                                continue;
                            } else {
                                split = line.Split(sep);
                                addToTypedTable(dt, split, strategyId);
                            }
                        }
                    }

                    decimal totalWeight = dt.Sum(r => r.Field<decimal>("Weighting"));

                    if (totalWeight != 1)
                        throw new Exception(String.Format("Upload Error: Weighting does not total 100% (currently {0:0.00%})", totalWeight));

                    string where = String.Format("StrategyID='{0}'", listModel.SelectedValue);
                    RSMTenon.Data.DataUtilities.UploadToDatabase(dt, tbl, where);
                    lblStatus.Text = String.Format("{0:#,##0} row(s) added to database", dt.Rows.Count);
                }
            } catch (Exception err) {
                lblStatus.Text = err.Message;
            }
        }
    }

    private void addToTypedTable(DataUpload.StrategicModelDataTable dt, string[] fields, string strategyId)
    {
        DataUpload.StrategicModelRow row = dt.NewStrategicModelRow();

        row.StrategyID = strategyId;
        AssetClass assetClass = null;
        assetClasses.TryGetValue(fields[0], out assetClass);

        if (assetClass == null)
            throw new Exception(String.Format("Upload Error: Asset Class '{0}' not recognized.", fields[0]));

        row.GUID = Guid.NewGuid();
        row.AssetClassID = assetClass.ID;
        row.Weighting = Convert.ToDecimal(fields[1]);

        dt.AddStrategicModelRow(row);
    }
}
