using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class WebsiteLink
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.WebsiteLink,
                            Name = ListsName.InternalName.WebsiteLink,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.WebsiteLink.InternalName.Description, FieldsName.WebsiteLink.DisplayName.Description));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.WebsiteLink.InternalName.Status, FieldsName.WebsiteLink.DisplayName.Status));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.WebsiteLink.InternalName.WebURL, FieldsName.WebsiteLink.DisplayName.WebURL));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.WebsiteLink.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.WebsiteLink.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.RowLimit = 100;
            defaultView.ViewFields.Add(Constants.EditColumn);
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.ViewFields.Add(FieldsName.WebsiteLink.InternalName.WebURL);
            defaultView.ViewFields.Add(FieldsName.WebsiteLink.InternalName.Status);
            defaultView.Update();
        }
    }
}
