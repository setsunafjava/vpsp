﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="LinkSiteUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.LinkSiteUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="pos_MOD">
    <div class="bg_title_mod" style="height:auto">
        Liên kết website</div>
    <div class="inner_pos_Mod">
        <div class="link_website">
            <asp:ListBox ID="lbWebURL" runat="server" CssClass="txt_s" Rows="5"></asp:ListBox>
        </div>
    </div>
</div>
<script type="text/javascript">
    function RedirectURL(strId) {
        try {            
            var ddl = document.getElementById(strId);            
            var url = ddl.options[ddl.selectedIndex].value;            
            window.open(url, "", "width=1002,height=700,location=1,menubar=1,scrollbars=1,status=1,resizable=1");            
        } catch (e) {
            location.target = "_blank";
            location.href = url;
        }
    }
</script>
<asp:Literal ID="ltrScript" runat="server"></asp:Literal>
