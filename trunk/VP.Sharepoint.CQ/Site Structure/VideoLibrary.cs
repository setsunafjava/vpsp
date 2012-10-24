using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class VideoLibrary
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.VideoLibrary,
                            Name = ListsName.InternalName.VideoLibrary,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.VideoLibrary.InternalName.Description, FieldsName.CategoryList.DisplayName.Description));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.VideoLibrary.InternalName.AlbumId, FieldsName.VideoLibrary.DisplayName.AlbumId));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.VideoLibrary.InternalName.CategoryId, FieldsName.VideoLibrary.DisplayName.CategoryId));
            helper.AddField(new NumberFieldCreator(FieldsName.VideoLibrary.InternalName.Status, FieldsName.VideoLibrary.DisplayName.Status));
            helper.AddField(new NumberFieldCreator(FieldsName.VideoLibrary.InternalName.Order, FieldsName.VideoLibrary.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.VideoLibrary.InternalName.FilePath, FieldsName.VideoLibrary.DisplayName.FilePath));            
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.VideoLibrary.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.VideoLibrary.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
