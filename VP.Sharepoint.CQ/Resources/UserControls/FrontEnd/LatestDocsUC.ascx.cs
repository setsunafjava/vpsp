using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class LatestDocsUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public string strHref = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptDocument.ItemDataBound += new RepeaterItemEventHandler(rptDocument_ItemDataBound);
            if (!Page.IsPostBack)
            {
                BindRepeater();
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater()
        {            
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            SPList list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.DocumentsList);
                            SPQuery query = new SPQuery();
                            query.Query="<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                            query.RowLimit = 10;
                            SPListItemCollection items = list.GetItems(query);
                            if (items!=null&&items.Count>0)
                            {
                                rptDocument.DataSource = items.GetDataTable();
                                rptDocument.DataBind();
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });

        }
        #endregion

        #region rptDocument_ItemDataBound
        protected void rptDocument_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                try
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;
                    HtmlAnchor aLinkHref = (HtmlAnchor)e.Item.FindControl("aLinkHref");
                    if (aLinkHref != null)
                    {
                        //aLinkHref.HRef = "../" + drv[FieldsName.DocumentsList.InternalName.FilePath];
                        aLinkHref.Attributes.Add("onclick", "DownloadFile('" + drv[FieldsName.DocumentsList.InternalName.FilePath] + "')");
                        aLinkHref.InnerText = drv[FieldsName.DocumentsList.InternalName.Title].ToString();
                    }  
                }
                catch (Exception ex)
                {
                    Utilities.LogToULS(ex);
                }         
            }
        }
        #endregion
    }
}
