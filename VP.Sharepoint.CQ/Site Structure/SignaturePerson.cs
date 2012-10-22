using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    class SignaturePerson
    {
        public static void CreateListStructure(SPWeb web)
        {

            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.SignaturePerson,
                            Name = ListsName.InternalName.SignaturePerson,
                            OnQuickLaunch = false,
                            EnableAttachments = true
                        };
            SPList list = helper.Apply();
            SPField fieldTitle = list.Fields.GetField(FieldsName.SignaturePerson.InternalName.Title);
            if (fieldTitle!=null)
            {
                fieldTitle.Title = FieldsName.SignaturePerson.DisplayName.Title;
                fieldTitle.Update();
            }
            list.Update();
        }
    }
}
