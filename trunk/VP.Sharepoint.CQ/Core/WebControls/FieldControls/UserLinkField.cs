using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    /// <summary>
    /// The <see cref="UserLinkField"/> class represents a people field for data bound user interface controls, such as <see cref="SPGridView"/> objects.
    /// </summary>
    public class UserLinkField : SPBoundField
    {
        public SPWeb Web { get; set; }

        public string ReturnUrl { get; set; }

        protected override void ChildControlDataBinding(Control childControl, object dataItem,
                                                        MemberDescriptor dataFieldPropertyDescriptor)
        {
            var placeHolder = (PlaceHolder) childControl;
            var propertyValue = GetPropertyValue(dataItem, dataFieldPropertyDescriptor.Name);
            if (!string.IsNullOrEmpty(propertyValue))
            {
                var web = Web ?? SPContext.Current.Web;

                var split = propertyValue.Split(new[] {";#"}, StringSplitOptions.None);
                var link = new HyperLink
                               {
                                   Text = split[1],
                                   NavigateUrl = string.Format("{0}/_layouts/userdisp.aspx?ID={1}&Source={2}", web.Url, split[0], ReturnUrl)
                               };
                link.Attributes.Add("onclick", string.Format("var options = SP.UI.$create_DialogOptions(); options.url = '{0}';SP.UI.ModalDialog.showModalDialog(options);return false;", link.NavigateUrl));
                placeHolder.Controls.Add(link);
            }
        }

        protected override Control GetChildControlInstance()
        {
            return new PlaceHolder();
        }
    }
}