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
    public partial class VideoHomeUC : FrontEndUC
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
                                Query = string.Empty
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.VideoLibrary);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                DataTable dt = items.GetDataTable();
                                dt.Columns.Add("FileUrl");
                                int i = 0;
                                foreach (SPListItem item in items)
                                {
                                    SPAttachmentCollection attachs = item.Attachments;
                                    string fileName = attachs[0];
                                    dt.Rows[i]["FileUrl"] = fileName;
                                    i++;
                                }

                                rptVideo.DataSource = dt;
                                rptVideo.DataBind();
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
        protected void rptVideo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");                                
                string fileUrl = WebUrl + "/Lists/" + ListsName.InternalName.VideoLibrary + "/Attachments/" + drv["ID"] + "/" + drv["FileUrl"].ToString();
                aLink.Attributes.Add("onclick", string.Format("PlayVideo('{0}')", fileUrl));

                if (e.Item.ItemIndex==0)
                {
                    ltrVideo.Text =
                       @"<embed
                                  flashvars='file=" + fileUrl + @"&autostart=falsee&dock=true' 
                                  allowfullscreen='true' 
                                  allowscripaccess='always' 
                                    quality='high'
                                  id='player' name='player' type='application/x-shockwave-flash' src= '" + SPContext.Current.Web.Url + "/" + ListsName.InternalName.ResourcesList + "/player.swf" + @"' width='285' height='197' wmode='transparent' />";                    
                }
            }
        }
    }
}
