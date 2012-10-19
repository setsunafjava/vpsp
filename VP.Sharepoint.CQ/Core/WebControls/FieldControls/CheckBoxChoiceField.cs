using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    /// <summary>
    ///   Represents the field control for the check box multichoice field.
    /// </summary>
    public class CheckBoxChoiceField : BaseFieldControl, IPostBackDataHandler
    {
        private List<string> selectedItems;
        private string fillInValue;
        private string selectedItemsString;

        public string LookupList
        {
            get
            {
                var value = ViewState["LookupList"];
                if (value == null)
                {
                    return string.Empty;
                }
                return (string) value;
            }
            set { ViewState["LookupList"] = value; }
        }

        public string LookupField
        {
            get
            {
                var value = ViewState["LookupField"];
                if (value == null)
                {
                    return string.Empty;
                }
                return (string) value;
            }
            set { ViewState["LookupField"] = value; }
        }

        public string WhereCondition
        {
            get
            {
                var value = ViewState["WhereCondition"];
                if (value == null)
                {
                    return string.Empty;
                }
                return (string) value;
            }
            set { ViewState["WhereCondition"] = value; }
        }

        public string SeparateCharacter
        {
            get
            {
                var value = ViewState["SeparateCharacter"];
                if (value == null)
                {
                    return string.Empty;
                }
                return (string) value;
            }
            set { ViewState["SeparateCharacter"] = value; }
        }

        [DefaultValue(false)]
        public bool AutoPostBack
        {
            get
            {
                var obj = ViewState["AutoPostBack"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set { ViewState["AutoPostBack"] = value; }
        }

        public List<string> SelectedItems
        {
            get { return selectedItems ?? (selectedItems = new List<string>()); }
        }

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

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(object sender, EventArgs args)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, args);
            }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            selectedItems = new List<string>();
            var postValues = postCollection[postDataKey];
            foreach (var postValue in postValues.Split(','))
            {
                selectedItems.Add(SPEncode.HtmlDecode(postValue));
            }

            fillInValue = postCollection[postDataKey + "$FillInChoiceValue"];
            if (!string.IsNullOrEmpty(fillInValue))
            {
                fillInValue = fillInValue.Trim();
            }

            if (!string.IsNullOrEmpty(fillInValue))
            {
                selectedItems.Add(fillInValue);
            }

            var oldSelectedItems = new List<string>();
            if (!string.IsNullOrEmpty(selectedItemsString))
            {
                oldSelectedItems.AddRange(selectedItemsString.Split(new [] {";#"}, StringSplitOptions.None));
            }

            // Sort
            selectedItems.Sort();
            oldSelectedItems.Sort();

            return !selectedItems.SequenceEqual(oldSelectedItems);
        }

        public void RaisePostDataChangedEvent()
        {
            OnSelectedIndexChanged(this, EventArgs.Empty);
        }

        #endregion

        private IList<string> GetChoiceValues()
        {
            List<string> values;

            if (string.IsNullOrEmpty(LookupList))
            {
                var field = (SPFieldMultiChoice) Field;
                values = field.Choices.Cast<string>().ToList();
            }
            else
            {
                var list = Web.Lists[LookupList];
                SPListItemCollection items;
                if (string.IsNullOrEmpty(WhereCondition))
                {
                    items = list.GetItems(new[] {LookupField});
                }
                else
                {
                    var query = new SPQuery {Query = WhereCondition};
                    items = list.GetItems(query);
                }

                if (string.IsNullOrEmpty(SeparateCharacter))
                {
                    values = (from SPListItem item in items select Convert.ToString(item[LookupField])).ToList();
                }
                else
                {
                    values = new List<string>();
                    foreach (var split in from SPListItem item in items
                                          select ConvertToString(item[LookupField])
                                          into value select SplitStringValue(value))
                    {
                        values.AddRange(split);
                    }
                }

                // Trim null or empty string
                values.RemoveAll(item => item == null || string.IsNullOrEmpty(item.Trim()));

                values.Sort();
            }

            return values;
        }

        private static string ConvertToString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        private string [] SplitStringValue(string value)
        {
            return SeparateCharacter == "\\r\\n" ? 
                value.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries) : 
                value.Split(new [] {SeparateCharacter}, StringSplitOptions.RemoveEmptyEntries);
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

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            var field = (SPFieldMultiChoice)Field;
            var values = GetChoiceValues();
            var rows = Math.Ceiling((double) values.Count/Columns);
            string postBackEventReference = null;

            if (AutoPostBack)
            {
                postBackEventReference = Page.ClientScript.GetPostBackEventReference(this, null);
            }

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

                    if (index < values.Count)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Title, values[index]);
                    }
                    
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);

                    if (index < values.Count)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_" + index);
                        writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
                        writer.AddAttribute(HtmlTextWriterAttribute.Value, SPEncode.HtmlEncode(values[index]));
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
                        if (SelectedItems.Contains(values[index]))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                        }
                        else
                        {
                            if (!Page.IsPostBack && ControlMode == SPControlMode.New)
                            {
                                if(values[index].Equals(field.DefaultValue))
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");        
                                }
                            }
                        }

                        if (AutoPostBack)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, postBackEventReference);
                        }

                        writer.RenderBeginTag(HtmlTextWriterTag.Input);
                        writer.RenderEndTag();

                        writer.AddAttribute(HtmlTextWriterAttribute.For, ClientID + "_" + index);
                        writer.RenderBeginTag(HtmlTextWriterTag.Label);
                        writer.Write(SPEncode.HtmlEncode(values[index]));
                        writer.RenderEndTag();
                    }
                    else
                    {
                        writer.Write("&nbsp;");
                    }
                    
                    writer.RenderEndTag(); // span
                    writer.RenderEndTag(); // td

                    index++;
                }
                writer.RenderEndTag(); // tr
            }

            if (field.FillInChoice)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, Columns.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_FillInChoice");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "$FillInChoice");
                if (!string.IsNullOrEmpty(fillInValue))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input

                writer.AddAttribute(HtmlTextWriterAttribute.For, ClientID + "_FillInChoice");
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(SPResource.GetString(Strings.ChoiceFieldFillInChoiceText));
                writer.RenderEndTag();

                writer.RenderEndTag(); // td

                writer.RenderEndTag(); // tr

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, Columns.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "$FillInChoiceValue");
                writer.AddAttribute("onfocus", string.Format("SetChoiceOption('{0}_FillInChoice')", ClientID));
                writer.AddAttribute(HtmlTextWriterAttribute.Value, fillInValue);
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input
                
                writer.RenderEndTag(); // td

                writer.RenderEndTag(); // tr
            }

            writer.RenderEndTag(); // table
            RenderValidationMessage(writer);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack && ControlMode == SPControlMode.Edit && ItemFieldValue != null)
            {
                var values = Convert.ToString(ItemFieldValue).Split(new[] { ";#" }, StringSplitOptions.None);
                selectedItems = new List<string>(values);
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
            ItemFieldValue = string.Join(";#", SelectedItems.ToArray());
        }

        protected override object SaveViewState()
        {
            selectedItemsString = string.Join(";#", SelectedItems.ToArray());

            var baseState = base.SaveViewState();
            var allStates = new object[2];
            allStates[0] = baseState;
            allStates[1] = selectedItemsString;
            return allStates;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                var allStates = (object[])savedState;
                if (allStates[0] != null)
                {
                    base.LoadViewState(allStates[0]);        
                }
                selectedItemsString = allStates[1] as string;
            }
        }
    }
}