<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ListNewsHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="mod_News_external">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_title_typ_News">
                Thông tin du học</div>
        </div>
    </div>
    <div class="content_typ_News">
        <div class="hotnews_test">
            <img src="<%=DocLibUrl%>/images769948_440002_2984_1924_EVE_1.jpg" />
            <h3>
                <a href="#">Mong ngóng ngày về của người nhà ngư dân...</a></h3>
            <span>Mấy ngày nay, bà Phan Thị Ánh, vợ thuyền trưởng Bùi Thu như "ngồi trên đống lửa"
                khi nghe tin chồng, con bị Trung Quốc bắt giữ trong lúc hành nghề đánh cá ở vùng
                biển Hoàng Sa. </span>
            <div class="cleaner">
            </div>
        </div>
        <div class="list_other_news">
            <ul>
                <li><a href="#">Doanh nghiệp vận tải xin giảm phí bảo trì đường </a></li>
                <li><a href="#">Chạy xe làm hỏng đường thì phải trả phí bảo trì'/ Xe máy, ôtô phải đóng
                    phí bảo trì đường bộ từ 1/6</a></li>
                <li><a href="#">Doanh nghiệp vận tải xin giảm phí bảo trì đường </a></li>
            </ul>
        </div>
    </div>
</div>
