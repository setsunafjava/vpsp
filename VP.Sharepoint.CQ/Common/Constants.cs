using Microsoft.SharePoint.WebControls;
namespace VP.Sharepoint.CQ.Common
{
    public static class Constants
    {
        public const string HomeTitle = "Sở giáo dục và đào tạo Vĩnh Phúc";
        public const string Title = "Title";
        public const string Modified = "Modified";
        public const string Created = "Created";
        public const string FieldTitleLinkToItem = "LinkTitle";
        public const string EditColumn = "Edit";
        public const string FieldLinkToFileName = "LinkFilename";
        public const string LinkTitleNoMenu = "LinkTitleNoMenu";
        public const string CreatedBy = "Author";
        public const string DefaultPage = "default";
        public const string NewsPage = "news";
        public const string NewsDetailPage = "newsdetail";
        public const string OrganizationPage = "organization";
        public const string LibraryPage = "library";
        public const string LibraryDetailPage = "librarydetail";
        public const string AboutPage = "about";
        public const string DocumentPage = "document";
        public const string DocumentDetailPage = "documentdetail";
        public const string StatisticPage = "statistic";
        public const SPControlMode EditForm = SPControlMode.Edit;
        public const SPControlMode NewForm = SPControlMode.New;
        public const SPControlMode DisplayForm = SPControlMode.Display;

        #region NewsStatus
        public class NewsStatus{
            public const string HomeNews = "Tin trang nhất";
            public const string HotNews = "Tin nổi bật";
            public const string SlideNews = "Tin slide show";
            public const string ShouldKnowNews = "Tin cần biết";
        }
        public class NewsShowHide
        {
            public const string Show = "Hiện";
            public const string Hide = "Ẩn";
        }
        #endregion

        #region Category type
        public class CategoryStatus
        {
            public const string News = "Tin tức";
            public const string Intro = "Giới thiệu";
            public const string Documents = "Văn bản";
            public const string ImagesGalery = "Thư viện ảnh";
            public const string VideoGalery = "Thư viện video";            
            public const string NeedToKnow = "Thông tin cần biết";
            public const string Resources = "Tài nguyên";
            public const string Statistic = "Thống kê";
            public const string Organization = "Sơ đồ tổ chức";
        }
        #endregion
    }
}
