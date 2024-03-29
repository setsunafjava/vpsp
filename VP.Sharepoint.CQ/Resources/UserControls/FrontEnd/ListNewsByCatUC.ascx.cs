﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ListNewsByCatUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        string catId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptListNews.ItemDataBound += new RepeaterItemEventHandler(rptListNews_ItemDataBound);
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["CatId"]!=null&&Page.Request.QueryString["CatId"]!=string.Empty)
                {
                    catId=Convert.ToString((Page.Request.QueryString["CatId"]));
                    dvCatTitle.InnerText = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", "Title");

                    BindRepeater();
                }
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater()
        {
            DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, catId);
            dt = Utilities.GetNewsWithRowLimit(dt, 1000);
            rptListNews.DataSource = dt;
            rptListNews.DataBind();
        }
        #endregion

        protected void rptListNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aImg = (HtmlAnchor)e.Item.FindControl("aImg");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgNews = (HtmlImage)e.Item.FindControl("imgNews");
                HtmlGenericControl dvDesc = (HtmlGenericControl)e.Item.FindControl("dvDesc");
                HtmlGenericControl spDate = (HtmlGenericControl)e.Item.FindControl("spDate");                
                

                if (Convert.ToString(drv[FieldsName.NewsList.InternalName.NewsUrl]) == string.Empty)
                {
                    aImg.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                    aLink.HRef = aImg.HRef;
                }
                else
                {
                    aImg.HRef = Convert.ToString(drv[FieldsName.NewsList.InternalName.NewsUrl]);
                    aImg.Target = "_blank";
                    aLink.HRef = aImg.HRef;
                    aLink.Target = aImg.Target;
                }


                aLink.InnerText = drv[FieldsName.NewsList.InternalName.Title].ToString();
                aLink.Title = drv[FieldsName.NewsList.InternalName.Title].ToString();
                //aLink.HRef = aImg.HRef;
                var imgUrl = Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                if (imgUrl.Contains("http://"))
                {
                    imgNews.Src = imgUrl;
                }
                else
                {
                    imgNews.Src = WebUrl + "/" + Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                }
                dvDesc.InnerText = Utilities.StripHTML(Convert.ToString(drv[FieldsName.NewsList.InternalName.Description]));
                //spDate.InnerText = string.Format("(Ngày {0} )", Convert.ToDateTime(drv[FieldsName.NewsList.InternalName.PostedDate]).ToString("dd-MM-yyyy"));
            }
        }
    }
}
