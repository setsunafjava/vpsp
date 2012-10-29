using System.Collections.Generic;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class ViewFieldRefEqualityComparer : IEqualityComparer<IViewFieldRef>
    {
        #region IEqualityComparer<IViewFieldRef> Members

        public bool Equals(IViewFieldRef x, IViewFieldRef y)
        {
            return x.FieldName.Equals(y.FieldName);
        }

        public int GetHashCode(IViewFieldRef obj)
        {
            return obj.FieldName.GetHashCode();
        }

        #endregion
    }
}
