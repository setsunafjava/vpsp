using System;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Globalization;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    public static class WebPageHelper
    {
        public static void CreateWebPage(SPWeb web, string fileName, byte[] fileContent)
        {
            web.RootFolder.Files.Add(fileName, fileContent);
        }

        public static void CreateDefaultWebPage(SPWeb web, string fileName, string masterUrl, bool overwrite)
        {
            CreateDefaultWebPage(web, fileName, masterUrl, overwrite, "Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c");
        }

        public static void CreateDefaultWebPage(SPWeb web, string fileName, string masterUrl, bool overwrite, string inherits)
        {
            var exists = web.RootFolder.Files.Cast<SPFile>().Any(file => file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

            if (exists && !overwrite)
            {
                return;
            }

            if (exists)
            {
                var file = web.RootFolder.Files[fileName];
                file.Delete();
            }

            var fileContent = BuildDefaultPageContent(inherits, masterUrl);
            var fileData = Encoding.UTF8.GetBytes(fileContent);
            CreateWebPage(web, fileName, fileData);
        }

        private static string BuildDefaultPageContent(string inherits, string masterUrl)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "<%@ Page language=\"C#\" MasterPageFile=\"" + masterUrl + "\" Inherits=\"{0}\" meta:webpartpageexpansion=\"full\" %>", inherits));
            sb.AppendLine("<%@ Register Tagprefix=\"SharePoint\" Namespace=\"Microsoft.SharePoint.WebControls\" Assembly=\"Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Register Tagprefix=\"Utilities\" Namespace=\"Microsoft.SharePoint.Utilities\" Assembly=\"Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Import Namespace=\"Microsoft.SharePoint\" %>");
            sb.AppendLine("<%@ Assembly Name=\"Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<%@ Register Tagprefix=\"WebPartPages\" Namespace=\"Microsoft.SharePoint.WebPartPages\" Assembly=\"Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderPageTitle\" ContentPlaceHolderId=\"PlaceHolderPageTitle\" runat=\"server\"></asp:Content>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderPageTitleInTitleArea\" ContentPlaceHolderId=\"PlaceHolderPageTitleInTitleArea\" runat=\"server\"></asp:Content>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderPageDescription\" ContentPlaceHolderId=\"PlaceHolderPageDescription\" runat=\"server\"></asp:Content>");
            sb.AppendLine("<asp:Content ID=\"PlaceHolderMain\" ContentPlaceHolderId=\"PlaceHolderMain\" runat=\"server\">");
            sb.AppendLine("<SharePoint:ScriptLink Name=\"SP.UI.Dialog.js\" runat=\"server\" OnDemand=\"true\" Localizable=\"false\" />");
            sb.AppendLine("<SharePoint:ScriptLink Name=\"SP.Ribbon.js\" runat=\"server\" OnDemand=\"true\" Localizable=\"false\" />");
            sb.AppendLine("<div id=\"wrapper\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:HeaderUC\" ID=\"HeaderUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("<div id=\"container_content\">");
            sb.AppendLine("<div class=\"left_content\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:HomeNewsUC\" ID=\"HomeNewsUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("<div class=\"cleaner\"></div>");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:HomeNewsCatUC\" ID=\"HomeNewsCatUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("<div class=\"container_left\">");
            sb.AppendLine("<div class=\"left_corner_COL\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:HomeNewsLeftUC\" ID=\"HomeNewsLeftUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"right_corner_COL\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:HomeNewsRightUC\" ID=\"HomeNewsRightUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("<div class=\"cleaner\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"right_content\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:RightHomeUC\" ID=\"RightHomeUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("<div class=\"cleaner\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"cleaner\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div id=\"footer\">");
            sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:FooterUC\" ID=\"FooterUC\" FrameType=\"None\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</asp:Content>");
            return sb.ToString();
        }

        /// <summary>
        /// Delete a page in root web by file name
        /// </summary>
        /// <param name="web"></param>
        /// <param name="fileName">A file name like Default.aspx</param>
        public static void DeleteWebPage(SPWeb web, string fileName)
        {
            var exists = web.RootFolder.Files.Cast<SPFile>().Any(file => file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                web.RootFolder.Files.Delete(fileName);
            }
        }
    }
}
