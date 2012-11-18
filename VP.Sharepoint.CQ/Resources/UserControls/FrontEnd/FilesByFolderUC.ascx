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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilesByFolderUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.FilesByFolderUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="sub_page">
    <div class="title_name_content">
        <a href="#">Tài Nguyên</a> &raquo; <a href="#" runat="server" id="aTitle"></a></div>
    <div class="content_follow">
        <table style="margin: 0 auto; width: 655px">
            <asp:Repeater ID="rptResources" runat="server" OnItemDataBound="rptResources_ItemDataBound">
                <ItemTemplate>
                    <% if (i % 5 == 0)
                       { %>
                    <tr>
                        <% } %>
                        <td>
                            <div class="ico_book">                                
                                <a id="aImg" runat="server">
                                    <img src="<%=DocLibUrl%>/ico_book1.gif" id="imgThumb" runat="server" /></a>
                            </div>
                            <div class="name_document">
                                <a id="aLink" runat="server">
                                    <%#Eval(FieldsName.ResourceLibrary.InternalName.Title) %></a>
                            </div>
                        </td>
                        <% if (i>0&&i % 5 == 0)
                           { %>
                    </tr>
                    <%  } %>
                    <%i++; %>
                </ItemTemplate>
            </asp:Repeater>
        </table>
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
