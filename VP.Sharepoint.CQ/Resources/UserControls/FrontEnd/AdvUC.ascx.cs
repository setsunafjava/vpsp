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
using System.Web;
using System.Data;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class AdvUC : FrontEndUC
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
                var parentWP = (ContainerWebPart)this.Parent;
                AdvBO.BindAdv(CurrentWeb, ListsName.InternalName.AdvList, rptAdv, parentWP.Title);
            }
        }
        #endregion

        protected void rptAdv_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView qcItem = (DataRowView)e.Item.DataItem;
                var aLink = (LinkButton)e.Item.FindControl("aLink");
                var ltrQC = (Literal)e.Item.FindControl("ltrQC");
                aLink.CommandArgument = Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvID]);
                var qcFile = WebUrl + "/" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvFile]);
                if ("Images".Equals(Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvType])))
                {
                    ltrQC.Text = "<img src='" + qcFile + "' width='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvWidth]) + 
                        "' height='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) +
                        "' alt='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.Title]) + "' title='" + 
                        Convert.ToString(qcItem[FieldsName.AdvList.InternalName.Title]) + "' />";
                }
                else if ("Flash".Equals(Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvType])))
                {
                    ltrQC.Text = @"<embed width='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvWidth]) +
                        "' height='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) + 
                        @"' align='middle' quality='high' wmode='transparent' allowscriptaccess='always' 
                                        type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' alt='' 
                                        src='" + qcFile + "' />";
                }
                else if ("Video".Equals(Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvType])))
                {
                    ltrQC.Text =
                        @"<embed
                                  flashvars='file=" + SPContext.Current.Web.Url + "/" + ListsName.InternalName.ResourcesList + @"/stylish_slim.swf&autostart=true'
                                  allowfullscreen='true'
                                  allowscripaccess='always'
                                  id='" + this.ID + "-quangcao-" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvID]) + @"'
                                  name='" + this.ID + "-quangcao-" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvID]) + @"'
                                  src='" + qcFile + @"'
                                  width='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvWidth]) + @"'
                                  height='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) + @"'
                                />";
                }
                else
                {
                    aLink.Visible = false;
                }
            }
        }

        protected void aLink_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((LinkButton)sender).CommandArgument))
            {
                var qcid = Convert.ToString(((LinkButton)sender).CommandArgument);
                var advUrl = string.Empty;
                var advOpen = string.Empty;
                AdvBO.UpdateAdv(CurrentWeb, ListsName.InternalName.AdvList, qcid, HttpContext.Current, ref advUrl, ref advOpen);
                if (!string.IsNullOrEmpty(advUrl))
                {
                    if (!string.IsNullOrEmpty(advOpen))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "qc-" + qcid, "window.open('" + advUrl + "');", true);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(advUrl);
                    }
                }
            }
        }
    }
}
