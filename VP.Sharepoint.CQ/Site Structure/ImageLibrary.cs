using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class CategoryList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.CategoryList,
                            Name = ListsName.InternalName.CategoryList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField( new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.Description, FieldsName.CategoryList.DisplayName.Description));
            helper.AddField(new NumberFieldCreator(FieldsName.CategoryList.InternalName.Type, FieldsName.CategoryList.DisplayName.Type));
            helper.AddField(new NumberFieldCreator(FieldsName.CategoryList.InternalName.Status, FieldsName.CategoryList.DisplayName.Status));
            helper.AddField(new NumberFieldCreator(FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.ImageDesc, FieldsName.CategoryList.DisplayName.ImageDesc));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.CategoryList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.CategoryList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
