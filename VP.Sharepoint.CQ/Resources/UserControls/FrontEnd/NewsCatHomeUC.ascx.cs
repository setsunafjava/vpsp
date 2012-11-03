using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using VP.Sharepoint.CQ.Core.WebParts;
using System.Web.UI.WebControls;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class NewsCatHomeUC : FrontEndUC
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
                BindRepeaterCategory();
            }
        }
        #endregion

        #region Bind Repeater Category
        private void BindRepeaterCategory() {
            var parentWebpart = this.Parent as ContainerWebPart;
            NewsBO.BindRepeaterCat(CurrentWeb, rptCate, ListsName.InternalName.CategoryList, parentWebpart.Title);
        }
        #endregion

        private void rptCate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
            }
        }
    }
}
