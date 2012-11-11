using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class BottomMenuUC : FrontEndUC
    {
        protected string HomeUrl;
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            HomeUrl = CurrentWeb.Url;
            if (!Page.IsPostBack)
            {
                MenuBO.BindMenu(CurrentWeb, ListsName.InternalName.MenuList, rptMenu, "Footer menu");
            }
        }
        #endregion

        protected void rptMenu_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
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
                Utilities.SetLinkMenu(CurrentWeb, WebUrl, drv, aLink);
            }
        }
    }
}
