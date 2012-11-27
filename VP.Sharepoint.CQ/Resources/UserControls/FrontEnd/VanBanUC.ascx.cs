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
using System.Collections.Generic;
using System.Text;

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
            try
            {
                rptVanBan.ItemDataBound += new RepeaterItemEventHandler(rptVanBan_ItemDataBound);
                ddlCoQuanBanHanh.SelectedIndexChanged += new EventHandler(ddlCoQuanBanHanh_SelectedIndexChanged);
                ddlLinhVuc.SelectedIndexChanged += new EventHandler(ddlLinhVuc_SelectedIndexChanged);
                ddlLoaiVanBan.SelectedIndexChanged += new EventHandler(ddlLoaiVanBan_SelectedIndexChanged);
                ddlNguoiKy.SelectedIndexChanged += new EventHandler(ddlNguoiKy_SelectedIndexChanged);
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

                ImageButton imgDownload = (ImageButton)e.Item.FindControl("imgDownload");
                imgDownload.ImageUrl = DocLibUrl + "/save.png";
                imgDownload.Attributes.Add("onclick", "DownloadFile('" + drv[FieldsName.DocumentsList.InternalName.FilePath] + "')");
                ltrDocumentNo.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.DocumentNo]);
                ltrTitle.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.Title]);
                ltrDivHead.Text = "<div style=\"display: none; border-top: 1px dashed #336666; margin-top: 10px\" class=\"vanban_details\" id='vbId_" + e.Item.ItemIndex + "' >";
                ltrCQ.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.PublishPlace]);
                ltrLoaiVB.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.DocumentType]);
                ltrLinhVuc.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.DocumentSubject]);
                ltrNguoiKy.Text = Convert.ToString(drv[FieldsName.DocumentsList.InternalName.SignaturePerson]);
                if (Convert.ToString(drv[FieldsName.DocumentsList.InternalName.EffectedDate]) != string.Empty)
                {
                    ltrNgayHieuLuc.Text = Convert.ToDateTime(drv[FieldsName.DocumentsList.InternalName.EffectedDate]).ToString("dd/MM/yyyy");
                }
                if (Convert.ToString(drv[FieldsName.DocumentsList.InternalName.ExpiredDate]) != string.Empty)
                {
                    lblNgayHetHieuLuc.Text = Convert.ToDateTime(drv[FieldsName.DocumentsList.InternalName.ExpiredDate]).ToString("dd/MM/yyyy");
                }
                ltrDivBottom.Text = "</div>";
                ltrNgayBanHanh.Text = ltrNgayHieuLuc.Text;
                aLink.Attributes.Add("onclick", string.Format("showDocumentDetail('vbId_{0}');", e.Item.ItemIndex));
            }
        }

        protected void imgDownload_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DownloadFile")
            {
                Utilities.DownloadFile(CurrentWeb, Convert.ToString(e.CommandArgument));
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
                            FilterItem fItem = new FilterItem();

                            if (ddlCoQuanBanHanh.SelectedValue != string.Empty)
                            {
                                fItem.PublishPlace = ddlCoQuanBanHanh.SelectedValue;
                            }
                            if (ddlLinhVuc.SelectedValue != string.Empty)
                            {
                                if (ddlLinhVuc.SelectedValue != string.Empty)
                                {
                                    fItem.DocumentSubject = ddlLinhVuc.SelectedValue;
                                }
                            }

                            if (ddlLoaiVanBan.SelectedValue != string.Empty)
                            {
                                if (ddlLoaiVanBan.SelectedValue != string.Empty)
                                {
                                    fItem.DocumentType = ddlLoaiVanBan.SelectedValue;
                                }
                            }

                            if (ddlNguoiKy.SelectedValue != string.Empty)
                            {
                                if (ddlNguoiKy.SelectedValue != string.Empty)
                                {
                                    fItem.SignaturePerson = ddlNguoiKy.SelectedValue;
                                }
                            }

                            //query += "</Where>";
                            SPQuery q = new SPQuery();
                            q.Query = fItem.CamlQuery;
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
    #region Dynamic calm query building
    public class FilterItem
    {
        public FilterItem()
        {
            this.PublishPlace = string.Empty;
            this.DocumentSubject = string.Empty;
            this.DocumentType = string.Empty;
            this.SignaturePerson = string.Empty;
        }
        // Create properties
        public string PublishPlace
        {
            get;
            set;
        }
        public string DocumentSubject
        {
            get;
            set;
        }
        public string DocumentType
        {
            get;
            set;
        }
        public string SignaturePerson
        {
            get;
            set;
        }
        public string CamlQuery
        {
            get
            {
                List<string> objCaml = new List<string>();
                StringBuilder _caml = new StringBuilder();

                if (!string.IsNullOrEmpty(this.PublishPlace))
                    objCaml.Add(this.CAML_PublishPlace);

                if (!string.IsNullOrEmpty(this.DocumentSubject))
                    objCaml.Add(this.CAML_DocumentSubject);

                if (!string.IsNullOrEmpty(this.DocumentType))
                    objCaml.Add(this.CAML_DocumentType);

                if (!string.IsNullOrEmpty(this.SignaturePerson))
                    objCaml.Add(this.CAML_SignaturePerson);

                for (int i = 1; i < objCaml.Count; i++)
                {
                    _caml.Append("<And>");
                }
                //Now create a string out of the CMAL snippets in the list.
                for (int i = 0; i < objCaml.Count; i++)
                {
                    string snippet = objCaml[i];
                    _caml.AppendFormat(snippet);
                    if (i == 1)
                    {
                        _caml.Append("</And>");
                    }

                    else if (i > 1)
                    {
                        _caml.Append("</And>");
                    }
                }
                string value = string.Empty;
                if (_caml.ToString().Trim().Length > 0)
                    value = string.Format(@"<Where>{0}</Where>", _caml.ToString().Trim());
                //Return the final CAML query
                return value;
            }
        }
        public string CAML_PublishPlace
        {
            get
            {
                return string.Format(@"<Eq>

                                         <FieldRef Name='PublishPlace'/>
                                         <Value Type='Lookup'>{0}</Value>
                                     </Eq>", this.PublishPlace);

            }
        }

        public string CAML_DocumentSubject
        {
            get
            {
                return string.Format(@"<Eq>
                                         <FieldRef Name='DocumentSubject'/>
                                         <Value Type='Lookup'>{0}</Value>
                                     </Eq>", this.DocumentSubject);
            }
        }

        public string CAML_DocumentType
        {
            get
            {
                return string.Format(@"<Eq>
                                         <FieldRef Name='DocumentType'/>
                                         <Value Type='Lookup'>{0}</Value>
                                     </Eq>", this.DocumentType);
            }
        }

        public string CAML_SignaturePerson
        {
            get
            {
                return string.Format(@"<Eq>
                                         <FieldRef Name='SignaturePerson'/>
                                         <Value Type='Lookup'>{0}</Value>
                                     </Eq>", this.SignaturePerson);
            }
        }
    }
    #endregion


}
