using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    using System;
    using System.IO;
    using Microsoft.SharePoint;
    

    public class DependentLookupFieldCreator : BaseFieldCreator
    {
        public DependentLookupFieldCreator(string internalName, string displayName)
            : base(internalName, displayName, SPFieldType.Lookup)
        {
        }
        
        /// <summary>
        /// Gets or sets a field name of lookup (Internal name) that this field is dependent.
        /// </summary>
        public string PrimaryLookupField { get; set; }

        /// <summary>
        /// Gets or sets a field name for lookup (Internal name).
        /// </summary>
        public string LookupField { get; set; }

        /// <summary>
        /// Gets or sets a list name for lookup (URL name)
        /// </summary>
        public string LookupList { get; set; }
        
        internal override void CreateField(SPList list)
        {
            if (list.Fields.ContainsField(Name)) return;

            var targetList = list.ParentWeb.Lists.TryGetList(this.LookupList);
            if (targetList == null)
            {
                string urlTargetList = list.ParentWeb.Url + "/Lists/" + this.LookupList;
                try
                {
                    targetList = list.ParentWeb.GetList(urlTargetList);
                }
                catch (FileNotFoundException fx) { Utilities.LogToULS(fx); }

                if (targetList == null)
                {
                    urlTargetList = list.ParentWeb.Url + "/" + this.LookupList;

                    targetList = list.ParentWeb.GetList(urlTargetList);
                }
            }
            
            string xmlField = @"<Field " +
                            @"WebId='" + list.ParentWeb.ID + "' " +
                            @"ID='{" + Guid.NewGuid() + "}' " +
                            @"Name='" + InternalName + "' " +                                
                            @"Type='" + SPFieldType.Lookup.ToString() + "' " +
                            @"DisplayName='" + Name + "' " +
                            @"Title='" + Name + "' " +
                            @"StaticName='" + InternalName + "' " +                                
                            @"List='{" + targetList.ID + @"}' " +
                            @"ShowField='" + this.LookupField + "' " +
                            @"FieldRef='" + list.Fields.GetFieldByInternalName(PrimaryLookupField).Id + "' " +
                            @"ReadOnly='TRUE' " +
                            @"UnlimitedLengthInDocumentLibrary='FALSE' " +
                        @">" +
                            @"<FieldRefs><FieldRef Name='" + InternalName + @"' /></FieldRefs>" +
                        @"</Field>";

            list.Fields.AddFieldAsXml(xmlField, true, SPAddFieldOptions.AddFieldInternalNameHint);
            list.Update();

            list.Fields.GetFieldByInternalName(InternalName).AllowDeletion = true;
            list.Fields.GetFieldByInternalName(InternalName).Update();
        }
    }
}