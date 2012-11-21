﻿using System;
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
using System.Web;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class SoDoToChucUC : FrontEndUC
    {
        private string currentCatId = string.Empty;
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rptToChuc.ItemDataBound += new RepeaterItemEventHandler(rptToChuc_ItemDataBound);
            currentCatId = HttpContext.Current.Request.QueryString["CatId"];
            if (!Page.IsPostBack)
            {
                Utilities.BindOrganizationToRpt(CurrentWeb, rptToChuc, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryLevel, 
                    "Text", "2", FieldsName.CategoryList.InternalName.Order);
            }
        }
        #endregion

        protected void rptToChuc_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Repeater rptSubToChuc = (Repeater)e.Item.FindControl("rptSubToChuc");
                rptSubToChuc.ItemDataBound += new RepeaterItemEventHandler(rptSubToChuc_ItemDataBound);
                //Bind data to URL
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.Title = Convert.ToString(drv["Title"]);
                aLink.InnerText = Convert.ToString(drv["Title"]);
                aLink.HRef = "javascript:void(0)";
                //Bind data to submenu
                Utilities.BindOrganizationToRpt(CurrentWeb, rptSubToChuc, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.ParentID,
                    "Text", Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]), FieldsName.CategoryList.InternalName.Order);
            }
        }

        void rptSubToChuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltrStyle = (Literal)e.Item.FindControl("ltrStyle");
                if (currentCatId.Equals(Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID])))
                {
                    ltrStyle.Text = "id='initialExpandedMenuItem' style='font-weight:bold;'";
                }
                //Bind data to URL
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.Title = Convert.ToString(drv["Title"]);
                aLink.InnerText = Convert.ToString(drv["Title"]);
                aLink.HRef = WebUrl + "/" + Constants.OrganizationPage + ".aspx?CatId=" + Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]);
            }
        }
    }
}
