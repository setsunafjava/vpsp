using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    [Serializable]
    [DataContract]
    public class RibbonMethodPostBackEventArgs
    {
        public RibbonMethodPostBackEventArgs()
        {
        }

        public RibbonMethodPostBackEventArgs(string methodName)
        {
            MethodName = methodName;
        }

        [DataMember(Name = "methodName", IsRequired = true, Order = 1)]
        public string MethodName { get; set; }

        [DataMember(Name = "args", EmitDefaultValue = false, Order = 2)]
        public object[] Arguments { get; set; }

        public static RibbonMethodPostBackEventArgs Deserialize(string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof (RibbonMethodPostBackEventArgs));
                try
                {
                    return serializer.ReadObject(ms) as RibbonMethodPostBackEventArgs;
                }
                catch (SerializationException)
                {
                    return null;
                }
            }
        }
    }
}
