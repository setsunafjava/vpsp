using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class MultipleCheckBoxLookupField : BaseFieldControl, IPostBackDataHandler
    {
        private List<int> selectedItems;

        /// <summary>
        ///   Gets or sets a value indicating whether the number of columns in check box list
        /// </summary>
        public int Columns
        {
            get
            {
                var value = ViewState["Columns"];
                if (value != null)
                {
                    return (int) value;
                }
                return 1;
            }
            set { ViewState["Columns"] = value; }
        }

        public Unit Height
        {
            get
            {
                var value = ViewState["Height"];
                if (value != null)
                {
                    return (Unit) value;
                }
                return Unit.Empty;
            }
            set { ViewState["Height"] = value; }
        }

        /// <summary>
        ///   Caml where condition for select from data source
        /// </summary>
        public string WhereCondition
        {
            get
            {
                var value = ViewState["WhereCondition"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set { ViewState["WhereCondition"] = value; }
        }

        /// <summary>
        ///   Gets selected values
        /// </summary>
        public List<int> SelectedItems
        {
            get { return selectedItems ?? (selectedItems = new List<int>()); }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var postedValue = postCollection[postDataKey];
            if (postedValue != null)
            {
                var split = postedValue.Split(new[] {","}, StringSplitOptions.None);
                foreach (var str in split)
                {
                    SelectedItems.Add(Convert.ToInt32(str));
                }
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack && ControlMode == SPControlMode.Edit && ItemFieldValue != null)
            {
                var values = (SPFieldLookupValueCollection) ItemFieldValue;
                foreach (var value in values)
                {
                    SelectedItems.Add(value.LookupId);
                }
            }
        }

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            var field = (SPFieldLookup) Field;
            if (!field.AllowMultipleValues)
            {
                throw new NotSupportedException();
            }

            var list = Web.Lists[new Guid(field.LookupList)];

            var whereCondition = WhereCondition;
            if (string.IsNullOrEmpty(whereCondition))
            {
                whereCondition = string.Format("<Where></Where><OrderBy><FieldRef Name='{0}' /></OrderBy>",
                                               field.LookupField);
            }

            var query = new SPQuery {Query = whereCondition};
            var items = list.GetItems(query);

            var rows = Math.Ceiling((double) items.Count/Columns);

            if (!Height.IsEmpty)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Height.ToString());
                writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "auto");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");

            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            var index = 0;

            for (var i = 1; i <= rows; i++)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                for (var j = 1; j <= Columns; j++)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);

                    if (index < items.Count)
                    {
                        var item = items[index];

                        writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_" + index);
                        writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
                        writer.AddAttribute(HtmlTextWriterAttribute.Value, item.ID.ToString());
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
                        if (SelectedItems.Contains(item.ID))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.Input);
                        writer.RenderEndTag();

                        writer.AddAttribute(HtmlTextWriterAttribute.For, ClientID + "_" + index);
                        writer.RenderBeginTag(HtmlTextWriterTag.Label);
                        writer.Write(SPEncode.HtmlEncode(item[field.LookupField].ToString()));
                        writer.RenderEndTag();
                    }
                    else
                    {
                        writer.Write("&nbsp;");
                    }

                    writer.RenderEndTag(); // td

                    index++;
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag(); // table

            writer.RenderEndTag(); // div

            RenderValidationMessage(writer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ControlMode == SPControlMode.Display)
            {
                RenderFieldForDisplay(writer);
            }
            else
            {
                RenderFieldForInput(writer);
            }
        }

        public override void Validate()
        {
            IsValid = true;
            if (Field.Required && SelectedItems.Count == 0)
            {
                IsValid = false;
                ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
            }
        }

        public override void UpdateFieldValueInItem()
        {
            var values = new SPFieldLookupValueCollection();
            values.AddRange(SelectedItems.Select(item => new SPFieldLookupValue(item, string.Empty)));
            ItemFieldValue = values;
        }

        protected override void OnPreRender(EventArgs e)
        {
            Page.RegisterRequiresPostBack(this);
            base.OnPreRender(e);
        }

        protected override void RenderFieldForDisplay(HtmlTextWriter writer)
        {
            if (SPContext.Current.FormContext.FormMode == SPControlMode.New)
            {
                writer.Write("&nbsp;");
            }
            else
            {
                base.RenderFieldForDisplay(writer);
            }
        }
    }
}