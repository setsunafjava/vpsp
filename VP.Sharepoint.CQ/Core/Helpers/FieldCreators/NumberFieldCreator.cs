using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class NumberFieldCreator : BaseFieldCreator
    {
        public NumberFieldCreator(string internalName, string displayName)
            : base(internalName, displayName, SPFieldType.Number)
        {
            this.MinimumValue = double.MinValue;
            this.MaximumValue = double.MaxValue;
        }

        /// <summary>
        /// Gets or sets the number of decimal places to use when displaying the field. 
        /// </summary>
        public SPNumberFormatTypes DisplayFormat { get; set; }

        /// <summary>
        /// Gets or sets a maximum value for the field. 
        /// </summary>
        public double MaximumValue { get; set; }

        /// <summary>
        /// Gets or sets a minimum value for the field. 
        /// </summary>
        public double MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether to render the field as a percentage. 
        /// </summary>
        public virtual bool ShowAsPercentage { get; set; }

        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsField(Name))
            {
                // var name = list.Fields.Add(InternalName, SPFieldType.Number, Required);
                list.Fields.AddFieldAsXml(this.XMLFieldFormat(string.Empty), true, SPAddFieldOptions.AddFieldInternalNameHint);
                list.Update();
            }

            var field = (SPFieldNumber) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = this.Description;
            field.MinimumValue = this.MinimumValue;
            field.MaximumValue = this.MaximumValue;
            field.DisplayFormat = this.DisplayFormat;
            field.DefaultValue = this.DefaultValue;
            field.ShowAsPercentage = this.ShowAsPercentage;

            if (EnforceUniqueValues)
            {
                field.Indexed = true;
                field.EnforceUniqueValues = true;
            }
            field.ValidationFormula = ValidationFormula;
            field.ValidationMessage = ValidationMessage;
            field.Title = Name;
            field.AllowDeletion = true;
            field.Update();
        }
    }
}