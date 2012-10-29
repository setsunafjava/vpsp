using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using VP.Sharepoint.CQ.Core.Helpers;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class TextFieldRef : BaseFieldRef, IGroupFieldRef
    {
        public TextFieldRef()
        {
            EmptyGroupString = "Empty";
        }

        #region IGroupFieldRef Members

        public string GetGroupFieldRef(string sortField, string sortDir)
        {
            ListSortDirection sortDirection;
            if (InternalFieldName.Equals(sortField))
            {
                sortDirection = sortDir == "ASC" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            }
            else
            {
                sortDirection = SortDirection;
            }

            return string.Format(sortDirection == ListSortDirection.Ascending
                                     ? "<FieldRef Name='{0}' />"
                                     : "<FieldRef Name='{0}' Ascending='FALSE' />", InternalFieldName);
        }

        public bool CollapsedGroup { get; set; }

        public bool CountGroupItems { get; set; }

        public bool SumGroupFieldData { get; set; }

        public IGroupFieldRef ParentGroup { get; set; }

        public string EmptyGroupString { get; set; }

        public ListSortDirection SortDirection { get; set; }

        public IEnumerable<IGrouping<object, DataRow>> GetGroupBy(DataTable dt)
        {
            return dt.AsEnumerable().GroupBy(g => g[FieldName], new StringComparer());
        }

        public IEnumerable<IGrouping<object, DataRow>> GetGroupBy(IGrouping<object, DataRow> grouping)
        {
            return grouping.GroupBy(item => item[FieldName], new StringComparer());
        }

        public Expression<Func<DataRow, bool>> AddFilterExpression(Expression<Func<DataRow, bool>> filter,
                                                                   IGrouping<object, DataRow> group)
        {
            if (group.Key is DBNull)
            {
                return filter.And(item => DataViewUtils.IsDBNull(item, FieldName));
            }

            return filter.And(item => DataViewUtils.CompareStringObject(group.Key, item[FieldName]));
        }

        public virtual void RenderCell(HtmlTextWriter writer, IGrouping<object, DataRow> grouping)
        {
            var groupKey = Convert.ToString(grouping.Key);
            if (string.IsNullOrEmpty(groupKey))
            {
                groupKey = EmptyGroupString;
            }

            writer.Write(SPEncode.HtmlEncode(groupKey));
        }

        public override void RenderCell(HtmlTextWriter writer, DataRow row)
        {
            var value = Convert.ToString(row[FieldName]);
            writer.Write(string.IsNullOrEmpty(value) ? DefaultValue : SPEncode.HtmlEncode(value));
        }

        #endregion

        public override string GetFilterCamlQuery()
        {
            return string.IsNullOrEmpty(FilterValue) ?
                            string.Format("<IsNull><FieldRef Name='{0}' /></IsNull>", InternalFieldName) :
                            string.Format("<Eq><FieldRef Name='{0}' /><Value Type='Text'><![CDATA[{1}]]></Value></Eq>", InternalFieldName, FilterValue);
        }

        internal class StringComparer : IEqualityComparer<object>
        {
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                if (ReferenceEquals(x, y)) return true;

                if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                    return false;
                return string.Equals(x.ToString(), y.ToString(), StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(object obj)
            {
                if (ReferenceEquals(obj, null))
                {
                    return 0;
                }
                return obj.ToString().ToLowerInvariant().GetHashCode();
            }
        }

        public override string GetCellTextValue(DataRow row)
        {
            var value = row[FieldName];
            if (value is DBNull)
            {
                return DefaultValue;
            }

            return SPEncode.HtmlEncode(value.ToString());
        }
    }
}
