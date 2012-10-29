using System.IO;
using System.Xml;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonToggleButton : RibbonButton
    {
        public RibbonToggleButton(string id) : base(id)
        {
        }

        public string QueryCommand { get; set; }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ToggleButton");

            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("Command", Command != null ? Command.Id : "");
            writer.WriteAttributeString("Sequence", Sequence.ToString());
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("Image16by16", Image16By16);
            writer.WriteAttributeString("Image16by16Top", Image16By16Top.ToString());
            writer.WriteAttributeString("Image16by16Left", Image16By16Left.ToString());
            writer.WriteAttributeString("Image32by32", Image32By32);
            writer.WriteAttributeString("Image32by32Top", Image32By32Top.ToString());
            writer.WriteAttributeString("Image32by32Left", Image32By32Left.ToString());
            writer.WriteAttributeString("LabelText", LabelText);
            writer.WriteAttributeString("TemplateAlias", TemplateAlias);
            writer.WriteAttributeString("QueryCommand", QueryCommand);

            if (!string.IsNullOrEmpty(ToolTipTitle))
            {
                writer.WriteAttributeString("ToolTipTitle", ToolTipTitle);
            }

            if (!string.IsNullOrEmpty(ToolTipDescription))
            {
                writer.WriteAttributeString("ToolTipDescription", ToolTipDescription);
            }

            writer.WriteEndElement();
        }

        public override XmlNode GetXmlDefinition()
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
            return xmlDocument.SelectSingleNode("Button");
        }
    }
}
