using System.Collections.Generic;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    internal class RibbonGroupTemplateComparer : IEqualityComparer<RibbonGroupTemplate>
    {
        #region IEqualityComparer<GroupTemplateElement> Members

        public bool Equals(RibbonGroupTemplate x, RibbonGroupTemplate y)
        {
            //Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            //Check whether the ribbon command id are equal.
            return x.Id == y.Id;
        }

        public int GetHashCode(RibbonGroupTemplate obj)
        {
            //Check whether the object is null
            return ReferenceEquals(obj, null) ? 0 : obj.Id.GetHashCode();
        }

        #endregion
    }
}
