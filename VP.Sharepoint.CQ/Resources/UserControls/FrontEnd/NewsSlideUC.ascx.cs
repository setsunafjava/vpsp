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
            if (!Page.IsPostBack)
            {
                web = SPContext.Current.Web;
                BindRepeater(web);
            }
        }
        #endregion

        #region BindRepeater
        private void BindRepeater(SPWeb spWeb)
        {
            DataTable dt = Utilities.GetNewsByStatus(spWeb, Constants.NewsStatus.HotNews);
            rptNewsHome.DataSource = dt;
            rptNewsHome.DataBind();
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
                    imgNewsHome.Src = Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                    imgNewsHome.Attributes.Remove("rel");
                    imgNewsHome.Attributes.Add("rel", string.Format("<h3>{0}</h3>{1}", drv[FieldsName.NewsList.InternalName.Title], drv[FieldsName.NewsList.InternalName.Description]));
                    aImg.HRef = string.Format("/{0}/?ID={1}", "newsdetail", drv["ID"]);
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
    }
}