using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class ExtendedUserField : BaseFieldControl, IPostBackDataHandler
    {
        /// <summary>
        /// Allow user select multiple values
        /// </summary>
        public bool AllowMultipleValues
        {
            get
            {
                var obj = ViewState["AllowMultipleValues"];
                if (obj != null)
                {
                    return (bool) obj;
                }
                return false;
            }
            set { ViewState["AllowMultipleValues"] = value; }
        }

        /// <summary>
        /// Format value of field
        /// </summary>
        public ExtraUserFieldFormat Format
        {
            get
            {
                var obj = ViewState["Format"];
                if (obj != null)
                {
                    return (ExtraUserFieldFormat)obj;
                }
                return ExtraUserFieldFormat.DisplayName;
            }
            set { ViewState["Format"] = value; }
        }

        /// <summary>
        /// Custom field value format.
        /// </summary>
        /// <remarks>{0}: Display name; {1}: Account name; {2}: Email address</remarks>
        public string CustomFormat
        {
            get
            {
                var obj = ViewState["CustomFormat"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return string.Empty;
            }
            set { ViewState["CustomFormat"] = value; }
        }

        public Unit Width
        {
            get
            {
                var obj = ViewState["Width"];
                if (obj != null)
                {
                    return (Unit) obj;
                }
                return Unit.Empty;
            }
            set { ViewState["Width"] = value; }
        }

        [DefaultValue(500)]
        public int DialogHeight
        {
            get
            {
                var obj = ViewState["DialogHeight"];
                if (obj != null)
                {
                    return (int) obj;
                }
                return 500;
            }
            set { ViewState["DialogHeight"] = value; }
        }

        /// <summary>
        /// Selection people only or people & group
        /// </summary>
        [DefaultValue(true)]
        public bool SelectPeopleOnly
        {
            get
            {
                var obj = ViewState["SelectPeopleOnly"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set { ViewState["SelectPeopleOnly"] = value; }
        }

        /// <summary>
        /// Number of rows for field display
        /// </summary>
        [DefaultValue(1)]
        public int Rows
        {
            get
            {
                var obj = ViewState["Rows"];
                if (obj != null)
                {
                    return (int)obj;
                }
                return 1;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The rows value cannot be less than 1.");
                }
                ViewState["Rows"] = value;
            }
        }

        [DefaultValue(575)]
        public int DialogWidth
        {
            get
            {
                var obj = ViewState["DialogWidth"];
                if (obj != null)
                {
                    return (int) obj;
                }
                return 575;
            }
            set { ViewState["DialogWidth"] = value; }
        }

        [DefaultValue("Select People")]
        public string DialogTitle
        {
            get
            {
                var obj = ViewState["DialogTitle"];
                if (obj != null)
                {
                    return (string) obj;
                }
                return "Select People";
            }
            set { ViewState["DialogTitle"] = value; }
        }

        [DefaultValue("/_layouts/images/people.gif")]
        public string DialogImage
        {
            get
            {
                var obj = ViewState["DialogImage"];
                if (obj != null)
                {
                    return (string) obj;
                }
                return "/_layouts/images/people.gif";
            }
            set { ViewState["DialogImage"] = value; }
        }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether a text box for typing an value.
        /// </summary>
        [DefaultValue(false)]
        public bool AllowFillIn
        {
            get
            {
                var obj = ViewState["AllowFillIn"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set { ViewState["AllowFillIn"] = value; }
        }

        [DefaultValue(true)]
        public bool OverrideValue
        {
            get
            {
                var obj = ViewState["OverrideValue"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set { ViewState["OverrideValue"] = value; }
        }

        [DefaultValue("ms-long")]
        public override string CssClass
        {
            get
            {
                var value = ViewState["CssClass"];
                if (value != null)
                {
                    return (string) value;
                }
                return "ms-long";
            }
            set { ViewState["CssClass"] = value; }
        }

        public override object Value
        {
            get { return ControlMode == SPControlMode.Display ? ItemFieldValue : ViewState["Value"]; }
            set { ViewState["Value"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (ControlMode != SPControlMode.Display)
            {
                ScriptLink.Register(Page, "entityeditor.js", false);    
            }
        }

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Title, Field.Title);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            if (!Width.IsEmpty)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Width.ToString());
            }

            if (!AllowFillIn)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
            }

            if (Rows == 1)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(Value));
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input    
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Rows, Rows.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
                writer.Write(Value);
                writer.RenderEndTag(); // textarea
            }


            writer.RenderEndTag(); // td

            writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "3px");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Browser");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("__Dialog__{0}()", ClientID));
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Browser");
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/addressbook.gif");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag(); // img

            writer.RenderEndTag(); // a
            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // table 

            RenderValidationMessage(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);

            var serverRelativeUrl = GetContextWeb(Context).ServerRelativeUrl;
            if (!serverRelativeUrl.EndsWith("/"))
            {
                serverRelativeUrl += "/";
            }
            var urlToEncode = SPHttpUtility.UrlPathEncode(serverRelativeUrl, false);

            writer.Write(string.Format("function __Dialog__{0}(){{", ClientID));
            writer.Write(string.Format("var dialogUrl = '{0}_layouts/Picker.aspx?MultiSelect={1}&CustomProperty={5}&DefaultSearch=&DialogTitle={2}&DialogImage={3}&PickerDialogType={4}&ForceClaims=False&DisableClaims=False&EnabledClaimProviders=&EntitySeparator=\u00253B\u0025EF\u0025BC\u00259B\u0025EF\u0025B9\u002594\u0025EF\u0025B8\u002594\u0025E2\u00258D\u0025AE\u0025E2\u002581\u00258F\u0025E1\u00258D\u0025A4\u0025D8\u00259B';",
                    urlToEncode,
                    AllowMultipleValues ? "True" : "False",
                    SPHttpUtility.UrlKeyValueEncode(DialogTitle),
                    SPHttpUtility.UrlKeyValueEncode(DialogImage),
                    SPHttpUtility.UrlKeyValueEncode(typeof (PeoplePickerDialog).AssemblyQualifiedName),
                    SelectPeopleOnly ? "User;;15;;;False" : "User,SecGroup,SPGroup;;15;;;False"));
            writer.Write(string.Format("var features = 'resizable: yes; status: no; scroll: no; help: no; center: yes; dialogWidth : {0}px; dialogHeight : {1}px; zoominherit : 1';", DialogWidth, DialogHeight));
            writer.Write(string.Format("commonShowModalDialog(dialogUrl, features, CallbackWrapper_{0});", ClientID));
            writer.Write("}");

            writer.WriteLine();

            writer.Write(string.Format("function CallbackWrapper_{0}(result){{", ClientID));
            writer.Write("var entities = GetEntities(result);");
            writer.Write("if (entities == null) return;");
            writer.Write("var values = [];");
            writer.Write("for(var x = 0; x < entities.childNodes.length; x++){"); // begin for
            writer.Write("var entity = entities.childNodes[x];");
            writer.Write("var accountName = entity.getAttribute(\"Key\");");
            writer.Write("var displayName = entity.getAttribute(\"DisplayText\");");
            writer.Write("var email = '';");

            writer.Write("var extraData = EntityEditor_SelectSingleNode(entity, \"ExtraData\");");
            writer.Write("if(extraData){"); // begin if
            writer.Write("var arrayOfDictionaryEntry = EntityEditor_SelectSingleNode(extraData, \"ArrayOfDictionaryEntry\");");
            writer.Write("if(arrayOfDictionaryEntry){"); // begin if
            writer.Write("for(var y = 0; y < arrayOfDictionaryEntry.childNodes.length; y++){"); // begin for
            writer.Write("var key = EntityEditor_SelectSingleNode(arrayOfDictionaryEntry.childNodes[y], \"Key\");");
            writer.Write("if(key.childNodes[0].nodeValue == 'Email'){");
            writer.Write("var value = EntityEditor_SelectSingleNode(arrayOfDictionaryEntry.childNodes[y], \"Value\");");
            writer.Write("if(value){email = value.childNodes[0].nodeValue;}");
            writer.Write("}"); // end if
            writer.Write("}"); // end for

            // Append data
            switch (Format)
            {
                case ExtraUserFieldFormat.DisplayName:
                    writer.Write("values.push(displayName);");
                    break;
                case ExtraUserFieldFormat.AccountName:
                    writer.Write("values.push(accountName);");
                    break;
                case ExtraUserFieldFormat.Email:
                    writer.Write("values.push(email);");
                    break;
                case ExtraUserFieldFormat.Custom:
                    writer.Write(string.Format("values.push(__Dialog_CustomFormat_{0}(displayName, accountName, email));", ClientID));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            writer.Write("}"); // end if
            writer.Write("}"); // end if
            writer.Write("}"); // end for

            // Set value for textbox
            if (OverrideValue)
            {
                writer.Write(string.Format("document.getElementById('{0}').value = values.join('; ');", ClientID));
            }
            else
            {
                writer.Write(string.Format("var element = document.getElementById('{0}');", ClientID));
                writer.Write("var oldValue = element.value + '';");
                writer.Write("var separate = ' ';");
                writer.Write("if(oldValue  == ''){separate = '';}");
                writer.Write("element.value = oldValue + separate + values.join('; ');");
            }

            writer.Write("}"); // end function
            writer.WriteLine();

            if (Format == ExtraUserFieldFormat.Custom)
            {
                writer.Write(string.Format("function __Dialog_CustomFormat_{0}(displayName, accountName, email){{", ClientID));
                writer.Write("var str = '{0}';", CustomFormat);
                writer.Write("str = str.replace(new RegExp(\"\\\\{0\\\\}\", \"gm\"), displayName);");
                writer.Write("str = str.replace(new RegExp(\"\\\\{1\\\\}\", \"gm\"), accountName);");
                writer.Write("str = str.replace(new RegExp(\"\\\\{2\\\\}\", \"gm\"), email);");
                writer.Write("return str;");
                writer.Write("}"); // end function
            }

            writer.RenderEndTag(); // script
        }

        public override void UpdateFieldValueInItem()
        {
            ItemFieldValue = Value;
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var postedValue = postCollection[postDataKey];
            if (!string.Equals(Value, postedValue))
            {
                Value = postedValue;
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        public override void Validate()
        {
            IsValid = true;
            if (Field.Required)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Value)))
                {
                    IsValid = false;
                    ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
                }
            }
        }
    }

    public enum ExtraUserFieldFormat
    {
        DisplayName,
        AccountName,
        Email,
        Custom,
    }
}