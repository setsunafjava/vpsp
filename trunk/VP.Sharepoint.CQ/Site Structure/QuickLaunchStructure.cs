using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Common;
using Microsoft.SharePoint.Utilities;
namespace VP.Sharepoint.CQ
{
    static class QuickLaunchStructure
    {
        public static void CreateQuickLaunch(SPWeb web)
        {
            web.AllowUnsafeUpdates = true;
            var quickLaunch = new QuickLaunchHelper(web, true);
            var groups = string.Empty;

            #region Quản lý chuyên mục
            var catList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.CategoryList);
            var catHead = quickLaunch.AddHeading("Quản lý chuyên mục", string.Empty, groups);
            QuickLaunchHelper.AddNavigationLink(catHead, "Tất cả chuyên mục", catList.DefaultViewUrl, groups);
            QuickLaunchHelper.AddNavigationLink(catHead, "Thêm mới chuyên mục", catList.Forms[PAGETYPE.PAGE_NEWFORM].Url, groups);
            #endregion

            #region Quản lý tin tức
            var newsList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.NewsList);
            var catNews = quickLaunch.AddHeading("Quản lý tin tức", string.Empty, groups);
            QuickLaunchHelper.AddNavigationLink(catNews, "Tất cả tin tức", newsList.DefaultViewUrl, groups);
            #endregion
        }
    }
}
