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
        protected string VideoUrl;
        protected string ImageUrl;
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
                                Query = "<OrderBy><FieldRef Name='" + FieldsName.VideoLibrary.InternalName.Order + "' Ascending='TRUE' /></OrderBy>",
                                RowLimit = 10
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.VideoLibrary);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                DataTable dt = new DataTable();
                                dt.Columns.Add("FileUrl");
                                dt.Columns.Add("ImageUrl");
                                dt.Columns.Add("ID");
                                dt.Columns.Add("Title");
                                foreach (SPListItem item in items)
                                {
                                    SPAttachmentCollection attachs = item.Attachments;
                                    if (attachs.Count > 1)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["ID"] = item.ID;
                                        dr["Title"] = item.Title;
                                        dr["ImageUrl"] = WebUrl + "/Lists/" + ListsName.InternalName.VideoLibrary + "/Attachments/" + item.ID + "/" + attachs[0];
                                        dr["FileUrl"] = WebUrl + "/Lists/" + ListsName.InternalName.VideoLibrary + "/Attachments/" + item.ID + "/" + attachs[1];
                                        dt.Rows.Add(dr);
                                    }
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    VideoUrl = dt.Rows[0]["FileUrl"].ToString();
                                    ImageUrl = dt.Rows[0]["ImageUrl"].ToString();
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
        protected void rptVideo_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                var aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = "javascript:void(0)";
                aLink.Title = Convert.ToString(drv["Title"]);
                var tvCode = "<embed flashvars=\"file=" + Convert.ToString(drv["FileUrl"]) + "&image=" + Convert.ToString(drv["ImageUrl"]) + "&autostart=false\" allowfullscreen=\"true\" allowscripaccess=\"always\" id=\"qn-video-div-player\" name=\"qn-video-div-player\" src=\"" + WebUrl + "/ResourcesList/player.swf\" width=\"286\" />";
                tvCode = tvCode.Replace("\r\n", "");
                tvCode = tvCode.Replace("\n", "");
                tvCode = tvCode.Replace("\r", "");
                tvCode = tvCode.Replace("'", "\\'");
                aLink.Attributes.Add("onclick", string.Format("javascript:setVideoPlay('{0}','{1}');return false;", Convert.ToString(drv["ID"]), tvCode));
            }
        }
    }
}
