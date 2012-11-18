using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ToChucDetailUC : FrontEndUC
    {
        public int i = 0;
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
                if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                {
                    string catId = Convert.ToString((Page.Request.QueryString["CatId"]));
                    divName.InnerText = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, 
                        FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", "Title");
                    bName.InnerText = divName.InnerText;
                    fDesc.InnerHtml = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList,
                        FieldsName.CategoryList.InternalName.CategoryID, catId, "Text", FieldsName.CategoryList.InternalName.Description);

                    Utilities.BindOrganizationDetailToRpt(CurrentWeb, rptTC, ListsName.InternalName.ProfilesList, FieldsName.ProfilesList.InternalName.CategoryId,
                    "Text", catId, FieldsName.ProfilesList.InternalName.Order);

                    BindRepeater(catId);

                    BindRepeater(CurrentWeb);
                }
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater(string catId)
        {
            DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, catId);
            dt = Utilities.GetNewsWithRowLimit(dt, 10);
            rptListNews.DataSource = dt;
            rptListNews.DataBind();
        }
        #endregion

        protected void rptListNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aImg = (HtmlAnchor)e.Item.FindControl("aImg");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgNews = (HtmlImage)e.Item.FindControl("imgNews");
                HtmlGenericControl dvDesc = (HtmlGenericControl)e.Item.FindControl("dvDesc");
                HtmlGenericControl spDate = (HtmlGenericControl)e.Item.FindControl("spDate");

                aImg.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.HRef = aImg.HRef;
                var imgUrl = Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                if (imgUrl.Contains("http://"))
                {
                    imgNews.Src = imgUrl;
                }
                else
                {
                    imgNews.Src = WebUrl + "/" + Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                }
                dvDesc.InnerText = Utilities.StripHTML(Convert.ToString(drv[FieldsName.NewsList.InternalName.Description]));
                //spDate.InnerText = string.Format("(Ngày {0} )", Convert.ToDateTime(drv[FieldsName.NewsList.InternalName.PostedDate]).ToString("dd-MM-yyyy"));
            }
        }

        #region Bind source to rptVanBan
        protected void BindRepeater(SPWeb spWeb)
        {
            try
            {
                SPList list = Utilities.GetCustomListByUrl(spWeb, ListsName.InternalName.DocumentsList);
                if (list != null)
                {
                    DataTable dt = list.Items.GetDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rptVanBan.DataSource = Utilities.GetNewsWithRowLimit(dt, 10); ;
                        rptVanBan.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        protected void rptVanBan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.Attributes.Add("onclick", string.Format("showDocumentDetail('vbId_{0}');", e.Item.ItemIndex));
            }
        }
    }
}
