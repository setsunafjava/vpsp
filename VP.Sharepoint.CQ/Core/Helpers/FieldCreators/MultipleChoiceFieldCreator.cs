using System;
using System.Collections.Specialized;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class MultipleChoiceFieldCreator : BaseFieldCreator
    {
        public MultipleChoiceFieldCreator(string internalName, string displayName): base(internalName, displayName, SPFieldType.MultiChoice)
        {
            Choices = new StringCollection();
        }

        /// <summary>
        /// Gets or sets a Boolean value that determines whether a text box for typing an alternative value is provided for the multichoice field. 
        /// </summary>
        public bool FillInChoice { get; set; }

        [Obsolete("Use FillInChoice")]
        public bool AllowFillIn { get; set; }

        public StringCollection Choices { get; set; }

        public override bool EnforceUniqueValues
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override string ValidationFormula
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsField(Name))
            {
                // var name = list.Fields.Add(InternalName, SPFieldType.MultiChoice, Required);
                list.Fields.AddFieldAsXml(this.XMLFieldFormat(string.Empty), true, SPAddFieldOptions.AddFieldInternalNameHint);
                list.Update();
            }

            var field = (SPFieldMultiChoice) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = Description;

            field.Choices.Clear();

            foreach (var choice in Choices)
            {
                field.Choices.Add(choice);
            }

            field.FillInChoice = FillInChoice;
            field.DefaultValue = DefaultValue;
            field.Title = Name;
            field.AllowDeletion = true;
            field.Update();
        }
    }
}