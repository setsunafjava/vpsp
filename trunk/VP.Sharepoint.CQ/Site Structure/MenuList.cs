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
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.MenuID, FieldsName.MenuList.DisplayName.MenuID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.MenuList.InternalName.MenuOrder, FieldsName.MenuList.DisplayName.MenuOrder));
            helper.AddField(new NumberFieldCreator(FieldsName.MenuList.InternalName.MenuOrderDisp, FieldsName.MenuList.DisplayName.MenuOrderDisp));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetFieldByInternalName(FieldsName.MenuList.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.MenuList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
