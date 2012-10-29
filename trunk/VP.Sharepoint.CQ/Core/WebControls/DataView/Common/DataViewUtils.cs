using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal static class DataViewUtils
    {
        #region Data View

        public static DataTable GetDataTable(SPListItemCollection items, IEnumerable<string> fields)
        {
            var dt = new DataTable();
            Hashtable hashtable = null;

            foreach (SPListItem item in items)
            {
                if (hashtable == null)
                {
                    hashtable = new Hashtable();
                    foreach (var fieldName in fields)
                    {
                        var field = item.Fields.GetFieldByInternalName(fieldName);
                        hashtable.Add(fieldName, field);

                        dt.Columns.Add(fieldName, typeof (object));
                    }
                }

                var row = dt.NewRow();
                foreach (var fieldName in fields)
                {
                    var field = (SPField) hashtable[fieldName];
                    var value = item[field.Title];
                    if (value != null)
                    {
                        switch (field.Type)
                        {
                            case SPFieldType.User:
                                if (value is SPFieldUserValueCollection)
                                {
                                    var users = (SPFieldUserValueCollection) value;
                                    row[fieldName] = string.Join(";#",
                                                                 users.Select(
                                                                     user => user.LookupId + ";#" + user.LookupValue).
                                                                     ToArray());
                                }
                                else
                                {
                                    row[fieldName] = value;
                                }
                                break;
                            case SPFieldType.Calculated:
                                var calculatedField = (SPFieldCalculated) field;
                                var split = value.ToString().Split(new[] {";#"}, StringSplitOptions.None);
                                try
                                {
                                    switch (calculatedField.OutputType)
                                    {
                                        case SPFieldType.Number:
                                        case SPFieldType.Currency:
                                            row[fieldName] = Convert.ToDouble(split[1], CultureInfo.InvariantCulture);
                                            break;
                                        case SPFieldType.DateTime:
                                            row[fieldName] = Convert.ToDateTime(split[1], CultureInfo.InvariantCulture);
                                            break;
                                        case SPFieldType.Boolean:
                                            row[fieldName] = split[1] == "1";
                                            break;
                                        default:
                                            row[fieldName] = split[1];
                                            break;
                                    }
                                }
                                catch (FormatException)
                                {
                                    row[fieldName] = DBNull.Value;
                                }
                                break;
                            default:
                                row[fieldName] = value;
                                break;
                        }
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        public static DataTable GetDataTable(DataTable dataTable, SPList list, IEnumerable<string> fields)
        {
            var dt = new DataTable();
            Hashtable hashtable = null;

            foreach (DataRow row in dataTable.Rows)
            {
                if (hashtable == null)
                {
                    hashtable = new Hashtable();
                    foreach (var fieldName in fields)
                    {
                        var field = list.Fields.GetFieldByInternalName(fieldName);
                        hashtable.Add(fieldName, field);

                        dt.Columns.Add(fieldName, typeof (object));
                    }
                    dt.Columns.Add("ListId", typeof (string));
                }

                var newRow = dt.NewRow();
                foreach (var fieldName in fields)
                {
                    var field = (SPField) hashtable[fieldName];
                    var value = Convert.ToString(row[fieldName]);
                    switch (field.Type)
                    {
                        case SPFieldType.Attachments:
                        case SPFieldType.Boolean:
                            newRow[fieldName] = Convert.ToString(value) == "1";
                            break;
                        case SPFieldType.Number:
                        case SPFieldType.Currency:
                            if (value != string.Empty)
                            {
                                newRow[fieldName] = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                            }
                            break;
                        case SPFieldType.DateTime:
                            if (value != string.Empty)
                            {
                                newRow[fieldName] = DateTime.Parse(value, CultureInfo.InvariantCulture);
                            }
                            break;
                        case SPFieldType.Calculated:
                            var split = value.Split(new[] {";#"}, StringSplitOptions.None);
                            if (split.Length == 2)
                            {
                                var calculatedField = (SPFieldCalculated) field;
                                try
                                {
                                    switch (calculatedField.OutputType)
                                    {
                                        case SPFieldType.Number:
                                        case SPFieldType.Currency:
                                            newRow[fieldName] = Convert.ToDouble(split[1], CultureInfo.InvariantCulture);
                                            break;
                                        case SPFieldType.DateTime:
                                            newRow[fieldName] = Convert.ToDateTime(split[1],
                                                                                   CultureInfo.InvariantCulture);
                                            break;
                                        case SPFieldType.Boolean:
                                            newRow[fieldName] = split[1] == "1";
                                            break;
                                        default:
                                            newRow[fieldName] = split[1];
                                            break;
                                    }
                                }
                                catch (FormatException)
                                {
                                    newRow[fieldName] = DBNull.Value;
                                }
                            }
                            break;
                        default:
                            newRow[fieldName] = value;
                            break;
                    }
                }
                newRow["ListId"] = row["ListId"];
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        public static bool CompareDateObject(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (!(x is DateTime) || !(y is DateTime))
            {
                return false;
            }

            var dt1 = (DateTime) x;
            var dt2 = (DateTime) y;
            return dt1.Date.Equals(dt2.Date);
        }

        public static bool CompareNumberObject(object x, object y, SPNumberFormatTypes numberFormatTypes)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (!(x is double) || !(y is double))
            {
                return x.ToString() == y.ToString();
            }

            string format;
            switch (numberFormatTypes)
            {
                case SPNumberFormatTypes.NoDecimal:
                    format = "N0";
                    break;
                case SPNumberFormatTypes.OneDecimal:
                    format = "N1";
                    break;
                case SPNumberFormatTypes.ThreeDecimals:
                    format = "N3";
                    break;
                case SPNumberFormatTypes.FourDecimals:
                    format = "N4";
                    break;
                case SPNumberFormatTypes.FiveDecimals:
                    format = "N5";
                    break;
                default:
                    format = "N2";
                    break;
            }

            var a = (double) x;
            var b = (double) y;
            return a.ToString(format, CultureInfo.InvariantCulture) == b.ToString(format, CultureInfo.InvariantCulture);
        }

        public static bool CompareBooleanObject(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (!(x is bool) || !(y is bool))
            {
                return false;
            }

            return (bool) x == (bool) y;
        }

        public static bool CompareStringObject(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (!(x is string) || !(y is string))
            {
                return false;
            }
            return string.Equals(x.ToString(), y.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CompareObject(SPFieldUserValue x, object y)
        {
            if (y == null || y is DBNull)
            {
                return false;
            }

            return x.LookupId == Utils.Cast<SPFieldUserValue>(y).LookupId;
        }

        public static bool CompareObject(SPFieldUserValueCollection x, object y)
        {
            if (y == null || y is DBNull)
            {
                return false;
            }

            return x.Sum(p => p.LookupId) == Utils.Cast<SPFieldUserValueCollection>(y).Sum(p => p.LookupId);
        }

        public static bool IsDBNull(DataRow row, string name)
        {
            var value = row[name];
            return value is DBNull;
        }

        public static string TrimStringOverMaxLength(string str)
        {
            const int maxLength = 40;
            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength) + "...";
            }
            return str;
        }

        /// <summary>
        ///   This function encodes special characters, with the exception of: * @ - _ + . /
        /// </summary>
        /// <param name = "str"></param>
        /// <returns></returns>
        public static string Escape(string str)
        {
            const string str2 = "0123456789ABCDEF";
            var length = str.Length;
            var builder = new StringBuilder(length*2);
            var num3 = -1;
            while (++num3 < length)
            {
                var ch = str[num3];
                int num2 = ch;
                if ((((0x41 > num2) || (num2 > 90)) && ((0x61 > num2) || (num2 > 0x7a))) &&
                    ((0x30 > num2) || (num2 > 0x39)))
                {
                    switch (ch)
                    {
                        case '@':
                        case '*':
                        case '_':
                        case '+':
                        case '-':
                        case '.':
                        case '/':
                            goto Label_0125;
                    }
                    builder.Append('%');
                    if (num2 < 0x100)
                    {
                        builder.Append(str2[num2/0x10]);
                        ch = str2[num2%0x10];
                    }
                    else
                    {
                        builder.Append('u');
                        builder.Append(str2[(num2 >> 12)%0x10]);
                        builder.Append(str2[(num2 >> 8)%0x10]);
                        builder.Append(str2[(num2 >> 4)%0x10]);
                        ch = str2[num2%0x10];
                    }
                }
                Label_0125:
                builder.Append(ch);
            }
            return builder.ToString();
        }

        /// <summary>
        ///   The JavaScript unescape() function decodes an encoded string.
        /// </summary>
        /// <param name = "str"></param>
        /// <returns></returns>
        public static string UnEscape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var length = str.Length;
            var builder = new StringBuilder(length);
            var num6 = -1;
            while (++num6 < length)
            {
                var ch = str[num6];
                if (ch == '%')
                {
                    int num2;
                    int num3;
                    int num4;
                    int num5;
                    if (((((num6 + 5) < length) && (str[num6 + 1] == 'u')) &&
                         (((num2 = HexDigit(str[num6 + 2])) != -1) && ((num3 = HexDigit(str[num6 + 3])) != -1))) &&
                        (((num4 = HexDigit(str[num6 + 4])) != -1) && ((num5 = HexDigit(str[num6 + 5])) != -1)))
                    {
                        ch = (char) ((((num2 << 12) + (num3 << 8)) + (num4 << 4)) + num5);
                        num6 += 5;
                    }
                    else if ((((num6 + 2) < length) && ((num2 = HexDigit(str[num6 + 1])) != -1)) &&
                             ((num3 = HexDigit(str[num6 + 2])) != -1))
                    {
                        ch = (char) ((num2 << 4) + num3);
                        num6 += 2;
                    }
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }

        internal static int HexDigit(char c)
        {
            if ((c >= '0') && (c <= '9'))
            {
                return (c - '0');
            }
            if ((c >= 'A') && (c <= 'F'))
            {
                return (('\n' + c) - 0x41);
            }
            if ((c >= 'a') && (c <= 'f'))
            {
                return (('\n' + c) - 0x61);
            }
            return -1;
        }

        #endregion
    }
}
