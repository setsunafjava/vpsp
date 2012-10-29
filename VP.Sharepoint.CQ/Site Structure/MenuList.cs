using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class MenuList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.MenuList,
                            Name = ListsName.InternalName.MenuList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.MenuList.InternalName.Description, FieldsName.MenuList.DisplayName.Description)
            {
                RichText = false,
                RichTextMode = SPRichTextMode.Compatible
            });
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.MenuID, FieldsName.MenuList.DisplayName.MenuID));
            helper.AddField(new NumberFieldCreator(FieldsName.MenuList.InternalName.MenuOrder, FieldsName.MenuList.DisplayName.MenuOrder));
            helper.AddField(new NumberFieldCreator(FieldsName.MenuList.InternalName.MenuLevel, FieldsName.MenuList.DisplayName.MenuLevel) { DefaultValue = "1"});
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.ParentID, FieldsName.MenuList.DisplayName.ParentID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.ParentName, FieldsName.MenuList.DisplayName.ParentName));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.CatID, FieldsName.MenuList.DisplayName.CatID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.CatName, FieldsName.MenuList.DisplayName.CatName));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.PageName, FieldsName.MenuList.DisplayName.PageName));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.MenuUrl, FieldsName.MenuList.DisplayName.MenuUrl));

            var choiceField = new ChoiceFieldCreator(FieldsName.MenuList.InternalName.Status, FieldsName.MenuList.DisplayName.Status);
            choiceField.Choices.AddRange(new[] { "Ẩn", "Hiện" });
            choiceField.DefaultValue = "Hiện";
            helper.AddField(choiceField);

            choiceField = new ChoiceFieldCreator(FieldsName.MenuList.InternalName.MenuType, FieldsName.MenuList.DisplayName.MenuType);
            choiceField.Choices.AddRange(new[] { "Link tới chuyên mục", "Đường link xác định" });
            choiceField.DefaultValue = "Link tới chuyên mục";
            helper.AddField(choiceField);

            var choiceFields = new MultipleChoiceFieldCreator(FieldsName.MenuList.InternalName.MenuPosition, FieldsName.MenuList.DisplayName.MenuPosition);
            choiceFields.Choices.AddRange(new[] { "Top menu", "Footer menu" });
            helper.AddField(choiceFields);

            choiceField = new ChoiceFieldCreator(FieldsName.MenuList.InternalName.OpenType, FieldsName.MenuList.DisplayName.OpenType);
            choiceField.Choices.AddRange(new[] { "Giữ nguyên cửa sổ hiện tại", "Mở cửa sổ mới" });
            choiceField.DefaultValue = "Giữ nguyên cửa sổ hiện tại";
            helper.AddField(choiceField);

            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetFieldByInternalName(FieldsName.MenuList.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.MenuList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/MenuList.ascx");
            //add view
            Utilities.AddStandardView(web, list, "AllMenu", "../../UserControls/MenuView.ascx", "", 100, true);
        }
    }
}
