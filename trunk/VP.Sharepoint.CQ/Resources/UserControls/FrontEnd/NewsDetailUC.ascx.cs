using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class NewsDetailUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        int itemId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"]!=null&&Request.QueryString["ID"]!=string.Empty)
                {
                    itemId = Convert.ToInt32(Request.QueryString["ID"]);
                }

                // Bind data
                SPListItem item = GetItemByID(itemId);
                if (item!=null)
                {
                    ltrTitle.Text = Convert.ToString(item[FieldsName.NewsList.InternalName.Title]);
                    ltrPostedDate.Text = string.Format("( Ngày {0} )", Convert.ToDateTime(item[FieldsName.NewsList.InternalName.PostedDate]).ToString("dd-MM-yyyy"));
                    ltrContent.Text = Convert.ToString(item[FieldsName.NewsList.InternalName.Content]);
                }
            }
        }
        #endregion

        #region Get item by id
        protected SPListItem GetItemByID(int id)
        {
            SPList newsList = Utilities.GetCustomListByUrl(CurrentWeb, ListsName.InternalName.NewsList);
            if (newsList!=null)
            {
                SPListItem item = newsList.GetItemById(id);
                return item;
            }
            return null;
        }
        #endregion
    }
}
