using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonLabel : IRibbonControl
    {
        public RibbonLabel(string id)
        {
            Id = id;
            ChildControls = new List<IRibbonControl>();
        }

        public string ForId { get; set; }

        #region IRibbonControl Members

        public string Id { get; set; }

        public IRibbonCommand Command { get; set; }

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
            writer.WriteStartElement("Label");

            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("Command", Command != null ? Command.Id : "");
            writer.WriteAttributeString("Sequence", Sequence.ToString());
            writer.WriteAttributeString("LabelText", LabelText);
            writer.WriteAttributeString("TemplateAlias", TemplateAlias);
            writer.WriteAttributeString("ForId", ForId);

            writer.WriteEndElement();
        }


        public int Sequence { get; set; }

        public string TemplateAlias { get; set; }

        public string LabelText { get; set; }

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
            return xmlDocument.SelectSingleNode("Label");
        }

        public IList<IRibbonControl> ChildControls { get; set; }

        #endregion
    }
}
