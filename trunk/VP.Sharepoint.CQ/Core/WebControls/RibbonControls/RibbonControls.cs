using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonControls : List<IRibbonControl>, IXmlSerializable
    {
        public RibbonControls(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

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
            writer.WriteStartElement("Controls");

            writer.WriteAttributeString("Id", Id);

            foreach (var button in this)
            {
                button.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        #endregion
    }
}
