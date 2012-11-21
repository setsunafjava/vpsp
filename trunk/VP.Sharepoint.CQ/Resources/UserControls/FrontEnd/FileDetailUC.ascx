<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="FileDetailUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.FileDetailUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="left_content">
    <div class="sub_page">
        <div class="title_name_content">
            <a href="#">Tài Nguyên</a> &raquo; <a id="aTitle" runat="server"></a></div>
        <div class="content_follow">
            <div>
                <div class="content_detail_doccument">
                    <h2>
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h2>
                    <p>
                        Tác giả: <asp:Literal ID="ltrAuthor" runat="server"></asp:Literal><br />
                        Kích thước: <asp:Literal ID="ltrSize" runat="server"></asp:Literal> MB<br />
                        Ngày gửi: <asp:Literal ID="ltrDate" runat="server"></asp:Literal><br />
                        Lượt tải: <asp:Literal ID="ltrDownloadCount" runat="server"></asp:Literal><br />
                        Tên file: <a href="#"><asp:Literal ID="ltrFileUrl" runat="server"></asp:Literal></a><span><a href="javascript:void(0)" id="aDownload" runat="server">
                        <asp:ImageButton  runat="server" id="ibDownloadFile" Width="100" /></a></span><br />                        
                        <br />
                    </p>
                </div>
                <div class="img_doccument">
                    <img id="imgAnh" runat="server" /></div>
                <div class="cleaner">
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function FileDetailDownload(fileUrl) {
        window.open(fileUrl, '', 'width:300, height:300');        
    }
</script>
