using System;
using System.Collections.Generic;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class DateTimeComparer : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (ReferenceEquals(x, null))
                return -1;

            if (ReferenceEquals(y, null))
                return 1;

            if (!(x is DateTime))
            {
                return -1;
            }

            if (!(y is DateTime))
            {
                return 1;
            }

            var dt1 = (DateTime)x;
            var dt2 = (DateTime)y;

            return dt1.CompareTo(dt2);
        }
    }
}
