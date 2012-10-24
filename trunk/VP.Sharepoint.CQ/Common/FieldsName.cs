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
                public const string Poster = "Poster";
                public const string PostedDate = "PostedDate";
                public const string Description = "Description";
                public const string Content = "Content";
                public const string Status = "Status";
                public const string ImageThumb = "ImageThumb";
                public const string ImageSmallThumb = "ImageSmallThumb";                

            }
            #endregion
            //Display Name
            #region Nested type: NewsList Display Name
            public static class DisplayName
            {
                public const string Title = "Tiêu đề bài viết";
                public const string NewsGroup = "Nhóm tin";
                public const string Poster = "Người đăng";
                public const string Description = "Mô tả";
                public const string Content = "Nội dung";
                public const string PostedDate = "Ngày đăng";
                public const string Status = "Trạng thái";
                public const string ImageThumb = "Ảnh trích dẫn";
                public const string ImageSmallThumb = "Ảnh trích dẫn nhỏ";
            }
            #endregion
        }
        #endregion

        #region NewsGroupList list
        public class NewsGroupList // --> ListName
        {
            //Internal Name
            #region Nested type: NewsGroupList Internal Name
            public class InternalName
            {
                public const string Title = "Title";
                public const string ParentID = "ParentID";
                public const string Description = "Description";
                public const string Status = "Status";
            }
            #endregion
            //Display Name
            #region Nested type: NewsList Display Name
            public static class DisplayName
            {
                public const string Title = "Tên nhóm";
                public const string ParentID = "Nhóm cha";
                public const string Description = "Mô tả";
                public const string Status = "Trạng thái";
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

        #region ExternalNews list
        public class ExternalNews
        {
            // Nested ExternalNews list
            public class InternalName
            {
                public const string Title = "Title";
                public const string Description = "Description";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";                
                public const string Description = "Mô tả";
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
                public const string Title = "Title";
                public const string Description = "Description";
                public const string Type = "Type";
                public const string Status = "Status";
                public const string Order = "Order";
                public const string ImageDesc = "ImageDesc";
            }

            public class DisplayName
            {
                public const string Title = "Tiêu đề";
                public const string Description = "Mô tả";
                public const string Type = "Loại chuyên mục";
                public const string Status = "Trạng thái";
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
    }
}
