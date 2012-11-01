using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using System.Data;
using System.Web.UI.HtmlControls;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class HomeHotNewsUC : FrontEndUC
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
                SPWeb web = SPContext.Current.Web;
                BindRepeater(web);
            }
        }
        #endregion

        #region BindRepeater
        private void BindRepeater(SPWeb spWeb)
        {
            DataTable dt = Utilities.GetNewsByStatus(spWeb, Constants.NewsStatus.HotNews);            
            rptHotNews.DataSource = dt;
            rptHotNews.DataBind();
        }
        #endregion

        #region rptHotNews_ItemDataBound
        protected void rptHotNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgThumb = (HtmlImage)e.Item.FindControl("imgThumb");
                imgThumb.Src = "../" + drv[FieldsName.NewsList.InternalName.ImageThumb];
                aLink.HRef = string.Format("../{0}?ID={1}", "newsdetail.aspx", drv["ID"]);
            }
        }
        #endregion
    }
}
