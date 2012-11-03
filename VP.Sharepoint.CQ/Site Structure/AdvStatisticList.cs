using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class AdvStatisticList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.AdvStatisticList,
                            Name = ListsName.InternalName.AdvStatisticList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvStatisticList.InternalName.AdvID, FieldsName.AdvStatisticList.DisplayName.AdvID));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvStatisticList.InternalName.UserBrowser, FieldsName.AdvStatisticList.DisplayName.UserBrowser));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvStatisticList.InternalName.UserIP, FieldsName.AdvStatisticList.DisplayName.UserIP));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.AdvStatisticList.InternalName.UserUrl, FieldsName.AdvStatisticList.DisplayName.UserUrl));            
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.AdvStatisticList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.AdvStatisticList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
