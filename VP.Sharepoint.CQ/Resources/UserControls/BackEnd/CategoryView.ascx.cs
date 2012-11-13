using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using VP.Sharepoint.CQ.Core.WebControls;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class CategoryView : BaseUserControl
    {
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
                ddlCategory.Items.Add(new ListItem("Tất cả",""));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.News, Constants.CategoryStatus.News));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.Intro, Constants.CategoryStatus.Intro));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.Documents, Constants.CategoryStatus.Documents));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.ImagesGalery, Constants.CategoryStatus.ImagesGalery));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.VideoGalery, Constants.CategoryStatus.VideoGalery));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.NeedToKnow, Constants.CategoryStatus.NeedToKnow));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.Resources, Constants.CategoryStatus.Resources));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.Statistic, Constants.CategoryStatus.Statistic));
                ddlCategory.Items.Add(new ListItem(Constants.CategoryStatus.Organization, Constants.CategoryStatus.Organization));
            }
        }
        #endregion

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
            {
                viewMenu.WhereCondition = "<Where><Eq><FieldRef Name='" + FieldsName.CategoryList.InternalName.Type + "' /><Value Type='Choice'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
            }
            else
            {
                viewMenu.WhereCondition = "";
            }
        }
    }
}
