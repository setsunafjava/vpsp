using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ExternalNewsView : BackEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.InternalName.CategoryLevel);
                viewRSS.WhereCondition = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Eq></Where>";
                viewRSS.EnableAddNewItem = false;

                viewNews.WhereCondition = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Eq></Where>";
                viewNews.EnableAddNewItem = false;
            }
        }
        #endregion

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewRSS.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.ExternalNewsLink.InternalName.NewsGroup + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
            viewNews.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.ExternalNews.InternalName.NewsGroup + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
