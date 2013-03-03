<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="VP.Sharepoint.CQ.Common" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="OtherNewsUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.OtherNewsUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#otherNews').pajinate({
            nav_label_first: '<<',
            nav_label_last: '>>',
            nav_label_prev: '<',
            nav_label_next: '>',
            items_per_page: 10,
            show_first_last: false
        });
    });      
</script>
<div class="sub_page">
    <div class="other_news">
        <div class="text_title">
            Các tin khác</div>
        <div id="otherNews">
            <ul class="content-paging">
                <asp:Repeater ID="rptOtherNews" runat="server">
                    <ItemTemplate>
                        <li><a href="#" id="aLink" runat="server">Tại Sao Bạn Chọn Du Học Anh?</a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>            
            <div class="page_navigation">
            </div>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
