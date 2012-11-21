using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class OtherNewsUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        int newsId = 0;
        string catId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptOtherNews.ItemDataBound += new RepeaterItemEventHandler(rptOtherNews_ItemDataBound);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"]!=null&&Request.QueryString["ID"]!=string.Empty)
                {
                    newsId = Convert.ToInt32(Request.QueryString["ID"]);
                }

                if (Request.QueryString["CatId"] != null && Request.QueryString["CatId"] != string.Empty)
                {
                    catId = Convert.ToString(Request.QueryString["CatId"]);
                }
                BindRepeater(newsId, catId);
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater(int newsid,string catid)
        {
            DataTable dt = NewsBO.GetNewsOtherByCatId(CurrentWeb, catid, newsid);
            DataTable dtResult = Utilities.GetNewsWithRowLimit(dt, 5);
            rptOtherNews.DataSource = dtResult;
            rptOtherNews.DataBind();
        }
        #endregion

        #region rptOtherNews_ItemDataBound
        protected void rptOtherNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = Convert.ToString(drv[FieldsName.NewsList.InternalName.Title]);
            }
        }
        #endregion
    }
}
