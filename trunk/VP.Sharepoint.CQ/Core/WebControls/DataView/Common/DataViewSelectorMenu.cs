using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class DataViewSelectorMenu : WebControl
    {
        public DataViewSelectorMenu() : base(HtmlTextWriterTag.Span)
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-ltviewselectormenuheader");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            RenderMenuSeparator(writer);

            var ctx = new Random(10).Next();
            var listId = SPContext.Current.ListId.ToString("B");
            var viewId = SPContext.Current.ViewContext.ViewId.ToString("B");

            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Change View");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-menu-althov ms-viewselector");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("zz{0}_ListTitleViewSelectorMenu_t", ctx));
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "white-space: nowrap;");
            writer.AddAttribute("onmouseover", "MMU_PopMenuIfShowing(this);MMU_EcbTableMouseOverOut(this, true)");
            writer.AddAttribute("onclick", string.Format("CoreInvoke('showViewSelector',event,document.getElementById('{0}'),{{showRepairView : false, showMergeView : false, showEditView: true, showCreateView: true, showApproverView:  false, listId: '{1}', viewId: '{2}', viewParameters: ''}});", ClientID, listId, viewId));
            writer.AddAttribute("oncontextmenu", "ClkElmt(this); return false;");
            writer.AddAttribute("foa", string.Format("MMU_GetMenuFromClientId('zz{0}_ListTitleViewSelectorMenu')", ctx));
            writer.AddAttribute("hoverinactive", "ms-menu-althov ms-viewselector");
            writer.AddAttribute("hoveractive", "ms-menu-althov-active ms-viewselectorhover");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-menu-a");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("zz{0}_ListTitleViewSelectorMenu", ctx));
            writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, "W");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "white-space: nowrap; cursor: pointer;");
            writer.AddAttribute("onkeydown", string.Format("MMU_EcbLinkOnKeyDown(byid('zz{0}_LTViewSelectorMenu'), MMU_GetMenuFromClientId('zz{1}_ListTitleViewSelectorMenu'), event);", ctx + 1, ctx));
            writer.AddAttribute("onclick", string.Format("CoreInvoke('showViewSelector',event,document.getElementById('{0}'),{{showRepairView : false, showMergeView : false, showEditView: true, showCreateView: true, showApproverView:  false, listId: '{1}', viewId: '{2}', viewParameters: ''}});", ClientID, listId, viewId));
            writer.AddAttribute("onfocus", string.Format("MMU_EcbLinkOnFocusBlur(byid('zz{0}_LTViewSelectorMenu'), this, true);", ctx + 1));
            writer.AddAttribute("oncontextmenu", "ClkElmt(this); return false;");
            writer.AddAttribute("href", "javascript:;");
            writer.AddAttribute("serverclientid", string.Format("zz{0}_ListTitleViewSelectorMenu", ctx + 1));
            writer.AddAttribute("menutokenvalues", string.Format("MENUCLIENTID=zz{0}_ListTitleViewSelectorMenu,TEMPLATECLIENTID=zz{1}_LTViewSelectorMenu", ctx, ctx + 1));
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(SPContext.Current.ViewContext.View.Title);
            writer.RenderEndTag(); // span

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "border-bottom: 0px; border-left: 0px; border-top: 0px; border-right: 0px;");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Use SHIFT+ENTER to open the menu (new window).");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/blank.gif");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag(); // img

            writer.RenderEndTag(); // a

            // Span
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-clust ms-viewselector-arrow ms-menu-stdarw");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "position: relative; width: 7px; display: inline-block; height: 4px; overflow: hidden;");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "position: absolute; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; top: -310px !important; left: 0px !important;");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Open Menu");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/fgimg.png");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            writer.RenderEndTag(); // span

            // Span
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-clust ms-viewselector-arrow ms-menu-hovarw");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "position: relative; width: 7px; display: inline-block; height: 4px; overflow: hidden;");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "position: absolute; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; top: -310px !important; left: 0px !important;");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Open Menu");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/fgimg.png");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            writer.RenderEndTag(); // span

            writer.RenderEndTag(); // span

            writer.RenderEndTag(); // span
        }

        private static void RenderMenuSeparator(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-clust ms-ltviewselectormenuseparator");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "position: relative; width: 11px; display: inline-block; height: 11px; overflow: hidden;");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "position: absolute; border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; top: -531px !important; left: 0px !important;");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, ":");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/fgimg.png");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            writer.RenderEndTag();

            writer.RenderEndTag(); // span
        }
    }
}
