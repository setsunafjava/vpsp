﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class IconLinkList
    {
        public static void CreateListStructure(SPWeb web)
        { 
            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.IconLinkList,
                            Name = ListsName.InternalName.IconLinkList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.IconLinkList.InternalName.Description, FieldsName.IconLinkList.DisplayName.Description));            
            helper.AddField(new NumberFieldCreator(FieldsName.IconLinkList.InternalName.Status, FieldsName.IconLinkList.DisplayName.Status));
            helper.AddField(new NumberFieldCreator(FieldsName.IconLinkList.InternalName.Order, FieldsName.IconLinkList.DisplayName.Order));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.IconLinkList.InternalName.LinkURL, FieldsName.IconLinkList.DisplayName.LinkURL));
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.IconLinkList.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.IconLinkList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.RowLimit = 100;
            defaultView.ViewFields.Add(Constants.EditColumn);
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.ViewFields.Add(FieldsName.IconLinkList.InternalName.Status);
            defaultView.ViewFields.Add(FieldsName.IconLinkList.InternalName.Order);
            defaultView.Update();
        }
    }
}
