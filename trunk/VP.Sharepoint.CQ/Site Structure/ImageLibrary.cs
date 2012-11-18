using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class ImageLibrary
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.ImageLibrary,
                            Name = ListsName.InternalName.ImageLibrary,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ImageLibrary.InternalName.Description, FieldsName.CategoryList.DisplayName.Description));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ImageLibrary.InternalName.AlbumId, FieldsName.ImageLibrary.DisplayName.AlbumId));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ImageLibrary.InternalName.CategoryId, FieldsName.ImageLibrary.DisplayName.CategoryId));
            helper.AddField(new NumberFieldCreator(FieldsName.ImageLibrary.InternalName.Status, FieldsName.ImageLibrary.DisplayName.Status));
            helper.AddField(new NumberFieldCreator(FieldsName.ImageLibrary.InternalName.Order, FieldsName.ImageLibrary.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ImageLibrary.InternalName.FilePath, FieldsName.ImageLibrary.DisplayName.FilePath));            
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.ImageLibrary.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.ImageLibrary.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.RowLimit = 100;
            defaultView.ViewFields.Add(Constants.EditColumn);
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.Update();
        }
    }
}
