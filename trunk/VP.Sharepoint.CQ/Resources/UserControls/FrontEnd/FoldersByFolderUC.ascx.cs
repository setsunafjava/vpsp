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
    public partial class FoldersByFolderUC : FrontEndUC
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
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                {
                    catId = Convert.ToString((Page.Request.QueryString["CatId"]));
                    BindRepeater();
                }
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater()
        {
            DataTable dt = NewsBO.GetCategoryByParent(CurrentWeb, catId);
            rptTree.DataSource = dt;
            rptTree.DataBind();
        }
        #endregion

        protected void rptTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                //HtmlAnchor aImg = (HtmlAnchor)e.Item.FindControl("aImg");
                Literal ltrSubMenu = (Literal)e.Item.FindControl("ltrSubMenu");
                //Get child data table
                DataTable dt = NewsBO.GetCategoryByParent(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ltrSubMenu.Text += string.Format("<ul><li class=\"submenu\">{0}", dt.Rows[i][FieldsName.CategoryList.InternalName.Title]);
                        DataTable dtChild = NewsBO.GetCategoryByParent(CurrentWeb, Convert.ToString(dt.Rows[i][FieldsName.CategoryList.InternalName.CategoryID]));

                    }
                    string strEnd = "</li></ul>";
                }
            }
        }

        #region Get sub menu
        protected void GetSubMenu(DataTable dt, Literal ltr)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //ltr.Text += string.Format("<ul><li class=\"submenu\">{0}</li></ul>", dt.Rows[i][FieldsName.CategoryList.InternalName.Title]);
            }
        }
        #endregion
    }
}
