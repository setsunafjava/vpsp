using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class NumberField : Microsoft.SharePoint.WebControls.NumberField
    {
        /// <summary>
        ///   Gets or sets the custom number format when display.
        /// </summary>
        public string NumberFormat { get; set; }

        /// <summary>
        /// Override Render field for Display
        /// </summary>
        /// <param name="writer">HtmlTextWriter obj</param>
        protected override void RenderFieldForDisplay(HtmlTextWriter writer)
        {
            if (SPContext.Current.FormContext.FormMode == SPControlMode.New)
            {
                writer.Write("&nbsp;");
            }
            else
            {
                if (string.IsNullOrEmpty(NumberFormat))
                {
                    base.RenderFieldForDisplay(writer);
                }
                else
                {
                    var value = ItemFieldValue;
                    writer.Write(value == null ? "&nbsp;" : Convert.ToDouble(value).ToString(NumberFormat));
                }
            }
        }

        /// <summary>
        /// Override Render field for Input
        /// </summary>
        /// <param name="writer">HtmlTextWriter obj</param>
        protected override void RenderFieldForInput(HtmlTextWriter output)
        {
            output.AddAttribute("class", "ms-input ms-customNumber");
            base.RenderFieldForInput(output);

            RenderClass(output);
        }

        /// <summary>
        /// Override OnPreRender
        /// </summary>
        /// <param name="e">EventArgs event</param>
        protected override void OnPreRender(EventArgs e)
        {

            if (SPContext.Current.FormContext.FormMode != SPControlMode.Display)
            {
                string formatValue = @"function addCommas(nStr) {
                                                                    nStr += '';
                                                                    x = nStr.split('.');
                                                                    x1 = x[0];
                                                                    x2 = x.length > 1 ? '.' + x[1] : '';
                                                                    var rgx = /(\d+)(\d{3})/;
                                                                    while (rgx.test(x1)) {
                                                                        x1 = x1.replace(rgx, '$1' + ',' + '$2');
                                                                    }
                                                                    return x1 + x2;
                                                                }";
                string onblur = @"function FormatNumber(t) {
                                                                t.value = addCommas(t.value.replace(/,/g,''));
                                                           }";

                string registerFunction = @"$(document).ready(function () { document.getElementById('" + this.ClientID + "_ctl00_TextField').onblur = function () { FormatNumber(this); }; });";

                string script = formatValue + onblur;

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "number_custom", script, true);

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID, registerFunction, true);
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Render Custom CSS
        /// </summary>
        /// <param name="writer">HtmlTextWriter obj</param>
        private void RenderClass(HtmlTextWriter writer)
        {
            string customClass = @".ms-customNumber 
                                    { 
                                        text-align:right;
                                    }";

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            writer.RenderBeginTag(HtmlTextWriterTag.Style);
            writer.Write(customClass);
            writer.RenderEndTag();
        }
    }
}