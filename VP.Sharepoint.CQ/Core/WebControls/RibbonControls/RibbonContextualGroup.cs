using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonContextualGroup : IXmlSerializable
    {
        public RibbonContextualGroup(string id)
        {
            Id = id;
            Tabs = new List<RibbonTab>();
        }

        public string Id { get; set; }

        public int Sequence { get; set; }

        public string Color { get; set; }

        public string Command { get; set; }

        public string ContextualGroupId { get; set; }

        public string Title { get; set; }

        public IList<RibbonTab> Tabs { get; private set; }

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
            writer.WriteStartElement("ContextualGroup");
            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("Title", Title);
            writer.WriteAttributeString("Color", Color);
            writer.WriteAttributeString("Command", Command);
            writer.WriteAttributeString("ContextualGroupId", ContextualGroupId);
            writer.WriteAttributeString("Sequence", Sequence.ToString());

            foreach (var tab in Tabs)
            {
                tab.WriteXml(writer);
            }

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
            return xmlDocument.SelectSingleNode("ContextualGroup");
        }
    }
}
