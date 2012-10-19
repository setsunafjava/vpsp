using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    /// <summary>
    ///   Represents a control containing multiple lookup fields on a form page (not a list view page).
    /// </summary>
    public class MultipleLookupField : Microsoft.SharePoint.WebControls.MultipleLookupField
    {
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