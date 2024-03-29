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

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class FilesByFolderUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        string catId = string.Empty;
        public int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptResources.ItemDataBound += new RepeaterItemEventHandler(rptResources_ItemDataBound);
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                    {
                        catId = Convert.ToString((Page.Request.QueryString["CatId"]));
                        aTitle.InnerText = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", FieldsName.CategoryList.InternalName.Title);
                        BindRepeater();
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogToULS(ex);
                }
            }
        }
        #endregion

        #region GetFileUrlOfItem
        protected string GetFileUrlOfItem(string itemId)
        {
            string fileUrl = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            SPList resourcesList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.ResourceLibrary);
                            SPListItem item = resourcesList.GetItemById(Convert.ToInt32(itemId));
                            if (item != null)
                            {
                                SPAttachmentCollection attachs = item.Attachments;
                                if (attachs.Count > 0)
                                {
                                    fileUrl = attachs.UrlPrefix + attachs[0];
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return fileUrl;
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater()
        {
            DataTable dt = ResourceLibraryBO.GetResourcesByCatId(CurrentWeb, catId);
            rptResources.DataSource = dt;
            rptResources.DataBind();
        }
        #endregion

        protected void rptResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aImg = (HtmlAnchor)e.Item.FindControl("aImg");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgThumb = (HtmlImage)e.Item.FindControl("imgThumb");
                aImg.HRef = string.Format("../librarydetail.aspx?ID={0}&CatId={1}", Convert.ToString(drv["ID"]), catId);
                imgThumb.Src = "../" + Convert.ToString(drv[FieldsName.ResourceLibrary.InternalName.ImgThumb]);
                //aImg.Target = "_blank";
                aLink.HRef = aImg.HRef;
                aLink.Target = aImg.Target;
                aLink.Title = Convert.ToString(drv[FieldsName.ResourceLibrary.InternalName.Title]);
                aLink.InnerText = Convert.ToString(drv[FieldsName.ResourceLibrary.InternalName.Title]);
                if (e.Item.ItemIndex % 5 == 0)
                {
                    Literal ltrTrUP = (Literal)e.Item.FindControl("ltrTrUP");
                    Literal ltrTrDown = (Literal)e.Item.FindControl("ltrTrDown");
                    ltrTrUP.Text = "<tr>";
                    if (e.Item.ItemIndex > 0)
                    {
                        ltrTrDown.Text = "</tr>";
                    }
                }
            }
        }
    }
}
