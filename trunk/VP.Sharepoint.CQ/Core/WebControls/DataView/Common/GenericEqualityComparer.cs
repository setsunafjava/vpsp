using System;
using System.Collections.Generic;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class GenericEqualityComparer : IEqualityComparer<object>
    {
        #region IEqualityComparer<object> Members

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is DBNull || y is DBNull)
            {
                return false;
            }

            if (x is DateTime && y is DateTime)
            {
                var dt1 = (DateTime) x;
                var dt2 = (DateTime) y;

                return dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return 0;
            }

            if (obj is DateTime)
            {
                var dt = (DateTime) obj;
                return dt.Year + dt.Month + dt.Day;
            }

            return obj.GetHashCode();
        }

        #endregion
    }
}
