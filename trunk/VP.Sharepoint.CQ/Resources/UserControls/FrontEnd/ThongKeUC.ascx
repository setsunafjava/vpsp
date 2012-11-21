<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="VP.Sharepoint.CQ.Common" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="ThongKeUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.ThongKeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="sub_page">
    <div class="title_name_content" id="dvCatTitle" runat="server">
        Số liệu tổng quan</div>
    <div class="content_follow">
        <asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>
                <div class="typ_static">
                    <div class="name_static">
                        <a href="#" id="aLink" runat="server"></a></div>
                    <div class="short_intro_static" id="dvDesc" runat="server">
                        </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>       
    </div>
    <table>
        <tbody>
            <tr>
                <td>
                    <a id="ctl00_ctl20_g_dd9bf7d6_ccaf_4e41_b3ec_5e8c21e06dc2_ctl00_lnkPrev">Trước</a>
                </td>
                <td>
                    <span id="ctl00_ctl20_g_dd9bf7d6_ccaf_4e41_b3ec_5e8c21e06dc2_ctl00_lblCurrpage">Trang:
                        1</span>
                </td>
                <td>
                    <a id="ctl00_ctl20_g_dd9bf7d6_ccaf_4e41_b3ec_5e8c21e06dc2_ctl00_lnkNext">Sau</a>
                </td>
            </tr>
        </tbody>
    </table>
</div>
