using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class CurrencyFieldCreator : NumberFieldCreator
    {
        public CurrencyFieldCreator(string internalName, string displayName): base(internalName, displayName)
        {
        }

        /// <summary>
        /// Gets or sets the currency symbol that is used to format the field's value, and also the position of the currency symbol (for TJ.NCC.MasterData, whether before or after numeric values).
        /// </summary>
        public int CurrencyLocaleId { get; set; }
        
        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsField(Name))
            {
                // var name = list.Fields.Add(InternalName, SPFieldType.Currency, Required);
                list.Fields.AddFieldAsXml(this.XMLFieldFormat(string.Empty), true, SPAddFieldOptions.AddFieldInternalNameHint);
                list.Update();
            }

            var field = (SPFieldCurrency) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = Description;
            field.MinimumValue = MinimumValue;
            field.MaximumValue = MaximumValue;
            field.DisplayFormat = DisplayFormat;
            field.DefaultValue = DefaultValue;
            field.CurrencyLocaleId = CurrencyLocaleId;
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