using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonGroups : List<RibbonGroup>, IXmlSerializable
    {
        public RibbonGroups()
        {
        }

        public RibbonGroups(string id)
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
            writer.WriteStartElement("Groups");

            writer.WriteAttributeString("Id", Id);

            foreach (var group in this)
            {
                group.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        #endregion
    }
}
