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
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.CategoryID, FieldsName.CategoryList.DisplayName.CategoryID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.Description, FieldsName.CategoryList.DisplayName.Description));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.DisplayName.ParentID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.ParentName, FieldsName.CategoryList.DisplayName.ParentName));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.CategoryLevel, FieldsName.CategoryList.DisplayName.CategoryLevel));
            helper.AddField(new ChoiceFieldCreator(FieldsName.CategoryList.InternalName.Type, FieldsName.CategoryList.DisplayName.Type)
            {
                Choices = { "Menu", "Tin tức", "Văn bản", "Thư viện ảnh", "Thư viện video", "Tài nguyên" },
                DefaultValue = "Menu"
            });
            helper.AddField(new ChoiceFieldCreator(FieldsName.CategoryList.InternalName.Status, FieldsName.CategoryList.DisplayName.Status) { Choices = { "Ẩn", "Hiển thị" }, DefaultValue = "Hiển thị", EditFormat = SPChoiceFormatType.RadioButtons });
            helper.AddField(new NumberFieldCreator(FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CategoryList.InternalName.ImageDesc, FieldsName.CategoryList.DisplayName.ImageDesc));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.CategoryList.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.CategoryList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/CategoryList.ascx");
        }
    }
}
