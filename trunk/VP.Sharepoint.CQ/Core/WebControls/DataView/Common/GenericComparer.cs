using System;
using System.Collections.Generic;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class GenericComparer<T> : IComparer<object>
    {
        #region IComparer<object> Members

        public int Compare(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x is DBNull)
            {
                return -1;
            }

            if (y is DBNull)
            {
                return 1;
            }

            if (!(x is T))
            {
                return -1;
            }

            if (!(y is T))
            {
                return 1;
            }

            if (x is IComparable)
            {
                return ((IComparable) x).CompareTo(y);
            }

            return -1;
        }

        #endregion
    }
}
