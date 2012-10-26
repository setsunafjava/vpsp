using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace VP.Sharepoint.CQ.Common
{
    public class BackEndUC : UserControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            Utilities.LoadJS(SPContext.Current.Web, this.Page, FieldsName.AC00004Resources.FieldValuesDefault.Name.Jquery);
            Utilities.LoadCSS(SPContext.Current.Web, this.Page, FieldsName.AC00004Resources.FieldValuesDefault.Name.B1);
            base.OnPreRender(e);
        }
    }
}
