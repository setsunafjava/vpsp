using Microsoft.SharePoint;
using System;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    /// <summary>
    /// Represents a field in a list on a SharePoint Foundation Web site.
    /// </summary>
    public abstract class BaseFieldCreator
    {
        protected BaseFieldCreator(string internalName, string displayName, SPFieldType type)
        {
            this.Name = displayName;
            this.InternalName = internalName;
            this.Type = type;
        }

        /// <summary>
        /// Get or set Type of field
        /// </summary>
        public SPFieldType Type { get; set; }

        /// <summary>
        /// 	Gets or sets the display name for the field.
        /// </summary>
        public string InternalName { protected get; set; }

        /// <summary>
        /// 	Gets or sets the display name for the field.
        /// </summary>
        public string Name { protected get; set; }

        /// <summary>
        /// Gets or sets the description for a field.
        /// </summary>
        public string Description { protected get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether users must enter a value for the field on New  and Edit forms. 
        /// </summary>
        public bool Required { protected get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether to enforce uniqueness in column values. The default is false.
        /// </summary>
        public virtual bool EnforceUniqueValues { get; set; }

        /// <summary>
        /// Gets or sets the default value for a field.
        /// </summary>
        public virtual string DefaultValue { get; set; }

        /// <summary>
        /// Get XML format of field
        /// </summary>
        /// <param name="guidLookupList">Guid of lookup list: string.Empty if Type != Lookup</param>
        /// <returns></returns>
        public string XMLFieldFormat(string guidLookupList)
        {
            string xmlField = @"<Field " +
                                @"SourceID='http://schemas.microsoft.com/sharepoint/v3' " +
                                @"ID='{" + Guid.NewGuid() + "}' " +
                                @"Name='" + this.InternalName + "' " +
                                @"Required='" + (Required ? "TRUE" : "FALSE") + "' " +
                                @"Type='" + this.Type.ToString() + "' " +
                                @"DisplayName='" + Name + "' " +
                                @"Title='" + Name + "' " +
                                @"StaticName='" + this.InternalName + "' " +
                                @"Filterable='TRUE' " +
                                (this.Type == SPFieldType.Calculated ? @"FromBaseType='FALSE' " : string.Empty) +
                                (this.Type == SPFieldType.Lookup ? @"List='{" + guidLookupList + @"} '" : string.Empty) +
                                (this.Type == SPFieldType.Calculated ? @"ReadOnly='TRUE' ResultType='Text' " : string.Empty) +
                            @">" +
                                @"<FieldRefs><FieldRef Name='" + this.InternalName + @"' /></FieldRefs>" +
                                (this.Type == SPFieldType.Calculated ? @"<Formula>=&quot;&quot;</Formula>" : string.Empty) +
                            @"</Field>";

            return xmlField;
        }

        /// <summary>
        /// Indicates the formula referenced by the field and is evaluated when a list item is added or updated.
        /// </summary>
        public virtual string ValidationFormula { get; set; }

        /// <summary>
        /// Gets or sets a message to display to the user if validation fails for this field. 
        /// </summary>
        public string ValidationMessage { get; set; }

        internal abstract void CreateField(SPList list);
    }
}