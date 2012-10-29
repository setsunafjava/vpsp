using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    [Serializable]
    public class RibbonTab : IXmlSerializable
    {
        public RibbonTab(string id)
        {
            Id = id;
            Groups = new RibbonGroups(id + ".Groups");
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Sequence { get; set; }

        public RibbonGroups Groups { get; private set; }

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
            writer.WriteStartElement("Tab");
            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("Title", Title);
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("Sequence", Sequence.ToString());

            // Scaling
            var maxSizes = Groups.SelectMany(item => item.MaxSizes).Where(item => item != null);
            var scales = Groups.SelectMany(item => item.Scales).Where(item => item != null);

            writer.WriteStartElement("Scaling");
            writer.WriteAttributeString("Id", Id + ".Scaling");

            // MaxSize
            foreach (var maxSize in maxSizes)
            {
                maxSize.WriteXml(writer);
            }

            // Scale
            foreach (var scale in scales)
            {
                scale.WriteXml(writer);
            }

            writer.WriteEndElement();

            // Groups
            Groups.WriteXml(writer);

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
            return xmlDocument.SelectSingleNode("Tab");
        }
    }
}
