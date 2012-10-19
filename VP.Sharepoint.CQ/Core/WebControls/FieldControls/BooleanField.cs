using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class BooleanField : BaseFieldControl, IPostBackDataHandler
    {
        [DefaultValue(false)]
        public bool AutoPostBack
        {
            get
            {
                var value = ViewState["AutoPostBack"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["AutoPostBack"] = value; }
        }

        public bool Checked
        {
            get
            {
                var value = ViewState["Checked"];
                if (value != null)
                {
                    return (bool) value;
                }

                switch (ControlMode)
                {
                    case SPControlMode.New:
                        value = Field.DefaultValue == "1";
                        break;
                    case SPControlMode.Display:
                    case SPControlMode.Edit:
                        value = (bool) ItemFieldValue;
                        break;
                    default:
                        value = false;
                        break;
                }

                ViewState["Checked"] = value;

                return (bool) value;
            }
            set { ViewState["Checked"] = value; }
        }

        public string OnClientClick
        {
            get
            {
                var value = ViewState["OnClientClick"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set { ViewState["OnClientClick"] = value; }
        }

        public override object Value
        {
            get { return Checked; }
            set { Checked = (bool) value; }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var postedValue = postCollection[postDataKey];
            if (postedValue == null)
            {
                if (Checked)
                {
                    Checked = false;
                    return true;
                }
            }
            else
            {
                if (!Checked)
                {
                    Checked = true;
                    return true;
                }
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            OnCheckedChanged(EventArgs.Empty);
        }

        #endregion

        public event EventHandler CheckedChanged;

        protected virtual void OnCheckedChanged(EventArgs args)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, args);
            }
        }

        protected override void RenderFieldForDisplay(HtmlTextWriter writer)
        {
            if (SPContext.Current.FormContext.FormMode == SPControlMode.New)
            {
                writer.Write("&nbsp;");
            }
            else
            {
                base.RenderFieldForDisplay(writer);
            }
        }

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);

            if (AutoPostBack)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                    Page.ClientScript.GetPostBackEventReference(this, null));
            }
            else
            {
                if (!string.IsNullOrEmpty(OnClientClick))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, OnClientClick);
                }
            }

            if (Checked)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Title, Field.Title);

            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresPostBack(this);
        }
    }
}