using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class HotNewsByCatUC : FrontEndUC
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
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["CatId"]!=null&&Page.Request.QueryString["CatId"]!=string.Empty)
                {
                    catId=Convert.ToString((Page.Request.QueryString["CatId"]));
                }
                SPWeb web = SPContext.Current.Web;
                BindRepeater(web);
            }
        }
        #endregion

        #region BindRepeater
        private void BindRepeater(SPWeb spWeb)
        {
            DataTable dt = Utilities.GetNewsByStatus(spWeb, Constants.NewsStatus.HotNews, catId);
            //Bind repeater news slide
            dt = Utilities.GetNewsWithRowLimit(dt, 20);
            rptHotNews.DataSource = dt;
            rptHotNews.DataBind();
        }
        #endregion

        #region rptHotNews_ItemDataBound
        protected void rptHotNews_ItemDataBound(object sender,RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgThumb = (HtmlImage)e.Item.FindControl("imgThumb");
                var imgUrl = Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                if (imgUrl.Contains("http://"))
                {
                    imgThumb.Src = imgUrl;
                }
                else
                {
                    imgThumb.Src = WebUrl + "/" + Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                }
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
            }
        }
        #endregion
    }
}
