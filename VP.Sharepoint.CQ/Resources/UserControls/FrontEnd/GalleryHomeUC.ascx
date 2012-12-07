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
<%@ Control Language="C#" CodeBehind="GalleryHomeUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.GalleryHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<style type="text/css">
    #slideshowImage
    {        
        position: relative;
        height: 315px;
        width: 289px;
        
    }
    
    #slideshowImage > div
    {
        position: absolute;        
    }
</style>
<div class="pos_MOD">
    <div class="bg_title_mod">
        Thư viện ảnh</div>
    <div class="inner_pos_Mod">
        <div class="img_adv_right_corner">
            <%--<img id="imgThumb" runat="server" width="289" alt="" />--%>
            <div id="slideshowImage">
                <asp:Repeater ID="rptImg" runat="server">
                    <ItemTemplate>
                        <div>
                            <img id="imgThumb" runat="server" width="289" height="289" alt="" />
                            <div style="text-align: center; padding-top: 5px; color: #0000FF;" id="dvTitle" runat="server">
                                Ảnh sân vận động thể thao</div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    $(document).ready(function () {
        $("#slideshowImage > div:gt(0)").hide();
        setInterval(function () {
            $('#slideshowImage > div:first')
    .fadeOut(1000)
    .next()
    .fadeIn(1000)
    .end()
    .appendTo('#slideshowImage');
        }, 3000);
    });
</script>
