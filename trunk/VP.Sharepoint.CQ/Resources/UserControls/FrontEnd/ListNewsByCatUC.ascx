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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsByCatUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ListNewsByCatUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="sub_page">
    <div class="title_name_content" runat="server" id="dvCatTitle">
        Thông tin du học</div>
    <div class="content_follow">
        <asp:Repeater ID="rptListNews" runat="server" OnItemDataBound="rptListNews_ItemDataBound">
            <ItemTemplate>
                <div class="news_artical">
                    <div class="name_artical">
                        <a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %></a> 
                        <span class="time_update" id="spDate" runat="server"></span>
                    </div>
                    <div class="img_thumbail_shortcontent">
                        <div class="img_thumbail">
                            <a id="aImg" runat="server"><img id="imgNews" runat="server" src="<%=DocLibUrl%>/17_7_1345027592_43_nu1.jpg" /></div></a>
                        <div class="short_content" id="dvDesc" runat="server">
                            Giáo dục bắt buộc ở Anh bắt đầu vào bậc tiểu học lúc 5 tuổi. Học sinh tiểu học sẽ
                            học từ năm nhất lên đến năm thứ sáu mà không phải qua một kỳ thi nào, tuy nhiên
                            sẽ có cuộc kiểm tra khả năng học sinh khi lên 7 tuổi. Học sinh được chú trọng vào
                            việc học bằng cách tự khám phá hơn là học thuộc lòng.
                        </div>
                        <div class="cleaner">
                        </div>
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
