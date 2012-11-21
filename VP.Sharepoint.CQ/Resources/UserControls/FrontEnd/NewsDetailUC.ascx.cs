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
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            if (!Page.IsPostBack)
                            {
                                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                                {
                                    itemId = Convert.ToInt32(Request.QueryString["ID"]);
                                }
                                try
                                {
                                    NewsBO.UpdateViewCount(adminWeb, Convert.ToInt32(itemId));
                                }
                                catch (Exception ex1)
                                {
                                    Utilities.LogToULS(ex1.ToString());
                                }
                                
                                // Bind data
                                SPList newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                                if (newsList != null)
                                {
                                    SPListItem item = newsList.GetItemById(itemId);
                                    if (item != null)
                                    {
                                        ltrTitle.Text = Convert.ToString(item[FieldsName.NewsList.InternalName.Title]);
                                        ltrPostedDate.Text = string.Format("( Ngày {0} )", Convert.ToDateTime(item[FieldsName.NewsList.InternalName.PostedDate]).ToString("dd-MM-yyyy"));
                                        ltrContent.Text = Convert.ToString(item[FieldsName.NewsList.InternalName.Content]);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToULS(ex.ToString());
                        }
                    }
                }
            });
        }
        #endregion         
    }
}
