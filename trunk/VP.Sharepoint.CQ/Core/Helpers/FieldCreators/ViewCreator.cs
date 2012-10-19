using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using System;
using System.Globalization;
using VP.Sharepoint.CQ.Core.Helpers;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public class ViewCreator
    {
        public ViewCreator(string name)
        {
            Name = name;
            RowLimit = 30;
            ViewFields = new StringCollection();
        }

        /// <summary>
        ///   A string that contains the name of the view.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   A collection that contains the internal names of the view fields.
        /// </summary>
        public StringCollection ViewFields { get; private set; }

        /// <summary>
        ///   A Collaborative Application Markup Language (CAML) string that contains the Where clause for the query.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        ///   The maximum number of items to return in the view.
        /// </summary>
        public uint RowLimit { get; set; }

        /// <summary>
        ///   True to make the view the default view; otherwise, False.
        /// </summary>
        public bool MakeViewDefault { get; set; }

        /// <summary>
        /// The relative user control path to replace default view display.
        /// </summary>
        public string UserControlPath { get; set; }

        internal SPView Apply(SPList list)
        {
            SPView view;
            try
            {
                view = list.Views[Name];
            }
            catch (Exception ex)
            {
                var viewFields = new StringCollection();
                foreach (var viewField in ViewFields)
                {
                    viewFields.Add(GetInternalFieldName(list, viewField));
                }

                var camlQuery = TransformCamlQuery(list, Query);

                view = list.Views.Add(Name, viewFields, camlQuery, RowLimit, true, MakeViewDefault);

                if (!string.IsNullOrEmpty(UserControlPath))
                {
                    var web = list.ParentWeb;
                    WebPartHelper.HideDefaultWebPartOnView(web, view);
                    var containerWebPart = WebPartHelper.GetContainerWebPart(web);
                    containerWebPart.Title = string.Format(CultureInfo.InvariantCulture, "{0} - Custom View", Name);
                    containerWebPart.UserControlPath = UserControlPath;
                    WebPartHelper.AddWebPartToViewPage(web, view, containerWebPart);
                }                
            }

            return view;
        }

        private static string TransformCamlQuery(SPList list, string camlQuery)
        {
            if (string.IsNullOrEmpty(camlQuery))
            {
                return camlQuery;
            }

            var builder = new StringBuilder(camlQuery);

            var regex = new Regex("[[][^]]+[]]");
            var matchResults = regex.Match(camlQuery);
            while (matchResults.Success)
            {
                var fieldName = matchResults.Value.TrimStart('[').TrimEnd(']');
                builder.Replace(matchResults.Value, GetInternalFieldName(list, fieldName));
                matchResults = matchResults.NextMatch();
            }
            return builder.ToString();
        }

        private static string GetInternalFieldName(SPList list, string fieldName)
        {
            return list.Fields[fieldName].InternalName;
        }
    }
}