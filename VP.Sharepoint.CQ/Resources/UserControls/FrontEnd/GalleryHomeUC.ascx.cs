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
using System.Web.UI.HtmlControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class GalleryHomeUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rptImg.ItemDataBound += new RepeaterItemEventHandler(rptImg_ItemDataBound);
            if (!Page.IsPostBack)
            {
                BindRepeater();
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
                            var query = new SPQuery()
                            {
                                Query = "<Where><Eq><FieldRef Name='DisplayStatus' /><Value Type='Choice'>Hiển thị</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>",    
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.ImageLibrary);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                DataTable dt = items.GetDataTable();
                                dt.Columns.Add("FileUrl");
                                if (dt!=null&&dt.Rows.Count>0)
                                {
                                    int i = 0;
                                    foreach (SPListItem item in items)
                                    {
                                        SPAttachmentCollection attachs = item.Attachments;                                        
                                        if (attachs.Count > 0)
                                        {                                            
                                            dt.Rows[i]["FileUrl"] = WebUrl + "/Lists/" + ListsName.InternalName.ImageLibrary + "/Attachments/" + item.ID + "/" + attachs[0];                                            
                                        }
                                        i++;
                                    }
                                    imgThumb.Src = dt.Rows[0]["FileUrl"].ToString();                                    
                                    dvTitle.InnerText = dt.Rows[0][FieldsName.ImageLibrary.InternalName.Title].ToString();

                                    dt.Rows.Remove(dt.Rows[0]);
                                }
                                rptImg.DataSource = dt;
                                rptImg.DataBind();
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

        protected void rptImg_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.Attributes.Add("onclick", "SwitchImage('" + drv["FileUrl"] + "')");
                aLink.InnerText = drv[FieldsName.ImageLibrary.InternalName.Title].ToString();
            }
        }
    }
}
