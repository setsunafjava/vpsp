using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class DocumentType
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.DocumentType,
                            Name = ListsName.InternalName.DocumentType,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.DocumentType.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.DocumentType.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.RowLimit = 100;
            defaultView.ViewFields.Add(Constants.EditColumn);
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.Update();
        }
    }
}
