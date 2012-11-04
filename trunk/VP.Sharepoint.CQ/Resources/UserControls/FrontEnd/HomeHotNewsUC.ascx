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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeHotNewsUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.HomeHotNewsUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                Tin nổi bật
            </div>
        </div>
        <div class="content_F_Right">
            <asp:Repeater ID="rptHotNews" runat="server" OnItemDataBound="rptHotNews_ItemDataBound">
                <ItemTemplate>
                    <div class="line_news">
                        <div class="thumb_img">
                            <img id="imgThumb" runat="server" alt="" width="100" height="78" /></div>
                        <div class="name_news">
                            <a id="aLink" runat="server">
                                <%#Eval(FieldsName.NewsList.InternalName.Title) %></a> <span class="time_update">(Ngày
                                    <%#Convert.ToDateTime(Eval(FieldsName.NewsList.InternalName.PostedDate)).ToString("dd-MM-yyyy") %>)</span>
                        </div>
                        <div class="cleaner">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="read_more">
                <a href="#">&raquo; Xem thêm</a></div>
        </div>
    </div>
</div>
