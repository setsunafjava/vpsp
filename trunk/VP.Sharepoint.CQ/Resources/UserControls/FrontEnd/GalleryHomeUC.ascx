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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleryHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.GalleryHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        Thư viện ảnh</div>
    <div class="inner_pos_Mod">
        <div class="img_adv_right_corner">
            <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" id="imgThumb" runat="server" width="289">
            <div style="text-align: center; padding-top: 5px; color: #0000FF;" id="dvTitle" runat="server">
                Ảnh sân vận động thể thao</div>

            <asp:Repeater ID="rptImg" runat="server" OnItemDataBound="rptImg_ItemDataBound">
                <ItemTemplate>
                    <div><a id="aLink" runat="server" style="cursor:hand"><%#Eval(FieldsName.ImageLibrary.InternalName.Title) %></a></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

<script type="text/javascript">
    function SwitchImage(strSrc) {
        var imgSrc = document.getElementById('<%=imgThumb.ClientID %>');
        imgSrc.src = strSrc;
    }
</script>
