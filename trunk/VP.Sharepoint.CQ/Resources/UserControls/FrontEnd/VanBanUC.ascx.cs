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
        SPWeb web;
        public int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    web = SPContext.Current.Web;
                    BindDropDownList(web);
                    BindRepeater(web);
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
            if (e.Item.ItemType.Equals(ListItemType.Item)||e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.Attributes.Add("onclick", string.Format("showDocumentDetail('vbId_{0}');", i));
            }
        }
    }
}
