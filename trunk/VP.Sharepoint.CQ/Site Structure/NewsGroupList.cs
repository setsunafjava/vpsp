using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class NewsGroupList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.NewsGroupList,
                            Name = ListsName.InternalName.NewsGroupList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsGroupList.InternalName.Description, FieldsName.NewsGroupList.DisplayName.Description));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsGroupList.InternalName.ParentID, FieldsName.NewsGroupList.DisplayName.ParentID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsGroupList.InternalName.Status, FieldsName.NewsGroupList.DisplayName.Status));
            SPList list = helper.Apply();            
            SPField fieldTitle = list.Fields.GetField(FieldsName.NewsGroupList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.NewsGroupList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
