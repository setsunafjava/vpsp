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
using VP.Sharepoint.CQ.Core.WebParts;

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
            var parentWebpart = this.Parent as ContainerWebPart;
            var newPos = NewsBO.BoxNewsPosition[parentWebpart.Title];
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Choice'>{1}</Value></Eq><Eq><FieldRef Name='{2}' /><Value Type='Choice'>{3}</Value></Eq></And></Where><OrderBy><FieldRef Name='{4}' /><FieldRef Name='{5}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.NewsPossition, newPos, FieldsName.CategoryList.InternalName.Type, "Tin tức", FieldsName.CategoryList.InternalName.CategoryLevel, FieldsName.CategoryList.InternalName.Order),                                
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                DataTable dt = items.GetDataTable();
                                rptCat.DataSource = dt;
                                rptCat.DataBind();
                            }
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
        }
        #endregion

        protected void rptCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = string.Format("../news.aspx?CatId={0}", drv["ID"], drv[FieldsName.CategoryList.InternalName.CategoryID]);
            }
        }
    }
}
