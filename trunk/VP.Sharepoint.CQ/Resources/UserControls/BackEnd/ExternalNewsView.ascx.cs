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
                viewCat.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.CategoryList.InternalName.Type + "' /><Value Type='Text'>Tin tức</Value></Eq></Where>";
            }
        }
        #endregion
    }
}
