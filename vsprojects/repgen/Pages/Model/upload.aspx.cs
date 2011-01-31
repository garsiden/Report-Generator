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

public partial class Pages_Model_upload : RepGenPage
{
    private static string tbl = "tblModel";
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
        var dt = new DataUpload.ModelDataTable();
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
                            if (line.StartsWith("SEDOL")) {
                                continue;
                            } else {
                                split = line.Split(sep);
                                addToTypedTable(dt, split, strategyId);
                            }
                        }
                    }
                    string where = String.Format("StrategyID='{0}'", listModel.SelectedValue);
                    RSMTenon.Data.DataUtilities.UploadToDatabase(dt, tbl, where);
                    lblStatus.Text = String.Format("{0:#,##0} row(s) added to database", dt.Rows.Count);
                }
            } catch (Exception err) {
                lblStatus.Text = err.Message;
            }
        }
    }

    private void addToTypedTable(DataUpload.ModelDataTable dt, string[] fields, string strategyId)
    {
        DataUpload.ModelRow row = dt.NewModelRow();

        row.GUID = Guid.NewGuid();
        row.StrategyID = strategyId;
        row.AssetClassID = assetClasses[fields[2]].ID;

        row.SEDOL = fields[0];
        row.InvestmentName = fields[1];
        row.WeightingHNW = Convert.ToDecimal(fields[3]);

        if (strategyId == "TC") {
            row.ExpectedYield = Convert.ToDecimal(fields[4]);
            row.PurchaseCharge = Convert.ToDecimal(fields[5]);
            row.WeightingAffluent = 0;
        } else {
            row.WeightingAffluent = Convert.ToDecimal(fields[4]);
            row.ExpectedYield = Convert.ToDecimal(fields[5]);
            row.PurchaseCharge = Convert.ToDecimal(fields[6]);
        }

        dt.AddModelRow(row);
    }
}
