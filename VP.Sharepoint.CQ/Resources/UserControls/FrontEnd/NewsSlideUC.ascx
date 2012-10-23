<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsSlideUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.NewsSlideUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div class="hot_news-content">
    <div class="artical_hottest">
        <!-------------slide News------------------------>
        <div id="gallery">
            <a href="#" class="show">
                <img src="<%=DocLibUrl%>/flowing-rock.jpg" alt="Flowing Rock" width="580"
                    height="360" title="" alt="" rel="" /><h3>
                        Hai nữ thủ khoa từng... trượt đại học</h3>
                Các bạn ấy cũng đã từng thi trượt đại học rồi sau đó quyết tâm thi lại vào năm sau
                và thi đậu với số điểm cao nhất. "/> </a><a href="#">
                    <img src="<%=DocLibUrl%>/grass-blades.jpg" alt="Grass Blades" width="580"
                        height="360" title="" alt="" rel="" /><h3>Đường tội lỗi của nữ giáo viên xuân sắc</h3>Người đàn bà đẹp được đào tạo ở nước ngoài, đứng trên bục giảng nhiều năm đâu ngờ có ngày lại giam thân trong chốn lao tù không biết ngày được trở về. " />
                </a>
            <div class="caption">
                <div class="content">
                </div>
            </div>
        </div>
        <!-------------End Slide News------------------------>
    </div>
</div>