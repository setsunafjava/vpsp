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
            CreatePage(web, Constants.DefaultPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.DefaultPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.DefaultPage, "topmenu", "HeaderUC", 1, "TopMenuUC");
            AddUserControlToPage(web, Constants.DefaultPage, "slidenews", "HomeNewsUC", 0, "NewsSlideUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxnewshome", "HomeNewsCatUC", 0, "BoxNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsCatUC1", "HomeNewsCatUC", 1, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "newscathome1", "HomeNewsCatUC", 2, "NewsCatHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsCatUC2", "HomeNewsCatUC", 3, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "newscathome2", "HomeNewsCatUC", 4, "NewsCatHomeUC");
            //left-left-home
            //System.Diagnostics.Debugger.Launch();
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsLeftUC0", "HomeNewsLeftUC", 0, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "ListNewsHomeUC1", "HomeNewsLeftUC", 1, "ListNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsLeftUC1", "HomeNewsLeftUC", 2, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "ListNewsHomeUC2", "HomeNewsLeftUC", 3, "ListNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsLeftUC2", "HomeNewsLeftUC", 4, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "ListNewsHomeUC3", "HomeNewsLeftUC", 5, "ListNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsLeftUC3", "HomeNewsLeftUC", 6, "AdvUC");
            //right-left-home
            AddUserControlToPage(web, Constants.DefaultPage, "knowledgehome", "HomeNewsRightUC", 0, "KnowledgeHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "linksitehome", "HomeNewsRightUC", 1, "LinkSiteUC");
            AddUserControlToPage(web, Constants.DefaultPage, "loginhome", "HomeNewsRightUC", 2, "LoginHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoHomeNewsRightUC", "HomeNewsRightUC", 3, "AdvUC");
            //right-home
            AddUserControlToPage(web, Constants.DefaultPage, "latestdocshome", "RightHomeUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.DefaultPage, "EmailBoxUC", "RightHomeUC", 1, "EmailBoxUC");
            AddUserControlToPage(web, Constants.DefaultPage, "IconLinkUC", "RightHomeUC", 2, "IconLinkUC");
            AddUserControlToPage(web, Constants.DefaultPage, "VideoHomeUC", "RightHomeUC", 3, "VideoHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "HomeHotNewsUC", "RightHomeUC", 4, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.DefaultPage, "quangcaoRightHomeUC", "RightHomeUC", 5, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "GalleryHomeUC", "RightHomeUC", 6, "GalleryHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "ListCatsHomeUC", "RightHomeUC", 7, "ListCatsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "HitCountUC", "RightHomeUC", 8, "HitCountUC");
            //footer
            AddUserControlToPage(web, Constants.DefaultPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.DefaultPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.DefaultPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //newspage
            CreateAppPage(web, Constants.NewsPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.NewsPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.NewsPage, "topmenu", "HeaderUC", 1, "TopMenuUC");
        }

        private static void CreatePage(SPWeb web, string pageName, string usercontrolName, string masterUrl, bool overWrite)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture,"/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateDefaultWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), masterUrl, overWrite);

            AddUserControlToPage(web, pageName, pageName, usercontrolName);
        }

        private static void CreatePage(SPWeb web, string pageName, string masterUrl, bool overWrite)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture, "/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateDefaultWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), masterUrl, overWrite);
        }

        private static void CreateAppPage(SPWeb web, string pageName, string masterUrl, bool overWrite)
        {
            string notifyPageUrl = string.Concat(web.Url, string.Format(CultureInfo.InvariantCulture, "/{0}.aspx", pageName));
            SPFile pageFile = web.GetFile(notifyPageUrl);
            if (pageFile.Exists) pageFile.Delete();

            WebPageHelper.CreateAppWebPage(web, string.Format(CultureInfo.InvariantCulture, "{0}.aspx", pageName), masterUrl, overWrite);
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

        private static void AddUserControlToPage(SPWeb web, string pageName, string pageTitle, string positionName, int positionNumber, string userControlName)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);
            if (containerWebPart != null)
            {
                containerWebPart.Title = pageTitle;
                containerWebPart.UserControlPath = "UserControls/" + userControlName + ".ascx";
                WebPartHelper.AddWebPart(web, pageName + ".aspx", containerWebPart, positionName, positionNumber);
            }
        }
    }
}
