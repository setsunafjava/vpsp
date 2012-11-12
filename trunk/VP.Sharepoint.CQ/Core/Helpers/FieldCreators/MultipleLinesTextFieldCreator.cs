using System;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class MultipleLinesTextFieldCreator : BaseFieldCreator
    {
        public MultipleLinesTextFieldCreator(string internalName, string displayName): base(internalName, displayName, SPFieldType.Note)
        {
            this.NumberOfLines = 6;
        }

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

        /// <summary>
        /// Gets or sets the number of lines to display in the field. 
        /// </summary>
        public int NumberOfLines { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether rich text formatting can be used in the field. 
        /// </summary>
        public bool RichText { get; set; }

        /// <summary>
        /// Gets or sets the rich text mode for the field.
        /// </summary>
        public SPRichTextMode RichTextMode { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that specifies whether to append changes to the existing text.
        /// </summary>
        public bool AppendOnly { get; set; }

        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsFieldWithStaticName(InternalName))
            {
                list.Fields.Add(InternalName, SPFieldType.Note, Required);
            }
            else
            {
                var oldField = list.Fields.GetFieldByInternalName(InternalName);
                if (!oldField.Type.Equals(SPFieldType.Note))
                {
                    oldField.Type = SPFieldType.Note;
                    oldField.Update();
                }
            }

            var field = (SPFieldMultiLineText) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = this.Description;
            field.NumberOfLines = this.NumberOfLines;
            field.RichText = this.RichText;
            if (this.RichText)
            {
                field.RichTextMode = this.RichTextMode;    
            }

            field.AppendOnly = this.AppendOnly;
            field.Title = this.Name;
            field.AllowDeletion = true;
            field.Update();
        }
    }
}