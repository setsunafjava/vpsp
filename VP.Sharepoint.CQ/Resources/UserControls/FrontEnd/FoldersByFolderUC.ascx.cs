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
        string ParentId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                {
                    catId = Convert.ToString((Page.Request.QueryString["CatId"]));                    
                }

                if (Page.Request.QueryString["ParentId"] != null && Page.Request.QueryString["ParentId"] != string.Empty)
                {
                    ParentId = Convert.ToString((Page.Request.QueryString["ParentId"]));                    
                }
                if (ParentId==string.Empty)
                {
                    ParentId = catId;
                }
                BindRepeater();
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater()
        {
            DataTable dt;
            if (ParentId != string.Empty)
            {
                dt = NewsBO.GetCategoryByParent(CurrentWeb, ParentId);
            }
            else
            {
                dt = NewsBO.GetCategoryByParent(CurrentWeb, catId);
            }
            rptTree.DataSource = dt;
            rptTree.DataBind();
        }
        #endregion

        protected void rptTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                int catLevel = 0;
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                //Literal ltrSubMenu = (Literal)e.Item.FindControl("ltrSubMenu");
                Repeater rptChild = (Repeater)e.Item.FindControl("rptChild1");
                aLink.HRef = "../library.aspx?CatId=" + drv[FieldsName.CategoryList.InternalName.CategoryID] + "&ParentId=" + ParentId;
                //Get child data table
                DataTable dt = NewsBO.GetCategoryByParent(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                if (dt != null && dt.Rows.Count > 0)
                {                   
                    rptChild.DataSource = dt;
                    rptChild.DataBind();
                }
            }
        }


        protected void rptChild1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                Repeater rptChild = (Repeater)e.Item.FindControl("rptChild2");
                aLink.HRef = "../library.aspx?CatId=" + drv[FieldsName.CategoryList.InternalName.CategoryID] + "&ParentId=" + ParentId;
                aLink.InnerText = Convert.ToString(drv[FieldsName.CategoryList.InternalName.Title]);

                DataTable dt = NewsBO.GetCategoryByParent(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                if (dt != null && dt.Rows.Count > 0)
                {                     
                    rptChild.DataSource = dt;
                    rptChild.DataBind();
                }
            }
        }

        protected void rptChild2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                Repeater rptChild = (Repeater)e.Item.FindControl("rptChild3");
                aLink.HRef = "../library.aspx?CatId=" + drv[FieldsName.CategoryList.InternalName.CategoryID] + "&ParentId=" + ParentId;
                aLink.InnerText = Convert.ToString(drv[FieldsName.CategoryList.InternalName.Title]);

                DataTable dt = NewsBO.GetCategoryByParent(CurrentWeb, Convert.ToString(drv[FieldsName.CategoryList.InternalName.CategoryID]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    rptChild.DataSource = dt;
                    rptChild.DataBind();
                }
            }
        }

        protected void rptChild3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                aLink.HRef = "../library.aspx?CatId=" + drv[FieldsName.CategoryList.InternalName.CategoryID] + "&ParentId=" + ParentId;
                aLink.InnerText = Convert.ToString(drv[FieldsName.CategoryList.InternalName.Title]);
            }
        }

        #region Get sub menu
        protected void GetSubMenu(DataTable dt, Literal ltr, ref int catLevel, string parentId)
        {            
            if (dt != null && dt.Rows.Count > 0)
            {
                catLevel = Convert.ToInt32(dt.Rows[0][FieldsName.CategoryList.InternalName.CategoryLevel]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(i==0)
                        ltr.Text += string.Format("<ul><li class=\"submenu\"><a href='library.aspx?CatId={0}&ParentId=" + parentId + "'>{1}</a>", dt.Rows[i][FieldsName.CategoryList.InternalName.CategoryID], dt.Rows[i][FieldsName.CategoryList.InternalName.Title]);
                    else
                        ltr.Text += string.Format("<li class=\"submenu\"><a href='library.aspx?CatId={0}&ParentId=" + parentId + "'>{1}</a>", dt.Rows[i][FieldsName.CategoryList.InternalName.CategoryID], dt.Rows[i][FieldsName.CategoryList.InternalName.Title]);
                    DataTable dtChild = NewsBO.GetCategoryByParent(CurrentWeb, Convert.ToString(dt.Rows[i][FieldsName.CategoryList.InternalName.CategoryID]));
                    GetSubMenu(dtChild, ltr, ref catLevel,parentId);
                }
            }
        }
        #endregion
    }
}
