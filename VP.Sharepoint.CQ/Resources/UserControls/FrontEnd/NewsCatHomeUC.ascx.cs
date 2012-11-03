using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using VP.Sharepoint.CQ.Core.WebParts;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class NewsCatHomeUC : FrontEndUC
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
                BindRepeaterCategory();
            }
        }
        #endregion

        #region Bind Repeater Category
        private void BindRepeaterCategory() {
            var parentWebpart = this.Parent as ContainerWebPart;
            NewsBO.BindRepeaterCat(CurrentWeb, rptCate, ListsName.InternalName.CategoryList, parentWebpart.Title);
        }
        #endregion

        protected void rptCate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;

                //Bind rptNews1
                if (e.Item.ItemIndex == 0)
                {
                    DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                    dt = Utilities.GetNewsWithRowLimit(dt, 6);

                    if (dt!=null&&dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ltrFirstNews.Text = string.Format("<div class='img_thumb_News'>" +
                                                           "<img src='{0}' /></div>" +
                                                            "<div class='intro_short_content_News'>" +
                                                            "<a href='{1}'>{2}</a></div>", dr[FieldsName.NewsList.InternalName.ImageThumb], "newsdetail.aspx?ID=" + dr["ID"], dr[FieldsName.NewsList.InternalName.Title]);
                    }

                    if (dt != null && dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[0]);
                        rptNews1.DataSource = dt;
                        rptNews1.DataBind();
                    }
                }

                //Bind rptNews2
                if (e.Item.ItemIndex == 1)
                {
                    DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                    dt = Utilities.GetNewsWithRowLimit(dt, 6);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ltrSecondNews.Text = string.Format("<div class='img_thumb_News'>" +
                                                           "<img src='{0}' /></div>" +
                                                            "<div class='intro_short_content_News'>" +
                                                            "<a href='{1}'>{2}</a></div>", dr[FieldsName.NewsList.InternalName.ImageThumb], "newsdetail.aspx?ID=" + dr["ID"], dr[FieldsName.NewsList.InternalName.Title]);
                    }

                    if (dt != null && dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[0]);
                        rptNews2.DataSource = dt;
                        rptNews2.DataBind();
                    }
                }

                //Bind rptNews3
                if (e.Item.ItemIndex == 2)
                {
                    DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                    dt = Utilities.GetNewsWithRowLimit(dt, 6);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ltrThirdNews.Text = string.Format("<div class='img_thumb_News'>" +
                                                           "<img src='{0}' /></div>" +
                                                            "<div class='intro_short_content_News'>" +
                                                            "<a href='{1}'>{2}</a></div>", dr[FieldsName.NewsList.InternalName.ImageThumb], "newsdetail.aspx?ID=" + dr["ID"], dr[FieldsName.NewsList.InternalName.Title]);
                    }

                    if (dt != null && dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[0]);
                        rptNews3.DataSource = dt;
                        rptNews3.DataBind();
                    }
                }
            }
        }

        protected void rptNews1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = e.Item.FindControl("aLink") as HtmlAnchor;
                aLink.HRef = "../newsdetail.aspx?ID=" + drv["ID"];
            }
        }
        protected void rptNews2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = e.Item.FindControl("aLink") as HtmlAnchor;
                aLink.HRef = "../newsdetail.aspx?ID=" + drv["ID"];
            }
        }
        protected void rptNews3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = e.Item.FindControl("aLink") as HtmlAnchor;
                aLink.HRef = "../newsdetail.aspx?ID=" + drv["ID"];
            }
        }
    }
}
