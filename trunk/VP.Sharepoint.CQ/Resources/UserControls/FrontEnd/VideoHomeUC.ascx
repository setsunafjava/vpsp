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
<%@ Control Language="C#" CodeBehind="VideoHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.VideoHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                <div class="video_R">
                    <a href="javascript:void(0);">Video Clip</a></div>
            </div>
        </div>
        <div class="content_F_Right" style="padding-left:6px;">
            <div id='qn-video-div'>
                <asp:Literal ID="ltrVideo" runat="server"></asp:Literal>
           </div>
           <asp:Repeater ID="rptVideo" runat="server">
                <HeaderTemplate><div class="list_video" style="height:100px; overflow-y:scroll;"><ul></HeaderTemplate>
                <ItemTemplate><li><a runat="server" id="aLink"></a></li></ItemTemplate>
				<FooterTemplate></ul></div></FooterTemplate>
			</asp:Repeater>
        </div>
    </div>
</div>
<script type="text/javascript">
    function setVideoPlay(strID, value) {
        document.getElementById("qn-video-div").innerHTML = value;
    }
</script>
