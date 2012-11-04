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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoxNewsHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.BoxNewsHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div class="slide_news">
    <div class="bg_top">
    </div>
    <div id="slider">
        <img class="scrollButtons left" src="<%=DocLibUrl%>/leftarrow.png">
        <div style="overflow: hidden;" class="scroll">
            <div class="scrollContainer">
                <asp:Repeater ID="rptNewsSlide" runat="server" OnItemDataBound="rptNewsSlide_ItemDataBound">
                    <ItemTemplate>                    
                        <div class="panel" id="panel_<%=i   %>">
                            <div class="inside">
                                <a id="aImg" runat="server"><img id="imgNews" runat="server" src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" alt="picture" /></a>
                                <a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %> </a>
                            </div>
                        </div>
                </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="left-shadow">
            </div>
            <div id="right-shadow">
            </div>
        </div>
        <img class="scrollButtons right" src="<%=DocLibUrl%>/rightarrow.png">
    </div>
    <div class="bg_bottom">
    </div>
</div>
