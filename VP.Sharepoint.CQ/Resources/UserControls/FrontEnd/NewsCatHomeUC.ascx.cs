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
            rptCate.ItemDataBound += new RepeaterItemEventHandler(rptCate_ItemDataBound);
            rptNews1.ItemDataBound += new RepeaterItemEventHandler(rptNews1_ItemDataBound);
            rptNews2.ItemDataBound += new RepeaterItemEventHandler(rptNews2_ItemDataBound);
            rptNews3.ItemDataBound += new RepeaterItemEventHandler(rptNews3_ItemDataBound);
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
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../news.aspx?CatId={0}", drv[FieldsName.CategoryList.InternalName.CategoryID]);
                aLink.InnerText = drv[FieldsName.NewsList.InternalName.Title].ToString();

                //Bind rptNews1
                if (e.Item.ItemIndex == 0)
                {
                    DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                    dt = Utilities.GetNewsWithRowLimit(dt, 6);

                    if (dt!=null&&dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        var imgUrl = Convert.ToString(dr[FieldsName.NewsList.InternalName.ImageThumb]);
                        if (!imgUrl.Contains("http://"))
                        {
                            imgUrl = WebUrl + "/" + imgUrl;
                        }
                        ltrFirstNews.Text = string.Format("<div class='img_thumb_News'>" +
                                                           "<img src='{0}' /></div>" +
                                                            "<div class='intro_short_content_News'>" +
                                                            "<a href='newsdetail.aspx?ID={1}&CatId={2}'>{3}</a></div>", imgUrl, dr["ID"], dr[FieldsName.NewsList.InternalName.NewsGroup], dr[FieldsName.NewsList.InternalName.Title]);
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
                        var imgUrl = Convert.ToString(dr[FieldsName.NewsList.InternalName.ImageThumb]);
                        if (!imgUrl.Contains("http://"))
                        {
                            imgUrl = WebUrl + "/" + imgUrl;
                        }
                        ltrSecondNews.Text = string.Format("<div class='img_thumb_News'>" +
                                                           "<img src='{0}' /></div>" +
                                                            "<div class='intro_short_content_News'>" +
                                                            "<a href='newsdetail.aspx?ID={1}&CatId={2}'>{3}</a></div>", imgUrl, dr["ID"], dr[FieldsName.NewsList.InternalName.NewsGroup], dr[FieldsName.NewsList.InternalName.Title]);
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
                        var imgUrl = Convert.ToString(dr[FieldsName.NewsList.InternalName.ImageThumb]);
                        if (!imgUrl.Contains("http://"))
                        {
                            imgUrl = WebUrl + "/" + imgUrl;
                        }

                        ltrThirdNews.Text = string.Format("<div class='img_thumb_News'>" +
                                                           "<img src='{0}' /></div>" +
                                                            "<div class='intro_short_content_News'>" +
                                                            "<a href='newsdetail.aspx?ID={1}&CatId={2}'>{3}</a></div>", imgUrl, dr["ID"], dr[FieldsName.NewsList.InternalName.NewsGroup], dr[FieldsName.NewsList.InternalName.Title]);
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
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = drv[FieldsName.NewsList.InternalName.Title].ToString();
            }
        }
        protected void rptNews2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = e.Item.FindControl("aLink") as HtmlAnchor;
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = drv[FieldsName.NewsList.InternalName.Title].ToString();
            }
        }
        protected void rptNews3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = e.Item.FindControl("aLink") as HtmlAnchor;
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = drv[FieldsName.NewsList.InternalName.Title].ToString();
            }
        }
    }
}
