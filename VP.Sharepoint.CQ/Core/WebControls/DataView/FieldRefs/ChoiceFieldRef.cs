using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class ChoiceFieldRef : TextFieldRef
    {
        public override SPFieldType FieldType
        {
            get { return SPFieldType.Choice; }
        }

        public override void RenderCell(HtmlTextWriter writer, DataRow row)
        {
            var value = Convert.ToString(row[FieldName]);
            if (string.IsNullOrEmpty(value))
            {
                writer.Write(DefaultValue);
                return;
            }

            value = SPEncode.HtmlEncode(value);
            var split = value.Split(new[] {";#"}, StringSplitOptions.RemoveEmptyEntries);

            writer.Write(string.Join("; ", split));
        }
    }
}
