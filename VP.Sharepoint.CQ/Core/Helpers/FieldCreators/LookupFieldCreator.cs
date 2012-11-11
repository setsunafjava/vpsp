using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    using System;
    using System.IO;
    using Microsoft.SharePoint;
    

    public class LookupFieldCreator : BaseFieldCreator
    {
        public LookupFieldCreator(string internalName, string displayName) : base(internalName, displayName, SPFieldType.Lookup)
        {
        }

        public override string ValidationFormula
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether multiple values can be used in the lookup field.
        /// </summary>
        public bool AllowMultipleValues { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the lookup field is discoverable from the list to which it looks for its value.
        /// </summary>
        public bool IsRelationship { get; set; }

        /// <summary>
        /// Gets or sets a field name for lookup (Internal Name).
        /// </summary>
        public string LookupField { get; set; }

        /// <summary>
        /// Gets or sets a list name for lookup (URL name)
        /// </summary>
        public string LookupList { get; set; }

        /// <summary>
        /// Gets or sets the delete behavior of the lookup field.
        /// </summary>
        public SPRelationshipDeleteBehavior RelationshipDeleteBehavior { get; set; }

        internal override void CreateField(SPList list)
        {
            var targetList = list.ParentWeb.Lists.TryGetList(this.LookupList);
            if (targetList == null)
            {
                string urlTargetList = Utilities.GetWebUrl(list.ParentWeb.Url) + "/Lists/" + this.LookupList;
                try
                {
                    targetList = list.ParentWeb.GetList(urlTargetList);
                }
                catch (FileNotFoundException fx) { Utilities.LogToULS(fx); }

                if (targetList == null)
                {
                    urlTargetList = Utilities.GetWebUrl(list.ParentWeb.Url) + "/" + this.LookupList;

                    targetList = list.ParentWeb.GetList(urlTargetList);
                }
            }

            if (!list.Fields.ContainsFieldWithStaticName(InternalName))
            {
                list.Fields.AddLookup(InternalName, targetList.ID, Required);
            }

            var field = (SPFieldLookup) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = this.Description;
            field.LookupField = this.LookupField;
            field.AllowMultipleValues = this.AllowMultipleValues;
            field.IsRelationship = this.IsRelationship;
            field.RelationshipDeleteBehavior = this.RelationshipDeleteBehavior;

            if (EnforceUniqueValues)
            {
                field.Indexed = true;
                field.EnforceUniqueValues = true;
            }

            field.Title = Name;
            field.AllowDeletion = true;
            field.Update();
        }
    }
}