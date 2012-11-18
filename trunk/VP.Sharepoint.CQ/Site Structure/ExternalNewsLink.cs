using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Common;
using VP.Sharepoint.CQ.Core.Helpers;

namespace VP.Sharepoint.CQ
{
    class ExternalNewsLink
    {
        public static void CreateListStructure(SPWeb web)
        {
            
            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.ExternalNewsLinkList,
                            Name = ListsName.InternalName.ExternalNewsLinkList,
                            OnQuickLaunch = false, EnableAttachments = false
                        };

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNewsLink.InternalName.NewsGroup, FieldsName.ExternalNewsLink.DisplayName.NewsGroup));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNewsLink.InternalName.NewsGroupName, FieldsName.ExternalNewsLink.DisplayName.NewsGroupName));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.ExternalNewsLink.InternalName.LinkPath, FieldsName.ExternalNewsLink.DisplayName.LinkPath) { EnforceUniqueValues = true });

            var list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.ExternalNewsLink.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.ExternalNewsLink.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/ExternalNewsRSS.ascx");

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.Query = "<GroupBy Collapse='TRUE' GroupLimit='50'><FieldRef Name='" + FieldsName.ExternalNewsLink.InternalName.NewsGroupName + "' /></GroupBy>";
            defaultView.RowLimit = 100;
            defaultView.ViewFields.Add(Constants.EditColumn);
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.ViewFields.Add(FieldsName.ExternalNewsLink.InternalName.LinkPath);
            defaultView.Update();
        }
    }
}
