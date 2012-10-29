using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RibbonGroupTemplate : IXmlSerializable
    {
        private readonly bool isInternalTemplate;
        private readonly string templateXml;

        #region Default group templates

        private static RibbonGroupTemplate oneLargeControl;
        private static RibbonGroupTemplate twoLargeControls;
        private static RibbonGroupTemplate threeLargeControls;
        private static RibbonGroupTemplate fourLargeControls;
        private static RibbonGroupTemplate fiveLargeControls;
        private static RibbonGroupTemplate flexible;
        private static RibbonGroupTemplate flexible2;
        private static RibbonGroupTemplate threeRowsThreeMedium;
        private static RibbonGroupTemplate twoRowsFourMedium;
        private static RibbonGroupTemplate manageViewsGroup;

        /// <summary>
        ///   One Large Control
        /// </summary>
        public static RibbonGroupTemplate OneLargeControl
        {
            get
            {
                return oneLargeControl ??
                       (oneLargeControl =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.OneLargeControl",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.OneLargeControl"">
                            <Layout Title=""Large"">
                                <Section Type=""OneRow"">
                                    <Row>
                                        <ControlRef TemplateAlias=""c1"" DisplayMode=""Large"" />
                                    </Row>
                                </Section>
                            </Layout>
                        </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate TwoLargeControls
        {
            get
            {
                return twoLargeControls ??
                       (twoLargeControls =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.TwoLargeControls",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.TwoLargeControls"">
                            <Layout Title=""Large"">
                                <Section Type=""OneRow"">
                                    <Row>
                                        <ControlRef TemplateAlias=""c1"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c2"" DisplayMode=""Large"" />
                                    </Row>
                                </Section>
                            </Layout>
                        </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate ThreeLargeControls
        {
            get
            {
                return threeLargeControls ??
                       (threeLargeControls =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.ThreeLargeControls",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.ThreeLargeControls"">
                            <Layout Title=""Large"">
                                <Section Type=""OneRow"">
                                    <Row>
                                        <ControlRef TemplateAlias=""c1"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c2"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c3"" DisplayMode=""Large"" />
                                    </Row>
                                </Section>
                            </Layout>
                        </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate FourLargeControls
        {
            get
            {
                return fourLargeControls ??
                       (fourLargeControls =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.FourLargeControls",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.FourLargeControls"">
                            <Layout Title=""Large"">
                                <Section Type=""OneRow"">
                                    <Row>
                                        <ControlRef TemplateAlias=""c1"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c2"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c3"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c4"" DisplayMode=""Large"" />
                                    </Row>
                                </Section>
                            </Layout>
                        </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate FiveLargeControls
        {
            get
            {
                return fiveLargeControls ??
                       (fiveLargeControls =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.FiveLargeControls",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.FiveLargeControls"">
                            <Layout Title=""Large"">
                                <Section Type=""OneRow"">
                                    <Row>
                                        <ControlRef TemplateAlias=""c1"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c2"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c3"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c4"" DisplayMode=""Large"" />
                                        <ControlRef TemplateAlias=""c5"" DisplayMode=""Large"" />
                                    </Row>
                                </Section>
                            </Layout>
                        </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate Flexible
        {
            get
            {
                return flexible ??
                       (flexible =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.Flexible",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.Flexible"">
                                <Layout Title=""Large"">
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c1"" DisplayMode=""Large""/>
                                </Layout>
                                <Layout Title=""Medium"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Medium""/>
                                </Layout>
                                <Layout Title=""MediumTwoRow"">
                                    <OverflowSection Type=""TwoRow"" TemplateAlias=""c1"" DisplayMode=""Medium"" />
                                </Layout>
                                <Layout Title=""Small"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Small"" />
                                </Layout>
                                <Layout Title=""Popup"" LayoutTitle=""Large"" />
                            </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate Flexible2
        {
            get
            {
                return flexible2 ??
                       (flexible2 =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.Flexible2",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.Flexible2"">
                                <Layout Title=""LargeLarge"">
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c1"" DisplayMode=""Large""/>
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c2"" DisplayMode=""Large""/>
                                </Layout>
                                <Layout Title=""LargeMedium"">
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c1"" DisplayMode=""Large""/>
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Medium""/>
                                </Layout>
                                <Layout Title=""LargeSmall"">
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c1"" DisplayMode=""Large"" />
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Small"" />
                                </Layout>
                                <Layout Title=""MediumLarge"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Medium"" />
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c2"" DisplayMode=""Large"" />
                                </Layout>
                                <Layout Title=""MediumMedium"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Medium"" />
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Medium"" />
                                </Layout>
                                <Layout Title=""MediumSmall"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Medium"" />
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Small"" />
                                </Layout>
                                <Layout Title=""SmallLarge"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Small"" />
                                    <OverflowSection Type=""OneRow"" TemplateAlias=""c2"" DisplayMode=""Large"" />
                                </Layout>
                                <Layout Title=""SmallMedium"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Small"" />
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Medium"" />
                                </Layout>
                                <Layout Title=""SmallSmall"">
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c1"" DisplayMode=""Small"" />
                                    <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Small"" />
                                </Layout>
                                <Layout Title=""Popup"" LayoutTitle=""LargeMedium"" />
                            </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate TwoRowsFourMedium
        {
            get
            {
                return twoRowsFourMedium ??
                       (twoRowsFourMedium =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.TwoRowsFourMedium",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.TwoRowsFourMedium"">
                                <Layout Title=""Large"">
                                  <Section Type=""TwoRow"">
                                    <Row>
                                      <ControlRef TemplateAlias=""c1"" DisplayMode=""Medium"" />
                                      <ControlRef TemplateAlias=""c2"" DisplayMode=""Medium"" />
                                    </Row>
                                    <Row>
                                      <ControlRef TemplateAlias=""c3"" DisplayMode=""Medium"" />
                                      <ControlRef TemplateAlias=""c4"" DisplayMode=""Medium"" />
                                    </Row>
                                  </Section>
                                </Layout>
                            </GroupTemplate>"));
            }
        }

        public static RibbonGroupTemplate ThreeRowsThreeMedium
        {
            get
            {
                return threeRowsThreeMedium ??
                       (threeRowsThreeMedium =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.ThreeRowsThreeMedium",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.ThreeRowsThreeMedium"">
                                <Layout Title=""Large"">
                                  <Section Type=""ThreeRow"">
                                    <Row>
                                      <ControlRef TemplateAlias=""c1"" DisplayMode=""Medium"" />
                                    </Row>
                                    <Row>
                                      <ControlRef TemplateAlias=""c2"" DisplayMode=""Medium"" />
                                    </Row>
                                    <Row>
                                      <ControlRef TemplateAlias=""c3"" DisplayMode=""Medium"" />
                                    </Row>
                                  </Section>
                                </Layout>
                            </GroupTemplate>"));
            }
        }

        internal static RibbonGroupTemplate ManageViewsGroup
        {
            get
            {
                return manageViewsGroup ??
                       (manageViewsGroup =
                        new RibbonGroupTemplate(
                            "VP.Sharepoint.CQ.Core.Ribbon.Templates.ManageViewsGroup",
                            @"<GroupTemplate Id=""VP.Sharepoint.CQ.Core.Ribbon.Templates.ManageViewsGroup"">
                                <Layout Title=""LargeMedium"">
                                  <OverflowSection Type=""OneRow"" TemplateAlias=""c1"" DisplayMode=""Large""/>
                                  <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Medium""/>
                                  <Section Type=""ThreeRow"">
                                    <Row>
                                      <ControlRef TemplateAlias=""row1control"" DisplayMode=""Medium"" />
                                    </Row>
                                    <Row>
                                      <ControlRef TemplateAlias=""row2control"" DisplayMode=""Medium"" />
                                    </Row>
                                    <Row>
                                      <Strip>            
                                        <ControlRef TemplateAlias=""previousPage"" DisplayMode=""Small"" />
                                        <ControlRef TemplateAlias=""currentPage"" DisplayMode=""Medium"" />
                                        <ControlRef TemplateAlias=""nextPage"" DisplayMode=""Small"" />
                                      </Strip>
                                    </Row>
                                  </Section>
                                 </Layout>
                                <Layout Title=""LargeSmall"">
                                  <OverflowSection Type=""OneRow"" TemplateAlias=""c1"" DisplayMode=""Large""/>
                                  <OverflowSection Type=""ThreeRow"" TemplateAlias=""c2"" DisplayMode=""Small""/>
                                  <Section Type=""ThreeRow"">
	                                <Row>
	                                  <ControlRef TemplateAlias=""row1control"" DisplayMode=""Medium"" />
	                                </Row>
	                                <Row>
	                                  <ControlRef TemplateAlias=""row2control"" DisplayMode=""Medium"" />
	                                </Row>
	                                <Row>
	                                  <Strip>            
		                                <ControlRef TemplateAlias=""previousPage"" DisplayMode=""Small"" />
		                                <ControlRef TemplateAlias=""currentPage"" DisplayMode=""Medium"" />
		                                <ControlRef TemplateAlias=""nextPage"" DisplayMode=""Small"" />
	                                  </Strip>
	                                </Row>
                                  </Section>
                                </Layout>
                                <Layout Title=""Popup"" LayoutTitle=""LargeMedium"" />
                            </GroupTemplate>"));
            }
        }

        #endregion

        public RibbonGroupTemplate(string id)
        {
            Id = id;
            Layouts = new List<RibbonLayout>();
        }

        internal RibbonGroupTemplate(string id, string templateXml)
        {
            Id = id;
            isInternalTemplate = true;
            this.templateXml = templateXml;
        }

        public string Id { get; set; }

        public IList<RibbonLayout> Layouts { get; private set; }

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
            writer.WriteStartElement("GroupTemplate");

            writer.WriteAttributeString("Id", Id);

            foreach (var layout in Layouts)
            {
                layout.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        #endregion

        public XmlNode GetXmlDefinition()
        {
            if (isInternalTemplate)
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(templateXml);
                return xmlDocument.SelectSingleNode("GroupTemplate");
            }
            else
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
                return xmlDocument.SelectSingleNode("GroupTemplate");
            }
        }
    }
}
