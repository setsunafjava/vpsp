using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class HeaderUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilities.LoadCSS(CurrentWeb, this.Page, "COREV4.CSS");
            Utilities.LoadCSS(CurrentWeb, this.Page, "styles.CSS");
            Utilities.LoadCSS(CurrentWeb, this.Page, "tabcontent.CSS");
            Utilities.LoadCSS(CurrentWeb, this.Page, "simpletree.CSS");
            Utilities.LoadCSS(CurrentWeb, this.Page, "slidedown-menu2.css");
            Utilities.LoadJS(CurrentWeb, this.Page, "jquery-1.7.1.js");
            Utilities.LoadJS(CurrentWeb, this.Page, "tabcontent.js");
            Utilities.LoadJS(CurrentWeb, this.Page, "script.js");
            Utilities.LoadJS(CurrentWeb, this.Page, "simpletreemenu.js");
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("PlaceHolderPageTitle");
            contentPlaceHolder.Controls.Clear();
            LiteralControl control = new LiteralControl();
            control.Text = Constants.HomeTitle;
            var currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            if (!currentUrl.Contains(".aspx") || currentUrl.Contains("default.aspx"))
            {
                control.Text = Constants.HomeTitle;
            }
            else
            {
                var catID = Convert.ToString(Request.QueryString["CatId"]);
                if (!string.IsNullOrEmpty(catID))
                {
                    control.Text += " - " + Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList,
                                   FieldsName.CategoryList.InternalName.CategoryID, catID, "Text", "Title");
                    if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                    {
                        var itemId = Convert.ToString(Request.QueryString["ID"]);
                        control.Text += " - " + Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.NewsList, "ID", itemId, "Counter", "Title");
                    }
                }

            }
            contentPlaceHolder.Controls.Add(control);
        }
    }
}
