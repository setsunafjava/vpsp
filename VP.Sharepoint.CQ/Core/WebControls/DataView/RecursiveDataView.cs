using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using VP.Sharepoint.CQ.Core.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RecursiveDataView : BaseDataView
    {
        #region Privates

        private readonly Hashtable ribbonControls = new Hashtable();
        private readonly Hashtable ribbonGroups = new Hashtable();
        private readonly Hashtable ribbonTabs = new Hashtable();
        private bool? enableAddNewItem;
        private bool? enableDeleteItem;
        private bool? enableEditItem;
        private int? rowLimit;
        private SPList list;
        private HiddenField hdfAddNewItem;
        
        #endregion

        public string ListName
        {
            get
            {
                var value = ViewState["ListName"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set
            {
                ViewState["ListName"] = value;
                list = null;
            }
        }

        public virtual SPList List
        {
            get
            {
                if (list == null && !string.IsNullOrEmpty(ListName))
                {
                    list = SPContext.Current.Web.Lists[ListName];
                }
                return list;
            }
        }

        public override int RowLimit
        {
            get
            {
                if (rowLimit.HasValue)
                {
                    return rowLimit.Value;
                }

                var viewContext = SPContext.Current.ViewContext;
                if (viewContext != null && viewContext.View != null)
                {
                    rowLimit = (int)viewContext.View.RowLimit;
                    return rowLimit.Value;
                }

                return base.RowLimit;
            }
            set
            {
                rowLimit = value;
            }
        }

        public virtual string WhereCondition
        {
            get
            {
                var value = ViewState["WhereCondition"];
                if (value != null)
                {
                    return (string)value;
                }
                return string.Empty;
            }
            set { ViewState["WhereCondition"] = value; }
        }

        private Hashtable ListItemCollectionPositions
        {
            get
            {
                var value = ViewState["ListItemCollectionPositions"];
                if (value != null)
                {
                    return (Hashtable) value;
                }
                value = new Hashtable();
                ViewState["ListItemCollectionPositions"] = value;
                return (Hashtable) value;
            }
        }

        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowRibbonTabs
        {
            get
            {
                var value = ViewState["ShowRibbonTabs"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["ShowRibbonTabs"] = value; }
        }

        protected DataTable AllDataSource { get; set; }

        public override bool EnableAddNewItem
        {
            get
            {
                if (!enableAddNewItem.HasValue)
                {
                    enableAddNewItem = List.DoesUserHavePermissions(SPBasePermissions.AddListItems);
                }
                return enableAddNewItem.Value;
            }
            set { enableAddNewItem = value; }
        }

        public override bool EnableEditItem
        {
            get
            {
                if (!enableEditItem.HasValue)
                {
                    enableEditItem = List.DoesUserHavePermissions(SPBasePermissions.EditListItems);
                }
                return enableEditItem.Value;
            }
            set { enableEditItem = value; }
        }

        public override bool EnableDeleteItem
        {
            get
            {
                if (!enableDeleteItem.HasValue)
                {
                    enableDeleteItem = List.DoesUserHavePermissions(SPBasePermissions.DeleteListItems);
                }
                return enableDeleteItem.Value;
            }
            set { enableDeleteItem = value; }
        }

        /// <summary>
        /// Enable show archive button
        /// </summary>
        public bool EnableArchiveList
        {
            get
            {
                var value = ViewState["EnableArchiveList"];
                if (value != null)
                {
                    return (bool)value;
                }
                return false;
            }
            set { ViewState["EnableArchiveList"] = value; }
        }

        /// <summary>
        /// Enable show archive items button
        /// </summary>
        public bool EnableArchiveListItems
        {
            get
            {
                var value = ViewState["EnableArchiveListItems"];
                if (value != null)
                {
                    return (bool)value;
                }
                return false;
            }
            set { ViewState["EnableArchiveListItems"] = value; }
        }

        /// <summary>
        /// Enable show restore button
        /// </summary>
        public bool EnableRestoreList
        {
            get
            {
                var value = ViewState["EnableRestoreList"];
                if (value != null)
                {
                    return (bool)value;
                }
                return false;
            }
            set { ViewState["EnableRestoreList"] = value; }
        }


        /// <summary>
        /// Enable show archive items button
        /// </summary>
        public bool EnableRestoreListItems
        {
            get
            {
                var value = ViewState["EnableRestoreListItems"];
                if (value != null)
                {
                    return (bool)value;
                }
                return false;
            }
            set { ViewState["EnableRestoreListItems"] = value; }
        }

        /// <summary>
        /// Show Print button to print item
        /// </summary>
        public bool EnablePrintItemButton
        {
            get
            {
                var value = ViewState["EnablePrintItemButton"];
                if (value != null)
                {
                    return (bool)value;
                }
                return false;
            }
            set { ViewState["EnablePrintItemButton"] = value; }
        }
        
        /// <summary>
        /// The print preview template options
        /// </summary>
        public DataViewTemplateOption PrintTemplate { get; set; }

        /// <summary>
        /// Show Send Item button to print item
        /// </summary>
        public bool EnableSendItemButton
        {
            get
            {
                var value = ViewState["EnableSendItemButton"];
                if (value != null)
                {
                    return (bool)value;
                }
                return false;
            }
            set { ViewState["EnableSendItemButton"] = value; }
        }

        /// <summary>
        /// The send item template options
        /// </summary>
        public DataViewTemplateOption SendItemTemplate { get; set; }

        protected override bool SupportAggregationFunctions
        {
            get { return AllDataSource != null; }
        }

        protected override void CreateChildControls()
        {
            foreach (var viewField in ViewFields.Cast<IViewFieldRef>().Where(item => !item.IsVirtualField))
            {
                var field = List.Fields[viewField.FieldName];
                viewField.Initialize(field);
            }

            foreach (var groupField in GroupFields.Cast<IGroupFieldRef>().Where(item => !item.IsVirtualField))
            {
                var field = List.Fields[groupField.FieldName];
                groupField.Initialize(field);
            }

            foreach (var sortField in SortFields.Cast<SortFieldRef>())
            {
                var field = List.Fields[sortField.FieldName];
                sortField.Initialize(field);
            }

            hdfAddNewItem = new HiddenField();
            Controls.Add(hdfAddNewItem);

            base.CreateChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            // Clear hdfAddNewItem state
            hdfAddNewItem.Value = string.Empty;
            hdfAddNewItem.RenderControl(writer);
        }

        protected override void BindDataSource()
        {
            var ds = new SPDataSource(List) {SelectCommand = BuildSelectCommand(false)};
            ds.SelectParameters.Add("nextpagedata", Page.Request.QueryString.ToString());
            ds.SelectParameters.Add("maximumrows", RowLimit.ToString());
            ds.SelectParameters.Add("rootfolder", Page.Request.QueryString["RootFolder"]);
            ds.SelectParameters.Add("prevpagedata", "");

            var view = ds.GetView();
            var args = new DataSourceSelectArguments();

            try
            {
                view.Select(args, DataSourceViewSelectCallback);
                NextPagePosition = ds.SelectParameters["nextpagedata"].DefaultValue;
                PrevPagePosition = ds.SelectParameters["prevpagedata"].DefaultValue;
            }
            catch (SPQueryThrottledException)
            {
                ThresholdException = true;
                ds = new SPDataSource(List) { SelectCommand = BuildSelectCommand(true) };
                ds.SelectParameters.Add("nextpagedata", Page.Request.QueryString.ToString());
                ds.SelectParameters.Add("maximumrows", RowLimit.ToString());
                ds.SelectParameters.Add("rootfolder", Page.Request.QueryString["RootFolder"]);
                ds.SelectParameters.Add("prevpagedata", "");

                view = ds.GetView();
                args = new DataSourceSelectArguments();

                try
                {
                    view.Select(args, DataSourceViewSelectCallback);
                    NextPagePosition = ds.SelectParameters["nextpagedata"].DefaultValue;
                    PrevPagePosition = ds.SelectParameters["prevpagedata"].DefaultValue;
                }
                catch (SPQueryThrottledException)
                {
                    // Empty data source
                    DataSource = new DataTable();
                }
            }

            if (RequiredAggregations)
            {
                BindAllDataSource();
            }
        }

        private void DataSourceViewSelectCallback(IEnumerable enumerable)
        {
            var items = enumerable.Cast<SPDataSourceViewResultItem>().Select(item => item.ResultItem).Cast<SPListItem>().ToList();
            var fieldNames = ViewFields.Cast<IViewFieldRef>().Where(item => !item.IsVirtualField).Select(viewField => viewField.FieldName).ToList();
            fieldNames.AddRange(GroupFields.Cast<IGroupFieldRef>().Select(groupField => groupField.FieldName));
            fieldNames.AddRange(SortFields.Cast<SortFieldRef>().Select(sortField => sortField.FieldName));
            fieldNames.AddRange(new[] { "ID", "Created" });
            fieldNames = fieldNames.Distinct().ToList();
            var fields = fieldNames.Select(f => List.Fields[f]).ToList();
            if (!fields.Any(f => f.InternalName == "FSObjType"))
            {
                fieldNames.Add("Item Type");
                fields.Add(List.Fields.GetFieldByInternalName("FSObjType"));
            }
            fieldNames.Add("ServerUrl");
            fields.Add(List.Fields.GetFieldByInternalName("ServerUrl"));
            DataTable dataSource = null;
            ConvertToDataTable(items, fieldNames, fields, List.ID, ref dataSource);
            DataSource = dataSource;
        }

        protected static void ConvertToDataTable(IEnumerable<SPListItem> items, List<string> fieldNames, List<SPField> fields, Guid listId, ref DataTable dt)
        {
            if (dt == null)
            {
                dt = new DataTable();
                var indexOf = 0;
                foreach (var fieldName in fieldNames)
                {
                    Type dataType;
                    var field = fields[indexOf];

                    switch (field.Type)
                    {
                        case SPFieldType.Number:
                        case SPFieldType.Currency:
                            dataType = typeof(double);
                            break;
                        case SPFieldType.DateTime:
                            dataType = typeof(DateTime);
                            break;
                        case SPFieldType.Boolean:
                        case SPFieldType.Attachments:
                            dataType = typeof(bool);
                            break;
                        case SPFieldType.Calculated:
                            dataType = typeof(object);
                            break;
                        case SPFieldType.Counter:
                            dataType = typeof(int);
                            break;
                        case SPFieldType.User:
                            dataType = ((SPFieldUser)field).AllowMultipleValues ? typeof(SPFieldUserValueCollection) : typeof(SPFieldUserValue);
                            break;
                        case SPFieldType.Lookup:
                            dataType = ((SPFieldLookup)field).AllowMultipleValues ? typeof(SPFieldLookupValueCollection) : typeof(SPFieldLookupValue);
                            break;
                        default:
                            dataType = typeof(string);
                            break;
                    }
                    var dataColumn = new DataColumn(fieldName, dataType);
                    dt.Columns.Add(dataColumn);
                    indexOf++;
                }

                dt.Columns.Add("ListId", typeof(string));
                dt.Columns.Add("RowIndex", typeof(int));
            }

            var rowIndex = 0;
            foreach (var item in items)
            {
                var row = dt.NewRow();
                var index = 0;

                foreach (var fieldName in fieldNames)
                {
                    var fieldValue = Convert.ToString(item[fieldName]);
                    var field = fields[index];
                    switch (field.Type)
                    {
                        case SPFieldType.User:
                        case SPFieldType.Lookup:
                            row[fieldName] = field.GetFieldValue(fieldValue);
                            break;
                        case SPFieldType.Calculated:
                            var split = item[fieldName].ToString().Split(new[] { ";#" }, StringSplitOptions.None);
                            var value = string.Join("", split, 1, split.Length - 1);
                            if (!string.IsNullOrEmpty(value))
                            {
                                var calculatedField = (SPFieldCalculated)field;
                                switch (calculatedField.OutputType)
                                {
                                    case SPFieldType.Number:
                                    case SPFieldType.Currency:
                                        try
                                        {
                                            row[fieldName] = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                                        }
                                        catch (FormatException)
                                        {
                                            row[fieldName] = value;
                                        }
                                        break;
                                    case SPFieldType.DateTime:
                                        try
                                        {
                                            row[fieldName] = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                                        }
                                        catch (FormatException)
                                        {
                                            row[fieldName] = value;
                                        }
                                        break;
                                    case SPFieldType.Boolean:
                                        switch (value)
                                        {
                                            case "1":
                                                row[fieldName] = true;
                                                break;
                                            case "0":
                                                row[fieldName] = false;
                                                break;
                                            default:
                                                row[fieldName] = value;
                                                break;
                                        }
                                        break;
                                    default:
                                        row[fieldName] = value;
                                        break;
                                }
                            }
                            break;
                        default:
                            row[fieldName] = item[fieldName] ?? DBNull.Value;
                            break;
                    }
                    index++;
                }

                row["ListId"] = listId.ToString();
                row["RowIndex"] = rowIndex;
                dt.Rows.Add(row);
                rowIndex++;
            }
        }

        private string BuildSelectCommand(bool forThresholdExceptionCase)
        {
            if (forThresholdExceptionCase)
            {
                if (!string.IsNullOrEmpty(WhereCondition) &&
                    !WhereCondition.Equals("<Where></Where>", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SPQueryThrottledException();
                }
            }

            if (forThresholdExceptionCase)
            {
                return "<Query><OrderBy Override='TRUE'><FieldRef Name='ID' Ascending='FALSE' /></OrderBy></Query>";
            }

            var sb = new StringBuilder();
            sb.Append("<View>");

            sb.Append("<ViewFields>");

            var fields = ViewFields.Cast<BaseFieldRef>().Where(f => !f.IsVirtualField).Select(f => f.InternalFieldName).ToList();
            fields.AddRange(GroupFields.Cast<IGroupFieldRef>().Where(f => !f.IsVirtualField).Select(f => f.InternalFieldName));
            fields.AddRange(new[] { "FSObjType", "Created", "ServerUrl" });
            
            foreach (var field in fields.Distinct())
            {
                sb.AppendFormat("<FieldRef Name='{0}' />", field);
            }
            
            sb.Append("</ViewFields>");

            sb.Append("<Query>");

            // Where condition
            sb.Append("<Where>");

            var filterCondition = new StringBuilder();
            filterCondition.Append((WhereCondition ?? string.Empty).Replace("<Where>", "").Replace("</Where>", ""));
            var hasFilter = filterCondition.Length > 0;
            
            foreach (var filtedField in ViewFields.Cast<BaseFieldRef>().Where(viewField => viewField.IsFilter))
            {
                filterCondition.Insert(0, filtedField.GetFilterCamlQuery());
                if (hasFilter)
                {
                    filterCondition.Insert(0, "<And>");
                    filterCondition.Append("</And>");
                }
                hasFilter = true;
            }

            sb.Append(filterCondition);

            sb.Append("</Where>");

            var sortFields = new List<string>();

            // Group by
            if (GroupFields.Count > 0)
            {
                sb.Append("<GroupBy Collapse=\"FALSE\">");
                var groupField = (IGroupFieldRef)GroupFields[0];
                if (groupField.InternalFieldName == SortField)
                {
                    sb.AppendFormat(SortDir == "ASC"
                            ? "<FieldRef Name=\"{0}\" />"
                            : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\" />", groupField.InternalFieldName);    
                    sortFields.Add(groupField.InternalFieldName);
                }
                else
                {
                    sb.AppendFormat(groupField.SortDirection == ListSortDirection.Ascending
                            ? "<FieldRef Name=\"{0}\" />"
                            : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\" />", groupField.InternalFieldName);    
                }

                if (GroupFields.Count > 1)
                {
                    groupField = (IGroupFieldRef)GroupFields[1];
                    if (groupField.InternalFieldName == SortField)
                    {
                        sb.AppendFormat(SortDir == "ASC"
                                ? "<FieldRef Name=\"{0}\" />"
                                : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\" />", groupField.InternalFieldName);
                        sortFields.Add(groupField.InternalFieldName);
                    }
                    else
                    {
                        sb.AppendFormat(groupField.SortDirection == ListSortDirection.Ascending
                                ? "<FieldRef Name=\"{0}\" />"
                                : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\" />", groupField.InternalFieldName);
                    }
                }
                sb.Append("</GroupBy>");
            }

            // Order by
            if (SortFields.Count > 0 || GroupFields.Count > 2)
            {
                sb.Append("<OrderBy>");

                if (GroupFields.Count > 2)
                {
                    for (var i = 2; i < GroupFields.Count; i++)
                    {
                        var groupField = (IGroupFieldRef)GroupFields[i];
                        if (groupField.InternalFieldName == SortField)
                        {
                            sb.AppendFormat(SortDir == "ASC" ? "<FieldRef Name=\"{0}\"/>" : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\"/>", SortField);
                        }
                        else
                        {
                            sb.AppendFormat(groupField.SortDirection == ListSortDirection.Ascending
                                    ? "<FieldRef Name=\"{0}\" />"
                                    : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\" />", groupField.InternalFieldName);    
                        }
                        
                        sortFields.Add(groupField.InternalFieldName);
                    }
                }

                if (!string.IsNullOrEmpty(SortField) && !sortFields.Contains(SortField))
                {
                    sb.AppendFormat(SortDir == "ASC" ? "<FieldRef Name=\"{0}\"/>" : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\"/>", SortField);
                    sortFields.Add(SortField);
                }

                foreach (var sortField in
                    SortFields.Cast<SortFieldRef>().Where(sortField => !sortFields.Contains(sortField.InternalFieldName)))
                {
                    sb.AppendFormat(sortField.SortDirection == ListSortDirection.Ascending
                                        ? "<FieldRef Name=\"{0}\"/>"
                                        : "<FieldRef Name=\"{0}\" Ascending=\"FALSE\"/>", sortField.InternalFieldName);
                    sortFields.Add(sortField.InternalFieldName);
                }

                sb.Append("</OrderBy>");
            }

            sb.Append("</Query>");
            sb.Append("</View>");

            return sb.ToString();
        }

        private void BindAllDataSource()
        {
            var fieldNames = ViewFields.Cast<BaseFieldRef>()
                .Where(f => !f.IsVirtualField && (f.SumFieldData || f.CountFieldData || f.Filterable || f.IsHidden)).Select(f => f.FieldName).ToList();
            
            if (ViewFields.Cast<BaseFieldRef>().Any(f => f.SumFieldData || f.CountFieldData) || GroupFields.Cast<IGroupFieldRef>().Any(f => f.CountGroupItems))
            {
                fieldNames.AddRange(GroupFields.Cast<IGroupFieldRef>().Where(f => !f.IsVirtualField).Select(f => f.FieldName));    
            }

            fieldNames.Add("ID");
            fieldNames.Add("Created");
            
            fieldNames = fieldNames.Distinct().ToList();

            var fields = fieldNames.Select(f => List.Fields[f]).ToList();

            SPListItemCollection items;
            SPListItemCollectionPosition position = null;
            DataTable dt = null;

            var query = new SPQuery
                            {
                                RowLimit = 5000,
                                ViewFields = string.Join("", fields.Select(f => string.Format("<FieldRef Name='{0}' />", f.InternalName)).ToArray()),
                                Query = WhereCondition,
                                ViewAttributes = "Scope=\"Recursive\""
                            };

            if (List.BaseTemplate == SPListTemplateType.DiscussionBoard)
            {
                query.ViewAttributes = string.Empty;
            }

            do
            {
                query.ListItemCollectionPosition = position;
                items = list.GetItems(query);
                position = items.ListItemCollectionPosition;
                GetDataTable(items.Cast<SPListItem>(), fieldNames, fields, ref dt);
            } while (position != null);

            AllDataSource = dt;
        }

        protected void GetDataTable(IEnumerable<SPListItem> items, List<string> fieldNames, List<SPField> fields, ref DataTable dt)
        {
            if (dt == null)
            {
                dt = new DataTable();
                foreach (var fieldName in fieldNames)
                {
                    Type dataType;
                    var field = List.Fields[fieldName];
                    
                    switch (field.Type)
                    {
                        case SPFieldType.Number:
                        case SPFieldType.Currency:
                            dataType = typeof(double);
                            break;
                        case SPFieldType.DateTime:
                            dataType = typeof(DateTime);
                            break;
                        case SPFieldType.Boolean:
                        case SPFieldType.Attachments:
                            dataType = typeof(bool);
                            break;
                        case SPFieldType.Calculated:
                            dataType = typeof(object);
                            break;
                        case SPFieldType.Counter:
                            dataType = typeof(int);
                            break;
                        case SPFieldType.User:
                            dataType = ((SPFieldUser)field).AllowMultipleValues ? typeof (SPFieldUserValueCollection) : typeof(SPFieldUserValue);
                            break;
                        default:
                            dataType = typeof(string);
                            break;
                    }
                    var dataColumn = new DataColumn(fieldName, dataType);
                    dt.Columns.Add(dataColumn);
                }

                dt.Columns.Add("ListId", typeof(string));
                dt.Columns.Add("RowIndex", typeof(int));
            }

            var rowIndex = 0;
            foreach (var item in items)
            {
                var row = dt.NewRow();
                var index = 0;
                
                foreach (var fieldName in fieldNames)
                {
                    var fieldValue = Convert.ToString(item[fieldName]);
                    var field = fields[index];
                    switch (field.Type)
                    {
                        case SPFieldType.User:
                            row[fieldName] = field.GetFieldValue(fieldValue);
                            break;
                        case SPFieldType.Calculated:
                            var split = item[fieldName].ToString().Split(new[] { ";#" }, StringSplitOptions.None);
                            var value = string.Join("", split, 1, split.Length - 1);
                            if (!string.IsNullOrEmpty(value))
                            {
                                var calculatedField = (SPFieldCalculated)field;
                                switch (calculatedField.OutputType)
                                {
                                    case SPFieldType.Number:
                                    case SPFieldType.Currency:
                                        try
                                        {
                                            row[fieldName] = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                                        }
                                        catch (FormatException)
                                        {
                                            row[fieldName] = value;
                                        }
                                        break;
                                    case SPFieldType.DateTime:
                                        try
                                        {
                                            row[fieldName] = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                                        }
                                        catch (FormatException)
                                        {
                                            row[fieldName] = value;
                                        }
                                        break;
                                    case SPFieldType.Boolean:
                                        switch (value)
                                        {
                                            case "1":
                                                row[fieldName] = true;
                                                break;
                                            case "0":
                                                row[fieldName] = false;
                                                break;
                                            default:
                                                row[fieldName] = value;
                                                break;
                                        }
                                        break;
                                    default:
                                        row[fieldName] = value;
                                        break;
                                }
                            }
                            break;
                        default:
                            row[fieldName] = item[fieldName] ?? DBNull.Value;
                            break;
                    }
                    index++;
                }

                row["ListId"] = List.ID.ToString();
                row["RowIndex"] = rowIndex;
                dt.Rows.Add(row);
                rowIndex++;
            }
        }

        protected override DateTime GetCreateDateTime(DataRow item)
        {
            return (DateTime)item["Created"];
        }

        protected string BuildViewFields()
        {
            var sb = new StringBuilder();

            var fields = ViewFields.Cast<IViewFieldRef>().Where(item => !item.IsVirtualField);
            fields = fields.Union(GroupFields.Cast<IViewFieldRef>().Where(item => !item.IsVirtualField));

            foreach (var field in fields.Distinct(new ViewFieldRefEqualityComparer()))
            {
                sb.Append(field.GetViewFieldRef());
            }
            return sb.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (ShowRibbonTabs)
            {
                RegisterListItemTab();
                RegisterListTab();
                RegisterOtherTabs();
            }
        }

        private void RegisterOtherTabs()
        {
            foreach (DictionaryEntry item in ribbonTabs)
            {
                var ribbonTab = (RibbonTab) item.Value;
                ((BaseUserControl)NamingContainer).RegisterRibbonTab(ribbonTab);
                ((BaseUserControl)NamingContainer).LoadRibbonTab(ribbonTab);    
            }
        }

        protected override void RenderEmptyData(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "100");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(string.Format("There are no items to show in this view of the \"{0}\" list.", List.Title));
            if (EnableAddNewItem)
            {
                writer.Write(" " + LocalizationHelper.GetString("wss", "noXinviewofY_DEFAULT"));
            }
            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // tbody
        }

        protected override void RenderAddNewLink(HtmlTextWriter writer)
        {
            var href = string.Format("{0}/_layouts/listform.aspx?PageType=8&ListId={1}&RootFolder={2}", SPContext.Current.Web.Url, List.ID, SPEncode.UrlEncode(Page.Request.QueryString["RootFolder"]));

            var handlerStatement = new StringBuilder();
            handlerStatement.Append("var options = SP.UI.$create_DialogOptions();");
            handlerStatement.AppendFormat("options.url = '{0}';", href);
            handlerStatement.Append("options.dialogReturnValueCallback = Function.createDelegate(null, function(result, target){if(result == SP.UI.DialogResult.OK){");
            handlerStatement.AppendFormat("$('#{0}').val('True');", hdfAddNewItem.ClientID);
            handlerStatement.Append("SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);");
            handlerStatement.Append("}});");
            handlerStatement.Append("SP.UI.ModalDialog.showModalDialog(options);return false;");

            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-addnew");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "3px");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-clust");
            writer.AddAttribute(HtmlTextWriterAttribute.Style,
                                "position: relative; width: 10px; display: inline-block; height: 10px; overflow: hidden;");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("<img style=\"position: absolute; top: -128px !important; left: 0px !important;\" alt=\"\" src=\"/_layouts/images/fgimg.png\" />");
            writer.RenderEndTag(); // span

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-addnew");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, href);
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, handlerStatement.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, "3px");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write(LocalizationHelper.GetString("wss", "addnewitem"));
            writer.RenderEndTag();

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // table
        }

        protected override int GetTotalItems()
        {
            return AllDataSource.Rows.Count;
        }

        protected override double GetSumFieldData(BaseFieldRef fieldRef)
        {
            return fieldRef.GetSumFieldData(AllDataSource);
        }

        protected override int GetCountFieldData(BaseFieldRef fieldRef, Func<DataRow, bool> countCondition)
        {
            return AllDataSource.AsEnumerable().Where(countCondition).Count();
        }

        protected override double GetSumFieldData(BaseFieldRef fieldRef, Func<DataRow, bool> whereCondition)
        {
            return fieldRef.GetSumFieldData(AllDataSource, whereCondition);
        }

        protected override int CountGroupItems(IGroupFieldRef fieldRef, Func<DataRow, bool> filter)
        {
            return AllDataSource.AsEnumerable().Where(filter).Count();
        }

        protected override IDictionary<string, string> GetFilterValues(BaseFieldRef fieldRef)
        {
            throw new NotSupportedException();
        }

        #region Ribbon Tabs

        private void RegisterListTab()
        {
            var lcid = Thread.CurrentThread.CurrentUICulture.LCID;

            var listTab = new RibbonTab("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List")
                              {
                                  Title = LocalizationHelper.GetStringFromCoreResource("cui_TabList"),
                                  Sequence = 500
                              };

            var viewFormatGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.ViewFormat")
                    {
                        Sequence = 10,
                        Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpViewFormat"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = -256,
                        Image32By32PopupTop = -256,
                        GroupTemplate = RibbonGroupTemplate.Flexible2
                    };
            viewFormatGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.List.Scaling.ViewFormat.MaxSize")
                                             {Sequence = 10, Group = viewFormatGroup, Size = RibbonSize.LargeLarge});
            viewFormatGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.ViewFormat.Popup")
                                           {Sequence = 240, Group = viewFormatGroup, Size = RibbonSize.Popup});
            listTab.Groups.Add(viewFormatGroup);
            RegisterRibbonControls(viewFormatGroup);

            var btnStandard =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.ViewFormat.Controls.Standard")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButStandardView"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -32,
                        Image16By16Left = -144,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -256,
                        Image32By32Left = -256,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButStandardView"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButStandardView"),
                        TemplateAlias = "c1",
                    };
            viewFormatGroup.Controls.Add(btnStandard);

            var btnDatasheet =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.ViewFormat.Controls.Datasheet")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButDataSheetView"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -48,
                        Image16By16Left = -144,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = 0,
                        Image32By32Left = -288,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButDataSheetView"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButDatasheetView"),
                        TemplateAlias = "c1",
                    };
            viewFormatGroup.Controls.Add(btnDatasheet);

            var datasheetGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Datasheet")
                    {
                        Sequence = 20,
                        Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpDatasheet"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = -448,
                        Image32By32PopupTop = -256,
                        GroupTemplate = RibbonGroupTemplate.Flexible2
                    };
            viewFormatGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.List.Scaling.Datasheet.MaxSize")
                                             {Sequence = 20, Group = datasheetGroup, Size = RibbonSize.LargeMedium});
            viewFormatGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Datasheet.LargeSmall")
                                           {Sequence = 90, Group = datasheetGroup, Size = RibbonSize.LargeSmall});
            viewFormatGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Datasheet.Popup")
                                           {Sequence = 170, Group = datasheetGroup, Size = RibbonSize.Popup});
            listTab.Groups.Add(datasheetGroup);
            RegisterRibbonControls(datasheetGroup);

            var btnNewRow =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Datasheet.Controls.NewRow")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButNewRow"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -80,
                        Image16By16Left = -192,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -160,
                        Image32By32Left = -384,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButNewRow"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButNewRow"),
                        TemplateAlias = "c1",
                    };
            datasheetGroup.Controls.Add(btnNewRow);

            var btnShowTaskPane =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Datasheet.Controls.ShowTaskPane")
                    {
                        Sequence = 20,
                        LabelText = "",
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -16,
                        Image16By16Left = -152,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -256,
                        Image32By32Left = -224,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButShowTaskPane"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButShowTaskPane"),
                        TemplateAlias = "c2",
                    };
            datasheetGroup.Controls.Add(btnShowTaskPane);

            var btnShowTotals =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Datasheet.Controls.ShowTotals")
                    {
                        Sequence = 30,
                        LabelText = "",
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -240,
                        Image16By16Left = -224,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -448,
                        Image32By32Left = -256,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButShowTotals"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButShowTotals"),
                        TemplateAlias = "c2",
                    };
            datasheetGroup.Controls.Add(btnShowTotals);

            var btnRefreshData =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Datasheet.Controls.RefreshData")
                    {
                        Sequence = 40,
                        LabelText = "",
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -240,
                        Image16By16Left = -208,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -448,
                        Image32By32Left = -224,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButRefreshData"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButRefreshData"),
                        TemplateAlias = "c2",
                    };
            datasheetGroup.Controls.Add(btnRefreshData);

            var customViewsGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.CustomViews")
                    {
                        Sequence = 40,
                        Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpManageViews"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = -352,
                        Image32By32PopupTop = -320,
                        GroupTemplate = RibbonGroupTemplate.ManageViewsGroup
                    };
            customViewsGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.List.Scaling.CustomViews.MaxSize")
                                              {Sequence = 40, Group = customViewsGroup, Size = RibbonSize.LargeMedium});
            customViewsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.CustomViews.LargeSmall")
                                            {Sequence = 160, Group = customViewsGroup, Size = RibbonSize.LargeSmall});
            customViewsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.CustomViews.Popup")
                                            {Sequence = 220, Group = customViewsGroup, Size = RibbonSize.Popup});
            listTab.Groups.Add(customViewsGroup);
            RegisterRibbonControls(customViewsGroup);

            var btnCreateView =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.CustomViews.Controls.CreateView")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButCreateView"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -48,
                        Image16By16Left = -192,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -352,
                        Image32By32Left = -352,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButCreateView"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButCreateView"),
                        TemplateAlias = "c1",
                    };
            customViewsGroup.Controls.Add(btnCreateView);

            var btnModifyView =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.CustomViews.Controls.ModifyView")
                    {
                        Sequence = 20,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButModifyThisView"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -32,
                        Image16By16Left = -192,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -352,
                        Image32By32Left = -320,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButModifyThisView"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButModifyThisView"),
                        TemplateAlias = "c2",
                    };
            customViewsGroup.Controls.Add(btnModifyView);

            var btnCreateColumn =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.CustomViews.Controls.CreateColumn")
                    {
                        Sequence = 30,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButCreateColumn"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -144,
                        Image16By16Left = -176,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -96,
                        Image32By32Left = -352,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButCreateColumn"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButCreateColumn"),
                        TemplateAlias = "c2",
                    };
            customViewsGroup.Controls.Add(btnCreateColumn);

            var btnNavigateUp =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.CustomViews.Controls.NavigateUp")
                    {
                        Sequence = 30,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButNavigateUp"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = 0,
                        Image16By16Left = -56,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -64,
                        Image32By32Left = 0,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButNavigateUp"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButNavigateUp"),
                        TemplateAlias = "c2",
                    };

            var rootFolder = Page.Request.QueryString["RootFolder"];
            if (!string.IsNullOrEmpty(rootFolder))
            {
                var folder = SPContext.Current.Web.GetFolder(rootFolder);
                if (folder.ParentFolder != null && folder.UniqueId != List.RootFolder.UniqueId)
                {
                    var urlBuilder = new UrlBuilder(Page.Request.RawUrl);
                    urlBuilder.AddQueryString("RootFolder", folder.ParentFolder.ServerRelativeUrl);
                    urlBuilder.RemoveAllFilterQueryString();
                    urlBuilder.RemoveQueryString("Source");

                    btnNavigateUp.Command =
                        new SPRibbonCommand("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Commands.NavigateUp",
                            string.Format("window.location = '{0}';", urlBuilder));
                }
            }

            customViewsGroup.Controls.Add(btnNavigateUp);

            var shareGroup = new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Share")
                                 {
                                     Sequence = 50,
                                     Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpShare"),
                                     Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                                     Image32By32PopupLeft = -448,
                                     Image32By32PopupTop = -256,
                                     GroupTemplate = RibbonGroupTemplate.Flexible2
                                 };
            shareGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.List.Scaling.Share.MaxSize")
                                        {Sequence = 40, Group = shareGroup, Size = RibbonSize.LargeLarge});
            shareGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Share.MediumMedium")
                                      {Sequence = 90, Group = shareGroup, Size = RibbonSize.MediumMedium});
            shareGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Share.Popup")
                                      {Sequence = 170, Group = shareGroup, Size = RibbonSize.Popup});
            listTab.Groups.Add(shareGroup);
            RegisterRibbonControls(shareGroup);

            var handlerStatement = new StringBuilder();
            handlerStatement.AppendFormat("window.location = 'mailto:?body={0}';", DataViewUtils.Escape(Page.Request.Url.OriginalString));

            var btnEmailLibraryLink =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Share.Controls.EmailLibraryLink")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButEmailLink"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -16,
                        Image16By16Left = -88,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -128,
                        Image32By32Left = -448,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButEmailLink"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButEmailLinkList"),
                        Command =
                            new SPRibbonCommand(
                            "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Commands.EmailLibraryLink",
                            handlerStatement.ToString()),
                        TemplateAlias = "c1",
                    };
            shareGroup.Controls.Add(btnEmailLibraryLink);

            handlerStatement = new StringBuilder();
            handlerStatement.AppendFormat("window.location = '{0}/_layouts/listfeed.aspx?List={1}';",
                                          SPContext.Current.Web.Url, List.ID);

            var btnViewRssFeed =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Share.Controls.ViewRSSFeed")
                    {
                        Sequence = 30,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButViewRSSFeed"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -128,
                        Image16By16Left = -112,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -224,
                        Image32By32Left = -128,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButViewRSSFeed"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButViewRSSFeedList"),
                        Command =
                            new SPRibbonCommand(
                            "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Commands.ViewRSSFeed",
                            handlerStatement.ToString()),
                        TemplateAlias = "c1",
                    };
            shareGroup.Controls.Add(btnViewRssFeed);

            var actionsGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Actions")
                {
                    Sequence = 60,
                    Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpConnect"),
                    Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                    Image32By32PopupLeft = -224,
                    Image32By32PopupTop = -288,
                    GroupTemplate = RibbonGroupTemplate.Flexible2
                };
            actionsGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.List.Scaling.Actions.MaxSize") { Sequence = 50, Group = actionsGroup, Size = RibbonSize.LargeMedium });
            actionsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Actions.MediumMedium") { Sequence = 120, Group = actionsGroup, Size = RibbonSize.MediumMedium });
            actionsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Actions.MediumSmall") { Sequence = 140, Group = actionsGroup, Size = RibbonSize.MediumSmall });
            listTab.Groups.Add(actionsGroup);

            var btnTakeOfflineToClient =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Actions.Controls.TakeOfflineToClient")
                {
                    Sequence = 20,
                    LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButSyncToComputer"),
                    Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                    Image16By16Top = -160,
                    Image16By16Left = -176,
                    Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                    Image32By32Top = -288,
                    Image32By32Left = -224,
                    ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButSyncToComputer"),
                    ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButSyncListToComputer"),
                    TemplateAlias = "c1",
                };
            actionsGroup.Controls.Add(btnTakeOfflineToClient);

            var btnConnectToClient =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Actions.Controls.ConnectToClient")
                {
                    Sequence = 30,
                    LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButConnectToClient"),
                    Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                    Image16By16Top = 0,
                    Image16By16Left = -168,
                    Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                    Image32By32Top = -128,
                    Image32By32Left = -352,
                    ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButConnectToClient"),
                    ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButConnectToClient"),
                    TemplateAlias = "c1",
                };
            actionsGroup.Controls.Add(btnConnectToClient);

            var exportToSpreadsheetCommand = string.Format("window.location='{0}/_vti_bin/owssvr.dll?CS=65001&Using=_layouts/query.iqy&List={1}&View={2}&RootFolder={3}&CacheControl=1';", SPContext.Current.Web.Url, List.ID, SPContext.Current.ViewContext.ViewId, List.RootFolder.Url);

            var btnExportToSpreadsheet =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Actions.Controls.ExportToSpreadsheet")
                {
                    Sequence = 30,
                    LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButExportToSpreadsheet"),
                    Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                    Image16By16Top = -152,
                    Image16By16Left = -32,
                    Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                    Image32By32Top = -352,
                    Image32By32Left = 0,
                    ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButExportToSpreadsheet"),
                    ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButExportListToSpreadsheet"),
                    TemplateAlias = "c1",
                    Command = new SPRibbonCommand("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Commands.ExportToSpreadsheet", exportToSpreadsheetCommand, "true")
                };
            actionsGroup.Controls.Add(btnExportToSpreadsheet);

            var settingsGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Settings")
                    {
                        Sequence = 80,
                        Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpSettings"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = 0,
                        Image32By32PopupTop = -384,
                        GroupTemplate = RibbonGroupTemplate.Flexible2
                    };
            settingsGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.List.Scaling.Settings.MaxSize")
                                           {Sequence = 70, Group = settingsGroup, Size = RibbonSize.LargeLarge});
            settingsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Settings.LargeMedium")
                                         {Sequence = 100, Group = settingsGroup, Size = RibbonSize.LargeMedium});
            settingsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Settings.LargeSmall")
                                         {Sequence = 130, Group = settingsGroup, Size = RibbonSize.LargeSmall});
            settingsGroup.Scales.Add(new RibbonScale("Ribbon.List.Scaling.Settings.Popup")
                                         {Sequence = 190, Group = settingsGroup, Size = RibbonSize.Popup});
            listTab.Groups.Add(settingsGroup);
            RegisterRibbonControls(settingsGroup);

            handlerStatement = new StringBuilder();
            handlerStatement.AppendFormat("window.location = '{0}/_layouts/listedit.aspx?List={1}';",
                                          SPContext.Current.Web.Url, List.ID);

            var btnListSettings =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Settings.Controls.ListSettings")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButListSettings"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -64,
                        Image16By16Left = -192,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = 0,
                        Image32By32Left = -384,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButListSettings"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButListSettings"),
                        Command =
                            new SPRibbonCommand(
                            "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Commands.ListSettings",
                            handlerStatement.ToString()),
                        TemplateAlias = "c1",
                    };
            settingsGroup.Controls.Add(btnListSettings);

            var btnListPermissions =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Settings.Controls.ListPermissions")
                    {
                        Sequence = 20,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButListPermissions"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -128,
                        Image16By16Left = 0,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = 0,
                        Image32By32Left = -416,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButListPermissions"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButListPermissions"),
                        TemplateAlias = "c2",
                    };
            settingsGroup.Controls.Add(btnListPermissions);

            var btnManageWorkflows =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.List.Groups.Settings.Controls.ManageWorkflows")
                    {
                        Sequence = 30,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButManageWorkflow"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -112,
                        Image16By16Left = -112,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -160,
                        Image32By32Left = -416,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButManageWorkflow"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButListManageWorkflow"),
                        TemplateAlias = "c2",
                    };
            settingsGroup.Controls.Add(btnManageWorkflows);

            // Register custom ribbon groups
            RegisterRibbonGroups(listTab);

            // Manual register group template
            ((BaseUserControl) NamingContainer).RegisterRibbonGroupTemplate(RibbonGroupTemplate.ManageViewsGroup);
            ((BaseUserControl) NamingContainer).RegisterRibbonTab(listTab, false);
            ((BaseUserControl) NamingContainer).LoadRibbonTab(listTab);
        }

        private void RegisterListItemTab()
        {
            var lcid = Thread.CurrentThread.CurrentUICulture.LCID;

            var listItemTab = new RibbonTab("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem")
                                  {
                                      Title = LocalizationHelper.GetStringFromCoreResource("cui_TabItems"),
                                      Sequence = 400
                                  };

            var newGroup = new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.New")
                               {
                                   Sequence = 10,
                                   Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpNew"),
                                   Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                                   Image32By32PopupLeft = -64,
                                   Image32By32PopupTop = -320,
                                   GroupTemplate = RibbonGroupTemplate.Flexible2
                               };
            newGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.ListItem.Scaling.New.MaxSize")
                                      {Sequence = 10, Group = newGroup, Size = RibbonSize.LargeLarge});
            newGroup.Scales.Add(new RibbonScale("Ribbon.ListItem.Scaling.New.Popup")
                                    {Sequence = 160, Group = newGroup, Size = RibbonSize.Popup});

            listItemTab.Groups.Add(newGroup);

            RegisterRibbonControls(newGroup);

            RegisterAddListItemControl(newGroup, lcid);

            var btnNewFolder =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.New.Controls.NewFolder")
                    {
                        Sequence = 20,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButNewFolder"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -16,
                        Image16By16Left = -248,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -448,
                        Image32By32Left = -320,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButNewFolder"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButNewListFolder"),
                        TemplateAlias = "c1"
                    };
            
            if (EnableAddNewItem && List.EnableFolderCreation)
            {
                var command = new StringBuilder();
                command.Append("var options = SP.UI.$create_DialogOptions();");
                command.AppendFormat(
                    "options.url = '{0}/_layouts/listform.aspx?&Type=1&PageType=8&ListId={1}&RootFolder={2}';",
                    SPContext.Current.Web.Url, List.ID, SPEncode.UrlEncode(Page.Request.QueryString["RootFolder"]));
                command.Append("options.dialogReturnValueCallback = Function.createDelegate(null, function(result, target){if(result == SP.UI.DialogResult.OK){");
                command.AppendFormat("$('#{0}').val('True');", hdfAddNewItem.ClientID);
                command.Append("SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);");
                command.Append("}});");
                command.Append("SP.UI.ModalDialog.showModalDialog(options);");

                var newItemCommand =
                    new SPRibbonCommand("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Commands.NewFolder")
                    {
                        HandlerStatement = command.ToString()
                    };
                btnNewFolder.Command = newItemCommand;
            }

            newGroup.Controls.Add(btnNewFolder);

            var manageGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Manage")
                    {
                        Sequence = 20,
                        Title = LocalizationHelper.GetStringFromCoreResource("GrpManage"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = -64,
                        Image32By32PopupTop = -448,
                        GroupTemplate = RibbonGroupTemplate.Flexible2
                    };
            manageGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.ListItem.Scaling.Manage.MaxSize")
                                         {Sequence = 10, Group = manageGroup, Size = RibbonSize.LargeMedium});
            manageGroup.Scales.Add(new RibbonScale("Ribbon.ListItem.Scaling.Manage.LargeSmall")
                                       {Group = manageGroup, Sequence = 100, Size = RibbonSize.LargeSmall});
            manageGroup.Scales.Add(new RibbonScale("Ribbon.ListItem.Scaling.Manage.Popup")
                                       {Group = manageGroup, Sequence = 150, Size = RibbonSize.Popup});
            listItemTab.Groups.Add(manageGroup);
            RegisterRibbonControls(manageGroup);

            var handlerStatement = new StringBuilder();
            handlerStatement.Append("var item = getSelectedItems()[0];");
            handlerStatement.Append("var options = SP.UI.$create_DialogOptions();");
            handlerStatement.AppendFormat(
                "options.url = '{0}/_layouts/listform.aspx?PageType=4&ListId=' + item.refListId + '&ID=' + item.refId;",
                SPContext.Current.Web.Url);
            handlerStatement.Append(
                "options.dialogReturnValueCallback = Function.createDelegate(null, function(result, target){if(result == SP.UI.DialogResult.OK){SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);}});");
            handlerStatement.Append("SP.UI.ModalDialog.showModalDialog(options);");

            var viewPropertiesCommand =
                new SPRibbonCommand("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Commands.ViewItem")
                    {
                        HandlerStatement = handlerStatement.ToString(),
                        EnabledStatement = "(getSelectedItems().length == 1)"
                    };

            var btnViewProperties =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Manage.Controls.ViewProperties")
                    {
                        Sequence = 20,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButViewItem"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -32,
                        Image16By16Left = -80,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -96,
                        Image32By32Left = -448,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButViewItem"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButViewItem"),
                        Command = viewPropertiesCommand,
                        TemplateAlias = "c1"
                    };
            manageGroup.Controls.Add(btnViewProperties);

            var btnEditProperties =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Manage.Controls.EditProperties")
                    {
                        Sequence = 20,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButEditItem"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -128,
                        Image16By16Left = -224,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -128,
                        Image32By32Left = -96,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButEditItem"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButEditItem"),
                        TemplateAlias = "c1"
                    };
            manageGroup.Controls.Add(btnEditProperties);

            if (EnableEditItem)
            {
                handlerStatement = new StringBuilder();
                handlerStatement.Append("var item = getSelectedItems()[0];");
                handlerStatement.Append("var options = SP.UI.$create_DialogOptions();");
                handlerStatement.AppendFormat(
                    "options.url = '{0}/_layouts/listform.aspx?PageType=6&ListId=' + item.refListId + '&ID=' + item.refId;",
                    SPContext.Current.Web.Url);
                handlerStatement.Append(
                    "options.dialogReturnValueCallback = Function.createDelegate(null, function(result, target){if(result == SP.UI.DialogResult.OK){SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);}});");
                handlerStatement.Append("SP.UI.ModalDialog.showModalDialog(options);");

                var editPropertiesCommand =
                    new SPRibbonCommand("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Commands.EditItem")
                        {
                            HandlerStatement = handlerStatement.ToString(),
                            EnabledStatement = "(getSelectedItems().length == 1)"
                        };
                btnEditProperties.Command = editPropertiesCommand;
            }

            var btnViewVersions =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Manage.Controls.ViewVersions")
                    {
                        Sequence = 30,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButVersionHistory"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -48,
                        Image16By16Left = -80,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -64,
                        Image32By32Left = -448,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButVersionHistory"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButItemVersionHistory"),
                        TemplateAlias = "c2"
                    };
            manageGroup.Controls.Add(btnViewVersions);

            var btnManagePermissions =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Manage.Controls.ManagePermissions")
                    {
                        Sequence = 40,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButItemPermissions"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -128,
                        Image16By16Left = 0,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = 0,
                        Image32By32Left = -416,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButItemPermissions"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButItemPermissions"),
                        TemplateAlias = "c2"
                    };
            manageGroup.Controls.Add(btnManagePermissions);

            var btnDelete =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Manage.Controls.Delete")
                    {
                        Sequence = 50,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButDeleteItem"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -112,
                        Image16By16Left = -224,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -128,
                        Image32By32Left = -128,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButDeleteItem"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButDeleteItem"),
                        TemplateAlias = "c2"
                    };
            manageGroup.Controls.Add(btnDelete);

            if (EnableDeleteItem)
            {
                handlerStatement = new StringBuilder();
                handlerStatement.Append(
                    "if(!confirm('Are you sure you want to send the item(s) to the site Recycle Bin?')) return;");
                handlerStatement.Append("var items = getSelectedItems();");
                handlerStatement.Append("var ctx = new SP.ClientContext.get_current();");
                if (SPContext.Current.Site.WebApplication.RecycleBinEnabled)
                {
                    handlerStatement.Append("$.each(items, function(index, item){var list = ctx.get_web().get_lists().getById(item.refListId);var listItem = list.getItemById(item.refId);listItem.recycle();});");    
                }
                else
                {
                    handlerStatement.Append("$.each(items, function(index, item){var list = ctx.get_web().get_lists().getById(item.refListId);var listItem = list.getItemById(item.refId);listItem.deleteObject();});");
                }
                
                handlerStatement.Append("ctx.executeQueryAsync(Function.createDelegate(null, function(){SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);}), null);");

                var deleteItemCommand =
                    new SPRibbonCommand(
                        "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Commands.DeleteItem")
                        {
                            HandlerStatement = handlerStatement.ToString(),
                            EnabledStatement = "(getSelectedItems().length > 0)"
                        };
                btnDelete.Command = deleteItemCommand;
            }

            var actionsGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Actions")
                    {
                        Sequence = 30,
                        Title = LocalizationHelper.GetStringFromCoreResource("cui_GrpActions"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = -352,
                        Image32By32PopupTop = -128,
                        GroupTemplate = RibbonGroupTemplate.Flexible2
                    };
            actionsGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.ListItem.Scaling.Actions.MaxSize")
                                          {Sequence = 30, Group = actionsGroup, Size = RibbonSize.LargeLarge});
            actionsGroup.Scales.Add(new RibbonScale("Ribbon.ListItem.Scaling.Actions.Popup")
                                        {Sequence = 130, Group = actionsGroup, Size = RibbonSize.Popup});
            listItemTab.Groups.Add(actionsGroup);
            RegisterRibbonControls(actionsGroup);

            var btnAttachFile =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Actions.Controls.AttachFile")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButAttachFile"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -128,
                        Image16By16Left = -144,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -352,
                        Image32By32Left = -128,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButAttachFile"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButAttachFile"),
                        TemplateAlias = "c1"
                    };
            actionsGroup.Controls.Add(btnAttachFile);

            var workflowGroup =
                new RibbonGroup("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Workflows")
                    {
                        Sequence = 50,
                        Title = LocalizationHelper.GetStringFromCoreResource("GrpWorkflow"),
                        Image32By32Popup = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32PopupLeft = -192,
                        Image32By32PopupTop = -416,
                        GroupTemplate = RibbonGroupTemplate.Flexible2
                    };
            workflowGroup.MaxSizes.Add(new RibbonMaxSize("Ribbon.ListItem.Scaling.Workflows.MaxSize")
                                           {Sequence = 50, Group = workflowGroup, Size = RibbonSize.LargeLarge});
            workflowGroup.Scales.Add(new RibbonScale("Ribbon.ListItem.Scaling.Workflows.MediumMedium")
                                         {Sequence = 70, Group = workflowGroup, Size = RibbonSize.MediumMedium});
            workflowGroup.Scales.Add(new RibbonScale("Ribbon.ListItem.Scaling.Workflows.Popup")
                                         {Sequence = 110, Group = workflowGroup, Size = RibbonSize.Popup});
            listItemTab.Groups.Add(workflowGroup);
            RegisterRibbonControls(workflowGroup);

            var btnViewWorkflows =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Workflows.Controls.ViewWorkflows")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButWorkflows"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -48,
                        Image16By16Left = -208,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -192,
                        Image32By32Left = -416,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButWorkflows"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButItemManageWorkflow"),
                        TemplateAlias = "c1"
                    };
            workflowGroup.Controls.Add(btnViewWorkflows);

            var btnModerate =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.Workflows.Controls.Moderate")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButApproveReject"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -48,
                        Image16By16Left = -240,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -448,
                        Image32By32Left = -384,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButApproveReject"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButItemApproveReject"),
                        TemplateAlias = "c1"
                    };
            workflowGroup.Controls.Add(btnModerate);

            // Register custom ribbon groups
            RegisterRibbonGroups(listItemTab);

            ((BaseUserControl) NamingContainer).RegisterRibbonTab(listItemTab);
            ((BaseUserControl) NamingContainer).LoadRibbonTab(listItemTab);
        }

        protected virtual void RegisterAddListItemControl(RibbonGroup newGroup, int lcid)
        {
            var btnNewItem =
                new RibbonButton(
                    "VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Groups.New.Controls.NewItem")
                    {
                        Sequence = 10,
                        LabelText = LocalizationHelper.GetStringFromCoreResource("cui_ButNewItem"),
                        Image16By16 = string.Format("/_layouts/{0}/images/formatmap16x16.png", lcid),
                        Image16By16Top = -176,
                        Image16By16Left = -64,
                        Image32By32 = string.Format("/_layouts/{0}/images/formatmap32x32.png", lcid),
                        Image32By32Top = -320,
                        Image32By32Left = -64,
                        ToolTipTitle = LocalizationHelper.GetStringFromCoreResource("cui_ButNewItem"),
                        ToolTipDescription = LocalizationHelper.GetStringFromCoreResource("cui_STT_ButNewItem"),
                        TemplateAlias = "c1"
                    };
            newGroup.Controls.Add(btnNewItem);

            if (EnableAddNewItem)
            {
                var handlerStatement = new StringBuilder();
                handlerStatement.Append("var options = SP.UI.$create_DialogOptions();");
                handlerStatement.AppendFormat(
                    "options.url = '{0}/_layouts/listform.aspx?PageType=8&ListId={1}&RootFolder={2}';",
                    SPContext.Current.Web.Url, List.ID, SPEncode.UrlEncode(Page.Request.QueryString["RootFolder"]));
                handlerStatement.Append("options.dialogReturnValueCallback = Function.createDelegate(null, function(result, target){if(result == SP.UI.DialogResult.OK){");
                handlerStatement.AppendFormat("$('#{0}').val('True');", hdfAddNewItem.ClientID);
                handlerStatement.Append("SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);");
                handlerStatement.Append("}});");
                handlerStatement.Append("SP.UI.ModalDialog.showModalDialog(options);");

                var newItemCommand =
                    new SPRibbonCommand("VP.Sharepoint.CQ.Core.Ribbon.Tabs.ListItem.Commands.NewItem")
                        {
                            HandlerStatement = handlerStatement.ToString()
                        };
                btnNewItem.Command = newItemCommand;
            }
        }

        public void AddRibbonTab(RibbonTab ribbonTab)
        {
            ribbonTabs.Add(ribbonTab.Id, ribbonTab);
        }

        public void AddRibbonGroup(RibbonGroup ribbonGroup, string location)
        {
            IList<RibbonGroup> groups;
            if (ribbonGroups.Contains(location))
            {
                groups = (IList<RibbonGroup>) ribbonGroups[location];
            }
            else
            {
                groups = new List<RibbonGroup>();
                ribbonGroups[location] = groups;
            }

            groups.Add(ribbonGroup);
        }

        public void AddRibbonControl(IRibbonControl ribbonControl, string location)
        {
            IList<IRibbonControl> controls;
            if (ribbonControls.Contains(location))
            {
                controls = (IList<IRibbonControl>) ribbonControls[location];
            }
            else
            {
                controls = new List<IRibbonControl>();
                ribbonControls[location] = controls;
            }

            controls.Add(ribbonControl);
        }

        private void RegisterRibbonGroups(RibbonTab ribbonTab)
        {
            if (ribbonGroups.Contains(ribbonTab.Id))
            {
                var groups = (IList<RibbonGroup>) ribbonGroups[ribbonTab.Id];
                ribbonTab.Groups.AddRange(groups);
            }
        }

        private void RegisterRibbonControls(RibbonGroup ribbonGroup)
        {
            if (ribbonControls.Contains(ribbonGroup.Id))
            {
                var controls = (IList<IRibbonControl>) ribbonControls[ribbonGroup.Id];
                ribbonGroup.Controls.AddRange(controls);
            }
        }

        #endregion

        protected override void OnFieldFilter()
        {
            base.OnFieldFilter();
            ListItemCollectionPositions.Clear();
        }

        protected override void OnFieldSorting()
        {
            base.OnFieldSorting();
            ListItemCollectionPositions.Clear();
        }

        protected override string GetDisplayItemLink(DataRow item, out bool showItemIndialog)
        {
            var web = SPContext.Current.Web;
            var rawUrl = SPEncode.UrlEncode(Page.Request.RawUrl);

            switch (List.BaseTemplate)
            {
                case SPListTemplateType.DiscussionBoard:
                    var discussionList = web.Lists[new Guid(item["ListId"].ToString())];
                    var discussionItem = discussionList.GetItemById(Convert.ToInt32(item["ID"]));
                    var rootFolder = SPEncode.UrlEncode(web.ServerRelativeUrl.TrimEnd('/') + "/" + discussionItem.Url);
                    showItemIndialog = false;
                    return string.Format("{0}/{1}/Flat.aspx?RootFolder={2}", web.Url, discussionList.RootFolder.Url, rootFolder);
                default:
                    var type = item["Item Type"].ToString();
                    if (type == "1;#")
                    {
                        showItemIndialog = false;
                        var urlBuilder = new UrlBuilder(Page.Request.Url);
                        urlBuilder.ClearQueryString();
                        urlBuilder.AddQueryString("RootFolder", item["ServerUrl"].ToString());
                        return urlBuilder.ToString();
                    }

                    showItemIndialog = true;
                    return string.Format("{0}/_layouts/listform.aspx?PageType=4&ListId={1}&ID={2}&Source={3}",
                                         web.Url, item["ListId"], item["ID"], rawUrl);
            }
        }
    }
}
