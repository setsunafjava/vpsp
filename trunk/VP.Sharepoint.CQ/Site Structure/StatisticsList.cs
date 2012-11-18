using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class StatisticsList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.StatisticsList,
                            Name = ListsName.InternalName.StatisticsList,
                            OnQuickLaunch = false,
                            EnableAttachments = false
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.StatisticsList.InternalName.UserBrowser, FieldsName.StatisticsList.DisplayName.UserBrowser));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.StatisticsList.InternalName.UserIP, FieldsName.StatisticsList.DisplayName.UserIP));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.StatisticsList.InternalName.UserUrl, FieldsName.StatisticsList.DisplayName.UserUrl));            
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.StatisticsList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.StatisticsList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.RowLimit = 100;
            defaultView.Query = "<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.ViewFields.Add(FieldsName.StatisticsList.InternalName.UserBrowser);
            defaultView.ViewFields.Add(FieldsName.StatisticsList.InternalName.UserIP);
            defaultView.ViewFields.Add(FieldsName.StatisticsList.InternalName.UserUrl);
            defaultView.Update();
        }
    }
}
