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
    public partial class ShouldToKnowUC : FrontEndUC
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
                BindRepeater();   
            }
        }
        #endregion

        #region BindRepeater
        protected void BindRepeater()
        {
            //Bind to category
            DataTable dt = NewsBO.GetCategoryByStatus(CurrentWeb, Constants.CategoryStatus.NeedToKnow, 10);
            rptCat.DataSource = dt;
            rptCat.DataBind();
            // Bind to news
            DataTable dtNews = Utilities.GetNewsByStatus(CurrentWeb, Constants.NewsStatus.ShouldKnowNews);
            rptNews.DataSource = dtNews;
            rptNews.DataBind();
        }
        #endregion

        protected void rptCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltrAdd = (Literal)e.Item.FindControl("ltrAdd");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../news?CatId={0}", drv[FieldsName.CategoryList.InternalName.CategoryID]);
                if (e.Item.ItemIndex!=rptCat.Items.Count-2)                
                {
                    ltrAdd.Text = "|";
                }
            }
        }

        protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aTitle = (HtmlAnchor)e.Item.FindControl("aTitle");
                HtmlAnchor aDesc = (HtmlAnchor)e.Item.FindControl("aDesc");
                HtmlAnchor aImg= (HtmlAnchor)e.Item.FindControl("aImg");

                if (Convert.ToString(drv[FieldsName.NewsList.InternalName.NewsUrl]) != string.Empty)
                {
                    aTitle.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                    aImg.HRef = aTitle.HRef;
                    aDesc.HRef = aTitle.HRef;
                }
                else
                {
                    aTitle.HRef = Convert.ToString(drv[FieldsName.NewsList.InternalName.NewsUrl]);
                    aImg.HRef = aTitle.HRef;
                    aDesc.HRef = aTitle.HRef;
                }
            }
        }
    }
}
