using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Data;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class LinkSiteUC : FrontEndUC
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
                lbWebURL.Width = 200;                
                BindDropDownList();
            }
        }
        #endregion

        #region BindDropDownList
        protected void BindDropDownList()
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
                            SPList iconLink = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.WebsiteLink);
                            SPQuery query = new SPQuery();
                            query.Query = "<OrderBy><FieldRef Name='Title' Ascending='True' /></OrderBy>";

                            SPListItemCollection items = iconLink.GetItems(query);
                            DataTable dt = items.GetDataTable();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                lbWebURL.DataSource = dt;
                                lbWebURL.DataTextField = FieldsName.WebsiteLink.InternalName.Title;
                                lbWebURL.DataValueField = FieldsName.WebsiteLink.InternalName.WebURL;
                                lbWebURL.DataBind();
                            }
                            //lbWebURL.Items.Insert(0, new ListItem("--Liên kết website--", string.Empty));
                            lbWebURL.Attributes.Add("onclick", string.Format("RedirectURL('{0}')", lbWebURL.ClientID));
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
        
    }
}
