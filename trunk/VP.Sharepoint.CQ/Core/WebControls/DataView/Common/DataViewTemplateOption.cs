using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class DataViewTemplateOption
    {
        /// <summary>
        ///   Relative template path in web folder
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        ///   The list name to lookup template
        /// </summary>
        public string TemplateList { get; set; }

        /// <summary>
        ///   The template name in list template
        /// </summary>
        /// <remarks>
        ///   The name gets by field Title
        /// </remarks>
        public string TemplateName { get; set; }

        /// <summary>
        ///   The field within list template store template content, example "Content"
        /// </summary>
        public string TemplateContentFieldName { get; set; }

        public override string ToString()
        {
            return string.Format("TemplatePath={0}&TemplateList={1}&TemplateName={2}&TemplateContentFieldName={3}",
                                 SPEncode.UrlEncode(TemplatePath), SPEncode.UrlEncode(TemplateList),
                                 SPEncode.UrlEncode(TemplateName), SPEncode.UrlEncode(TemplateContentFieldName));
        }
    }
}
