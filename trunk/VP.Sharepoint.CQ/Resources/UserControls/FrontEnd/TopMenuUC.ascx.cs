using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class TopMenuUC : FrontEndUC
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
                MenuBO.BindMenu(CurrentWeb, ListsName.InternalName.MenuList, rptMenu, "Top menu");
            }
        }        
        #endregion

        protected void rptMenu_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Repeater rptSubMenu = (Repeater)e.Item.FindControl("rptSubMenu");
                rptSubMenu.ItemDataBound += new RepeaterItemEventHandler(rptSubMenu_ItemDataBound);
                //Literal ltrStyle = (Literal)e.Item.FindControl("ltrStyle");
                //var itemUrl = Convert.ToString(drv["MenuUrl"]);
                //var currentUrl = HttpContext.Current.Request.Url.AbsoluteUri + "&";

                //if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                //{
                //    ltrStyle.Text = " class='current'";
                //}
                //else
                //{
                //    var newsId = Request.QueryString[Constants.NewsId];
                //    if (!string.IsNullOrEmpty(newsId))
                //    {
                //        var catValue = Utilities.GetCatsByNewsID(newsId);
                //        foreach (SPFieldLookupValue value in catValue)
                //        {
                //            var catUrl = "/" + Constants.PageInWeb.SubPage + ".aspx?CategoryId=" + value.LookupId + "&";
                //            if (!string.IsNullOrEmpty(itemUrl) && (itemUrl + "&").Contains(catUrl))
                //            {
                //                ltrStyle.Text = " class='current'";
                //                break;
                //            }
                //        }
                //    }
                //}

                //Bind data to URL
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                string catId = Convert.ToString(drv[FieldsName.MenuList.InternalName.CatID]);
                string catType = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", FieldsName.CategoryList.InternalName.Type);
                string pageName = string.Empty;

                switch (catType)
                {
                    case Constants.CategoryStatus.News:
                    case Constants.CategoryStatus.NeedToKnow:                    
                        pageName = Constants.NewsPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Documents:
                        pageName = Constants.DocumentPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Intro:
                        pageName = Constants.AboutPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Resources:
                        pageName = Constants.LibraryPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Statistic:
                        pageName = Constants.StatisticPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Organization:
                        pageName = Constants.OrganizationPage + ".aspx";
                        break;
                    default:
                        break;
                }

                aLink.Title = Convert.ToString(drv["Title"]);
                aLink.InnerText = Convert.ToString(drv["Title"]);
                aLink.HRef = WebUrl + "/" + pageName + "?CatId=" + catId;

                //if (string.IsNullOrEmpty(Convert.ToString(drv["MenuType"])))
                //{
                //    aLink.HRef = itemUrl;
                //    if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                //    {
                //        ltrStyle.Text = " class='current'";
                //    }
                //}
                //else
                //{
                //    aLink.HRef = itemUrl;
                //    var lkMenuType = Utilities.GetMenuType(ListsName.English.MenuList, Convert.ToInt32(drv["ID"]), "MenuType");
                //    var colNameT = string.Empty;
                //    var catNameT = string.Empty;
                //    var newsNameT = string.Empty;
                //    var checkMT = Utilities.CheckMenuType("MenuType", lkMenuType.LookupId, ref colNameT, ref catNameT, ref newsNameT);
                //    if (checkMT)
                //    {
                //        if (!string.IsNullOrEmpty(Convert.ToString(drv[colNameT])))
                //        {
                //            var lkMenu = Utilities.GetMenuType(ListsName.English.MenuList, Convert.ToInt32(drv["ID"]), colNameT);
                //            aLink.HRef = SPContext.Current.Web.Url + "/" + Constants.PageInWeb.SubPage +
                //                         ".aspx?CategoryId=" + lkMenu.LookupId + "&ListCategoryName=" + catNameT +
                //                         "&ListName=" + newsNameT;

                //            var catID = Request.QueryString[Constants.CategoryId];
                //            var catName = Request.QueryString[Constants.ListCategoryName];
                //            var newsName = Request.QueryString[Constants.ListName];
                //            if (!string.IsNullOrEmpty(catID))
                //            {
                //                if (lkMenu.LookupId.ToString().Equals(catID) && catNameT.Equals(catName) && newsNameT.Equals(newsName))
                //                {
                //                    ltrStyle.Text = " class='current'";
                //                }
                //            }
                //        }
                //    }
                //}

                //Bind data to submenu
                MenuBO.BindMenu(CurrentWeb, ListsName.InternalName.MenuList, rptSubMenu, "Top menu", Convert.ToString(drv["MenuID"]));
            }
        }

        void rptSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //Literal ltrStyle = (Literal)e.Item.Parent.Parent.FindControl("ltrStyle");
                //var itemUrl = Convert.ToString(drv["Url"]);
                //var currentUrl = HttpContext.Current.Request.Url.AbsoluteUri + "&";

                //if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                //{
                //    ltrStyle.Text = " class='current'";
                //}
                //else
                //{
                //    var newsId = Request.QueryString[Constants.NewsId];
                //    if (!string.IsNullOrEmpty(newsId))
                //    {
                //        var catValue = Utilities.GetCatsByNewsID(newsId);
                //        foreach (SPFieldLookupValue value in catValue)
                //        {
                //            var catUrl = "/" + Constants.PageInWeb.SubPage + ".aspx?CategoryId=" + value.LookupId + "&";
                //            if (!string.IsNullOrEmpty(itemUrl) && (itemUrl + "&").Contains(catUrl))
                //            {
                //                ltrStyle.Text = " class='current'";
                //                break;
                //            }
                //        }
                //    }
                //}

                //Bind data to URL
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                string catId = Convert.ToString(drv[FieldsName.MenuList.InternalName.CatID]);
                string catType = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", FieldsName.CategoryList.InternalName.Type);
                string pageName = string.Empty;

                switch (catType)
                {
                    case Constants.CategoryStatus.News:
                    case Constants.CategoryStatus.NeedToKnow:
                        pageName = Constants.NewsPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Documents:
                        pageName = Constants.DocumentPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Intro:
                        pageName = Constants.AboutPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Resources:
                        pageName = Constants.LibraryPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Statistic:
                        pageName = Constants.StatisticPage + ".aspx";
                        break;
                    case Constants.CategoryStatus.Organization:
                        pageName = Constants.OrganizationPage + ".aspx";
                        break;
                    default:
                        break;
                }

                aLink.Title = Convert.ToString(drv["Title"]);
                aLink.InnerText = Convert.ToString(drv["Title"]);
                aLink.HRef = WebUrl + "/" + pageName + "?CatId=" + catId;

                //if (string.IsNullOrEmpty(Convert.ToString(drv["MenuType"])))
                //{
                //    aLink.HRef = itemUrl;
                //    if (!string.IsNullOrEmpty(itemUrl) && currentUrl.Contains(itemUrl + "&"))
                //    {
                //        ltrStyle.Text = " class='current'";
                //    }
                //}
                //else
                //{
                //    aLink.HRef = itemUrl;
                //    var lkMenuType = Utilities.GetMenuType(ListsName.English.MenuList, Convert.ToInt32(drv["ID"]), "MenuType");
                //    var colNameT = string.Empty;
                //    var catNameT = string.Empty;
                //    var newsNameT = string.Empty;
                //    var checkMT = Utilities.CheckMenuType("MenuType", lkMenuType.LookupId, ref colNameT, ref catNameT, ref newsNameT);
                //    if (checkMT)
                //    {
                //        if (!string.IsNullOrEmpty(Convert.ToString(drv[colNameT])))
                //        {
                //            var lkMenu = Utilities.GetMenuType(ListsName.English.MenuList, Convert.ToInt32(drv["ID"]), colNameT);
                //            aLink.HRef = SPContext.Current.Web.Url + "/" + Constants.PageInWeb.SubPage +
                //                         ".aspx?CategoryId=" + lkMenu.LookupId + "&ListCategoryName=" + catNameT +
                //                         "&ListName=" + newsNameT;

                //            var catID = Request.QueryString[Constants.CategoryId];
                //            var catName = Request.QueryString[Constants.ListCategoryName];
                //            var newsName = Request.QueryString[Constants.ListName];
                //            if (!string.IsNullOrEmpty(catID))
                //            {
                //                if (lkMenu.LookupId.ToString().Equals(catID) && catNameT.Equals(catName) && newsNameT.Equals(newsName))
                //                {
                //                    ltrStyle.Text = " class='current'";
                //                }
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}
