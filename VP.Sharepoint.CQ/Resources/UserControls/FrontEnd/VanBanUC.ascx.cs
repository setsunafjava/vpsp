using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class VanBanUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///         
        public int i = 0;
        static DataTable dt;
        string catId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptVanBan.ItemDataBound += new RepeaterItemEventHandler(rptVanBan_ItemDataBound);
            try
            {
                if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                {
                    catId = Convert.ToString(Page.Request.QueryString["CatId"]);
                }
                if (!Page.IsPostBack)
                {
                    BindDropDownList(CurrentWeb);
                    BindRepeater(CurrentWeb, catId);
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        #region Bind DropDownList
        protected void BindDropDownList(SPWeb spWeb)
        {
            try
            {
                // Bind ddlCoQuanBanHanh
                SPList listCQBH = Utilities.GetCustomListByUrl(spWeb, ListsName.InternalName.PublishPlace);
                if (listCQBH != null)
                {
                    SPListItemCollection itemCollection = listCQBH.Items;
                    DataTable dt = itemCollection.GetDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlCoQuanBanHanh.DataSource = dt;
                        ddlCoQuanBanHanh.DataTextField = FieldsName.PublishPlace.InternalName.Title;
                        ddlCoQuanBanHanh.DataTextField = FieldsName.PublishPlace.InternalName.Title;
                        ddlCoQuanBanHanh.DataBind();
                    }
                    ddlCoQuanBanHanh.Items.Insert(0, new ListItem("-- Cơ quan ban hành --", string.Empty));
                }

                // Bind ddlLoaiVanBan
                SPList listLoaiVB = Utilities.GetCustomListByUrl(spWeb, ListsName.InternalName.DocumentType);
                if (listCQBH != null)
                {
                    SPListItemCollection itemCollection = listLoaiVB.Items;
                    DataTable dt = itemCollection.GetDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlLoaiVanBan.DataSource = dt;
                        ddlLoaiVanBan.DataTextField = FieldsName.DocumentType.InternalName.Title;
                        ddlLoaiVanBan.DataTextField = FieldsName.DocumentType.InternalName.Title;
                        ddlLoaiVanBan.DataBind();
                    }
                    ddlLoaiVanBan.Items.Insert(0, new ListItem("-- Các loại Văn Bản --", string.Empty));
                }

                // Bind ddlLinhVuc
                SPList listLinhVuc = Utilities.GetCustomListByUrl(spWeb, ListsName.InternalName.DocumentSubject);
                if (listCQBH != null)
                {
                    SPListItemCollection itemCollection = listLinhVuc.Items;
                    DataTable dt = itemCollection.GetDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlLinhVuc.DataSource = dt;
                        ddlLinhVuc.DataTextField = FieldsName.DocumentSubject.InternalName.Title;
                        ddlLinhVuc.DataTextField = FieldsName.DocumentSubject.InternalName.Title;
                        ddlLinhVuc.DataBind();
                    }

                    ddlLinhVuc.Items.Insert(0, new ListItem("-- Lĩnh vực --", string.Empty));
                }

                // Bind ddlNguoiKy
                SPList listNguoiKy = Utilities.GetCustomListByUrl(spWeb, ListsName.InternalName.SignaturePerson);
                if (listCQBH != null)
                {
                    SPListItemCollection itemCollection = listNguoiKy.Items;
                    DataTable dt = itemCollection.GetDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlNguoiKy.DataSource = dt;
                        ddlNguoiKy.DataTextField = FieldsName.SignaturePerson.InternalName.Title;
                        ddlNguoiKy.DataTextField = FieldsName.SignaturePerson.InternalName.Title;
                        ddlNguoiKy.DataBind();
                    }
                    ddlNguoiKy.Items.Insert(0, new ListItem("-- Người ký --", string.Empty));
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        #region Bind source to rptVanBan
        protected void BindRepeater(SPWeb spWeb, string catId)
        {
            try
            {
                SPList list = Utilities.GetCustomListByUrl(spWeb, ListsName.InternalName.DocumentsList);
                if (list != null)
                {
                    dt = ResourceLibraryBO.GetDocumentsByCatId(spWeb, catId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rptVanBan.DataSource = dt;
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

        #region SelectedIndexChange

        protected void ddlCoQuanBanHanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDocument();
        }

        protected void ddlLoaiVanBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDocument();
        }

        protected void ddlLinhVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDocument();
        }

        protected void ddlNguoiKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDocument();
        }
        #endregion

        #region FillDocument
        protected void FillDocument()
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            string query = "<Where>";
                            if (ddlCoQuanBanHanh.SelectedValue != string.Empty)
                            {
                                query += string.Format("<And><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq>", FieldsName.DocumentsList.InternalName.PublishPlace, HttpUtility.HtmlEncode(ddlCoQuanBanHanh.SelectedValue));
                                query += string.Format("<IsNotNull><FieldRef Name='{0}' /></IsNotNull></And>", FieldsName.DocumentsList.InternalName.PublishPlace);
                            }
                            if (ddlLinhVuc.SelectedValue != string.Empty)
                            {
                                if (ddlLinhVuc.SelectedValue != string.Empty)
                                {
                                    query += string.Format("<And><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq>", FieldsName.DocumentsList.InternalName.DocumentSubject, HttpUtility.HtmlEncode(ddlLinhVuc.SelectedValue));
                                    query += string.Format("<IsNotNull><FieldRef Name='{0}' /></IsNotNull></And>", FieldsName.DocumentsList.InternalName.DocumentSubject);
                                }
                            }

                            if (ddlLoaiVanBan.SelectedValue != string.Empty)
                            {
                                if (ddlLoaiVanBan.SelectedValue != string.Empty)
                                {
                                    query += string.Format("<And><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq>", FieldsName.DocumentsList.InternalName.DocumentType, HttpUtility.HtmlEncode(ddlLoaiVanBan.SelectedValue));
                                    query += string.Format("<IsNotNull><FieldRef Name='{0}' /></IsNotNull></And>", FieldsName.DocumentsList.InternalName.DocumentType);
                                }
                                else
                                {
                                }
                            }

                            if (ddlNguoiKy.SelectedValue != string.Empty)
                            {
                                if (ddlNguoiKy.SelectedValue != string.Empty)
                                {
                                    query += string.Format("<And><Eq><FieldRef Name='{0}' /><Value Type='Lookup'>{1}</Value></Eq>", FieldsName.DocumentsList.InternalName.SignaturePerson, HttpUtility.HtmlEncode(ddlNguoiKy.SelectedValue));
                                    query += string.Format("<IsNotNull><FieldRef Name='{0}' /></IsNotNull></And>", FieldsName.DocumentsList.InternalName.SignaturePerson);
                                }
                            }

                            query += "</Where>";
                            SPQuery q = new SPQuery();
                            q.Query = query;
                            SPList list = Utilities.GetCustomListByUrl(CurrentWeb, ListsName.InternalName.DocumentsList);
                            DataTable dt = list.GetItems(q).GetDataTable();
                            rptVanBan.DataSource = dt;
                            rptVanBan.DataBind();

                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
        }
        #endregion


    }
}
