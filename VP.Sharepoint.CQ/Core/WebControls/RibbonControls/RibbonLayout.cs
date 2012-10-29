using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonLayout : IXmlSerializable
    {
        public RibbonLayout()
        {
            Sections = new List<RibbonSection>();
        }

        public string Title { get; set; }
        public string LayoutTitle { get; set; }
        public IList<RibbonSection> Sections { get; private set; }

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
            writer.WriteStartElement("Layout");

            writer.WriteAttributeString("Title", Title);
            writer.WriteAttributeString("LayoutTitle", LayoutTitle);

            foreach (var section in Sections)
            {
                section.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        #endregion
    }
}
