using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class AdvList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.AdvList,
                            Name = ListsName.InternalName.AdvList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AlbumList.InternalName.Description, FieldsName.AlbumList.DisplayName.Description));
            helper.AddField(new NumberFieldCreator(FieldsName.AlbumList.InternalName.Type, FieldsName.AlbumList.DisplayName.Type));
            helper.AddField(new NumberFieldCreator(FieldsName.AlbumList.InternalName.Status, FieldsName.AlbumList.DisplayName.Status));
            helper.AddField(new NumberFieldCreator(FieldsName.AlbumList.InternalName.Order, FieldsName.AlbumList.DisplayName.Order));            
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.AlbumList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.AlbumList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
