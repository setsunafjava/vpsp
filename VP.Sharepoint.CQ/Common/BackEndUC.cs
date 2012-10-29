using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Common
{
    public class BackEndUC : UserControl
    {
        public SPWeb CurrentWeb
        {
            get { return SPContext.Current.Web; }
        }
        public SPListItem CurrentItem
        {
            get { return SPContext.Current.ListItem; }
        }
        public SPControlMode CurrentMode
        {
            get { return SPContext.Current.FormContext.FormMode; }
        }
        protected override void OnPreRender(EventArgs e)
        {
            Utilities.LoadJS(SPContext.Current.Web, this.Page, "jquery-1.7.1.js");
            base.OnPreRender(e);
        }
    }
}
