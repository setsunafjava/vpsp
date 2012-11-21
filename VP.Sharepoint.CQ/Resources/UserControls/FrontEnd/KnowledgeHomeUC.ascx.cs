using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Data;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class KnowledgeHomeUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rptTiGia.ItemDataBound += new RepeaterItemEventHandler(rptTiGia_ItemDataBound);
            var webUrl = CurrentWeb.ServerRelativeUrl;
            if (webUrl.Equals("/"))
            {
                webUrl = "";
            }
            ltrRoot.Text = "<input type='hidden' id='RootFileUrl' value='" + webUrl + "/" + ListsName.InternalName.WeatherList + "/" + "' />";
            if (!IsPostBack)
            {
                lbKQXS.OnClientClick = "javascript:location.href='http://kqxs.vn';return false;";
                lbBD.OnClientClick = "javascript:location.href='http://bongdaso.vn/livescore.aspx';return false;";

                try
                {
                    string Url = "http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx";
                    DataSet ds = new DataSet();
                    string currencyString = "USD SGD JPY EUR RUB";
                    ds.ReadXml(Url);

                    if (ds.Tables.Count > 0)
                    {

                        DataTable dt = new DataTable();
                        DataTable result = new DataTable("Result");
                        result.Columns.Add(new DataColumn("CurrencyCode"));
                        result.Columns.Add(new DataColumn("Transfer"));
                        result.Columns.Add(new DataColumn("Sell"));
                        DataRow row;
                        dt = ds.Tables["Exrate"];

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (currencyString.Contains(Convert.ToString(dr["CurrencyCode"])))
                            {
                                row = result.NewRow();
                                row["CurrencyCode"] = Convert.ToString(dr["CurrencyCode"]);
                                row["Transfer"] = Convert.ToString(dr["Transfer"]);
                                row["Sell"] = Convert.ToString(dr["Sell"]);

                                result.Rows.Add(row);

                            }
                        }

                        rptTiGia.DataSource = result;
                        rptTiGia.DataBind();
                    }

                }
                catch (Exception)
                {

                }
                try
                {
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Sonla.xml", "Sonla");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Viettri.xml", "Viettri");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Haiphong.xml", "Haiphong");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Hanoi.xml", "Hanoi");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Vinh.xml", "Vinh");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Danang.xml", "Danang");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Nhatrang.xml", "Nhatrang");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/Pleicu.xml", "Pleicu");
                    SaveFileToDocLib("http://vnexpress.net/ListFile/Weather/HCM.xml", "HCM");
                }
                catch (Exception)
                {

                }
            }
        }

        private void SaveFileToDocLib(string url, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)
                        WebRequest.Create(url);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
            request.GetResponse();

            // we will read data via the response stream
            Stream ReceiveStream = response.GetResponseStream();

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        try
                        {
                            using (MemoryStream stream = new MemoryStream())
                            {
                                // Create a 4K buffer to chunk the file

                                byte[] MyBuffer = new byte[4096];

                                int BytesRead;

                                // Read the chunk of the web response into the buffer

                                while (0 < (BytesRead = ReceiveStream.Read(MyBuffer, 0, MyBuffer.Length)))
                                {

                                    // Write the chunk from the buffer to the file

                                    stream.Write(MyBuffer, 0, BytesRead);

                                }
                                web.AllowUnsafeUpdates = true;
                                SPFile file =
                                    web.Files.Add(string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", web.Url,
                                                                ListsName.InternalName.WeatherList, fileName + ".xml"),
                                                  stream, true, "Get latest file", false);
                                file.Item[SPBuiltInFieldId.FileLeafRef] = fileName;
                                web.AllowUnsafeUpdates = true;
                                file.Item.Update();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
            });
        }
        #endregion

        protected void rptTiGia_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltrCurrencyCode = (Literal)e.Item.FindControl("ltrCurrencyCode");
                Literal ltrTransfer = (Literal)e.Item.FindControl("ltrTransfer");
                Literal ltrSell = (Literal)e.Item.FindControl("ltrSell");
                ltrCurrencyCode.Text = Convert.ToString(drv["CurrencyCode"]);
                ltrTransfer.Text = Convert.ToString(drv["Transfer"]);
                ltrSell.Text = Convert.ToString(drv["Sell"]);
            }
        }
    }
}
