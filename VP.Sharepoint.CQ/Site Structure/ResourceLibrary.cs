using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class ResourceLibrary
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.ResourceLibrary,
                            Name = ListsName.InternalName.ResourceLibrary,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.ResourceLibrary.InternalName.Description, FieldsName.ResourceLibrary.DisplayName.Description)
            {
                RichText = true,
                RichTextMode = SPRichTextMode.FullHtml
            });            
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ResourceLibrary.InternalName.CategoryId, FieldsName.ResourceLibrary.DisplayName.CategoryId));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ResourceLibrary.InternalName.CategoryName, FieldsName.ResourceLibrary.DisplayName.CategoryName));
            helper.AddField(new ChoiceFieldCreator(FieldsName.ResourceLibrary.InternalName.Status, FieldsName.ResourceLibrary.DisplayName.Status) { Choices = { "Ẩn", "Hiện" }, DefaultValue="Hiện" });
            helper.AddField(new NumberFieldCreator(FieldsName.ResourceLibrary.InternalName.Order, FieldsName.ResourceLibrary.DisplayName.Order));
            helper.AddField(new DateTimeFieldCreator(FieldsName.ResourceLibrary.InternalName.PostedDate, FieldsName.ResourceLibrary.DisplayName.PostedDate));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ResourceLibrary.InternalName.Author, FieldsName.ResourceLibrary.DisplayName.Author));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ResourceLibrary.InternalName.ImgThumb, FieldsName.ResourceLibrary.DisplayName.ImgThumb));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ResourceLibrary.InternalName.FileUrl, FieldsName.ResourceLibrary.DisplayName.FileUrl));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.ResourceLibrary.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.ResourceLibrary.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/ResourceLibrary.ascx");
        }
    }
}
