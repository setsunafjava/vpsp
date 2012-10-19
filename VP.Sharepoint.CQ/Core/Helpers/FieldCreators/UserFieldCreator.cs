using System;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class UserFieldCreator : BaseFieldCreator
    {
        public UserFieldCreator(string internalName, string displayName): base(internalName, displayName, SPFieldType.User)
        {
        }

        public override string ValidationFormula
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public SPFieldUserSelectionMode SelectionMode { get; set; }
        public bool AllowMultipleValues { get; set; }
        public int SelectionGroup { get; set; }
        public string ShowField { get; set; }
        
        internal override void CreateField(SPList list)
        {
            if (!list.Fields.ContainsFieldWithStaticName(InternalName))
            {
                list.Fields.AddFieldAsXml(string.Format("<Field Type=\"UserMulti\" DisplayName=\"{0}\" List=\"UserInfo\" ShowField=\"ImnName\" Required=\"{1}\" UserSelectionMode=\"{2}\" UserSelectionScope=\"{3}\" Mult=\"TRUE\" Sortable=\"FALSE\" />", InternalName, Required.ToString().ToUpperInvariant(), SelectionMode, SelectionGroup), false, SPAddFieldOptions.Default);
            }

            var field = (SPFieldUser) list.Fields.GetFieldByInternalName(InternalName);
            field.Description = Description;
            field.SelectionMode = SelectionMode;
            field.AllowMultipleValues = AllowMultipleValues;
            
            if (!string.IsNullOrEmpty(ShowField))
                field.LookupField = ShowField;
            else
                field.LookupField = "ImnName";

            if (SelectionGroup > 0)
            {
                field.SelectionGroup = SelectionGroup;
            }

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