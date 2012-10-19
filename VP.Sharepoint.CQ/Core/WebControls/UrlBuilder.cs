namespace VP.Sharepoint.CQ.Core.WebControls
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Globalization;

    public class UrlBuilder
    {
        private readonly Hashtable keyValues;
        private readonly string path;

        public UrlBuilder(Uri uri) : this()
        {
            this.path = uri.GetLeftPart(UriPartial.Path);
            this.AppendQueryString(uri.Query);
        }

        public UrlBuilder(string path) : this()
        {
            var indexOf = path.IndexOf('?');
            if (indexOf > -1)
            {
                this.path = path.Substring(0, indexOf);
                this.AppendQueryString(path.Substring(indexOf + 1));    
            }
            else
            {
                this.path = path;
            }
        }

        private UrlBuilder()
        {
            this.keyValues = new Hashtable(StringComparer.OrdinalIgnoreCase);
        }

        public void AppendQueryString(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return;
            }

            var queryStrings = queryString.TrimStart('?').Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in queryStrings)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                var split = str.Split('=');
                var value = split.Length == 2 ? HttpUtility.UrlDecode(split[1]) : string.Empty;
                this.keyValues[split[0]] = value;
            }
        }

        public void AddQueryString(string key, string value)
        {
            this.keyValues[key] = value;
        }

        public void RemoveQueryString(string key)
        {
            this.keyValues.Remove(key);
        }

        public void ClearQueryString()
        {
            this.keyValues.Clear();
        }

        public T GetQueryStringValue<T>(string key)
        {
            var value = this.keyValues[key];
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return default(T);
            }

            try
            {
                return (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture);
            }
            catch(FormatException)
            {
                return default(T);
            }
        }

        internal string GetUrlWithoutFilterValue(string fieldName)
        {
            var hashtable = (Hashtable)this.keyValues.Clone();

            var key = (from entry in hashtable.Cast<DictionaryEntry>().Where(entry => String.Equals(Convert.ToString(entry.Value, CultureInfo.InvariantCulture), fieldName, StringComparison.OrdinalIgnoreCase))
                          where entry.Key.ToString().StartsWith("FilterField", StringComparison.OrdinalIgnoreCase)
                          select entry.Key.ToString()).FirstOrDefault();

            if (!string.IsNullOrEmpty(key))
            {
                var surfix = key.Replace("FilterField", string.Empty);
                hashtable.Remove(key);
                hashtable.Remove("FilterValue" + surfix);
            }

            hashtable.Remove("PageFirstRow");
            hashtable.Remove("Paged");
            hashtable["FilterClear"] = "1";

            return ToString(this.path, hashtable);
        }

        internal void RemoveAllFilterQueryString()
        {
            var keys = this.keyValues.Keys.Cast<string>().Where(k => k.StartsWith("FilterField", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var key in keys)
            {
                var surfix = key.Replace("FilterField", string.Empty);
                this.keyValues.Remove(key);
                this.keyValues.Remove("FilterValue" + surfix);
            }
        }

        internal void RemoveAllSortQueryString()
        {
            var keys = this.keyValues.Keys.Cast<string>().Where(k => k.StartsWith("p_", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var key in keys)
            {
                this.keyValues.Remove(key);
            }
            this.keyValues.Remove("PageFirstRow");
        }

        public override string ToString()
        {
            return ToString(path, this.keyValues);
        }

        private static string ToString(string urlPath, Hashtable queryStrings)
        {
            var sb = new StringBuilder();
            sb.Append(urlPath);

            if (queryStrings.Count > 0)
            {
                var hasQueryString = false;
                sb.Append("?");
                foreach (DictionaryEntry entry in queryStrings)
                {
                    if (hasQueryString)
                    {
                        sb.Append("&");
                    }

                    sb.Append(entry.Key);
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(Convert.ToString(entry.Value, CultureInfo.InvariantCulture)));
                    hasQueryString = true;
                }
            }

            return sb.ToString();
        }
    }
}