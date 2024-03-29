﻿using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
    public partial class NewsSlideUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        SPWeb web;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptNewsHome.ItemDataBound += new RepeaterItemEventHandler(rptNewsHome_ItemDataBound);
            rptMoiNhat.ItemDataBound += new RepeaterItemEventHandler(rptMoiNhat_ItemDataBound);
            rptDocNhieu.ItemDataBound += new RepeaterItemEventHandler(rptDocNhieu_ItemDataBound);
            if (!Page.IsPostBack)
            {
                Utilities.LoadJS(CurrentWeb, this.Page, "slide_news.js");
                Utilities.LoadJS(CurrentWeb, this.Page, "slider.js");

                web = SPContext.Current.Web;
                BindRepeater(web);
            }
        }
        #endregion

        #region BindRepeater
        private void BindRepeater(SPWeb spWeb)
        {
            try
            {
                DataTable dt = Utilities.GetNewsByStatus(spWeb, Constants.NewsStatus.HotNews);
                //Bind repeater news slide
                rptNewsHome.DataSource = dt;
                rptNewsHome.DataBind();

                // Bind repeater new news
                DataTable dtMoiNhat = Utilities.GetNewsByStatus(spWeb, Constants.NewsStatus.NewNews);
                dtMoiNhat = Utilities.GetNewsWithRowLimit(dtMoiNhat, 8);
                rptMoiNhat.DataSource = dtMoiNhat;
                rptMoiNhat.DataBind();

                // Bind repeater most read

                DataTable dtMostRead = NewsBO.GetMostViewCount(CurrentWeb);
                dtMostRead = Utilities.GetNewsWithRowLimit(dtMostRead, 8);
                rptDocNhieu.DataSource = dtMostRead;
                rptDocNhieu.DataBind();
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex.ToString());
            }
        }
        #endregion
        protected void rptNewsHome_ItemDataBound(object sender,RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;
                    HtmlImage imgNewsHome = (HtmlImage)e.Item.FindControl("imgNewsHome");
                    HtmlAnchor aImg = (HtmlAnchor)e.Item.FindControl("aImg");
                    // Todo: Need to reset Src property

                    var imgUrl = Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                    if (imgUrl.Contains("http://"))
                    {
                        imgNewsHome.Src = imgUrl;
                    }
                    else
                    {
                        imgNewsHome.Src = WebUrl + "/" + Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                    }
                    imgNewsHome.Attributes.Remove("rel");
                    imgNewsHome.Attributes.Add("rel", string.Format("<h3>{0}</h3>{1}", drv[FieldsName.NewsList.InternalName.Title], Utilities.StripHTML(Convert.ToString(drv[FieldsName.NewsList.InternalName.Description]))));
                    aImg.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }

        protected void rptMoiNhat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = Convert.ToString(drv[FieldsName.NewsList.InternalName.Title]);
            }
        }

        protected void rptDocNhieu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = Convert.ToString(drv[FieldsName.NewsList.InternalName.Title]);
            }
        }
    }
}
