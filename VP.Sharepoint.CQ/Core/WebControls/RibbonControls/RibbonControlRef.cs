using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonControlRef : IXmlSerializable
    {
        public RibbonDisplayMode DisplayMode { get; set; }
        public string TemplateAlias { get; set; }

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
            writer.WriteStartElement("ControlRef");
            writer.WriteAttributeString("DisplayMode", DisplayMode.ToString());
            writer.WriteAttributeString("TemplateAlias", TemplateAlias);
            writer.WriteEndElement();
        }

        #endregion
    }
}
