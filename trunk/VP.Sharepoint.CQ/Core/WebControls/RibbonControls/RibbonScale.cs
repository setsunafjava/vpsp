using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonScale : IXmlSerializable
    {
        public RibbonScale(string id)
        {
            Id = id;
            Size = RibbonSize.OneLargeTwoMedium;
        }

        public string Id { get; set; }
        public RibbonGroup Group { get; set; }
        public RibbonSize Size { get; set; }
        public int Sequence { get; set; }

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
            writer.WriteStartElement("Scale");

            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("GroupId", Group.Id);
            writer.WriteAttributeString("Size", Size.ToString());
            writer.WriteAttributeString("Sequence", Sequence.ToString());

            writer.WriteEndElement();
        }

        #endregion

        public XmlNode GetXmlDefinition()
        {
            var ms = new MemoryStream();
            var xmlWriter = XmlWriter.Create(ms);
            xmlWriter.WriteStartDocument();
            WriteXml(xmlWriter);
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            ms.Position = 0;
            ms.Seek(0, SeekOrigin.Begin);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(ms);
            return xmlDocument.SelectSingleNode("Scale");
        }
    }
}
