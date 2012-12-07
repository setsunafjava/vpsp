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
<%@ Control Language="C#" CodeBehind="FilesByFolderUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.FilesByFolderUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#tainguyen-paging').tablePagination({});
    });
</script>

<div class="sub_page">
    <div class="title_name_content">
        <a href="#">Tài Nguyên</a> &raquo; <a href="#" runat="server" id="aTitle"></a></div>
    <div class="content_follow">
        <table style="margin: 0 auto; width: 655px" id="tainguyen-paging">
            <asp:Repeater ID="rptResources" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="ltrTrUP" runat="server"></asp:Literal>
                    <td>
                        <div class="ico_book">                                
                            <a id="aImg" runat="server">
                                <img id="imgThumb" runat="server" alt="" /></a>
                        </div>
                        <div class="name_document">
                            <a id="aLink" runat="server"></a>
                        </div>
                    </td>
                    <asp:Literal ID="ltrTrDown" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="clear:both;"></div>
    </div>    
</div>
