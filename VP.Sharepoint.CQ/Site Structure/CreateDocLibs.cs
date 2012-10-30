using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    public class CreateDocLibs
    {
        public static void CreateListStructure(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.NewsImagesList,
                Name = ListsName.InternalName.NewsImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType=SPListTemplateType.DocumentLibrary
            };
            helper.Apply();

            helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.NewsContentImagesList,
                Name = ListsName.InternalName.NewsContentImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType = SPListTemplateType.PictureLibrary
            };
            helper.Apply();

            helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.AllImagesList,
                Name = ListsName.InternalName.AllImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType = SPListTemplateType.PictureLibrary
            };
            helper.Apply();

            helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.VideoImagesList,
                Name = ListsName.InternalName.VideoImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.Apply();

            helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.AlbumImagesList,
                Name = ListsName.InternalName.AlbumImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.Apply();

            helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.CategoryImagesList,
                Name = ListsName.InternalName.CategoryImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.Apply();

            helper = new ListHelper(web)
            {
                Title = ListsName.DisplayName.GalleryImagesList,
                Name = ListsName.InternalName.GalleryImagesList,
                OnQuickLaunch = false,
                EnableAttachments = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.Apply();
        }
    }
}
