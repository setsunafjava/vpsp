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
            rptTC.ItemDataBound += new RepeaterItemEventHandler(rptTC_ItemDataBound);
            rptListNews.ItemDataBound += new RepeaterItemEventHandler(rptListNews_ItemDataBound);
            rptVanBan.ItemDataBound += new RepeaterItemEventHandler(rptVanBan_ItemDataBound);
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
                aLink.InnerText = Convert.ToString(drv["Title"]);
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
                HtmlAnchor aDownload = (HtmlAnchor)e.Item.FindControl("aDownload");
                Literal ltrDocumentNo = (Literal)e.Item.FindControl("ltrDocumentNo");
                Literal ltrTitle = (Literal)e.Item.FindControl("ltrTitle");
                Literal ltrDivHead = (Literal)e.Item.FindControl("ltrDivHead");
                Literal ltrCQ = (Literal)e.Item.FindControl("ltrCQ");
                Literal ltrLoaiVB = (Literal)e.Item.FindControl("ltrLoaiVB");
                Literal ltrLinhVuc = (Literal)e.Item.FindControl("ltrLinhVuc");
                Literal ltrNguoiKy = (Literal)e.Item.FindControl("ltrNguoiKy");
                Literal ltrNgayHieuLuc = (Literal)e.Item.FindControl("ltrNgayHieuLuc");
                Literal lblNgayHetHieuLuc = (Literal)e.Item.FindControl("lblNgayHetHieuLuc");
                Literal ltrDivBottom = (Literal)e.Item.FindControl("ltrDivBottom");
                Literal ltrNgayBanHanh = (Literal)e.Item.FindControl("ltrNgayBanHanh");

                HtmlImage imgDownload = (HtmlImage)e.Item.FindControl("imgDownload");
                imgDownload.Src = DocLibUrl + "/save.png";

                ltrDocumentNo.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.DocumentNo]);
                ltrTitle.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.Title]);
                ltrDivHead.Text = "<div style=\"display: none; border-top: 1px dashed #336666; margin-top: 10px\" class=\"vanban_details\" id='vbId_" + e.Item.ItemIndex + "' >";
                ltrCQ.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.PublishPlace]);
                ltrLoaiVB.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.DocumentType]);
                ltrLinhVuc.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.DocumentSubject]);
                ltrNguoiKy.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.SignaturePerson]);
                ltrNgayHieuLuc.Text = Convert.ToDateTime(drv[FieldsName.DocumentsList.InternalName.EffectedDate]).ToString("dd/MM/yyyy");
                lblNgayHetHieuLuc.Text = Convert.ToDateTime(drv[FieldsName.DocumentsList.InternalName.ExpiredDate]).ToString("dd/MM/yyyy");
                ltrDivBottom.Text = "</div>";
                ltrNgayBanHanh.Text = ltrNgayHieuLuc.Text;



                aLink.Attributes.Add("onclick", string.Format("showDocumentDetail('vbId_{0}');", e.Item.ItemIndex));
                aDownload.HRef = "../" + drv[FieldsName.DocumentsList.InternalName.FilePath];
            }
        }

        protected void rptTC_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltrPosition = (Literal)e.Item.FindControl("ltrPosition");
                Literal ltrTitle = (Literal)e.Item.FindControl("ltrTitle");
                Literal ltrDescription = (Literal)e.Item.FindControl("ltrDescription");
                Literal ltrMobile = (Literal)e.Item.FindControl("ltrMobile");
                Literal ltrEmail = (Literal)e.Item.FindControl("ltrEmail");
                ltrPosition.Text = Convert.ToString(drv["Position"]);
                ltrTitle.Text = Convert.ToString(drv["Title"]);
                ltrDescription.Text = Convert.ToString(drv["Description"]);
                ltrMobile.Text = Convert.ToString(drv["Mobile"]);
                ltrEmail.Text = Convert.ToString(drv["Email"]);
            }
        }
    }
}
