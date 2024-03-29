﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class DocumentsList
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.DocumentsList,
                            Name = ListsName.InternalName.DocumentsList,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.DocumentsList.InternalName.DocumentNo, FieldsName.DocumentsList.DisplayName.DocumentNo)
            {
            });

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.DocumentsList.InternalName.CategoryId, FieldsName.DocumentsList.DisplayName.CategoryId));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.DocumentsList.InternalName.CategoryName, FieldsName.DocumentsList.DisplayName.CategoryName));
            helper.AddField(new LookupFieldCreator(FieldsName.DocumentsList.InternalName.DocumentSubject, FieldsName.DocumentsList.DisplayName.DocumentSubject)
             {
                 LookupList = ListsName.InternalName.DocumentSubject,
                 LookupField = FieldsName.DocumentSubject.InternalName.Title
             });

            helper.AddField(new LookupFieldCreator(FieldsName.DocumentsList.InternalName.DocumentType, FieldsName.DocumentsList.DisplayName.DocumentType)
            {
                LookupList = ListsName.InternalName.DocumentType,
                LookupField = FieldsName.DocumentType.InternalName.Title
            });

           helper.AddField(new LookupFieldCreator(FieldsName.DocumentsList.InternalName.PublishPlace, FieldsName.DocumentsList.DisplayName.PublishPlace)
           {
               LookupList = ListsName.InternalName.PublishPlace,
               LookupField = FieldsName.PublishPlace.InternalName.Title
           });

           helper.AddField(new LookupFieldCreator(FieldsName.DocumentsList.InternalName.SignaturePerson, FieldsName.DocumentsList.DisplayName.SignaturePerson)
           {
               LookupList = ListsName.InternalName.SignaturePerson,
               LookupField = FieldsName.SignaturePerson.InternalName.Title
           });

           helper.AddField(new DateTimeFieldCreator(FieldsName.DocumentsList.InternalName.EffectedDate, FieldsName.DocumentsList.DisplayName.EffectedDate)
           {
              DisplayFormat=SPDateTimeFieldFormatType.DateOnly

           });

           helper.AddField(new DateTimeFieldCreator(FieldsName.DocumentsList.InternalName.ExpiredDate, FieldsName.DocumentsList.DisplayName.ExpiredDate)
           {
               DisplayFormat = SPDateTimeFieldFormatType.DateOnly

           });
           helper.AddField(new SingleLineTextFieldCreator(FieldsName.DocumentsList.InternalName.FilePath, FieldsName.DocumentsList.DisplayName.FilePath));

            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.DocumentsList.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.DocumentsList.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/DocumentsList.ascx");

            var defaultView = list.DefaultView;
            defaultView.ViewFields.DeleteAll();
            defaultView.RowLimit = 100;
            defaultView.Query = "<GroupBy Collapse='TRUE' GroupLimit='50'><FieldRef Name='" + FieldsName.DocumentsList.InternalName.DocumentType + "' /><FieldRef Name='" + FieldsName.DocumentsList.InternalName.DocumentSubject + "' /></GroupBy>";
            defaultView.ViewFields.Add(Constants.EditColumn);
            defaultView.ViewFields.Add(Constants.FieldTitleLinkToItem);
            defaultView.ViewFields.Add(FieldsName.DocumentsList.InternalName.DocumentNo);
            defaultView.ViewFields.Add(FieldsName.DocumentsList.InternalName.PublishPlace);
            defaultView.ViewFields.Add(FieldsName.DocumentsList.InternalName.SignaturePerson);
            defaultView.ViewFields.Add(FieldsName.DocumentsList.InternalName.EffectedDate);
            defaultView.ViewFields.Add(FieldsName.DocumentsList.InternalName.ExpiredDate);
            defaultView.Update();
        }
    }
}
