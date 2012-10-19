using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;


namespace VP.Sharepoint.CQ.Core.WebControls
{
    [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true),
     AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
     SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true),
     AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class RadioButtonChoiceField : BaseFieldControl, IPostBackDataHandler
    {
        private const string FillInChoiceValue = "[RadioButtonChoiceField-FillInChoiceValue]";

        [DefaultValue(false)]
        public bool AutoPostBack
        {
            get
            {
                var obj = ViewState["AutoPostBack"];
                if (obj != null)
                {
                    return (bool) obj;
                }
                return false;
            }
            set { ViewState["AutoPostBack"] = value; }
        }

        public StringCollection Choices
        {
            get
            {
                var obj = ViewState["Choices"];
                if (obj != null)
                {
                    return (StringCollection) obj;
                }

                obj = new StringCollection();
                ViewState["Choices"] = obj;
                return (StringCollection) obj;
            }
        }

        public string OnClientClick
        {
            get { return ViewState["OnClientClick"] as string; }
            set { ViewState["OnClientClick"] = value; }
        }

        public override object Value
        {
            get { return ViewState["Value"]; }
            set { ViewState["Value"] = value; }
        }

        public string DefaultValue
        {
            get
            {
                var value = ViewState["DefaultValue"];
                return value == null ? Field.DefaultValue : value.ToString();
            }
            set { ViewState["DefaultValue"] = value; }
        }

        [DefaultValue(1)]
        public int Columns
        {
            get
            {
                var value = ViewState["Columns"];
                if (value != null)
                {
                    return (int) value;
                }
                return 1;
            }
            set { ViewState["Columns"] = value; }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var keys = new List<string>(postCollection.AllKeys);

            if (!keys.Contains(postDataKey) && !keys.Contains(postDataKey + "$FillInChoice"))
            {
                return false;
            }

            var postedValue = postCollection[postDataKey];
            if (string.Equals(postedValue, FillInChoiceValue))
            {
                postedValue = Utilities.Trim(postCollection[postDataKey + "$FillInChoice"]);
            }
            else
            {
                postedValue = SPEncode.HtmlDecode(postedValue);
            }

            if (!string.Equals(postedValue, Value))
            {
                Value = postedValue;
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            OnSelectedIndexChanged(this, EventArgs.Empty);
        }

        #endregion

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(object sender, EventArgs args)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, args);
            }
        }

        /*protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Page != null)
            {
                Page.RegisterRequiresPostBack(this);
            }
        }*/

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Page.IsPostBack && ControlMode == SPControlMode.Edit)
            {
                Value = ItemFieldValue;
            }
        }

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            var choices = Choices;
            if (choices.Count == 0)
            {
                choices = ((SPFieldChoice) Field).Choices;
            }

            if (choices.Count == 0)
            {
                writer.Write("&nbsp;");
                return;
            }

            var value = Convert.ToString(Value);
            if (!Page.IsPostBack && ControlMode == SPControlMode.New)
            {
                value = DefaultValue;
            }

            var rows = Math.Ceiling((double) choices.Count/Columns);
            
            var onClientClick = OnClientClick;
            if (AutoPostBack)
            {
                onClientClick = Page.ClientScript.GetPostBackEventReference(this, null);
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            var index = 0;
            var flag = false;

            for (var i = 1; i <= rows; i++)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                for (var j = 1; j <= Columns; j++)
                {
                    writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);

                    if (index < choices.Count)
                    {
                        var item = choices[index];

                        writer.AddAttribute(HtmlTextWriterAttribute.Title, SPEncode.HtmlEncode(item));
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-RadioText");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);

                        if (string.Equals(value, item))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                            flag = true;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(onClientClick))
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClientClick);       
                            }
                        }

                        writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_{1}", ClientID, index));
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
                        writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                        writer.AddAttribute(HtmlTextWriterAttribute.Value, SPEncode.HtmlEncode(item));
                        writer.RenderBeginTag(HtmlTextWriterTag.Input);
                        writer.RenderEndTag(); // input

                        writer.AddAttribute(HtmlTextWriterAttribute.For, string.Format("{0}_{1}", ClientID, index));
                        writer.RenderBeginTag(HtmlTextWriterTag.Label);
                        writer.Write(SPEncode.HtmlEncode(item));
                        writer.RenderEndTag(); // label

                        writer.RenderEndTag(); // span
                    }
                    else
                    {
                        // Empty cell
                        writer.Write("&nbsp;");
                    }

                    writer.RenderEndTag(); // td

                    index++;
                }

                writer.RenderEndTag(); // tr
            }

            if (Utilities.Cast<SPFieldChoice>(Field).FillInChoice)
            {
                var title = SPResource.GetString(Strings.ChoiceFieldFillInChoiceText);

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, Columns.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-RadioText");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, title);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);

                writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_FillInChoice", ClientID));
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
                writer.AddAttribute(HtmlTextWriterAttribute.Value, FillInChoiceValue);
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");

                if (!flag)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input

                writer.AddAttribute(HtmlTextWriterAttribute.For, string.Format("{0}_FillInChoice", ClientID));
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(title);
                writer.RenderEndTag(); // label

                writer.RenderEndTag(); // span

                writer.RenderEndTag(); // td
                writer.RenderEndTag(); // tr

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, Columns.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, title);
                writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, "255");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, string.Format("{0}$FillInChoice", UniqueID));
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("SetChoiceOption('{0}_FillInChoice')", ClientID));

                if (!flag)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, SPEncode.HtmlEncode(value));
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input

                writer.RenderEndTag(); // td

                writer.RenderEndTag(); // tr
            }

            writer.RenderEndTag(); // table
            
            RenderValidationMessage(writer);
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

        public override void Validate()
        {
            IsValid = true;
            if (Field.Required)
            {
                var value = Value as string;
                if (string.IsNullOrEmpty(value))
                {
                    IsValid = false;
                    ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
                }
            }
        }

        public override void UpdateFieldValueInItem()
        {
            ItemFieldValue = Value;
        }
    }
}