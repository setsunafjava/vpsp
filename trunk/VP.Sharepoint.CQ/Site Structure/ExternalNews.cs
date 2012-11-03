using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Common;
using VP.Sharepoint.CQ.Core.Helpers;

namespace VP.Sharepoint.CQ
{
    class ExternalNews
    {
        public static void CreateListStructure(SPWeb web)
        {
            
            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.ExternalNewsList,
                            Name = ListsName.InternalName.ExternalNewsList,
                            OnQuickLaunch = false, EnableAttachments = true
                        };

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNews.InternalName.Description, FieldsName.ExternalNews.DisplayName.Description));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNews.InternalName.NewsGroup, FieldsName.ExternalNews.DisplayName.NewsGroup));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNews.InternalName.LinkPath, FieldsName.ExternalNews.DisplayName.LinkPath));
            helper.AddField(new ChoiceFieldCreator(FieldsName.ExternalNews.InternalName.Status, FieldsName.ExternalNews.DisplayName.Status) { Choices = { "Hiển thị" } });
            helper.AddField(new NumberFieldCreator(FieldsName.ExternalNews.InternalName.Order, FieldsName.ExternalNews.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNews.InternalName.ImageThumb, FieldsName.ExternalNews.DisplayName.ImageThumb));
            helper.AddField(new UrlFieldCreator(FieldsName.ExternalNews.InternalName.ImageDsp, FieldsName.ExternalNews.DisplayName.ImageDsp) { DisplayFormat = SPUrlFieldFormatType.Image });

            var list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.ExternalNews.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.ExternalNews.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();


            //Set menu link
            //Utilities.SetMenuLink(list, FieldsName.ExternalNews.InternalName.Status);
            

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/ExternalNews.ascx");
        }
    }
}
