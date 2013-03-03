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
<%@ Control Language="C#" CodeBehind="BoxNewsHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.BoxNewsHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div class="slide_news">
    <div class="bg_top">
    </div>
    <div id="slider">
        <img class="scrollButtons left" runat="server" id="imgSlide" alt="" />
        <div style="overflow: hidden;" class="scroll">
            <div class="scrollContainer">
                <asp:Repeater ID="rptNewsSlide" runat="server">
                    <ItemTemplate>                    
                       <asp:Literal ID="ltrDivHead" runat="server"></asp:Literal>                       
                            <div class="inside">
                                <a id="aImg" runat="server"><img id="imgNews" runat="server"  alt="picture"  /></a>
                                <a id="aLink" runat="server"></a>
                            </div>
                       <asp:Literal ID="ltrDivBottom" runat="server"></asp:Literal>
                </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="left-shadow">
            </div>
            <div id="right-shadow">
            </div>
        </div>
        <img class="scrollButtons right" runat="server" id="imgSlideR" alt="" />
    </div>
    <div class="bg_bottom">
    </div>
</div>
