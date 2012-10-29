using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public interface IViewFieldRef
    {
        /// <summary>
        ///   Gets or sets the display name for the field.
        /// </summary>
        string FieldName { get; set; }

        string FieldNameWithTranslate { get; set; }

        /// <summary>
        ///   Gets the internal name that is used for the field.
        /// </summary>
        string InternalFieldName { get; }

        bool IsVirtualField { get; }

        /// <summary>
        ///   Gets or sets the heading text for the field.
        /// </summary>
        string HeaderText { get; set; }

        void Initialize(SPField field);

        void RenderCell(HtmlTextWriter writer, DataRow row);

        string GetViewFieldRef();
    }
}
