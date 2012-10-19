using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class SingleLineTextFieldCreator : BaseFieldCreator
    {
        public SingleLineTextFieldCreator(string internalName, string displayName)
            : base(internalName, displayName, SPFieldType.Text)
        {
            this.MaxLength = 255;
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be typed in the field. 
        /// </summary>
        public int MaxLength { get; set; }

        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsFieldWithStaticName(InternalName))
            {
                list.Fields.Add(InternalName, SPFieldType.Text, Required);
            }

            var field = (SPFieldText)list.Fields.GetFieldByInternalName(InternalName);
            field.Description = Description;
            field.MaxLength = MaxLength;
            field.DefaultValue = DefaultValue;

            if (EnforceUniqueValues)
            {
                field.Indexed = true;
                field.EnforceUniqueValues = true;    
            }

            field.ValidationFormula = ValidationFormula;
            field.ValidationMessage = ValidationMessage;
            field.AllowDeletion = true;
            field.Update();            
        }
    }
}