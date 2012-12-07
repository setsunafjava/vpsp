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
<%@ Control Language="C#" CodeBehind="HotNewsByCatUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.HotNewsByCatUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('#homehotnewsuc').pajinate({
            nav_label_first: '<<',
            nav_label_last: '>>',
            nav_label_prev: '<',
            nav_label_next: '>',
            items_per_page: 10,
            num_page_links_to_display: 3,
            show_first_last: false
        });
    });      
</script>

<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                Tin nổi bật
            </div>
        </div>
        <div class="content_F_Right" id="homehotnewsuc">
        <div class="content-paging">
            <asp:Repeater ID="rptHotNews" runat="server">
                <ItemTemplate>
                    <div class="line_news">
                        <div class="thumb_img">
                            <img id="imgThumb" runat="server" alt="" width="100" height="78" /></div>
                        <div class="name_news">
                            <a id="aLink" runat="server">
                                </a> <span class="time_update"></span>
                        </div>
                        <div class="cleaner">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>            
            </div>
            <div class="page_navigation"></div>
            <div class="cleaner"></div>
        </div>
    </div>
</div>
