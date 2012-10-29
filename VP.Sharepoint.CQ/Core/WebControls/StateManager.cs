using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class StateManager : WebControl, IPostBackDataHandler
    {
        private readonly Hashtable states;

        public StateManager()
        {
            states = new Hashtable();
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            foreach (var key in postCollection.AllKeys.Where(key => key != null && key.StartsWith(postDataKey)))
            {
                states.Add(key.Replace(postDataKey, string.Empty), postCollection[key]);
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion

        public void AddState(string key, string value)
        {
            states.Add(key, value);
        }

        public string GetState(string key)
        {
            return Convert.ToString(states[key]);
        }

        public void Clear()
        {
            states.Clear();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Page != null)
            {
                Page.RegisterRequiresPostBack(this);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (DictionaryEntry item in states)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + item.Key);
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + item.Key);
                writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(item.Value));
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag(); // input
            }
        }
    }
}
