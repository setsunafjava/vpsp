using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Common
{
    public class BackEndUC : UserControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            Utilities.LoadJS(SPContext.Current.Web, this.Page, "jquery-1.7.1.js");
            base.OnPreRender(e);
        }
    }
}
