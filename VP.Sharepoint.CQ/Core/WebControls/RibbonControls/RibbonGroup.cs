using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonGroup : IXmlSerializable
    {
        public RibbonGroup(string id)
        {
            Id = id;
            Controls = new RibbonControls(id + ".Controls");
            MaxSizes = new List<RibbonMaxSize>();
            Scales = new List<RibbonScale>();
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public int Sequence { get; set; }

        public string Image32By32Popup { get; set; }

        public int Image32By32PopupTop { get; set; }

        public int Image32By32PopupLeft { get; set; }

        public IList<RibbonMaxSize> MaxSizes { get; private set; }

        public IList<RibbonScale> Scales { get; private set; }

        public RibbonGroupTemplate GroupTemplate { get; set; }

        public RibbonControls Controls { get; private set; }

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
            writer.WriteStartElement("Group");

            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("Title", Title);
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("Image32by32Popup", Image32By32Popup);
            writer.WriteAttributeString("Image32by32PopupTop", Image32By32PopupTop.ToString());
            writer.WriteAttributeString("Image32by32PopupLeft", Image32By32PopupLeft.ToString());
            writer.WriteAttributeString("Sequence", Sequence.ToString());
            writer.WriteAttributeString("Template", GroupTemplate.Id);

            Controls.WriteXml(writer);

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
            return xmlDocument.SelectSingleNode("Group");
        }
    }
}
