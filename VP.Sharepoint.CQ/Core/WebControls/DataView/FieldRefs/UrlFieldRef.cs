using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class UrlFieldRef : BaseFieldRef
    {
        public override bool Sortable
        {
            get { return false; }
            set { base.Sortable = value; }
        }

        public override SPFieldType FieldType
        {
            get { return SPFieldType.URL; }
        }

        public override bool Filterable
        {
            get { return false; }
            set { base.Filterable = value; }
        }

        public override void RenderCell(HtmlTextWriter writer, DataRow row)
        {
            var value = Convert.ToString(row[FieldName]);
            if (string.IsNullOrEmpty(value))
            {
                writer.Write(DefaultValue);
                return;
            }

            var split = value.Split(new [] {", "}, StringSplitOptions.None);
            if (split.Length == 2)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, split[0]);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(SPEncode.HtmlEncode(split[1]));
                writer.RenderEndTag(); // a    
            }
            else
            {
                writer.Write(DefaultValue);
            }
        }

        public override string GetFilterCamlQuery()
        {
            throw new NotSupportedException();
        }

        public override string GetCellTextValue(DataRow row)
        {
            throw new NotSupportedException();
        }
    }
}
