using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core
{
    /// <summary>
    ///   Contains utility methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        ///   Convert <see cref = "SPFieldUserValueCollection" /> and <see cref = "SPFieldLookupValueCollection" /> object to string.
        /// </summary>
        /// <param name = "obj"></param>
        /// <returns></returns>
        public static string ConvertLookupToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (obj is SPFieldUserValueCollection)
            {
                var users = (SPFieldUserValueCollection) obj;
                return string.Join(";#", users.Select(item => item.LookupId + ";#" + item.LookupValue).ToArray());
            }

            if (obj is SPFieldLookupValueCollection)
            {
                var values = (SPFieldLookupValueCollection) obj;
                return string.Join(";#", values.Select(item => item.LookupId + ";#" + item.LookupValue).ToArray());
            }

            return obj.ToString();
        }

        /// <summary>
        ///   Remove Lookup Id from string or <see cref = "SPFieldUserValueCollection" />, <see cref = "SPFieldLookupValueCollection" />
        /// </summary>
        /// <param name = "obj"></param>
        /// <returns></returns>
        public static string RemoveLookupId(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (obj is SPFieldUserValueCollection)
            {
                var users = (SPFieldUserValueCollection) obj;
                return string.Join(";#", users.Select(item => item.LookupValue).ToArray());
            }

            if (obj is SPFieldLookupValueCollection)
            {
                var values = (SPFieldLookupValueCollection) obj;
                return string.Join(";#", values.Select(item => item.LookupValue).ToArray());
            }

            var split = obj.ToString().Split(new[] {";#"}, StringSplitOptions.None);
            return string.Join(";#", split.Where((item, i) => i%2 != 0).ToArray());
        }

        public static string ConstructQueryString(this NameValueCollection parameters)
        {
            return string.Join("&", (from string name in parameters select String.Concat(name, "=", SPEncode.UrlEncode(parameters[name]))).ToArray());
        }

        public static T Cast<T>(object obj)
        {
            return (T) obj;
        }

        internal static SPList SafeGetList(SPWeb web, Guid listId)
        {
            try
            {
                return web.Lists[listId];
            }
            catch (ArgumentException)
            {
                SPList list = null;
                var webId = web.ID;
                var siteId = web.Site.ID;
                SPSecurity.RunWithElevatedPrivileges(() =>
                                                         {
                                                             using (var site = new SPSite(siteId))
                                                             {
                                                                 using (var thisWeb = site.OpenWeb(webId))
                                                                 {
                                                                     try
                                                                     {
                                                                         list = thisWeb.Lists[listId];
                                                                     }
                                                                     catch(ArgumentException)
                                                                     {
                                                                         list = null;
                                                                     }
                                                                 }
                                                             }
                                                         });
                return list;
            }
        }

        public static string Trim(string value)
        {
            return value == null ? value : value.Trim();
        }

        public static string SerializeBase64(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            var bytes = memoryStream.GetBuffer();
            return bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
        }

        public static T DeserializeBase64<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            var indexOf = value.IndexOf(':');
            var length = Convert.ToInt32(value.Substring(0, indexOf));
            var data = Convert.FromBase64String(value.Substring(indexOf + 1));
            var memoryStream = new MemoryStream(data, 0, length);
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
