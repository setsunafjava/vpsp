using System;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    [Serializable]
    [Obsolete]
    public class RibbonPostBackEventArgs : EventArgs
    {
        public string Id;
        public object[] Args;
    }
}
