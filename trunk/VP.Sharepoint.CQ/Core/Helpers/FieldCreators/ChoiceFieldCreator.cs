namespace VP.Sharepoint.CQ.Core.Helpers
{
    using System.Collections.Specialized;
    using Microsoft.SharePoint;

    public class ChoiceFieldCreator : BaseFieldCreator
    {
        public ChoiceFieldCreator(string internalName, string displayName): base(internalName, displayName, SPFieldType.Choice)
        {
            Choices = new StringCollection();
        }

        /// <summary>
        /// Determines whether to display the choice field as radio buttons or as a drop-down list. 
        /// </summary>
        public SPChoiceFormatType EditFormat { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether a text box for typing an alternative value is provided for the multichoice field.
        /// </summary>
        public bool FillInChoice { get; set; }

        /// <summary>
        /// Gets the choices that are used in the multichoice field.
        /// </summary>
        public StringCollection Choices { get; set; }

        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsField(Name))
            {
                // var name = list.Fields.Add(InternalName, SPFieldType.Choice, Required);
                list.Fields.AddFieldAsXml(this.XMLFieldFormat(string.Empty), true, SPAddFieldOptions.AddFieldInternalNameHint);
                list.Update();
            }

            var field = (SPFieldChoice) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = Description;

            field.Choices.Clear();

            foreach (var choice in this.Choices)
            {
                field.Choices.Add(choice);
            }
            field.EditFormat = this.EditFormat;
            field.FillInChoice = this.FillInChoice;
            field.DefaultValue = this.DefaultValue;
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