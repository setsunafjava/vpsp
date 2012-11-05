using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Data;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ListCatsHomeUC : FrontEndUC
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRepeater();
            }
        }
        #region BindRepeater
        protected void BindRepeater()
        {
            NewsBO.BindRepeaterCat(CurrentWeb, rptCat, ListsName.InternalName.CategoryList, "Trang chủ - Chuyên mục giải trí");
        }
        #endregion

        protected void rptCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;                
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../news.aspx.aspx?CatId={0}", drv["ID"], drv[FieldsName.CategoryList.InternalName.CategoryID]);
            }
        }
    }
}
