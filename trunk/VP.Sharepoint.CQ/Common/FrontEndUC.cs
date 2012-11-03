using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Common
{
    public class FrontEndUC : UserControl
    {
        private string _docLibUrl;

        public string DocLibUrl
        {
            get
            {
                var webUrl = SPContext.Current.Web.ServerRelativeUrl;
                if (webUrl.Equals("/"))
                {
                    webUrl = "";
                }
                return webUrl + "/" + ListsName.InternalName.ResourcesList;
            }
        }

        public SPWeb CurrentWeb
        {
            get { return SPContext.Current.Web; }
        }

        public string WebUrl
        {
            get
            {
                var webUrl = SPContext.Current.Web.ServerRelativeUrl;
                if (webUrl.Equals("/"))
                {
                    webUrl = "";
                }
                return webUrl;
            }
        }
    }
}
