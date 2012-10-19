using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class LookupField : BaseFieldControl
    {
        protected const string TextField = "TextField";
        protected const string ValueField = "ValueField";
        internal System.Data.DataView dataSource;
        private Image dropImage;
        private DropDownList dropList;
        private bool hasValueSet;
        private List<int> ids;
        private SPListItemCollection listItems;
        private int selectedValueIndex;
        private TextBox textBox;
        private bool throttled;
        private object value;
        private SPWeb webForeign;

        public LookupField()
        {
            selectedValueIndex = -1;
        }

        private string Choices
        {
            get
            {
                var builder = new StringBuilder();
                var flag = true;
                foreach (DataRowView view in DataSource)
                {
                    var str = (view["TextField"] as string).Replace("|", "||");
                    if (flag)
                    {
                        builder.Append(str);
                        flag = false;
                        builder.AppendFormat("|{0}", view["ValueField"]);
                    }
                    else
                    {
                        builder.AppendFormat("|{0}", str);
                        builder.AppendFormat("|{0}", view["ValueField"]);
                    }
                }
                return builder.ToString();
            }
        }

        internal virtual ICollection DataSource
        {
            get
            {
                if (dataSource == null)
                {
                    SPField field;
                    var num = 0;
                    var lookup = (SPFieldLookup) Field;
                    if (lookup == null)
                    {
                        return null;
                    }
                    var lookupField = lookup.LookupField;
                    if (string.IsNullOrEmpty(lookupField))
                    {
                        lookupField = "Title";
                    }
                    var num2 = ItemIds.Count > 0 ? ItemIds[0] : 0;
                    var table = new DataTable {Locale = CultureInfo.InvariantCulture};
                    table.Columns.Add(new DataColumn("ValueField", typeof (int)));
                    table.Columns.Add(new DataColumn("TextField", typeof (string)));
                    if (!lookup.Required && !lookup.AllowMultipleValues)
                    {
                        var row = table.NewRow();
                        row[0] = 0;
                        row[1] = SPResource.GetString("LookupFieldNoneOption", new object[0]);
                        table.Rows.Add(row);
                        if (((int) row[0]) == num2)
                        {
                            selectedValueIndex = num;
                        }
                        num++;
                    }
                    var lookupList = LookupList;
                    if (lookupList == null)
                    {
                        return null;
                    }
                    try
                    {
                        field = lookupList.Fields.GetField(lookupField);
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                    if (LookupListItems != null)
                    {
                        for (var i = 0; i < LookupListItems.Count; i++)
                        {
                            var fieldValueAsText = field.GetFieldValueAsText(listItems[i][lookupField]);
                            if (!string.IsNullOrEmpty(fieldValueAsText))
                            {
                                var row2 = table.NewRow();
                                row2[0] = LookupListItems[i].ID;
                                row2[1] = fieldValueAsText;
                                table.Rows.Add(row2);
                                if (((int) row2[0]) == num2)
                                {
                                    selectedValueIndex = num;
                                }
                                num++;
                            }
                        }
                    }
                    dataSource = new System.Data.DataView(table);
                }
                return dataSource;
            }
        }

        private string HiddenFieldName
        {
            get { return ("SP" + Field.InternalName + "_Hidden"); }
        }

        protected virtual IList<int> ItemIds
        {
            get
            {
                SPFieldLookupValue value2;
                if (ids != null)
                {
                    goto Label_0083;
                }
                if (value == null)
                {
                    value2 = new SPFieldLookupValue();
                }
                else if (value is SPFieldLookupValue)
                {
                    value2 = (SPFieldLookupValue) value;
                }
                else
                {
                    if (value is string)
                    {
                        try
                        {
                            value2 = new SPFieldLookupValue((string) value);
                            goto Label_0064;
                        }
                        catch (ArgumentException)
                        {
                            return new List<int>();
                        }
                    }
                    throw new ArgumentException();
                }
                Label_0064:
                var num = value2.LookupId;
                ids = new List<int>(1) {num};
                Label_0083:
                return ids;
            }
        }

        internal SPList LookupList
        {
            get
            {
                SPList list = null;
                var field = (SPFieldLookup) Field;
                if (field.LookupList == "UserInfo")
                {
                    return Web.SiteUserInfoList;
                }
                if (field.LookupList == "Docs")
                {
                    return null;
                }
                if (field.LookupList == "Self")
                {
                    return Web.Lists[ListId];
                }

                using (new SPSecurity.SuppressAccessDeniedRedirectInScope())
                {
                    if (field.LookupWebId != Web.ID)
                    {
                        try
                        {
                            webForeign = Web.Site.OpenWeb(field.LookupWebId);
                        }
                        catch (ArgumentException)
                        {
                            webForeign = Web;
                        }
                        catch (FileNotFoundException)
                        {
                            webForeign = Web;
                        }
                        catch (DirectoryNotFoundException)
                        {
                            webForeign = Web;
                        }
                        catch (UnauthorizedAccessException)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        webForeign = Web;
                    }

                    try
                    {
                        if (field.LookupList != null)
                        {
                            list = webForeign.Lists[new Guid(field.LookupList)];
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return null;
                    }
                    catch (SPException)
                    {
                        return null;
                    }
                }
                return list;
            }
        }

        internal SPListItemCollection LookupListItems
        {
            get
            {
                if (((listItems == null) && !throttled) && (LookupList != null))
                {
                    if (LookupList.IsThrottled && !Web.Site.WebApplication.CurrentUserIgnoreThrottle())
                    {
                        throttled = true;
                    }
                    else
                    {
                        var field = (SPFieldLookup) Field;
                        if (field == null)
                        {
                            return null;
                        }
                        var lookupField = field.LookupField;
                        if (string.IsNullOrEmpty(lookupField))
                        {
                            lookupField = "Title";
                        }
                        var query = new SPQuery {DatesInUtc = true};
                        var builder = new StringBuilder("<View Scope=\"RecursiveAll\"><Query>");

                        if (!string.IsNullOrEmpty(WhereCondition))
                        {
                            var xmlDocument = new XmlDocument();
                            xmlDocument.LoadXml(WhereCondition);
                            builder.Append(xmlDocument.OuterXml);
                        }

                        builder.Append("<OrderBy><FieldRef Name=\"");
                        builder.Append(lookupField);
                        builder.Append("\"/></OrderBy></Query><ViewFields><FieldRef Name=\"");
                        builder.Append(lookupField);
                        builder.Append("\"/></ViewFields>");
                        builder.Append("</View>");
                        query.ViewXml = builder.ToString();
                        listItems = LookupList.GetItems(query);
                    }
                }
                return listItems;
            }
        }

        private bool Throttled
        {
            get
            {
                if ((!throttled && (LookupList != null)) &&
                    (LookupList.IsThrottled && !Web.Site.WebApplication.CurrentUserIgnoreThrottle()))
                {
                    throttled = true;
                }
                return throttled;
            }
        }

        public override object Value
        {
            get
            {
                EnsureChildControls();
                if (textBox != null)
                {
                    if (Page.IsPostBack)
                    {
                        var str = Context.Request.Form[HiddenFieldName];
                        return (string.IsNullOrEmpty(str) ? 0 : int.Parse(str, CultureInfo.InstalledUICulture));
                    }
                    return ((selectedValueIndex >= 0) ? selectedValueIndex : 0);
                }
                if (dropList == null)
                {
                    return value;
                }

                if (dropList.SelectedIndex < 0 || dropList.SelectedValue == "0")
                {
                    return null;
                }

                return Convert.ToInt32(dropList.SelectedValue);
            }
            set
            {
                EnsureChildControls();
                SetFieldControlValue(value);
            }
        }

        public string WhereCondition
        {
            get
            {
                var obj = ViewState["WhereCondition"];
                if (obj != null)
                {
                    return (string) obj;
                }
                return string.Empty;
            }
            set { ViewState["WhereCondition"] = value; }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether a postback to the server automatically occurs when the user changes the list selection.
        /// </summary>
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

        /// <summary>
        ///   Determines whether the select element to be rendered.
        /// </summary>
        public bool AccessibilityMode
        {
            get
            {
                var obj = ViewState["AccessibilityMode"];
                if (obj != null)
                {
                    return (bool) obj;
                }
                return false;
            }
            set { ViewState["AccessibilityMode"] = value; }
        }

        private void Clear()
        {
            ids = null;
            dataSource = null;
            selectedValueIndex = -1;
        }

        [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
         SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
        protected override void CreateChildControls()
        {
            if (IsFieldValueCached)
            {
                base.CreateChildControls();
            }
            else if (Field != null)
            {
                base.CreateChildControls();
                if (ControlMode != SPControlMode.Display)
                {
                    var field = (SPFieldLookup) Field;
                    if (!field.AllowMultipleValues)
                    {
                        Controls.Clear();
                        if (Throttled)
                        {
                            var maxItemsPerThrottledOperation = Web.Site.WebApplication.MaxItemsPerThrottledOperation;
                            var str =
                                SPResource.GetString(
                                    Field.Required ? "RequiredLookupThrottleMessage" : "LookupThrottleMessage",
                                    new object[]
                                        {
                                            maxItemsPerThrottledOperation.ToString(
                                                CultureInfo.InvariantCulture)
                                        });
                            var child = new Literal {Text = SPHttpUtility.HtmlEncode(str)};
                            var literal2 = new Literal {Text = @"<span style=""vertical-align:middle"">"};
                            var literal3 = new Literal {Text = @"</span>"};
                            Controls.Add(literal2);
                            Controls.Add(child);
                            Controls.Add(literal3);
                        }
                        else
                        {
                            if (AutoPostBack || ((DataSource == null) || (DataSource.Count <= 20)) ||
                                ((InDesign || !IsIE55Up(Page.Request)) || IsAccessibilityMode(Page.Request)))
                            {
                                dropList = new DropDownList
                                               {
                                                   ID = "Lookup",
                                                   TabIndex = TabIndex,
                                                   DataSource = DataSource,
                                                   DataValueField = "ValueField",
                                                   DataTextField = "TextField",
                                                   ToolTip = SPHttpUtility.NoEncode(field.Title),
                                               };

                                if (AutoPostBack)
                                {
                                    dropList.AutoPostBack = true;
                                    dropList.SelectedIndexChanged += OnSelectedIndexChanged;
                                }

                                dropList.DataBind();
                                Controls.Add(dropList);
                            }
                            else
                            {
                                textBox = new TextBox();
                                textBox.Attributes.Add("choices", Choices);
                                textBox.Attributes.Add("match", "");
                                textBox.Attributes.Add("onkeydown", "CoreInvoke('HandleKey')");
                                textBox.Attributes.Add("onkeypress", "CoreInvoke('HandleChar')");
                                textBox.Attributes.Add("onfocusout", "CoreInvoke('HandleLoseFocus')");
                                textBox.Attributes.Add("onchange", "CoreInvoke('HandleChange')");
                                textBox.Attributes.Add("class", "ms-lookuptypeintextbox");
                                textBox.Attributes.Add("title", SPHttpUtility.HtmlEncode(field.Title));
                                textBox.TabIndex = TabIndex;
                                textBox.Attributes["optHid"] = HiddenFieldName;
                                var literal4 = new Literal {Text = @"<span style=""vertical-align:middle"">"};
                                var literal5 = new Literal {Text = @"</span>"};
                                Controls.Add(literal4);
                                Controls.Add(textBox);
                                textBox.Attributes.Add("opt", "_Select");
                                dropImage = new Image {ImageUrl = "/_layouts/images/dropdown.gif"};
                                dropImage.Attributes.Add("alt",
                                                         SPResource.GetString("LookupWordWheelDropdownAlt",
                                                                              new object[0]));
                                dropImage.Attributes.Add("style", "vertical-align:middle;");
                                Controls.Add(dropImage);
                                Controls.Add(literal5);
                            }
                            if (webForeign != null)
                            {
                                webForeign.Close();
                                webForeign = null;
                            }
                            Controls.Add(new LiteralControl("<br/>"));
                            SetFieldControlValue(ItemFieldValue);
                        }
                    }
                }
            }
        }

        internal static bool IsIE55Up(HttpRequest req)
        {
            int num;
            double num2;
            var flag = false;
            if (IEVersion(req, out num, out num2))
            {
                flag = (num >= 6) || ((num >= 5) && (num2 >= 0.5));
            }
            return flag;
        }

        internal static bool IEVersion(HttpRequest req, out int majorVersion, out double minorVersion)
        {
            var flag = false;
            majorVersion = 0;
            minorVersion = 0.0;
            if (req != null)
            {
                var browser = req.Browser;
                if (((browser != null) && (browser.Type.IndexOf("IE") >= 0)) && browser.Win32)
                {
                    majorVersion = browser.MajorVersion;
                    minorVersion = browser.MinorVersion;
                    flag = true;
                }
            }
            return flag;
        }

        internal bool IsAccessibilityMode(HttpRequest req)
        {
            if (AccessibilityMode)
            {
                return true;
            }

            var cookie = req.Cookies["WSS_AccessibilityFeature"];
            return ((cookie != null) && (cookie.Value == "true"));
        }

        [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
        public override void Focus()
        {
            if (!InDesign)
            {
                EnsureChildControls();
                if (textBox != null)
                {
                    textBox.Focus();
                }
                else if (dropList != null)
                {
                    dropList.Focus();
                }
            }
        }

        [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
        protected override void OnInit(EventArgs e)
        {
            CanCacheRenderedFieldValue = false;
            base.OnInit(e);
        }

        [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
         AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
         SharePointPermission(SecurityAction.Demand, ObjectModel = true),
         SharePointPermission(SecurityAction.Assert, ObjectModel = true)]
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (dropImage != null)
            {
                dropImage.Attributes.Add("onclick", "CoreInvoke('ShowDropdown','" + textBox.ClientID + "');");
            }
        }

        private void SetFieldControlValue(object val)
        {
            if ((value != val) || !hasValueSet)
            {
                Clear();
                value = val;
                hasValueSet = true;
                if (DataSource != null)
                {
                    if (dropList != null)
                    {
                        if (selectedValueIndex >= 0)
                        {
                            dropList.SelectedIndex = selectedValueIndex;
                        }
                        else
                        {
                            dropList.SelectedIndex = -1;
                        }
                    }
                    else if (textBox != null)
                    {
                        DataRowView view;
                        if (selectedValueIndex >= 0)
                        {
                            view = dataSource[selectedValueIndex];
                            textBox.Text = view["TextField"] as string;
                        }
                        if (Page != null)
                        {
                            var str = "0";
                            if (selectedValueIndex >= 0)
                            {
                                view = dataSource[selectedValueIndex];
                                str = ((int) view["ValueField"]).ToString(CultureInfo.InvariantCulture);
                            }
                            else if (Page.IsPostBack)
                            {
                                str = Context.Request.Form[HiddenFieldName];
                                if (string.IsNullOrEmpty(str))
                                {
                                    str = "0";
                                }
                            }
                            Page.ClientScript.RegisterHiddenField(HiddenFieldName, str);
                        }
                    }
                }
            }
        }

        [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
        public override void Validate()
        {
            if (((ControlMode != SPControlMode.Display) && IsValid) && ((textBox != null) || throttled))
            {
                var num = 0;
                if (!throttled)
                {
                    base.Validate();
                    num = (int) Value;
                }
                if ((num <= 0) && Field.Required)
                {
                    IsValid = false;
                    ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
                }
            }
        }

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(object sender, EventArgs args)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, args);
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
    }
}