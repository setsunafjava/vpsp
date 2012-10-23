<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImagesHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ImagesHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="slide_news">
    <div class="bg_top">
    </div>
    <div id="slider">
        <img class="scrollButtons left" src="<%=DocLibUrl%>/leftarrow.png">
        <div style="overflow: hidden;" class="scroll">
            <div class="scrollContainer">
                <div class="panel" id="panel_1">
                    <div class="inside">
                        <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" alt="picture" />
                        <a href="#">Trẻ luyện chữ trước khi vào lớp 1: Vấn đề không ở phụ huynh! </a>
                    </div>
                </div>
                <div class="panel" id="panel_2">
                    <div class="inside">
                        <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" alt="picture" />
                        <a href="#">Trẻ luyện chữ trước khi vào lớp 1: Vấn đề không ở phụ huynh! </a>
                    </div>
                </div>
                <div class="panel" id="panel_3">
                    <div class="inside">
                        <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" alt="picture" />
                        <a href="#">Trẻ luyện chữ trước khi vào lớp 1: Vấn đề không ở phụ huynh! </a>
                    </div>
                </div>
                <div class="panel" id="panel_4">
                    <div class="inside">
                        <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" alt="picture" />
                        <a href="#">Trẻ luyện chữ trước khi vào lớp 1: Vấn đề không ở phụ huynh! </a>
                    </div>
                </div>
                <div class="panel" id="panel_5">
                    <div class="inside">
                        <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" alt="picture" />
                        <a href="#">Trẻ luyện chữ trước khi vào lớp 1: Vấn đề không ở phụ huynh! </a>
                    </div>
                </div>
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
