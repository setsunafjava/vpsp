using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using VP.Sharepoint.CQ.Common;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Core.WebParts;

namespace VP.Sharepoint.CQ
{
    public class PagesStructure
    {
        public static void Create(SPWeb web)
        {
            //Create pages
            //Home page
            var webUrl = web.ServerRelativeUrl;
            if (webUrl.Equals("/"))
            {
                webUrl = "";
            }
            CreatePage(web, "default", "HomePage", webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
        }

        private static void CreatePage(SPWeb web, string pageName, string usercontrolName, string masterUrl, bool overWrite)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture,"/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateDefaultWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), masterUrl, overWrite);

            AddUserControlToPage(web, pageName, pageName, usercontrolName);
        }

        private static void AddUserControlToPage(SPWeb web, string pageName, string pageTitle, string userControlName)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);
            if (containerWebPart != null)
            {
                containerWebPart.Title = pageTitle;
                containerWebPart.UserControlPath = "UserControls/" + userControlName + ".ascx";
                WebPartHelper.AddWebPart(web, pageName + ".aspx", containerWebPart, "Main", 0);
            }
        }
    }
}
