using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonRow : IXmlSerializable
    {
        public RibbonRow()
        {
            ControlRefs = new List<RibbonControlRef>();
        }

        public IList<RibbonControlRef> ControlRefs { get; private set; }

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Row");

            foreach (var controlRef in ControlRefs)
            {
                controlRef.WriteXml(writer);    
            }
            
            writer.WriteEndElement();
        }

        #endregion
    }
}
