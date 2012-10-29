using System;
using System.Data;
using System.Web.UI;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class TypeFieldRef : BaseFieldRef
    {
        public TypeFieldRef()
        {
            FieldName = "Item Type";
            HeaderText = "Type";
        }

        public override bool Sortable
        {
            get { return false; }
            set { base.Sortable = value; }
        }

        public override bool Filterable
        {
            get { return false; }
            set { base.Filterable = value; }
        }

        public override void RenderCell(HtmlTextWriter writer, DataRow row)
        {
            var value = Convert.ToString(row[FieldName]);
            writer.Write(value != null && value.StartsWith("1")
                             ? "<img alt='' src='/_layouts/images/folder.gif' />"
                             : "<img alt='' src='/_layouts/images/icgen.gif' />");
        }

        public override string GetCellTextValue(DataRow row)
        {
            throw new NotImplementedException();
        }

        public override string GetFilterCamlQuery()
        {
            throw new NotImplementedException();
        }
    }
}
