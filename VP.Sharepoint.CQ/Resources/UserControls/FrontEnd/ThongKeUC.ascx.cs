using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ThongKeUC : FrontEndUC
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
            rptNews.ItemDataBound += new RepeaterItemEventHandler(rptNews_ItemDataBound);
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                {
                    catId = Convert.ToString(Page.Request.QueryString["CatId"]);
                }
                dvCatTitle.InnerText = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", FieldsName.CategoryList.InternalName.Title);
                BindRepeater();                           
            }
        }
        #endregion

        #region Bind Repeater
        protected void BindRepeater()
        {
            DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, catId);
            rptNews.DataSource = dt;
            rptNews.DataBind();

        }
        #endregion

        protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlGenericControl dvDesc = (HtmlGenericControl)e.Item.FindControl("dvDesc");
                aLink.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.InnerText = Convert.ToString(drv[FieldsName.NewsList.InternalName.Title]);
                dvDesc.InnerHtml = Convert.ToString(drv[FieldsName.NewsList.InternalName.Description]);
            }
        }
    }
}
