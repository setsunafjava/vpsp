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
    public partial class InitUC : FrontEndUC
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
                Utilities.LoadCSS(CurrentWeb, this.Page, "COREV4.CSS");
                Utilities.LoadCSS(CurrentWeb, this.Page, "styles.CSS");
                Utilities.LoadCSS(CurrentWeb, this.Page, "tabcontent.CSS");
                Utilities.LoadCSS(CurrentWeb, this.Page, "simpletree.CSS");
                Utilities.LoadCSS(CurrentWeb, this.Page, "slidedown-menu2.css");
                Utilities.LoadJS(CurrentWeb, this.Page, "jquery-1.7.1.js");
                Utilities.LoadJS(CurrentWeb, this.Page, "tabcontent.js");
                Utilities.LoadJS(CurrentWeb, this.Page, "script.js");
                Utilities.LoadJS(CurrentWeb, this.Page, "simpletreemenu.js");
                ltrIE6.Text = "<link rel=\"stylesheet\" type=\"text/css\" href='" + DocLibUrl + "/ie6.css' />";
                ltrIE7.Text = "<link rel=\"stylesheet\" type=\"text/css\" href='" + DocLibUrl + "/ie7.css' />";               
            }
        }
        #endregion
    }
}
