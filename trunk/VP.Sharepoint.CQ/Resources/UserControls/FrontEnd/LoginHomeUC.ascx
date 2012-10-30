﻿<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
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
            <label>
                Tên đăng nhập</label>
            <input type="text" value="" />
            <label class="pass">
                Mật khẩu</label>
            <input type="text" value="" />
        </div>
        <input type="checkbox" class="styled" id="chkbox_remember" value="" name="chkbox_remember"><span>Tự
            dộng đăng nhập</span>
        <input type="button" title="Sign In" class="btn-signin" value="Đăng nhập" name=""
            style="background: #FF3300; border: 0; color: #FFFFFF; font-size: 11px; padding: 3px;">
    </div>
</div>