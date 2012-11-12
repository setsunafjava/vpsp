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
using System.Xml;
using System.ServiceModel.Syndication;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ProfilesView : BackEndUC
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
            }
        }
        #endregion

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewProfiles.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.ProfilesList.InternalName.CategoryId + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
        }
    }
}
