using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonButton : IRibbonControl
    {
        public RibbonButton(string id)
        {
            Id = id;
            ChildControls = new List<IRibbonControl>();
        }

        public string Description { get; set; }

        public string Image16By16 { get; set; }

        public int Image16By16Top { get; set; }

        public int Image16By16Left { get; set; }

        public string Image32By32 { get; set; }

        public int Image32By32Top { get; set; }

        public int Image32By32Left { get; set; }

        public string ToolTipTitle { get; set; }

        public string ToolTipDescription { get; set; }

        public string MenuItemId { get; set; }

        #region IRibbonControl Members

        public string Id { get; set; }

        public virtual IRibbonCommand Command { get; set; }

        public int Sequence { get; set; }
        public string LabelText { get; set; }

        public string TemplateAlias { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Button");

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

            if (!string.IsNullOrEmpty(TemplateAlias))
            {
                writer.WriteAttributeString("TemplateAlias", TemplateAlias);    
            }
            
            writer.WriteAttributeString("MenuItemId", MenuItemId);

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

        public virtual XmlNode GetXmlDefinition()
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


        public IList<IRibbonControl> ChildControls { get; set; }

        #endregion
    }
}
