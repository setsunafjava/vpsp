namespace VP.Sharepoint.CQ.Common
{
    public class FieldsName
    {
        #region News list
        public class NewsList // --> ListName
        {
            //Internal Name
            #region Nested type: NewsList Internal Name
            public class InternalName
            {
                public const string Title = "Title";
                public const string NewsGroup = "NewsGroup";
                public const string NewsGroupName = "NewsGroupName";
                public const string Poster = "Poster";
                public const string PostedDate = "PostedDate";
                public const string Description = "Description";
                public const string Content = "Content";
                public const string Status = "Status";
                public const string ImageThumb = "ImageThumb";
                public const string ImageSmallThumb = "ImageSmallThumb";
                public const string ImageHot = "ImageHot";
                public const string ImageDsp = "ImageDsp";
                public const string NewsCount = "NewsCount";
                public const string NewsUrl = "NewsUrl";
                public const string RSSName = "RSSName";
                public const string RSSLink = "RSSLink";
                public const string SourceName = "SourceName";
                public const string ShowHide = "ShowHide";
            }
            #endregion
            //Display Name
            #region Nested type: NewsList Display Name
            public static class DisplayName
            {
                public const string Title = "Tiêu đề bài viết";
                public const string NewsGroup = "Chuyên mục";
                public const string NewsGroupName = "Tên chuyên mục";
                public const string Poster = "Người đăng";
                public const string Description = "Mô tả";
                public const string Content = "Nội dung";
                public const string PostedDate = "Ngày đăng";
                public const string Status = "Trạng thái";
                public const string ImageThumb = "Ảnh trích dẫn";
                public const string ImageSmallThumb = "Ảnh trích dẫn nhỏ";
                public const string ImageHot = "Ảnh nổi bật";
                public const string ImageDsp = "Ảnh mô tả";
                public const string NewsCount = "Số lần đọc";
                public const string NewsUrl = "URL (link ngoài)";
                public const string RSSName = "Tên RSS";
                public const string RSSLink = "Link RSS";
                public const string SourceName = "Theo nguồn";
                public const string ShowHide = "Hiển thị";
            }
            #endregion
        }
        #endregion

        #region MenuList list
        public class MenuList // --> ListName
        {
            //Internal Name
            #region Nested type: MenuList Internal Name
            public class InternalName
            {
                public const string Title = "Title";
                public const string MenuID = "MenuID";
                public const string MenuType = "MenuType";
                public const string MenuPosition = "MenuPosition";
                public const string MenuOrder = "MenuOrder";
                public const string ParentID = "ParentID";
                public const string ParentName = "ParentName";
                public const string Description = "Description";
                public const string Status = "Status";
                public const string OpenType = "OpenType";
                public const string CatID = "CatID";
                public const string CatName = "CatName";
                public const string PageName = "PageName";
                public const string MenuUrl = "MenuUrl";
                public const string MenuLevel = "MenuLevel";
            }
            #endregion
            //Display Name
            #region Nested type: MenuList Display Name
            public static class DisplayName
            {
                public const string Title = "Tên menu";
                public const string MenuID = "MenuID";
                public const string MenuType = "Loại menu";
                public const string MenuPosition = "Vị trí menu";
                public const string MenuOrder = "Thứ tự";
                public const string ParentID = "Menu cha";
                public const string ParentName = "Tên menu cha";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string OpenType = "Kiểu trả về";
                public const string CatID = "Chuyên mục ID";
                public const string CatName = "Tên chuyên mục";
                public const string PageName = "Trên trang";
                public const string MenuUrl = "Đường link";
                public const string MenuLevel = "MenuLevel";
            }
            #endregion
        }
        #endregion

        #region DocumentType list
        public class DocumentType
        {
            // Nested document type list
            public class InternalName
            {
                public const string Title = "Title";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
            }
        }
        #endregion

        #region PublishPlace list
        public class PublishPlace
        {
            // Nested Publish Place list
            public class InternalName
            {
                public const string Title = "Title";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
            }
        }
        #endregion

        #region DocumentSubject
        public class DocumentSubject
        {
            // Nested Document Subject list
            public class InternalName
            {
                public const string Title = "Title";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
            }
        }
        #endregion

        #region SignaturePerson
        public class SignaturePerson
        {
            // Nested Document Subject list
            public class InternalName
            {
                public const string Title = "Title";
            }

            public class DisplayName
            {
                public const string Title = "Ký bởi";
            }
        }
        #endregion

        #region DocumentsList
        public class DocumentsList
        {
            // Nested Documents list
            public class InternalName
            {
                public const string DocumentNo = "DocumentNo";
                public const string Title = "Title";
                public const string PublishPlace = "PublishPlace";
                public const string DocumentType = "DocumentType";
                public const string DocumentSubject = "DocumentSubject";
                public const string SignaturePerson = "SignaturePerson";
                public const string EffectedDate = "EffectedDate";
                public const string ExpiredDate = "ExpiredDate";
                public const string FilePath = "LinkToFile";
            }

            public class DisplayName
            {
                public const string DocumentNo = "Số/Ký hiệu";
                public const string Title = "Tiêu đề";
                public const string PublishPlace = "Cơ quan ban hành";
                public const string DocumentType = "Loại văn bản";
                public const string DocumentSubject = "Lĩnh vực";
                public const string SignaturePerson = "Người ký";
                public const string EffectedDate = "Ngày hiệu lực";
                public const string ExpiredDate = "Ngày hết hiệu lực";
                public const string FilePath = "Đường dẫn file";
            }
        }
        #endregion

        #region ExternalNewsLink list
        public class ExternalNewsLink
        {
            // Nested ExternalNewsLink list
            public class InternalName
            {
                public const string Title = "Title";
                public const string NewsGroup = "NewsGroup";
                public const string NewsGroupName = "NewsGroupName";
                public const string LinkPath = "LinkPath";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string NewsGroup = "Nhóm tin";
                public const string NewsGroupName = "Tên nhóm tin";
                public const string LinkPath = "Đường link";
            }
        }
        #endregion

        #region Album list
        public class AlbumList
        {
            // Nested Album list
            public class InternalName
            {
                public const string Title = "Title";
                public const string Description = "Description";
                public const string Type = "Type";
                public const string Order = "Order";
                public const string Status = "Status";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string Description = "Mô tả";
                public const string Type = "Loại album";
                public const string Order = "Thứ tự";
                public const string Status = "Trạng thái";
            }
        }
        #endregion

        #region ImageLibrary list
        public class ImageLibrary
        {
            // Nested ImageLibrary list
            public class InternalName
            {
                public const string Title = "Title";
                public const string AlbumId = "AlbumId";
                public const string CategoryId = "CategoryId";
                public const string Description = "Description";
                public const string Status = "Status";
                public const string Order = "Order";
                public const string FilePath = "FilePath";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string AlbumId = "Album";
                public const string CategoryId = "Chuyên mục";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string Order = "Thứ tự";
                public const string FilePath = "Đường dẫn file";
            }
        }
        #endregion

        #region VideoLibrary list
        public class VideoLibrary
        {
            // Nested VideoLibrary list
            public class InternalName
            {
                public const string Title = "Title";
                public const string AlbumId = "AlbumId";
                public const string CategoryId = "CategoryId";
                public const string Description = "Description";
                public const string Status = "Status";
                public const string Order = "Order";
                public const string FilePath = "FilePath";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string AlbumId = "Album";
                public const string CategoryId = "Chuyên mục";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string Order = "Thứ tự";
                public const string FilePath = "Đường dẫn file";
            }
        }
        #endregion

        #region CategoryList
        public class CategoryList
        {
            // Nested CategoryList list
            public class InternalName
            {
                public const string CategoryID = "CategoryID";
                public const string Title = "Title";
                public const string Description = "Description";
                public const string ParentID = "ParentID";
                public const string ParentName = "ParentName";
                public const string CategoryLevel = "CategoryLevel";
                public const string Type = "CategoryType";
                public const string Status = "Status";
                public const string NewsPossition = "NewsPossition";
                public const string Order = "Order";
                public const string ImageDesc = "ImageDesc";
            }

            public class DisplayName
            {
                public const string CategoryID = "Category ID";
                public const string Title = "Tiêu đề";
                public const string Description = "Mô tả";
                public const string ParentID = "Chuyên mục cha id";
                public const string ParentName = "Tên chuyên mục cha";
                public const string CategoryLevel = "Mức chuyên mục";
                public const string Type = "Loại chuyên mục";
                public const string Status = "Trạng thái";
                public const string NewsPossition = "Vị trí";
                public const string Order = "Thứ tự";
                public const string ImageDesc = "Ảnh trích dẫn";
            }
        }
        #endregion

        #region WebsiteLink
        public class WebsiteLink
        {
            // Nested WebsiteLink list
            public class InternalName
            {
                public const string Title = "Title";
                public const string Description = "Description";
                public const string Status = "Status";
                public const string Order = "Order";
                public const string WebURL = "WebURL";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string Order = "Thứ tự";
                public const string WebURL = "Đường dẫn";
            }
        }
        #endregion

        #region IconLinkList
        public class IconLinkList
        {
            // Nested IconLinkList list
            public class InternalName
            {
                public const string Title = "Title";
                public const string Description = "Description";
                public const string Status = "IconStatus";
                public const string Order = "IConOrder";
                public const string LinkURL = "LinkURL";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string Order = "Thứ tự";
                public const string LinkURL = "Đường dẫn";
            }
        }
        #endregion

        #region Adv list
        public class AdvList
        {
            // Nested Adv list
            public class InternalName
            {
                public const string Title = "Title";
                public const string Description = "Description";
                public const string AdvID = "AdvID";
                public const string AdvType = "AdvType";
                public const string AdvFile = "AdvFile";
                public const string AdvClick = "AdvClick";
                public const string AdvOpenType = "AdvOpenType";
                public const string AdvUrl = "AdvUrl";
                public const string AdvWidth = "AdvWidth";
                public const string AdvHeight = "AdvHeight";
                public const string AdvStartDate = "AdvStartDate";
                public const string AdvEndDate = "AdvEndDate";
                public const string CustomerName = "CustomerName";
                public const string CustomerAddress = "CustomerAddress";
                public const string CustomerPhone = "CustomerPhone";
                public const string CustomerMobile = "CustomerMobile";
                public const string AdvCat = "AdvCat";
                public const string AdvDisplay = "AdvDisplay";
                public const string AdvPosition = "AdvPosition";
                public const string AdvStatus = "AdvStatus";
                public const string AdvOrder = "AdvOrder";
            }

            public class DisplayName
            {
                public const string Title = "Tên quảng cáo";
                public const string Description = "Mô tả";
                public const string AdvID = "AdvID";
                public const string AdvType = "Loại file";
                public const string AdvFile = "Đường dẫn file";
                public const string AdvClick = "Số lần click";
                public const string AdvOpenType = "Kiểu mở link";
                public const string AdvUrl = "Đường link";
                public const string AdvWidth = "Chiều rộng";
                public const string AdvHeight = "Chiều cao";
                public const string AdvStartDate = "Ngày hiệu lực";
                public const string AdvEndDate = "Ngày kết thúc";
                public const string CustomerName = "Tên khách hàng";
                public const string CustomerAddress = "Địa chỉ khách hàng";
                public const string CustomerPhone = "Số điện thọai";
                public const string CustomerMobile = "Số di động";
                public const string AdvCat = "Chuyên mục";
                public const string AdvDisplay = "Hiển thị theo";
                public const string AdvPosition = "Vị trí hiển thị";
                public const string AdvStatus = "Trạng thái";
                public const string AdvOrder = "Thứ tự";
            }
        }
        #endregion

        #region AdvStatistic List
        public class AdvStatisticList
        {
            // Nested AdvStatistic List
            public class InternalName
            {
                public const string Title = "Title";
                public const string AdvID = "AdvID";
                public const string UserBrowser = "UserBrowser";
                public const string UserIP = "UserIP";
                public const string UserUrl = "UserUrl";
            }

            public class DisplayName
            {
                public const string Title = "Tên quảng cáo";
                public const string AdvID = "AdvID";
                public const string UserBrowser = "Trình duyệt";
                public const string UserIP = "Địa chỉ IP";
                public const string UserUrl = "Trang hiển thị quảng cáo";
            }
        }
        #endregion

        #region ResourceLibrary list
        public class ResourceLibrary
        {            
            public class InternalName
            {
                public const string Title = "Title";                
                public const string CategoryId = "CategoryId";
                public const string CategoryName = "CategoryName";
                public const string Description = "Description";
                public const string Status = "Status";
                public const string Order = "Order";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string CategoryId = "Chuyên mục";
                public const string CategoryName = "Tên chuyên mục";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string Order = "Thứ tự";
            }
        }
        #endregion

        #region StatisticsList List
        public class StatisticsList
        {
            // Nested StatisticsList List
            public class InternalName
            {
                public const string Title = "Title";
                public const string UserBrowser = "UserBrowser";
                public const string UserIP = "UserIP";
                public const string UserUrl = "UserUrl";
            }

            public class DisplayName
            {
                public const string Title = "Người truy cập";
                public const string UserBrowser = "Trình duyệt";
                public const string UserIP = "Địa chỉ IP";
                public const string UserUrl = "Trang truy cập";
            }
        }
        #endregion

        #region ProfilesList list
        public class ProfilesList
        {
            public class InternalName
            {
                public const string Title = "Title";
                public const string Position = "Position";
                public const string Mobile = "Mobile";
                public const string Education = "Education";
                public const string DateOfBirth = "DateOfBirth";
                public const string Phone = "Phone";
                public const string Specialized = "Specialized";
                public const string Email = "Email";
                public const string CategoryId = "CategoryId";
                public const string CategoryName = "CategoryName";
                public const string Description = "Description";
                public const string Status = "Status";
                public const string Order = "Order";
            }

            public class DisplayName
            {
                public const string Title = "Họ và tên";
                public const string Position = "Vị trí";
                public const string Mobile = "Di động";
                public const string Education = "Trình độ";
                public const string DateOfBirth = "Ngày sinh";
                public const string Phone = "Điện thoại";
                public const string Specialized = "Chuyên ngành";
                public const string Email = "Email";
                public const string CategoryId = "Chuyên mục";
                public const string CategoryName = "Tên tổ chức";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
                public const string Order = "Thứ tự";
            }
        }
        #endregion
    }
}
