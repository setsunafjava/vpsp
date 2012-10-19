using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class SectionPanel : Panel, IPostBackDataHandler
    {
        protected HiddenField hdfState;

        public bool ReadOnly
        {
            get
            {
                var value = ViewState["ReadOnly"];
                if (value == null)
                {
                    return false;
                }
                return (bool) value;
            }
            set { ViewState["ReadOnly"] = value; }
        }

        [Browsable(true)]
        [Category("Properties")]
        public string SectionTitle
        {
            get
            {
                var s = (string) ViewState["SectionTitle"];
                return (s ?? String.Empty);
            }

            set { ViewState["SectionTitle"] = value; }
        }

        [Browsable(true)]
        [Category("Properties")]
        public string SectionHeaderCssClass
        {
            get
            {
                var s = (string) ViewState["SectionHeaderCssClass"];
                return (s ?? string.Empty);
            }

            set { ViewState["SectionHeaderCssClass"] = value; }
        }

        [Browsable(true)]
        [Category("Properties")]
        public string SectionHeaderTextCssClass
        {
            get
            {
                var s = (string) ViewState["SectionHeaderTextCssClass"];
                return (s ?? string.Empty);
            }

            set { ViewState["SectionHeaderTextCssClass"] = value; }
        }

        [Browsable(true)]
        [Category("Properties")]
        public string SectionContentCssClass
        {
            get
            {
                var s = (string) ViewState["SectionContentCssClass"];
                return (s ?? String.Empty);
            }

            set { ViewState["SectionContentCssClass"] = value; }
        }

        [Browsable(true)]
        [Category("Properties")]
        [DefaultValue(false)]
        public bool Collapsed
        {
            get
            {
                EnsureChildControls();
                var value = hdfState.Value;
                if (string.IsNullOrEmpty(value))
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }

            set
            {
                EnsureChildControls();
                hdfState.Value = value.ToString();
            }
        }

        public override Unit Width
        {
            get
            {
                if (base.Width == Unit.Empty)
                    base.Width = Unit.Percentage(100);
                return base.Width;
            }
            set { base.Width = value; }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            EnsureChildControls();
            var oldValue = hdfState.Value;
            var postedValue = postCollection[hdfState.UniqueID];
            if (oldValue != postedValue)
            {
                hdfState.Value = postedValue;
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (ID != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            }

            AddScrollingAttribute(ScrollBars, writer);
            var horizontalAlign = HorizontalAlign;
            if (horizontalAlign != HorizontalAlign.NotSet)
            {
                var converter = TypeDescriptor.GetConverter(typeof (HorizontalAlign));
                writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign,
                                         converter.ConvertToInvariantString(horizontalAlign).ToLowerInvariant());
            }
            if (!Wrap)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            }

            if (Direction == ContentDirection.LeftToRight)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Dir, "ltr");
            }

            else if (Direction == ContentDirection.RightToLeft)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Dir, "rtl");
            }
        }

        private static void AddScrollingAttribute(ScrollBars scrollBars, HtmlTextWriter writer)
        {
            switch (scrollBars)
            {
                case ScrollBars.Horizontal:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
                    return;

                case ScrollBars.Vertical:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
                    return;

                case ScrollBars.Both:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                    return;

                case ScrollBars.Auto:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
                    return;
            }
        }
                
        protected override void CreateChildControls()
        {
            hdfState = new HiddenField {Value = "False"};
            Controls.Add(hdfState);
            base.CreateChildControls();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresPostBack(this);

            // Set Control Mode
            var formContext = SPContext.Current.FormContext;
            if (formContext != null)
            {
                if (formContext.FormMode != SPControlMode.Display && ReadOnly)
                {
                    foreach (var control in Controls.OfType<BaseFieldControl>())
                    {
                        control.ControlMode = SPControlMode.Display;
                    }
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(CssClass))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "5px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.MarginBottom, "5px");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            }

            // custom
            writer.AddStyleAttribute("border", "#e0e0e0 1px solid");
            // end

            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Width.ToString());

            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Render header
            if (string.IsNullOrEmpty(SectionHeaderCssClass))
            {
                writer.AddStyleAttribute("min-height", "25px");
                writer.AddStyleAttribute("background", "url(/_layouts/images/selbg.png) #f6f6f6 repeat-x left top");
                writer.AddStyleAttribute("border-bottom", "#e0e0e0 1px solid");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "25px");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, SectionHeaderCssClass);
            }

            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");

            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Render header text
            if (string.IsNullOrEmpty(SectionHeaderTextCssClass))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, SectionHeaderTextCssClass);
            }

            writer.AddStyleAttribute("float", "left");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            writer.AddStyleAttribute("line-height", "25px");
            writer.AddAttribute("onmouseover", "this.style.textDecoration = 'none';");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write(SectionTitle);
            writer.RenderEndTag();

            // Render header status icon
            writer.AddStyleAttribute("float", "right");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px");
            writer.AddStyleAttribute("line-height", "25px");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                string.Format("SectionControl_OnClick('{0}', '{1}');return false;", hdfState.ClientID,
                                              ClientID));
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            writer.AddAttribute(HtmlTextWriterAttribute.Border, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Src,
                                Collapsed ? "/_layouts/images/dlmax.gif" : "/_layouts/images/dlmin.gif");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_Icon");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag(); // img

            writer.RenderEndTag(); // p

            writer.RenderEndTag(); // div

            // Render Content
            if (Collapsed)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            base.Render(writer);

            writer.RenderEndTag(); // div
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            const string key = "SectionControl_OnClick";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), key))
            {
                var script = new StringBuilder();
                script.Append("function SectionControl_OnClick(hdfStateClientId, contentClientId){");
                script.Append("var hdfState = $('#' + hdfStateClientId);");
                script.Append("if(hdfState.val() == 'False'){");
                script.Append("$('#' + contentClientId).slideUp('fast');");
                script.Append("$('#' + contentClientId + '_Icon').attr('src', '/_layouts/images/dlmax.gif');");
                script.Append("hdfState.val('True');}");
                script.Append("else{$('#' + contentClientId).slideDown('fast');");
                script.Append("$('#' + contentClientId + '_Icon').attr('src', '/_layouts/images/dlmin.gif');");
                script.Append("hdfState.val('False');}");
                script.Append("}"); // end function
                Page.ClientScript.RegisterClientScriptBlock(GetType(), key, script.ToString(), true);
            }
        }
    }
}