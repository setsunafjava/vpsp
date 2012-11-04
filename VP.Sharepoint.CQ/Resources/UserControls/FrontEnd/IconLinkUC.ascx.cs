using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class IconLinkUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///         
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindRepeater();
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        #region BindRepeater
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
                            SPList iconLink = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.IconLinkList);
                            SPQuery query = new SPQuery();
                            query.Query = "<OrderBy><FieldRef Name='IConOrder' Ascending='True' /></OrderBy>";

                            SPListItemCollection items = iconLink.GetItems(query);                            
                            DataTable dt = items.GetDataTable();
                            if (dt!=null&&dt.Rows.Count>0)
                            {
                                rptLinkIcon.DataSource = dt;
                                rptLinkIcon.DataBind();
                            }
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
        }
        #endregion

        protected void rptLinkIcon_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                SPListItem item = (SPListItem)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgLink = (HtmlImage)e.Item.FindControl("imgLink");
                aLink.HRef = Convert.ToString(drv[FieldsName.IconLinkList.InternalName.LinkURL]);                
                //SPFile file = (SPFile)drv["Attachments"];
                //imgLink.Src = WebUrl + "/Lists/" + ListsName.InternalName.IconLinkList + "1.gif";
            }
        }
    }
}
