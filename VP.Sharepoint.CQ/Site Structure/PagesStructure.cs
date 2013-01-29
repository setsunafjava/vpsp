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
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeleft1", "HomeNewsCatUC", 1, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxnewshomebig1", "HomeNewsCatUC", 2, "NewsCatHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeleft2", "HomeNewsCatUC", 3, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxnewshomebig2", "HomeNewsCatUC", 4, "NewsCatHomeUC");
            //left-left-home
            //System.Diagnostics.Debugger.Launch();
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeleft3", "HomeNewsLeftUC", 0, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxnewshomesmall1", "HomeNewsLeftUC", 11, "ListNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeleft4", "HomeNewsLeftUC", 22, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxnewshomesmall2", "HomeNewsLeftUC", 33, "ListNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeleft5", "HomeNewsLeftUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxnewshomesmall3", "HomeNewsLeftUC", 55, "ListNewsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeleft6", "HomeNewsLeftUC", 66, "AdvUC");
            //right-left-home
            AddUserControlToPage(web, Constants.DefaultPage, "knowledgehome", "HomeNewsRightUC", 0, "KnowledgeHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "linksitehome", "HomeNewsRightUC", 1, "LinkSiteUC");
            //AddUserControlToPage(web, Constants.DefaultPage, "loginhome", "HomeNewsRightUC", 2, "LoginHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "advhomecenter", "HomeNewsRightUC", 3, "AdvUC");
            //right-home
            AddUserControlToPage(web, Constants.DefaultPage, "latestdocshome", "RightHomeUC", 0, "LatestDocsUC");

            AddUserControlToPage(web, Constants.DefaultPage, "SoDoToChucDefaultPageUC", "RightUC", 5, "SoDoToChucUC");

            AddUserControlToPage(web, Constants.DefaultPage, "EmailBoxUC", "RightHomeUC", 11, "EmailBoxUC");
            AddUserControlToPage(web, Constants.DefaultPage, "IconLinkUC", "RightHomeUC", 22, "IconLinkUC");
            AddUserControlToPage(web, Constants.DefaultPage, "VideoHomeUC", "RightHomeUC", 33, "VideoHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "HomeHotNewsUC", "RightHomeUC", 44, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.DefaultPage, "advhomeright", "RightHomeUC", 99, "AdvUC");
            AddUserControlToPage(web, Constants.DefaultPage, "GalleryHomeUC", "RightHomeUC", 66, "GalleryHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "boxgiaitri", "RightHomeUC", 77, "ListCatsHomeUC");
            AddUserControlToPage(web, Constants.DefaultPage, "HitCountUC", "RightHomeUC", 111, "HitCountUC");
            //footer
            AddUserControlToPage(web, Constants.DefaultPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.DefaultPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.DefaultPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //newspage
            CreateAppPage(web, Constants.NewsPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.NewsPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.NewsPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.NewsPage, "ListNewsByCatUC", "LeftUC", 0, "ListNewsByCatUC");

            AddUserControlToPage(web, Constants.NewsPage, "HotNewsByCatUC", "RightUC", 11, "HotNewsByCatUC");
            AddUserControlToPage(web, Constants.NewsPage, "latestdocshome", "RightUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.NewsPage, "EmailBoxUC", "RightUC", 22, "EmailBoxUC");
            AddUserControlToPage(web, Constants.NewsPage, "IconLinkUC", "RightUC", 33, "IconLinkUC");
            AddUserControlToPage(web, Constants.NewsPage, "advnews", "RightUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.NewsPage, "HitCountUC", "RightUC", 55, "HitCountUC");

            AddUserControlToPage(web, Constants.NewsPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.NewsPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.NewsPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //newsdetailpage
            CreateAppPage(web, Constants.NewsDetailPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.NewsDetailPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.NewsDetailPage, "NewsDetailUC", "LeftUC", 0, "NewsDetailUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "OtherNewsUC", "LeftUC", 1, "OtherNewsUC");

            AddUserControlToPage(web, Constants.NewsDetailPage, "HotNewsByCatUC", "RightUC", 11, "HotNewsByCatUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "latestdocshome", "RightUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "EmailBoxUC", "RightUC", 22, "EmailBoxUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "IconLinkUC", "RightUC", 33, "IconLinkUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "advnewsdetail", "RightUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "HitCountUC", "RightUC", 55, "HitCountUC");

            AddUserControlToPage(web, Constants.NewsDetailPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.NewsDetailPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //OrganizationPage
            CreateAppPage(web, Constants.OrganizationPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.OrganizationPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.OrganizationPage, "ToChucDetailUC", "LeftUC", 0, "ToChucDetailUC");

            AddUserControlToPage(web, Constants.OrganizationPage, "SoDoToChucUC", "RightUC", 0, "SoDoToChucUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "HomeHotNewsUC", "RightUC", 22, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "latestdocshome", "RightUC", 11, "LatestDocsUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "EmailBoxUC", "RightUC", 33, "EmailBoxUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "IconLinkUC", "RightUC", 44, "IconLinkUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "advorganization", "RightUC", 55, "AdvUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "HitCountUC", "RightUC", 66, "HitCountUC");

            AddUserControlToPage(web, Constants.OrganizationPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.OrganizationPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //LibraryPage
            CreateAppPage(web, Constants.LibraryPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.LibraryPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.LibraryPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.LibraryPage, "FilesByFolderUC", "LeftUC", 0, "FilesByFolderUC");

            AddUserControlToPage(web, Constants.LibraryPage, "FoldersByFolderUC", "RightUC", 0, "FoldersByFolderUC");
            AddUserControlToPage(web, Constants.LibraryPage, "HomeHotNewsUC", "RightUC", 22, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.LibraryPage, "latestdocshome", "RightUC", 11, "LatestDocsUC");
            AddUserControlToPage(web, Constants.LibraryPage, "EmailBoxUC", "RightUC", 33, "EmailBoxUC");
            AddUserControlToPage(web, Constants.LibraryPage, "IconLinkUC", "RightUC", 44, "IconLinkUC");
            AddUserControlToPage(web, Constants.LibraryPage, "advlibrary", "RightUC", 55, "AdvUC");
            AddUserControlToPage(web, Constants.LibraryPage, "HitCountUC", "RightUC", 66, "HitCountUC");

            AddUserControlToPage(web, Constants.LibraryPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.LibraryPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.LibraryPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //LibraryDetailPage
            CreateAppPage(web, Constants.LibraryDetailPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.LibraryDetailPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.LibraryDetailPage, "FileDetailUC", "LeftUC", 0, "FileDetailUC");

            AddUserControlToPage(web, Constants.LibraryDetailPage, "FoldersByFolderUC", "RightUC", 0, "FoldersByFolderUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "HomeHotNewsUC", "RightUC", 22, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "latestdocshome", "RightUC", 11, "LatestDocsUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "EmailBoxUC", "RightUC", 33, "EmailBoxUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "IconLinkUC", "RightUC", 44, "IconLinkUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "advlibrarydetail", "RightUC", 55, "AdvUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "HitCountUC", "RightUC", 66, "HitCountUC");

            AddUserControlToPage(web, Constants.LibraryDetailPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.LibraryDetailPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //AboutPage
            CreateAppPage(web, Constants.AboutPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.AboutPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.AboutPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.AboutPage, "AboutUC", "LeftUC", 0, "AboutUC");

            AddUserControlToPage(web, Constants.AboutPage, "HomeHotNewsUC", "RightUC", 11, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.AboutPage, "latestdocshome", "RightUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.AboutPage, "EmailBoxUC", "RightUC", 22, "EmailBoxUC");
            AddUserControlToPage(web, Constants.AboutPage, "IconLinkUC", "RightUC", 33, "IconLinkUC");
            AddUserControlToPage(web, Constants.AboutPage, "advabout", "RightUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.AboutPage, "HitCountUC", "RightUC", 55, "HitCountUC");

            AddUserControlToPage(web, Constants.AboutPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.AboutPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.AboutPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //DocumentPage
            CreateAppPage(web, Constants.DocumentPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.DocumentPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.DocumentPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.DocumentPage, "VanBanUC", "LeftUC", 0, "VanBanUC");

            AddUserControlToPage(web, Constants.DocumentPage, "HomeHotNewsUC", "RightUC", 11, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.DocumentPage, "latestdocshome", "RightUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.DocumentPage, "EmailBoxUC", "RightUC", 22, "EmailBoxUC");
            AddUserControlToPage(web, Constants.DocumentPage, "IconLinkUC", "RightUC", 33, "IconLinkUC");
            AddUserControlToPage(web, Constants.DocumentPage, "advdocument", "RightUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.DocumentPage, "HitCountUC", "RightUC", 55, "HitCountUC");

            AddUserControlToPage(web, Constants.DocumentPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.DocumentPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.DocumentPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //DocumentDetailPage
            CreateAppPage(web, Constants.DocumentDetailPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.DocumentDetailPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.DocumentDetailPage, "VanBanDetailUC", "LeftUC", 0, "VanBanDetailUC");

            AddUserControlToPage(web, Constants.DocumentDetailPage, "HomeHotNewsUC", "RightUC", 11, "HomeHotNewsUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "latestdocshome", "RightUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "EmailBoxUC", "RightUC", 22, "EmailBoxUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "IconLinkUC", "RightUC", 33, "IconLinkUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "advlibrarydetail", "RightUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "HitCountUC", "RightUC", 55, "HitCountUC");

            AddUserControlToPage(web, Constants.DocumentDetailPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.DocumentDetailPage, "FooterUC", "FooterUC", 2, "FooterUC");

            //StatisticPage
            CreateAppPage(web, Constants.StatisticPage, webUrl + "/" + ListsName.InternalName.ResourcesList + "/VP4.master", true);
            AddUserControlToPage(web, Constants.StatisticPage, "header", "HeaderUC", 0, "HeaderUC");
            AddUserControlToPage(web, Constants.StatisticPage, "topmenu", "HeaderUC", 1, "TopMenuUC");

            AddUserControlToPage(web, Constants.StatisticPage, "ThongKeUC", "LeftUC", 0, "ThongKeUC");

            AddUserControlToPage(web, Constants.StatisticPage, "HotNewsByCatUC", "RightUC", 11, "HotNewsByCatUC");
            AddUserControlToPage(web, Constants.StatisticPage, "latestdocshome", "RightUC", 0, "LatestDocsUC");
            AddUserControlToPage(web, Constants.StatisticPage, "EmailBoxUC", "RightUC", 22, "EmailBoxUC");
            AddUserControlToPage(web, Constants.StatisticPage, "IconLinkUC", "RightUC", 33, "IconLinkUC");
            AddUserControlToPage(web, Constants.StatisticPage, "advstatistic", "RightUC", 44, "AdvUC");
            AddUserControlToPage(web, Constants.StatisticPage, "HitCountUC", "RightUC", 55, "HitCountUC");

            AddUserControlToPage(web, Constants.StatisticPage, "ShouldToKnowUC", "FooterUC", 0, "ShouldToKnowUC");
            AddUserControlToPage(web, Constants.StatisticPage, "BottomMenuUC", "FooterUC", 1, "BottomMenuUC");
            AddUserControlToPage(web, Constants.StatisticPage, "FooterUC", "FooterUC", 2, "FooterUC");
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
            var webUrl = web.ServerRelativeUrl;
            if (webUrl.Equals("/"))
            {
                webUrl = "";
            }
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);
            if (containerWebPart != null)
            {
                containerWebPart.Title = pageTitle;
                containerWebPart.UserControlPath = webUrl + "/" + ListsName.InternalName.ResourcesList + "/" + userControlName + ".ascx";
                //containerWebPart.UserControlPath = "UserControls/" + userControlName + ".ascx";
                WebPartHelper.AddWebPart(web, pageName + ".aspx", containerWebPart, "Main", 0);
            }
        }

        private static void AddUserControlToPage(SPWeb web, string pageName, string pageTitle, string positionName, int positionNumber, string userControlName)
        {
            var webUrl = web.ServerRelativeUrl;
            if (webUrl.Equals("/"))
            {
                webUrl = "";
            }
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);
            if (containerWebPart != null)
            {
                containerWebPart.Title = pageTitle;
                //containerWebPart.UserControlPath = "UserControls/" + userControlName + ".ascx";
                containerWebPart.UserControlPath = webUrl + "/" + ListsName.InternalName.ResourcesList + "/" + userControlName + ".ascx";
                WebPartHelper.AddWebPart(web, pageName + ".aspx", containerWebPart, positionName, positionNumber);
            }
        }
    }
}
