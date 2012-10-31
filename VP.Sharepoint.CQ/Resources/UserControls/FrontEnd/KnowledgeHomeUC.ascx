<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.KnowledgeHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="pos_MOD">
    <div class="bg_title_mod">
        Thông tin cần biết</div>
    <div class="inner_pos_Mod">
        <div class="wheather">
            <div class="area">
                <select class="txt_s" style="width: 190px;">
                    <option value="1">Hồ Chí Minh</option>
                    <option value="2">Hà Nội</option>
                    <option value="3">Đà Nẵng</option>
                    <option value="4">Bà Rịa Vũng Tàu</option>
                    <option value="5">Bình Dương</option>
                </select>
            </div>
            <div class="info_wheather">
                <img src="<%=DocLibUrl%>/wheather.jpg"></div>
            <div class="gold_rate">
                Tỷ Giá
            </div>
            <div>
                <img src="<%=DocLibUrl%>/info_rate.jpg" /></div>
            <div class="ball">
                <a href="#">Bóng đá</a>
            </div>
            <div class="resul">
                <a href="#">Kết quả Xổ Số</a>
            </div>
        </div>
    </div>
</div>
