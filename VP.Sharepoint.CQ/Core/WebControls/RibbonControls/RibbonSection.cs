using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonSection : IXmlSerializable
    {
        public RibbonSection()
        {
            Alignment = RibbonAlignment.Top;
            Type = RibbonSectionType.OneRow;
            Rows = new List<RibbonRow>();
        }

        public RibbonAlignment Alignment { get; set; }
        public RibbonSectionType Type { get; set; }
        public List<RibbonRow> Rows { get; private set; }

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
            writer.WriteStartElement("Section");
            writer.WriteAttributeString("Alignment", Alignment.ToString());
            writer.WriteAttributeString("Type", Type.ToString());

            foreach (var row in Rows)
            {
                row.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        #endregion
    }
}
