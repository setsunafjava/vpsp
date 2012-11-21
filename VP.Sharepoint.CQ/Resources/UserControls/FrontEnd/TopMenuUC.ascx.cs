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
            rptMenu.ItemDataBound += new RepeaterItemEventHandler(rptMenu_ItemDataBound);
            aHome.HRef = CurrentWeb.Url;
            var currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            if (!currentUrl.Contains(".aspx") || currentUrl.Contains("default.aspx"))
            {
                aHome.Attributes.Add("class", "current");
            }
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
                Literal ltrStyle = (Literal)e.Item.FindControl("ltrStyle");
                //Bind data to URL
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                Utilities.SetLinkMenu(CurrentWeb, HttpContext.Current, WebUrl, drv, aLink, ltrStyle, false);
                //Bind data to submenu
                MenuBO.BindMenu(CurrentWeb, ListsName.InternalName.MenuList, rptSubMenu, "Top menu", Convert.ToString(drv["MenuID"]));
            }
        }

        void rptSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltrStyle = (Literal)e.Item.Parent.Parent.FindControl("ltrStyle");
                //Bind data to URL
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                Utilities.SetLinkMenu(CurrentWeb, HttpContext.Current, WebUrl, drv, aLink, ltrStyle, true);
            }
        }
    }
}
