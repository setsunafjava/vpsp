using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;


namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class DialogList : BaseFieldControl, IPostBackDataHandler
    {
        private Image btnShowDialog;
        private bool focus;
        private HiddenField hdfDialogSectionUrl;
        
        /// <summary>
        ///   Collection of choices, separated by ;#
        /// </summary>
        public string Choices { get; set; }

        public override string CssClass
        {
            get
            {
                var value = ViewState["CssClass"];
                return value == null ? "ms-long" : value.ToString();
            }
            set { ViewState["CssClass"] = value; }
        }

        public string IconUrl
        {
            get
            {
                var value = ViewState["IconUrl"];
                return value == null ? "/_layouts/images/edit.gif" : value.ToString();
            }
            set { ViewState["IconUrl"] = value; }
        }

        public string DialogTitle
        {
            get
            {
                var value = ViewState["DialogTitle"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["DialogTitle"] = value; }
        }

        public bool AllowMultipleValues
        {
            get
            {
                var value = ViewState["AllowMultipleValues"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["AllowMultipleValues"] = value; }
        }

        public bool AllowFillInValue
        {
            get
            {
                var value = ViewState["AllowFillInValue"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["AllowFillInValue"] = value; }
        }

        public string LookupList
        {
            get
            {
                var value = ViewState["LookupList"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["LookupList"] = value; }
        }

        public string LookupField
        {
            get
            {
                var value = ViewState["LookupField"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["LookupField"] = value; }
        }

        public bool LookupWithHighPermission
        {
            get
            {
                var value = ViewState["LookupWithHighPermission"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["LookupWithHighPermission"] = value; }
        }

        public string WhereCondition
        {
            get
            {
                var value = ViewState["WhereCondition"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["WhereCondition"] = value; }
        }

        public string SeparateCharacter
        {
            get
            {
                var value = ViewState["SeparateCharacter"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["SeparateCharacter"] = value; }
        }

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

        public bool IsLookupField
        {
            get
            {
                var value = ViewState["IsLookupField"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["IsLookupField"] = value; }
        }

        public bool CombineDataSources
        {
            get
            {
                var value = ViewState["CombineDataSources"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["CombineDataSources"] = value; }
        }

        public override object Value
        {
            get { return ViewState["Value"]; }
            set
            {
                if (value != null)
                {
                    if (value is SPFieldUserValueCollection)
                    {
                        ViewState["Value"] = string.Join(";#", ((SPFieldUserValueCollection)value).Select(item => item.LookupId + ";#" + item.LookupValue).ToArray());
                    }
                    else if(value is SPFieldLookupValueCollection)
                    {
                        ViewState["Value"] = string.Join(";#", ((SPFieldLookupValueCollection)value).Select(item => item.LookupId + ";#" + item.LookupValue).ToArray());
                    }
                    else
                    {
                        ViewState["Value"] = value;
                    }
                }
                else
                {
                    ViewState["Value"] = null;   
                }
            }
        }

        public Unit Width
        {
            get
            {
                var value = ViewState["Width"];
                if (value != null)
                {
                    return (Unit) value;
                }
                return Unit.Empty;
            }
            set { ViewState["Width"] = value; }
        }

        public int Rows
        {
            get
            {
                var value = ViewState["Rows"];
                if (value != null)
                {
                    return (int) value;
                }
                return 1;
            }
            set { ViewState["Rows"] = value; }
        }

        public bool MultiLine
        {
            get
            {
                var value = ViewState["MultiLine"];
                if (value == null)
                {
                    return false;
                }

                return (bool) value;
            }
            set { ViewState["MultiLine"] = value; }
        }

        public bool EnableQuickSearch
        {
            get
            {
                var value = ViewState["EnableQuickSearch"];
                if (value == null)
                {
                    return false;
                }

                return (bool) value;
            }
            set { ViewState["EnableQuickSearch"] = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DataGrid DataGrid { get; set; }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var postData = postCollection[postDataKey];
            if (postData != Convert.ToString(Value))
            {
                if (string.IsNullOrEmpty(postData))
                {
                    Value = null;
                }
                else
                {
                    Value = IsLookupField ? postData : postData.Replace(";#", MultiLine ? Environment.NewLine : "; ");
                }
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            OnDataChanged();
        }

        #endregion

        public event EventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged(this, EventArgs.Empty);
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (ControlMode != SPControlMode.Display)
            {
                hdfDialogSectionUrl = new HiddenField {ID = "hdfDialogSectionUrl"};
                Controls.Add(hdfDialogSectionUrl);

                btnShowDialog = new Image {ID = "btnShowDialog", ImageUrl = IconUrl};
                btnShowDialog.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                btnShowDialog.Style.Add(HtmlTextWriterStyle.PaddingLeft, "2px");
                Controls.Add(btnShowDialog);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Page.IsPostBack && ControlMode == SPControlMode.Edit)
            {
                Value = ItemFieldValue;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (ControlMode == SPControlMode.Display)
            {
                return;
            }

            EnsureChildControls();

            var key = "DialogFieldScript_" + ID;
            if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof (DialogList), key))
            {
                var dataGridString = DataGrid != null ? SPEncode.UrlEncode(DataGrid.ToString()) : string.Empty;
                const string urlFormat =
                    "{0}/DialogBox.aspx?IsDlg=1&Choices={1}&AllowMultipleValues={2}&AllowFillInValue={3}&LookupList={4}&LookupField={5}&WhereCondition={6}&SeparateCharacter={7}&CombineDataSources={8}&MultiLine={9}&EnableQuickSearch={10}&LookupWithHighPermission={11}&DataGrid={12}";
                var url = string.Format(urlFormat, Web.Url, SPEncode.UrlEncode(Choices), AllowMultipleValues,
                                        AllowFillInValue, SPEncode.UrlEncode(LookupList),
                                        SPEncode.UrlEncode(LookupField), SPEncode.UrlEncode(WhereCondition),
                                        SPEncode.UrlEncode(SeparateCharacter), CombineDataSources, MultiLine,
                                        EnableQuickSearch, LookupWithHighPermission, dataGridString);
                hdfDialogSectionUrl.Value = url;

                var script = new StringBuilder();
                script.AppendFormat("function dfShowModalDialog_{0}(){{", ClientID);
                script.AppendFormat("var url = $('#{0}').val();", hdfDialogSectionUrl.ClientID);
                script.AppendFormat("var value = $('#{0}').val();", ClientID);
                script.Append("value = encodeURIComponent(value);");
                script.Append("url = url + '&Value=' + value;");

                script.Append("var options = SP.UI.$create_DialogOptions();");
                script.Append("options.url = url;");
                script.Append("options.allowMaximize = false;");
                script.Append("options.showMaximized = false;");
                script.AppendFormat("options.title = '{0}';", DialogTitle);
                script.AppendFormat(
                    "options.dialogReturnValueCallback = Function.createDelegate(null, dfOnDialogCloseCallback_{0});",
                    ID);
                script.Append("var dialog = SP.UI.ModalDialog.showModalDialog(options);");
                script.Append("return false;");
                script.Append("}");

                script.AppendFormat("function dfOnDialogCloseCallback_{0}(result, target){{", ID);
                script.Append("if(result == SP.UI.DialogResult.OK){");
                script.AppendFormat("$('#{0}').val(target);", ClientID);

                if (IsLookupField)
                {
                    script.Append("var split = target.split(';#');");
                    script.Append("var values = [];");
                    script.Append("for(i=0;i<=split.length;i++){");
                    script.Append("if(i%2 != 0){values.push(split[i]);}");
                    script.Append("}");
                    script.AppendFormat(MultiLine
                                            ? "$('#{0}_Input').val(values.join('\\r\\n'));"
                                            : "$('#{0}_Input').val(values.join('; '));", ClientID);
                }
                else
                {
                    script.AppendFormat(MultiLine
                                            ? "$('#{0}_Input').val(target.replace(/;#/g, '\\r\\n'));"
                                            : "$('#{0}_Input').val(target.replace(/;#/g, '; '));", ClientID);
                }

                if (AutoPostBack)
                {
                    script.Append(Page.ClientScript.GetPostBackEventReference(this, ""));
                }

                script.Append("}");
                script.Append("}");

                if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), key, script.ToString(), true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(DialogList), key, script.ToString(), true);    
                }
            }

            if (focus)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Focus",
                                                        string.Format(
                                                            "$(document).ready(function(){{$('#{0}_ShowModalDialog').focus();}});",
                                                            ClientID), true);
            }
        }

        public override void Validate()
        {
            IsValid = true;

            if (Field.Required && string.IsNullOrEmpty(Convert.ToString(Value)))
            {
                IsValid = false;
                ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
            }
        }

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Title, Field.Title);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_Input");
            writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
            if (!string.IsNullOrEmpty(CssClass))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);    
            }

            if (!Width.IsEmpty)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, Width.ToString());
            }

            if (AllowMultipleValues && MultiLine)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
                if (IsLookupField)
                {
                    var values = Utilities.RemoveLookupId(Value).Split(new[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                    writer.Write(string.Join(Environment.NewLine, values));
                }
                else
                {
                    writer.Write(Convert.ToString(Value).Replace(";#", Environment.NewLine));
                }
                writer.RenderEndTag();
            }
            else
            {
                if (IsLookupField)
                {
                    var values = Utilities.RemoveLookupId(Value).Split(new[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, string.Join("; ", values));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(Value).Replace(";#", "; "));
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input    
            }
            
            // Render hidden field value
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(Value));
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            hdfDialogSectionUrl.RenderControl(writer);

            writer.RenderEndTag();

            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "2px");

            if (MultiLine)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "top");
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            if (TabIndex > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString());
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("dfShowModalDialog_{0}();", ClientID));
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_ShowModalDialog");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            btnShowDialog.RenderControl(writer);
            writer.RenderEndTag(); // a
            writer.RenderEndTag(); // td

            writer.RenderEndTag();
            writer.RenderEndTag();

            RenderValidationMessage(writer);
        }

        protected override void RenderFieldForDisplay(HtmlTextWriter writer)
        {
            if (ItemFieldValue == null)
            {
                writer.Write("&nbsp;");
                return;
            }

            if (IsLookupField)
            {
                var web = SPContext.Current.Web;

                if (ItemFieldValue is SPFieldUserValueCollection)
                {
                    var users = (SPFieldUserValueCollection) ItemFieldValue;

                    writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    foreach (var user in users)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);

                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "GoToLinkOrDialogNewWindow(this);return false;");
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("/_layouts/userdisp.aspx?ID={0}", user.LookupId));
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.Write(SPEncode.HtmlEncode(user.LookupValue));
                        writer.RenderEndTag(); // a

                        writer.RenderEndTag(); // td
                        writer.RenderEndTag(); // tr
                    }
                    writer.RenderEndTag(); // table
                    return;
                }
                
                if (ItemFieldValue is SPFieldLookupValueCollection)
                {
                    var field = (SPFieldLookup) Field;
                    var list = web.Lists[new Guid(field.LookupList)];
                    var lookups = (SPFieldLookupValueCollection) ItemFieldValue;
                    for (var index = 0; index < lookups.Count; index++)
                    {
                        var lookup = lookups[index];
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                            "GoToLinkOrDialogNewWindow(this);return false;");
                        writer.AddAttribute(HtmlTextWriterAttribute.Href,string.Format("{0}{1}?ID={2}", web.Url, list.DefaultDisplayFormUrl, lookup.LookupId));
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.Write(SPEncode.HtmlEncode(lookup.LookupValue));
                        writer.RenderEndTag(); // a
                        if (index < lookups.Count - 1)
                        {
                            writer.Write("; ");
                        }
                    }
                    return;
                }
                
                var split = ItemFieldValue.ToString().Split(new [] {";#"}, StringSplitOptions.None);
                if (Field is SPFieldUser)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "GoToLinkOrDialogNewWindow(this);return false;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("/_layouts/userdisp.aspx?ID={0}", split[0]));
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(SPEncode.HtmlEncode(split[1]));
                    writer.RenderEndTag(); // a
                }
                else
                {
                    var field = (SPFieldLookup)Field;
                    var list = web.Lists[new Guid(field.LookupList)];

                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                            "GoToLinkOrDialogNewWindow(this);return false;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("{0}{1}?ID={2}", web.Url, list.DefaultDisplayFormUrl, split[1]));
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(SPEncode.HtmlEncode(split[1]));
                    writer.RenderEndTag(); // a
                }
            }
            else
            {
                var values = Convert.ToString(ItemFieldValue).Split(new[] {";#", "; ", "\r\n", "\n", "\r"},
                                                           StringSplitOptions.RemoveEmptyEntries).ToList();
                writer.Write(MultiLine
                             ? string.Join("<br/>", values.Select(SPEncode.HtmlEncode).ToArray())
                             : string.Join("; ", values.Select(SPEncode.HtmlEncode).ToArray()));
            }
        }

        public override void Focus()
        {
            focus = true;
        }
    }
}