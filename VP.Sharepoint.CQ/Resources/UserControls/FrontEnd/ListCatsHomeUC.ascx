<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListCatsHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ListCatsHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="pos_MOD">
    <div class="bg_title_mod">Chuyên mục giải trí</div>
    <div class="inner_pos_Mod">
        <div class="inner_list_company_adv">
            <ul>
                <asp:Repeater ID="rptCat" runat="server" OnItemDataBound="rptCat_ItemDataBound">
                    <ItemTemplate>
                        <li><a href="#" id="aLink" runat="server">Văn học </a></li>
                    </ItemTemplate>
                </asp:Repeater>                
                <%--<li><a href="#">Văn học </a></li>
                <li><a href="#">Câu lạc bộ thơ</a></li>
                <li><a href="#">Câu lạc bộ âm nhạc</a></li>
                <li><a href="#">Câu lạc bộ ngoại ngữ</a></li>
                <li><a href="#">truyện cười</a></li>
                <li><a href="#">Câu lạc bộ thời trang</a></li>--%>
            </ul>
        </div>
    </div>
</div>
