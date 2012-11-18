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
            QuickLaunchHelper.AddNavigationLink(catNews, "Tin ngoài", newsList.Views["ExternalNews"].Url, groups);
            #endregion

            #region Quản lý văn bản
            var vbHead = quickLaunch.AddHeading("Quản lý văn bản", string.Empty, groups);
            var vbTypeList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.DocumentType);
            QuickLaunchHelper.AddNavigationLink(vbHead, "Loại văn bản", vbTypeList.DefaultViewUrl, groups);
            var vbSubList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.DocumentSubject);
            QuickLaunchHelper.AddNavigationLink(vbHead, "Lĩnh vực", vbSubList.DefaultViewUrl, groups);
            var ppList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.PublishPlace);
            QuickLaunchHelper.AddNavigationLink(vbHead, "Cơ quan ban hành", ppList.DefaultViewUrl, groups);
            var spList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.SignaturePerson);
            QuickLaunchHelper.AddNavigationLink(vbHead, "Người ký", spList.DefaultViewUrl, groups);
            var dList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.DocumentsList);
            QuickLaunchHelper.AddNavigationLink(vbHead, "Danh sách văn bản", dList.DefaultViewUrl, groups);
            #endregion

            #region Quản lý quảng cáo
            var qcHead = quickLaunch.AddHeading("Quản lý quảng cáo", string.Empty, groups);
            var qcList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.AdvList);
            QuickLaunchHelper.AddNavigationLink(qcHead, "Danh sách quảng cáo", qcList.DefaultViewUrl, groups);
            var tkqcList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.AdvStatisticList);
            QuickLaunchHelper.AddNavigationLink(qcHead, "Thống kê quảng cáo", tkqcList.DefaultViewUrl, groups);
            #endregion

            #region Quản lý chung
            var cHead = quickLaunch.AddHeading("Quản lý chung", string.Empty, groups);
            var mnList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.MenuList);
            QuickLaunchHelper.AddNavigationLink(cHead, "Danh sách menu", mnList.DefaultViewUrl, groups);
            var lkiconList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.IconLinkList);
            QuickLaunchHelper.AddNavigationLink(cHead, "Liên kết Icon", lkiconList.DefaultViewUrl, groups);
            var lksiteList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.WebsiteLink);
            QuickLaunchHelper.AddNavigationLink(cHead, "Liên kết website", lksiteList.DefaultViewUrl, groups);
            var pList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.ProfilesList);
            QuickLaunchHelper.AddNavigationLink(cHead, "Sơ đồ tổ chức", pList.DefaultViewUrl, groups);
            var tnList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.ResourceLibrary);
            QuickLaunchHelper.AddNavigationLink(cHead, "Quản lý thư viện", tnList.DefaultViewUrl, groups);
            var vdList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.VideoLibrary);
            QuickLaunchHelper.AddNavigationLink(cHead, "Thư viện Video", vdList.DefaultViewUrl, groups);
            var imgList = Utilities.GetCustomListByUrl(web, ListsName.InternalName.ImageLibrary);
            QuickLaunchHelper.AddNavigationLink(cHead, "Thư viện hình ảnh", imgList.DefaultViewUrl, groups);
            #endregion
        }
    }
}
