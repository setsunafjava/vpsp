using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class AboutUC : FrontEndUC
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
                if (Page.Request.QueryString["CatId"]!=null&&Page.Request.QueryString["CatId"]!=string.Empty)
                {
                    catId = Convert.ToString(Page.Request.QueryString["CatId"]);
                }

                DataTable dt = NewsBO.GetNewsByCatId(CurrentWeb, catId);
                dt = Utilities.GetNewsWithRowLimit(dt, 1);

                if (dt!=null&&dt.Rows.Count>0)
                {
                    dvCatTitle.InnerText = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, Convert.ToString(dt.Rows[0][FieldsName.NewsList.InternalName.NewsGroup]), "Text", FieldsName.CategoryList.InternalName.Title);
                    dvContent.InnerText = Convert.ToString(dt.Rows[0][FieldsName.NewsList.InternalName.Content]);
                }
            }
        }
        #endregion
    }
}
