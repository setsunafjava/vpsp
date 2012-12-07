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
<%@ Control Language="C#" CodeBehind="VanBanUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.VanBanUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<script type="text/javascript">

    //paging_document
    $(document).ready(function () {
        $('#vanbantb2443').tablePagination({});
    });

    function showDocumentDetail(id) {
        var divId = document.getElementById(id);
        if (divId.style.display == "none")
            divId.style.display = "block";
        else
            divId.style.display = "none";
    }
</script>
<div class="sub_page">
    <div class="title_name_content">
        Sở Giáo dục và đào tạo</div>
    <div class="content_follow">
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlCoQuanBanHanh" runat="server" CssClass="input" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLoaiVanBan" runat="server" CssClass="input" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="input" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNguoiKy" runat="server" CssClass="input" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="vanbantb no-arrow rowstyle-alt colstyle-alt paginate-15 max-pages-2"
            id="vanbantb2443" width="100%" style="margin-top: 10px;">
            <thead>
                <tr>
                    <th width="15%">
                        Số/ký hiệu
                    </th>
                    <th>
                        Trích yếu/về việc
                    </th>
                    <th width="15%">
                        Ban hành
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptVanBan" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td valign="top">
                                <asp:Literal ID="ltrDocumentNo" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <a style="font-weight: bold" href="javascript:void(0);" id="aLink" runat="server">
                                    <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                                </a>
                                <asp:Literal ID="ltrDivHead" runat="server"></asp:Literal>
                                <b>Cơ quan ban hành:</b>
                                <asp:Literal ID="ltrCQ" runat="server"></asp:Literal><br />
                                <b>Loại văn bản:</b>
                                <asp:Literal ID="ltrLoaiVB" runat="server"></asp:Literal><br />
                                <b>Lĩnh vực:</b>
                                <asp:Literal ID="ltrLinhVuc" runat="server"></asp:Literal><br />
                                <b>Người ký:</b>
                                <asp:Literal ID="ltrNguoiKy" runat="server"></asp:Literal><br />
                                <b>Ngày hiệu lực:</b>
                                <asp:Literal ID="ltrNgayHieuLuc" runat="server"></asp:Literal><br />
                                <b>Ngày hết hiệu lực:</b>
                                <asp:Literal ID="lblNgayHetHieuLuc" runat="server"></asp:Literal><br />
                                <b>Tải về:</b><asp:ImageButton ID="imgDownload" runat="server" /></<br />
                                <asp:Literal ID="ltrDivBottom" runat="server"></asp:Literal>
                            </td>
                            <td valign="top">
                                <asp:Literal ID="ltrNgayBanHanh" runat="server"></asp:Literal><br />
                            </td>
                        </tr>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div style="clear:both;"></div>
    </div>
</div>
<script type="text/javascript">
    function DownloadFile(fileUrl) {
        window.open(fileUrl, '', 'width:300, height:300');
    }
</script>
