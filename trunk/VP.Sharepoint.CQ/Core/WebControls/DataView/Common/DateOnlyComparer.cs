using System;
using System.Collections.Generic;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class DateOnlyComparer : IEqualityComparer<object>
    {
        #region IEqualityComparer<object> Members

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (!(x is DateTime) || !(y is DateTime))
            {
                return false;
            }

            var dt1 = (DateTime) x;
            var dt2 = (DateTime) y;

            return dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day;
        }

        public int GetHashCode(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 0;
            }

            if (!(obj is DateTime))
            {
                return 0;
            }

            var dt = (DateTime) obj;
            return dt.Year + dt.Month + dt.Day;
        }

        #endregion
    }
}
