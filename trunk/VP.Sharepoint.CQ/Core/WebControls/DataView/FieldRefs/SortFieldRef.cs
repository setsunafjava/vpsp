using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class SortFieldRef : Control
    {
        public ListSortDirection SortDirection { get; set; }

        public string FieldName { get; set; }

        public string InternalFieldName { get; set; }

        public void Initialize(SPField field)
        {
            InternalFieldName = field.InternalName;
        }

        public string GetSortFieldRef()
        {
            return string.Format(SortDirection == ListSortDirection.Ascending
                                     ? "<FieldRef Name='{0}' />"
                                     : "<FieldRef Name='{0}' Ascending='FALSE' />", InternalFieldName);
        }
    }

    public class SortFieldRefCollectionEditor : CollectionEditor
    {
        public SortFieldRefCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof (SortFieldRef);
        }
    }
}
