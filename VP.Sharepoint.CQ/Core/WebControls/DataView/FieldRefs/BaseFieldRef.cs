using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VP.Sharepoint.CQ.Core.Helpers;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public abstract class BaseFieldRef : Control, IViewFieldRef
    {
        private string headerText;

        protected BaseFieldRef()
        {
            DefaultValue = "&nbsp;";
        }

        private string internalFieldName;

        /// <summary>
        ///   Gets the internal name that is used for the field.
        /// </summary>
        public virtual string InternalFieldName
        {
            get { return internalFieldName ?? FieldName; }
            internal set { internalFieldName = value; }
        }

        /// <summary>
        ///   Gets or sets the default value for a field.
        /// </summary>
        public string DefaultValue { get; set; }

        public bool IsHidden { get; set; }

        public virtual bool IsVirtualField
        {
            get { return false; }
        }

        /// <summary>
        ///   Gets or sets the type of the field.
        /// </summary>
        public virtual SPFieldType FieldType
        {
            get { return SPFieldType.Text; }
        }

        public virtual bool TextAlignRight
        {
            get { return false; }
        }

        public Unit Width { get; set; }

        #region Filterable

        /// <summary>
        ///   Gets a Boolean value that indicates whether the field can be filtered.
        /// </summary>
        public virtual bool Filterable { get; set; }

        public bool IsFilter { get; internal set; }

        public string FilterValue { get; internal set; }

        #endregion

        #region Sortable

        private bool sortable;

        /// <summary>
        ///   Gets a Boolean value that indicates whether the field can be sorted.
        /// </summary>
        public virtual bool Sortable
        {
            get { return sortable || Filterable; }
            set { sortable = value; }
        }

        #endregion

        #region Aggregations

        private bool sumFieldData;

        public virtual bool SupportSumFieldData
        {
            get { return false; }
        }

        public bool SumFieldData
        {
            get { return sumFieldData; }
            set
            {
                if (!SupportSumFieldData)
                {
                    throw new ArgumentException(string.Format("The field '{0}' not support sum field data.", FieldName));
                }
                sumFieldData = value;
            }
        }

        private string countStringFormat;

        /// <summary>
        /// The text will show for count field, default is Count = {0}
        /// </summary>
        public string CountStringFormat
        {
            get { return string.IsNullOrEmpty(countStringFormat) ? string.Format("{0} = {{0}}", "Count") : countStringFormat; }
            set { countStringFormat = value; }
        }

        public bool CountFieldData { get; set; }

        private string totalStringFormat;

        /// <summary>
        /// The text will show for total field, default is Total = {0}
        /// </summary>
        public string TotalStringFormat
        {
            get { return string.IsNullOrEmpty(totalStringFormat) ? string.Format("{0} = {{0}}", "Sum") : totalStringFormat; }
            set { totalStringFormat = value; }
        }

        public virtual void RenderSumFieldData(HtmlTextWriter writer, double value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Privates

        internal bool IsLastField { get; set; }

        #endregion

        #region IViewFieldRef Members

        /// <summary>
        ///   Gets or sets the display name for the field.
        /// </summary>
        public string FieldName{ get; set; }

        public string FieldNameWithTranslate{ get; set;}

        /// <summary>
        ///   Gets or sets the heading text for the field.
        /// </summary>
        public string HeaderText
        {
            get { return headerText ?? FieldNameWithTranslate; }
            set { headerText = value; }
        }

        public abstract void RenderCell(HtmlTextWriter writer, DataRow row);
        
        /// <summary>
        /// Return cell value with html encode
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public abstract string GetCellTextValue(DataRow row);

        public virtual string GetViewFieldRef()
        {
            return string.Format("<FieldRef Name='{0}' />", InternalFieldName);
        }

        public virtual void Initialize(SPField field)
        {
            InternalFieldName = field.InternalName;
            FieldNameWithTranslate = field.Title;
        }

        #endregion

        [Obsolete]
        public virtual string[] GetFilterQuery()
        {
            return string.IsNullOrEmpty(FilterValue)
                       ? new []{ string.Format("<IsNull><FieldRef Name='{0}' /></IsNull>", InternalFieldName) }
                       : new []{ string.Format("<Eq><FieldRef Name='{0}' /><Value Type='Text'><![CDATA[{1}]]></Value></Eq>", InternalFieldName, FilterValue) };
        }

        public abstract string GetFilterCamlQuery();

        [Obsolete]
        public virtual Dictionary<string, string> GetFilterValues(DataTable dt)
        {
            var empty = "(Empty)";

            var dic = dt.AsEnumerable().Select(item => Convert.ToString(item[FieldName]))
                .OrderBy(item => item).Distinct(StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(item => item, item => string.IsNullOrEmpty(item) ? empty : DataViewUtils.TrimStringOverMaxLength(item));
            return dic;
        }

        public virtual double GetSumFieldData(DataTable dt)
        {
            throw new NotImplementedException();
        }

        public virtual double GetSumFieldData(DataTable dt, Func<DataRow, bool> whereCondition)
        {
            throw new NotImplementedException();
        }
    }

    public class ViewFieldRefCollectionEditor : CollectionEditor
    {
        public ViewFieldRefCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof (BaseFieldRef);
        }
    }
}
