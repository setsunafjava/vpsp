<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileDetailUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.FileDetailUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="left_content">
    <div class="sub_page">
        <div class="title_name_content">
            <a href="#">Tài Nguyên</a> &raquo; <a href="#">Tài liệu tham khảo</a></div>
        <div class="content_follow">
            <div>
                <div class="content_detail_doccument">
                    <h2>
                        <%=title %></h2>
                    <p>
                        Tác giả: <%=author %><br />
                        Kích thước: <%=sizeOfFile %> MB<br />
                        Ngày gửi: <%=postedDate %><br />
                        Lượt tải: <%=downloadCount %><br />
                        Đường dẫn: <a href="#" onclick="DownloadFile()"><%=fileName %></a><span><a href="#" onclick="DownloadFile()">
                        <img src="<%=DocLibUrl%>/images_download.jpg" style="width: 100px;border:1px border-color:#cfcfcf; padding:1px;" /></a></span><br />                        
                        <br />
                    </p>
                </div>
                <div class="img_doccument">
                    <img src="<%=imgThumb%>" /></div>
                <div class="cleaner">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:Literal ID="ltrScript" runat="server"></asp:Literal>
<script type="text/javascript">
    function DownloadFile() {
        window.open('<%=urlDownload %>', '', 'width:300, height:300');
        __doPostBack("", "UpdateDownloadCount");        
    }
</script>
