using System.IO;
using System.Xml;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonSplitButton : RibbonButton
    {
        public RibbonSplitButton(string id) : base(id)
        {
        }

        public string CommandMenuOpen { get; set; }

        public string MenuAlt { get; set; }

        public bool PopulateDynamically { get; set; }

        public string PopulateQueryCommand { get; set; }

        public bool PopulateOnlyOnce { get; set; }

        public IRibbonCommand MenuCommand { get; set; }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("SplitButton");

            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("Command", Command != null ? Command.Id : "");
            writer.WriteAttributeString("MenuCommand", MenuCommand != null ? MenuCommand.Id : "");
            writer.WriteAttributeString("Sequence", Sequence.ToString());
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("CommandMenuOpen", CommandMenuOpen);
            writer.WriteAttributeString("Image16by16", Image16By16);
            writer.WriteAttributeString("Image16by16Top", Image16By16Top.ToString());
            writer.WriteAttributeString("Image16by16Left", Image16By16Left.ToString());
            writer.WriteAttributeString("Image32by32", Image32By32);
            writer.WriteAttributeString("Image32by32Top", Image32By32Top.ToString());
            writer.WriteAttributeString("Image32by32Left", Image32By32Left.ToString());
            writer.WriteAttributeString("LabelText", LabelText);
            writer.WriteAttributeString("TemplateAlias", TemplateAlias);
            writer.WriteAttributeString("MenuAlt", MenuAlt);
            writer.WriteAttributeString("PopulateQueryCommand", PopulateQueryCommand);
            writer.WriteAttributeString("PopulateDynamically", PopulateDynamically.ToString().ToUpperInvariant());
            writer.WriteAttributeString("PopulateOnlyOnce", PopulateOnlyOnce.ToString().ToUpperInvariant());

            if (!string.IsNullOrEmpty(ToolTipTitle))
            {
                writer.WriteAttributeString("ToolTipTitle", ToolTipTitle);
            }

            if (!string.IsNullOrEmpty(ToolTipDescription))
            {
                writer.WriteAttributeString("ToolTipDescription", ToolTipDescription);
            }

            writer.WriteStartElement("Menu");
            writer.WriteAttributeString("Id", Id + ".Menu");

            writer.WriteStartElement("MenuSection");
            writer.WriteAttributeString("Id", Id + ".Menu.Actions");
            writer.WriteAttributeString("DisplayMode", "Menu32");

            writer.WriteStartElement("Controls");
            writer.WriteAttributeString("Id", Id + ".Menu.Actions.Controls");

            foreach (var control in ChildControls)
            {
                control.WriteXml(writer);
            }

            writer.WriteEndElement(); // Controls
            writer.WriteEndElement(); // MenuSection
            writer.WriteEndElement(); // Menu

            writer.WriteEndElement(); // SplitButton
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
            return xmlDocument.SelectSingleNode("SplitButton");
        }
    }
}
