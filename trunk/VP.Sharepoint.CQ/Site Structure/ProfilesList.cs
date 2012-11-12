using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class ProfilesList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.ProfilesList,
                            Name = ListsName.InternalName.ProfilesList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.ProfilesList.InternalName.Description, FieldsName.ProfilesList.DisplayName.Description)
            {
                RichText = true,
                RichTextMode = SPRichTextMode.FullHtml
            });
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.CategoryId, FieldsName.ProfilesList.DisplayName.CategoryId));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.CategoryName, FieldsName.ProfilesList.DisplayName.CategoryName));
            helper.AddField(new ChoiceFieldCreator(FieldsName.ProfilesList.InternalName.Status, FieldsName.ProfilesList.DisplayName.Status) { Choices = { "Ẩn", "Hiện" }, DefaultValue = "Hiện" });
            helper.AddField(new NumberFieldCreator(FieldsName.ProfilesList.InternalName.Order, FieldsName.ProfilesList.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.Position, FieldsName.ProfilesList.DisplayName.Position));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.Mobile, FieldsName.ProfilesList.DisplayName.Mobile));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.Education, FieldsName.ProfilesList.DisplayName.Education));
            helper.AddField(new DateTimeFieldCreator(FieldsName.ProfilesList.InternalName.DateOfBirth, FieldsName.ProfilesList.DisplayName.DateOfBirth) { DisplayFormat = SPDateTimeFieldFormatType.DateOnly});
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.Phone, FieldsName.ProfilesList.DisplayName.Phone));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.Specialized, FieldsName.ProfilesList.DisplayName.Specialized));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ProfilesList.InternalName.Email, FieldsName.ProfilesList.DisplayName.Email));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.ProfilesList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.ProfilesList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/ProfilesList.ascx");
            //add view
            Utilities.AddStandardView(web, list, "AllProfiles", "../../UserControls/ProfilesView.ascx", "", 100, true);
        }
    }
}
