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
    public class DropDownChoiceField : BaseFieldControl, IPostBackDataHandler
    {
        private StringCollection items;
        private bool fillInChoiceSelected;
        
        [DefaultValue(false)]
        public bool AutoPostBack
        {
            get
            {
                var value = ViewState["AutoPostBack"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["AutoPostBack"] = value; }
        }

        public string LookupList
        {
            get
            {
                var value = ViewState["LookupList"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["LookupList"] = value; }
        }

        public string LookupField
        {
            get
            {
                var value = ViewState["LookupField"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["LookupField"] = value; }
        }

        public string DefaultValue
        {
            get
            {
                var value = ViewState["DefaultValue"];
                return value == null ? Field.DefaultValue : value.ToString();
            }
            set { ViewState["DefaultValue"] = value; }
        }

        public override object Value
        {
            get
            {
                var value = ViewState["Value"];
                return value == null ? string.Empty : value.ToString();
            }
            set { ViewState["Value"] = value; }
        }

        public bool FillInChoice
        {
            get { return ((SPFieldChoice) Field).FillInChoice; }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var keys = new List<string>(postCollection.AllKeys);

            if (!keys.Contains(postDataKey) && !keys.Contains(postDataKey + "$FillInChoice"))
            {
                return false;
            }

            fillInChoiceSelected = postCollection[postDataKey + "$FillInChoice"] == "FillInButton";

            var selectedItem = fillInChoiceSelected ? postCollection[postDataKey + "$FillInChoiceValue"] : SPEncode.HtmlDecode(postCollection[postDataKey]);

            if (!string.Equals(selectedItem, Convert.ToString(Value)))
            {
                Value = selectedItem;
                return true;
            }

            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        #endregion

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(EventArgs args)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, args);
            }
        }

        /// <summary>
        ///   Removes all objects from the collection.
        /// </summary>
        public void Clear()
        {
            items = null;
        }

        /// <summary>
        ///   Add object into the collection.
        /// </summary>
        /// <param name = "item"></param>
        public void AddItem(string item)
        {
            if (items == null)
            {
                items = new StringCollection();
            }
            items.Add(item);
        }

        protected override void RenderFieldForInput(HtmlTextWriter writer)
        {
            var options = GetSelectOptions();

            var value = Convert.ToString(Value);
            if (!Page.IsPostBack && ControlMode == SPControlMode.New)
            {
                value = DefaultValue;
            }

            if (!string.IsNullOrEmpty(value))
            {
                if (!options.Contains(value))
                {
                    fillInChoiceSelected = true;
                }
            }

            var fillInChoice = FillInChoice;

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            // Tr
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            if (fillInChoice)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                if (!fillInChoiceSelected)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format("{0}: {1}", Field.Title, SPResource.GetString("DropDownChoiceButtonToolTip")));
                writer.AddAttribute(HtmlTextWriterAttribute.Value, "DropDownButton");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "$FillInChoice");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_DropDownButton");
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input
                writer.RenderEndTag(); // td
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Title, Field.Title);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-RadioText");

            if (fillInChoice)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("document.getElementById('{0}_DropDownButton').checked = true;", ClientID));    
            }

            if (AutoPostBack)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onchange, Page.ClientScript.GetPostBackEventReference(this, null));
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Select);

            foreach (var option in options)
            {
                if (String.Equals(option, value, StringComparison.InvariantCultureIgnoreCase))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Selected, "selected");
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Value, SPEncode.HtmlEncode(option));
                writer.RenderBeginTag(HtmlTextWriterTag.Option);
                writer.Write(SPEncode.HtmlEncode(option));
                writer.RenderEndTag();
            }

            writer.RenderEndTag(); // select

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr

            if (fillInChoice)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                if (fillInChoiceSelected)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_FillInButton");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "$FillInChoice");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, "FillInButton");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format("{0}: {1}", Field.Title, SPResource.GetString("ChoiceFieldFillInChoiceText")));
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input
                writer.RenderEndTag(); // td

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(SPResource.GetString("ChoiceFieldFillInChoiceText", new object[0]));
                writer.RenderEndTag(); // td

                writer.RenderEndTag(); // tr

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("&nbsp;");
                writer.RenderEndTag(); // td

                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                writer.AddAttribute(HtmlTextWriterAttribute.Title, Field.Title);
                writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, "255");
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "$FillInChoiceValue");
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("document.getElementById('{0}_FillInButton').checked = true;", ClientID));

                if (fillInChoiceSelected)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(Value));
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input

                writer.RenderEndTag(); // td

                writer.RenderEndTag(); // tr
            }

            writer.RenderEndTag(); // table
            
            RenderValidationMessage(writer);
        }

        private IEnumerable<string> GetSelectOptions()
        {
            // Manual items source
            if (items != null && items.Count > 0)
            {
                return items.Cast<string>().ToList();
            }

            // Lookup List Field data source
            if (!string.IsNullOrEmpty(LookupList))
            {
                var web = SPContext.Current.Web;
                var list = web.Lists[LookupList];
                var field = list.Fields[LookupField];

                var listItems = list.GetItems(field.InternalName);
                return listItems.Cast<SPListItem>()
                    .Select(i => Convert.ToString(i[field.Id])).Distinct(StringComparer.InvariantCultureIgnoreCase).
                    OrderBy(i => i)
                    .ToList();
            }

            // Default field data source
            var thisField = (SPFieldChoice) Field;

            return thisField.Choices.Cast<string>().ToList();
        }

        protected override void RenderFieldForDisplay(HtmlTextWriter writer)
        {
            if (SPContext.Current.FormContext.FormMode == SPControlMode.New)
            {
                writer.Write("&nbsp;");
            }
            else
            {
                writer.Write(ItemFieldValue == null ? "&nbsp;" : SPEncode.HtmlEncode(ItemFieldValue.ToString()));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (Page != null)
            {
                Page.RegisterRequiresControlState(this);
                Page.RegisterRequiresPostBack(this);    
            }

            base.OnInit(e);
        }

        protected override object SaveControlState()
        {
            var controlState = base.SaveControlState();
            var allState = new object[2];
            allState[0] = controlState;
            allState[1] = items;
            return allState;
        }

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
            {
                var allState = (object[])savedState;
                base.LoadControlState(allState[0]);
                items = allState[1] as StringCollection;    
            }
        }

        public override void Validate()
        {
            IsValid = true;
            if (Field.Required && string.IsNullOrEmpty(Convert.ToString(Value)))
            {
                IsValid = false;
                ErrorMessage = SPResource.GetString("MissingRequiredField", new object[0]);
            }
        }
    }
}