﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="VP.Sharepoint.CQ.Common" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsCatHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.NewsCatHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="mod_content_News_1">
    <div class="bg_title_ModNews">
        <div class="cate_News_Mod1">
            <asp:Repeater ID="rptCate" runat="server" OnItemDataBound="rptCate_ItemDataBound">
                <ItemTemplate>
                    <div><%#Eval(FieldsName.CategoryList.InternalName.Title) %></div>                
                </ItemTemplate>
            </asp:Repeater>
            <div class="cleaner">
            </div>
        </div>
        <div class="inner_content_ModNews1">
            <div class="cont_News">
                <asp:Literal ID="ltrFirstNews" runat="server"></asp:Literal>
                <%--<div class="img_thumb_News">
                    <img src="<%=DocLibUrl%>/images.jpg" /></div>
                <div class="intro_short_content_News">
                    <a href="#">Hội nghị xúc tiến thương mại...</a>
                </div>--%>
                <div class="list_other_news">
                    <ul>
                        <asp:Repeater ID="rptNews1" runat="server" OnItemDataBound="rptNews1_ItemDataBound">
                            <ItemTemplate>
                                <li><a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>                       
                    </ul>
                </div>
            </div>
            <div class="cont_News">
               <%-- <div class="img_thumb_News">
                    <img src="<%=DocLibUrl%>/images.jpg" /></div>
                <div class="intro_short_content_News">
                    <a href="#">Hội nghị xúc tiến thương mại...</a>
                </div>--%>
                <asp:Literal ID="ltrSecondNews" runat="server"></asp:Literal>
                <div class="list_other_news">
                    <ul>
                        <asp:Repeater ID="rptNews2" runat="server" OnItemDataBound="rptNews2_ItemDataBound">
                            <ItemTemplate>
                                <li><a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>                        
                    </ul>
                </div>
            </div>
            <div class="cont_News">
                <%--<div class="img_thumb_News">
                    <img src="<%=DocLibUrl%>/images.jpg" /></div>
                <div class="intro_short_content_News">
                    <a href="#">Hội nghị xúc tiến thương mại...</a>
                </div>--%>
                <asp:Literal ID="ltrThirdNews" runat="server"></asp:Literal>
                <div class="list_other_news">
                    <ul>
                        <asp:Repeater ID="rptNews3" runat="server" OnItemDataBound="rptNews3_ItemDataBound">
                            <ItemTemplate>
                                <li><a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="cleaner" style="clear:both;">
            </div>
        </div>
    </div>
</div>
