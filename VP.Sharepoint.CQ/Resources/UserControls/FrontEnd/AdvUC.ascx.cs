﻿using System;
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
using System.Web.UI.HtmlControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class AdvUC : FrontEndUC
    {
        private string wpTitle = string.Empty;
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rptAdv.ItemDataBound += new RepeaterItemEventHandler(rptAdv_ItemDataBound);
            wpTitle = ((ContainerWebPart)this.Parent).Title;
            if (!Page.IsPostBack)
            {
                AdvBO.BindAdv(CurrentWeb, ListsName.InternalName.AdvList, rptAdv, wpTitle);
            }
            else
            {
                var argValue1 = Request.Form["__EVENTTARGET"];
                if (argValue1.Contains("aLink"))
                {
                    if (!string.IsNullOrEmpty(hdQC.Value))
                    {
                        SetQC(hdQC.Value);
                    }
                }
            }
        }
        #endregion

        protected void rptAdv_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView qcItem = (DataRowView)e.Item.DataItem;
                var aLink = (LinkButton)e.Item.FindControl("aLink");
                aLink.Click += new EventHandler(aLink_OnClick);
                aLink.Attributes.Add("onclick", "SetValueAlink('" + hdQC.ClientID + "','" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvID]) + "')");
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
                    HtmlGenericControl divAdv = (HtmlGenericControl)e.Item.FindControl("divAdv");
                    divAdv.Attributes.Add("style", "height:" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) + "px;overflow-y:hidden;");
                    ltrQC.Text = @"<embed width='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvWidth]) +
                        "px' height='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) + 
                        @"px' align='middle' quality='high' wmode='transparent' allowscriptaccess='always' 
                                        type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' alt='' 
                                        src='" + qcFile + "' /><div style=\"height: " + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) + 
                                               "px; width: " + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvWidth])
                                               + "px;  position: relative; z-index: 9; top: -" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) +
                                               "px; background-image: url('" + DocLibUrl + "/spacer.gif'); background-repeat:repeat;\"></div>";
                }
                else if ("Video".Equals(Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvType])))
                {
                    ltrQC.Text =
                        @"<embed
                                  flashvars='file=" + WebUrl + "/" + ListsName.InternalName.ResourcesList + @"/stylish_slim.swf&autostart=true'
                                  allowfullscreen='true'
                                  allowscripaccess='always'
                                  id='" + this.ID + "-quangcao-" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvID]) + @"'
                                  name='" + this.ID + "-quangcao-" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvID]) + @"'
                                  src='" + qcFile + @"'
                                  width='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvWidth]) + @"'
                                  height='" + Convert.ToString(qcItem[FieldsName.AdvList.InternalName.AdvHeight]) + @"'
                                ></embed>";
                }
                else
                {
                    aLink.Visible = false;
                }
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                if ("advhomecenter".Equals(wpTitle) ||
                    "advhomeright".Equals(wpTitle) ||
                    "advnews".Equals(wpTitle) ||
                    "advnewsdetail".Equals(wpTitle) ||
                    "advorganization".Equals(wpTitle) ||
                    "advlibrary".Equals(wpTitle) ||
                    "advlibrarydetail".Equals(wpTitle) ||
                    "advabout".Equals(wpTitle) ||
                    "advdocument".Equals(wpTitle) ||
                    "advstatistic".Equals(wpTitle))
                {
                    //var ltrHeader = (Literal)e.Item.FindControl("ltrHeader");
                    //ltrHeader.Text = "<div class='pos_MOD'><div class='bg_title_mod'>Quảng cáo</div><div style='text-align:center; padding: 7px 0px;'>";
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if ("advhomecenter".Equals(wpTitle) ||
                    "advhomeright".Equals(wpTitle) ||
                    "advnews".Equals(wpTitle) ||
                    "advnewsdetail".Equals(wpTitle) ||
                    "advorganization".Equals(wpTitle) ||
                    "advlibrary".Equals(wpTitle) ||
                    "advlibrarydetail".Equals(wpTitle) ||
                    "advabout".Equals(wpTitle) ||
                    "advdocument".Equals(wpTitle) ||
                    "advstatistic".Equals(wpTitle))
                {
                    //var ltrFooter = (Literal)e.Item.FindControl("ltrFooter");
                    //ltrFooter.Text = "</div></div>";
                }
            }
        }

        protected void aLink_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((LinkButton)sender).CommandArgument))
            {
                var qcid = Convert.ToString(((LinkButton)sender).CommandArgument);
                //var advUrl = string.Empty;
                //var advOpen = string.Empty;
                //AdvBO.UpdateAdv(CurrentWeb, ListsName.InternalName.AdvList, qcid, HttpContext.Current, ref advUrl, ref advOpen);
                //if (!string.IsNullOrEmpty(advUrl))
                //{
                //    if (!string.IsNullOrEmpty(advOpen))
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "qc-" + qcid, "window.open('" + advUrl + "');", true);
                //    }
                //    else
                //    {
                //        HttpContext.Current.Response.Redirect(advUrl);
                //    }
                //}
                SetQC(qcid);
            }
        }

        private void SetQC(string qcid)
        {
            var advUrl = string.Empty;
            var advOpen = string.Empty;
            AdvBO.UpdateAdv(CurrentWeb, ListsName.InternalName.AdvList, qcid, HttpContext.Current, ref advUrl, ref advOpen);
            if (!string.IsNullOrEmpty(advUrl))
            {
                if (!string.IsNullOrEmpty(advOpen))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "qc-" + qcid, "window.open('" + advUrl + "','" + Utilities.GetPreByTime(DateTime.Now) + "_window');", true);
                }
                else
                {
                    HttpContext.Current.Response.Redirect(advUrl);
                }
            }
        }
    }
}
