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
                public const string Poster = "Người đăng";
                public const string Description = "Mô tả";
                public const string Content = "Nội dung";
                public const string PostedDate = "Ngày đăng";
                public const string Status = "Trạng thái";
                public const string ImageThumb = "Anh trich dan";
                public const string ImageSmallThumb = "Anh trich dan nho";
            }
            #endregion
        }
        #endregion
    }
}
