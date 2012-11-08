﻿using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using VP.Sharepoint.CQ.Core.WebParts;
using System.Web.UI.WebControls;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ListNewsHomeUC : FrontEndUC
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
                GetCategoryByStatus();
            }
        }
        #endregion

        #region GetCategoryByStatus
        protected void GetCategoryByStatus()
        {
            var parentWebpart = this.Parent as ContainerWebPart;
            var newPos = NewsBO.BoxNewsPosition[parentWebpart.Title];
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Choice'>{1}</Value></Eq><Eq><FieldRef Name='{2}' /><Value Type='Choice'>{3}</Value></Eq></And></Where><OrderBy><FieldRef Name='{4}' /><FieldRef Name='{5}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.NewsPossition, newPos, FieldsName.CategoryList.InternalName.Type, "Tin tức", FieldsName.CategoryList.InternalName.CategoryLevel, FieldsName.CategoryList.InternalName.Order),
                                RowLimit = 1
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                aTitle.InnerText = Convert.ToString(items[0][FieldsName.CategoryList.InternalName.Title]);
                                aTitle.HRef = string.Format("../news.aspx?CatId={0}", items[0][FieldsName.CategoryList.InternalName.CategoryID]);
                                BindRepeaterNews(items[0][FieldsName.CategoryList.InternalName.CategoryID].ToString());
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

        #region Bind Repeater News
        protected void BindRepeaterNews(string catId)
        {
            DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb,catId);
            if (dt!=null&& dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                aImg.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", dr["ID"], dr[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.HRef = aImg.HRef;
                aLink.InnerText = Convert.ToString(dr[FieldsName.NewsList.InternalName.Title]);
                var imgUrl = Convert.ToString(dr[FieldsName.NewsList.InternalName.ImageThumb]);
                if (imgUrl.Contains("http://"))
                {
                    imgNews.Src = imgUrl;
                }
                else
                {
                    imgNews.Src = WebUrl + "/" + Convert.ToString(dr[FieldsName.NewsList.InternalName.ImageThumb]);
                }
                spDesc.InnerText = Utilities.StripHTML(Convert.ToString(dr[FieldsName.NewsList.InternalName.Description]));
            }
            if (dt != null && dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[0]);
                rptNews.DataSource = dt;
                rptNews.DataBind();
            }
        }
        #endregion

        protected void rptNewsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
            }
        }
    }
}
