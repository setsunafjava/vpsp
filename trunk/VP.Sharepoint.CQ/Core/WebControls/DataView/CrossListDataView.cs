using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class CrossListDataView : ListDataView
    {
        #region Privates

        private IList<SPList> lists;
        
        #endregion

        [Browsable(true)]
        public string ListNames
        {
            get
            {
                var value = ViewState["ListNames"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set
            {
                ViewState["ListNames"] = value;
                lists = null;
            }
        }

        public IList<SPList> Lists
        {
            get
            {
                if (lists == null)
                {
                    var split = ListNames.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                    lists = new ReadOnlyCollection<SPList>(split.Select(str => SPContext.Current.Web.Lists[str]).ToList());
                }
                return lists;
            }
        }

        public override SPList List
        {
            get
            {
                return Lists[0];
            }
        }

        public override int CurrentPage
        {
            get
            {
                var currentPage = Page.Request.QueryString["CurrentPage"];
                return !string.IsNullOrEmpty(currentPage) ? Convert.ToInt32(currentPage) : 1;
            }
            set
            {
                base.CurrentPage = value;
            }
        }

        protected override void BindDataSource()
        {
            var startRowIndex = (CurrentPage - 1) * RowLimit;

            try
            {
                var siteDataQuery = new SPSiteDataQuery();
                
                var sb = new StringBuilder();

                // Lists
                sb.Append("<Lists>");
                foreach (var list in Lists)
                {
                    sb.AppendFormat("<List ID='{0}' />", list.ID.ToString("B"));
                }
                sb.Append("</Lists>");
                siteDataQuery.Lists = sb.ToString();

                // ViewFields
                siteDataQuery.ViewFields = BuildViewFields();

                // Where
                sb = new StringBuilder();
                var filterCondition = new StringBuilder();
                var hasFilter = false;
                foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(viewField => viewField.IsFilter))
                {
                    filterCondition.Append(viewField.GetFilterCamlQuery());
                    if (hasFilter)
                    {
                        filterCondition.Insert(0, "<And>");
                        filterCondition.Append("</And>");
                    }
                    hasFilter = true;
                }

                if (!hasFilter)
                {
                    sb.Append(WhereCondition);
                }
                else
                {
                    if (string.IsNullOrEmpty(WhereCondition))
                    {
                        sb.AppendFormat("<Where>{0}</Where>", filterCondition);
                    }
                    else
                    {
                        var whereCondition = WhereCondition.Replace("<Where>", "").Replace("</Where>", "");
                        if (!string.IsNullOrEmpty(whereCondition))
                        {
                            whereCondition = string.Format("<Where><And>{0}{1}</And></Where>", filterCondition,
                                                           whereCondition);
                            sb.Append(whereCondition);
                        }
                    }
                }

                var sortedFields = new List<string>();

                sb.Append("<OrderBy>");

                foreach (
                    var groupField in
                        GroupFields.Cast<IGroupFieldRef>().Where(item => !sortedFields.Contains(item.InternalFieldName)))
                {
                    sb.AppendFormat(groupField.GetGroupFieldRef(SortField, SortDir));
                    sortedFields.Add(groupField.InternalFieldName);
                }

                if (!string.IsNullOrEmpty(SortField))
                {
                    sb.AppendFormat(
                        SortDir == "ASC" ? "<FieldRef Name='{0}' />" : "<FieldRef Name='{0}' Ascending='FALSE' />",
                        SortField);
                    sortedFields.Add(SortField);
                }

                // OrderBy
                if (SortFields.Count > 0)
                {
                    foreach (
                        var field in
                            SortFields.Cast<SortFieldRef>().Where(field => !sortedFields.Contains(field.FieldName)))
                    {
                        sb.AppendFormat(field.GetSortFieldRef());
                    }
                }
                sb.Append("</OrderBy>");
                siteDataQuery.Query = sb.ToString();
                
                // Webs
                siteDataQuery.Webs = "<Webs Scope='SiteCollection' />";

                AllDataSource = GetDataTable(SPContext.Current.Web.GetSiteData(siteDataQuery));

                DataSource = AllDataSource.Clone();
                
                for (var i = 0; i < RowLimit; i++)
                {
                    try
                    {
                        var row = AllDataSource.Rows[startRowIndex + i];
                        DataSource.ImportRow(row);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                }
                
                if (AllDataSource.Rows.Count > startRowIndex + RowLimit)
                {
                    NextPagePosition = string.Format("CurrentPage={0}&PageFirstRow={1}", CurrentPage + 1, startRowIndex);
                }

                if (CurrentPage > 1)
                {
                    PrevPagePosition = string.Format("CurrentPage={0}&PageFirstRow={1}", CurrentPage - 1, startRowIndex);
                }
            }
            catch (SPQueryThrottledException)
            {
                ThresholdException = true;
                return;
            }
        }

        private DataTable GetDataTable(DataTable dataTable)
        {
            var dt = new DataTable();

            var fieldNames = ViewFields.Cast<IViewFieldRef>().Where(item => !item.IsVirtualField).Select(viewField => viewField.FieldName).ToList();
            fieldNames.AddRange(GroupFields.Cast<IGroupFieldRef>().Select(groupField => groupField.FieldName));
            fieldNames.AddRange(SortFields.Cast<SortFieldRef>().Select(sortField => sortField.FieldName));
            fieldNames.AddRange(new[] { "ID", "Created" });
            fieldNames = fieldNames.Distinct().ToList();

            var fields = new List<SPField>();

            foreach (var fieldName in fieldNames)
            {
                var field = List.Fields[fieldName];
                fields.Add(field);

                Type fieldType;
                switch (field.Type)
                {
                    case SPFieldType.Number:
                    case SPFieldType.Currency:
                        fieldType = typeof (double);
                        break;
                    case SPFieldType.DateTime:
                    fieldType = typeof(DateTime);
                        break;
                    case SPFieldType.Attachments:
                    case SPFieldType.Boolean:
                        fieldType = typeof(bool);
                        break;
                    case SPFieldType.Calculated:
                        fieldType = typeof(object);
                        break;
                    case SPFieldType.User:
                        fieldType = ((SPFieldUser)field).AllowMultipleValues ? typeof(SPFieldUserValueCollection) : typeof(SPFieldUserValue);
                        break;
                    default:
                        fieldType = typeof (string);
                        break;
                }
                dt.Columns.Add(fieldName, fieldType);
            }

            dt.Columns.Add("ListId", typeof(string));
            dt.Columns.Add("RowIndex", typeof(int));

            var rowIndex = 0;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var newRow = dt.NewRow();
                var index = 0;
                foreach (var fieldName in fieldNames)
                {
                    var field = fields[index];
                    var value = Convert.ToString(dataRow[field.InternalName]);
                    if (!string.IsNullOrEmpty(value))
                    {
                        switch (field.Type)
                        {
                            case SPFieldType.Attachments:
                            case SPFieldType.Boolean:
                                newRow[fieldName] = value == "1";
                                break;
                            case SPFieldType.Number:
                            case SPFieldType.Currency:
                                newRow[fieldName] = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                                break;
                            case SPFieldType.DateTime:
                                newRow[fieldName] = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                                break;
                            case SPFieldType.Calculated:
                                var split = value.Split(new[] { ";#" }, StringSplitOptions.None);
                                var splitValue = string.Join("", split, 1, split.Length - 1);
                                if (!string.IsNullOrEmpty(splitValue))
                                {
                                    var calculatedField = (SPFieldCalculated)field;
                                    switch (calculatedField.OutputType)
                                    {
                                        case SPFieldType.Number:
                                        case SPFieldType.Currency:
                                            try
                                            {
                                                newRow[fieldName] = Convert.ToDouble(splitValue, CultureInfo.InvariantCulture);
                                            }
                                            catch (FormatException)
                                            {
                                                newRow[fieldName] = splitValue;
                                            }
                                            break;
                                        case SPFieldType.DateTime:
                                            try
                                            {
                                                newRow[fieldName] = Convert.ToDateTime(splitValue, CultureInfo.InvariantCulture);
                                            }
                                            catch (FormatException)
                                            {
                                                newRow[fieldName] = splitValue;
                                            }
                                            break;
                                        case SPFieldType.Boolean:
                                            switch (splitValue)
                                            {
                                                case "1":
                                                    newRow[fieldName] = true;
                                                    break;
                                                case "0":
                                                    newRow[fieldName] = false;
                                                    break;
                                                default:
                                                    newRow[fieldName] = splitValue;
                                                    break;
                                            }
                                            break;
                                        default:
                                            newRow[fieldName] = splitValue;
                                            break;
                                    }   
                                }
                                break;
                            case SPFieldType.User:
                                newRow[fieldName] = field.GetFieldValue(value);
                                break;
                            default:
                                newRow[fieldName] = value;
                                break;
                        }
                    }
                    index++;
                }

                newRow["ListId"] = dataRow["ListId"];
                newRow["RowIndex"] = rowIndex;
                rowIndex++;

                dt.Rows.Add(newRow);
            }

            return dt;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ThresholdException)
            {
                var maxQueryLookupFields = SPContext.Current.Site.WebApplication.MaxItemsPerThrottledOperation;
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(SPResource.GetString("DataViewThrottleText", new object[] { maxQueryLookupFields }));
                writer.RenderEndTag(); // span
            }
            else
            {
                base.Render(writer);    
            }
        }

        protected override string GetDisplayItemLink(DataRow item, out bool showItemIndialog)
        {
            var web = SPContext.Current.Web;
            var list = web.Lists[new Guid(item["ListId"].ToString())];
            var rawUrl = SPEncode.UrlEncode(Page.Request.RawUrl);

            switch (list.BaseTemplate)
            {
                case SPListTemplateType.DiscussionBoard:
                    var discussionItem = list.GetItemById(Convert.ToInt32(item["ID"]));
                    var rootFolder = SPEncode.UrlEncode(web.ServerRelativeUrl.TrimEnd('/') + "/" + discussionItem.Url);
                    showItemIndialog = false;
                    return string.Format("{0}/{1}/Flat.aspx?RootFolder={2}", web.Url, list.RootFolder.Url, rootFolder);
                default:
                    var contentType = item["Content Type ID"].ToString();
                    if (contentType.StartsWith("0x0120"))
                    {
                        var folder = list.GetItemById(Convert.ToInt32(item["ID"]));
                        var queryString = HttpUtility.ParseQueryString(Page.Request.Url.Query);
                        queryString["RootFolder"] = folder.Folder.ServerRelativeUrl;
                        showItemIndialog = false;
                        return string.Format("{0}?{1}", Page.Request.Url.GetLeftPart(UriPartial.Path), queryString.ConstructQueryString());
                    }

                    showItemIndialog = true;
                    return string.Format("{0}/_layouts/listform.aspx?PageType=4&ListId={1}&ID={2}&Source={3}", web.Url, item["ListId"], item["ID"], rawUrl);
            }
        }

        protected override void RenderEmptyData(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "100");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("There are no items to show in this view.");
            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // tbody
        }
    }
}
