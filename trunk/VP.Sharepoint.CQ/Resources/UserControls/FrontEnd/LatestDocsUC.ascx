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
<%@ Control Language="C#" CodeBehind="LatestDocsUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.LatestDocsUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                Văn bản mới</div>
        </div>
        <div class="content_F_Right">
            <marquee direction="up" scrolldelay="50" scrollamount="1" truespeed="true" onmouseover="this.stop()"
                onmouseout="this.start()" height="200px">
				<ul>
                <asp:Repeater ID="rptDocument" runat="server">
                    <ItemTemplate>
					<li>
					    <a id="aLinkHref" href="javascript:void(0)" runat="server"></a>
					    <span class="time_update" runat="server" id="spDate"></span>
					</li>
                    </ItemTemplate>
                </asp:Repeater>		
				</ul>
			</marquee>
        </div>
    </div>
</div>

<script type="text/javascript">
    function DownloadFile(fileUrl) {
        window.open(fileUrl, '', 'width:300, height:300');
    }
</script>
