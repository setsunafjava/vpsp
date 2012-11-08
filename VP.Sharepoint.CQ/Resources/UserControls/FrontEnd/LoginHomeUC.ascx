<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.LoginHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        <div class="member_login">
            Thành viên đăng nhập</div>
    </div>
    <div class="inner_pos_Mod">
        <div class="form_login">
            <div style="margin-top:5px; float:left">Tên đăng nhập</div><input type="text" value="" style="float:right" />
            <div style="clear:both"></div>
            <div style="margin-top:5px; float:left">Mật khẩu</div><input type="password" value="" style="float:right" />
            <div style="clear:both"></div>
        </div>
        <input type="checkbox" class="styled" id="chkbox_remember" value="" name="chkbox_remember"><span>Tự
            dộng đăng nhập</span>
        <input type="button" title="Sign In" class="btn-signin" value="Đăng nhập" name=""
            style="background: #FF3300; border: 0; color: #FFFFFF; font-size: 11px; padding: 3px;">
    </div>
</div>
