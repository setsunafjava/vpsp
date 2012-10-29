using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public interface IGroupFieldRef : IViewFieldRef
    {
        bool CountGroupItems { get; set; }

        bool CountFieldData { get; set; }

        bool SumGroupFieldData { get; set; }

        bool CollapsedGroup { get; set; }

        IGroupFieldRef ParentGroup { get; set; }

        string EmptyGroupString { get; set; }

        ListSortDirection SortDirection { get; set; }

        Expression<Func<DataRow, bool>> AddFilterExpression(Expression<Func<DataRow, bool>> filter,
                                                            IGrouping<object, DataRow> group);

        IEnumerable<IGrouping<object, DataRow>> GetGroupBy(DataTable dt);

        IEnumerable<IGrouping<object, DataRow>> GetGroupBy(IGrouping<object, DataRow> grouping);

        void RenderCell(HtmlTextWriter writer, IGrouping<object, DataRow> grouping);

        string GetGroupFieldRef(string sortField, string sortDir);
    }
}
