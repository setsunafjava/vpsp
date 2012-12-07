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
<%@ Control Language="C#" CodeBehind="ThongKeUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.ThongKeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#thongkeuc').pajinate({
            nav_label_first: '<<',
            nav_label_last: '>>',
            nav_label_prev: '<',
            nav_label_next: '>',
            items_per_page: 10,
            show_first_last: true
        });
    });      
</script>
<div class="sub_page">
    <div class="title_name_content" id="dvCatTitle" runat="server">
        Số liệu tổng quan</div>
    <div class="content_follow" id="thongkeuc">
        <div class="content-paging">
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <div class="typ_static">
                        <div class="name_static">
                            <a href="#" id="aLink" runat="server"></a>
                        </div>
                        <div class="short_intro_static" id="dvDesc" runat="server">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="info_text">
        </div>
        <div class="page_navigation">
        </div>
        <div class="cleaner">
        </div>
        </div>
</div>
