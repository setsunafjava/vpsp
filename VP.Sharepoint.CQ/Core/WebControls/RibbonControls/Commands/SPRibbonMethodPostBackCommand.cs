using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class SPRibbonMethodPostBackCommand : SPRibbonCommand
    {
        private readonly object[] eventArgs;
        private readonly IPostBackEventHandler eventHandler;

        public SPRibbonMethodPostBackCommand(string commandId, IPostBackEventHandler eventHandler, Action<object[]> action)
            : this(commandId, eventHandler, action, null, null)
        {
        }

        public SPRibbonMethodPostBackCommand(string commandId, IPostBackEventHandler eventHandler,
                                             Action<object[]> action, string confirmMsg)
            : this(commandId, eventHandler, action, confirmMsg, null)
        {
        }

        public SPRibbonMethodPostBackCommand(string commandId, IPostBackEventHandler eventHandler,
                                             Action<object[]> action, object[] eventArgs)
            : this(commandId, eventHandler, action, null, eventArgs)
        {
        }

        public SPRibbonMethodPostBackCommand(string commandId, IPostBackEventHandler eventHandler,
                                             Action<object[]> action, string confirmMsg, object[] eventArgs) : base(commandId)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException("eventHandler");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.eventHandler = eventHandler;
            ActionMethod = action;
            ConfirmMessage = confirmMsg;
            this.eventArgs = eventArgs;
        }

        public Action<object[]> ActionMethod { get; set; }

        public string ConfirmMessage { get; set; }

        /// <summary>
        /// The function will call before command execute. This function can cancel command by return false.
        /// </summary>
        public string OnClientClick { get; set; }

        public override string HandlerStatement
        {
            get
            {
                var methodName = ActionMethod.Method.Name;
                var control = (Control) eventHandler;
                var argument =
                    SerializePostBackEvent(new RibbonMethodPostBackEventArgs(methodName) {Arguments = eventArgs});

                var handlerStatement = new StringBuilder();
                if (!string.IsNullOrEmpty(OnClientClick))
                {
                    handlerStatement.AppendFormat("var val = {0};", OnClientClick.TrimEnd(';'));
                    handlerStatement.Append("if(val != undefined && !val){return;}");
                }
                
                if (!string.IsNullOrEmpty(ConfirmMessage))
                {
                    handlerStatement.AppendFormat("if(!confirm('{0}')){{ return;}}", ConfirmMessage);
                }

                handlerStatement.Append(control.Page.ClientScript.GetPostBackEventReference(control, argument));
                return handlerStatement.ToString();
            }
            set { base.HandlerStatement = value; }
        }

        private static string SerializePostBackEvent(RibbonMethodPostBackEventArgs arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            var serializer = new DataContractJsonSerializer(typeof (RibbonMethodPostBackEventArgs));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, arguments);
                return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int) stream.Length);
            }
        }
    }
}
