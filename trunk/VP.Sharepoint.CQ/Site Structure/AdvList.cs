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
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.AdvList.InternalName.Description, FieldsName.AdvList.DisplayName.Description)
            {
                RichText = false,
                RichTextMode = SPRichTextMode.Compatible
            });
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.AdvID, FieldsName.AdvList.DisplayName.AdvID));

            var choiceField = new ChoiceFieldCreator(FieldsName.AdvList.InternalName.AdvType, FieldsName.AdvList.DisplayName.AdvType);
            choiceField.Choices.AddRange(new[] { "Images", "Flash", "Video" });
            choiceField.DefaultValue = "Images";
            choiceField.Required = true;
            helper.AddField(choiceField);

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.AdvFile, FieldsName.AdvList.DisplayName.AdvFile));

            choiceField = new ChoiceFieldCreator(FieldsName.AdvList.InternalName.AdvOpenType, FieldsName.AdvList.DisplayName.AdvOpenType);
            choiceField.Choices.AddRange(new[] { "Giữ nguyên cửa sổ hiện tại", "Mở cửa sổ mới" });
            choiceField.DefaultValue = "Mở cửa sổ mới";
            choiceField.Required = true;
            helper.AddField(choiceField);

            helper.AddField(new NumberFieldCreator(FieldsName.AdvList.InternalName.AdvClick, FieldsName.AdvList.DisplayName.AdvClick));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.AdvUrl, FieldsName.AdvList.DisplayName.AdvUrl) { Required = true });
            helper.AddField(new NumberFieldCreator(FieldsName.AdvList.InternalName.AdvWidth, FieldsName.AdvList.DisplayName.AdvWidth) { Required = true });
            helper.AddField(new NumberFieldCreator(FieldsName.AdvList.InternalName.AdvHeight, FieldsName.AdvList.DisplayName.AdvHeight) { Required = true });
            helper.AddField(new DateTimeFieldCreator(FieldsName.AdvList.InternalName.AdvStartDate, FieldsName.AdvList.DisplayName.AdvStartDate) { DisplayFormat = SPDateTimeFieldFormatType.DateTime, Required = true, DefaultValue = "[Today]" });
            helper.AddField(new DateTimeFieldCreator(FieldsName.AdvList.InternalName.AdvEndDate, FieldsName.AdvList.DisplayName.AdvEndDate) { DisplayFormat = SPDateTimeFieldFormatType.DateTime, Required = true, DefaultValue = "[Today]" });
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.CustomerName, FieldsName.AdvList.DisplayName.CustomerName));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.CustomerAddress, FieldsName.AdvList.DisplayName.CustomerAddress));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.CustomerPhone, FieldsName.AdvList.DisplayName.CustomerPhone));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.CustomerMobile, FieldsName.AdvList.DisplayName.CustomerMobile));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvList.InternalName.AdvCat, FieldsName.AdvList.DisplayName.AdvCat));

            choiceField = new ChoiceFieldCreator(FieldsName.AdvList.InternalName.AdvDisplay, FieldsName.AdvList.DisplayName.AdvDisplay);
            choiceField.Choices.AddRange(new[] { "Hiển thị tất cả", "Hiển thị theo chuyên mục" });
            choiceField.DefaultValue = "Hiển thị tất cả";
            choiceField.Required = true;
            helper.AddField(choiceField);

            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.AdvList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.AdvList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/AdvList.ascx");
        }
    }
}
