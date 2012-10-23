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

            var list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.ExternalNews.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.ExternalNews.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();


            //Set menu link
            //Utilities.SetMenuLink(list, FieldsName.NewsList.InternalName.Status);
            

            //Add custom usercontrol to form
            //Utilities.AddForms(web, list, "../../UserControls/NewsList.ascx");
        }
    }
}
