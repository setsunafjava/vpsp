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
        protected void Page_Load(object sender, EventArgs e)
        {
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
                            SPList list = Utilities.GetCustomListByUrl(CurrentWeb, ListsName.InternalName.DocumentsList);
                            SPQuery query = new SPQuery();
                            query.Query="<OrderBy><FieldRef Name='EffectedDate' Ascending='False' /></OrderBy>";
                            query.RowLimit = 10;
                            SPListItemCollection items = list.GetItems(query);
                            if (items!=null&&items.Count>0)
                            {
                                rptDocument.DataSource = items.GetDataTable();
                                rptDocument.DataBind();
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

        #region rptDocument_ItemDataBound
        protected void rptDocument_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //HtmlGenericControl spDate = (HtmlGenericControl)e.Item.FindControl("spDate");
                //spDate.InnerText = string.Format("(Ngày {0})", Convert.ToDateTime(drv[FieldsName.DocumentsList.InternalName.EffectedDate]).ToString("dd-MM-yyyy"));

            }
        }
        #endregion
    }
}
