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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VanBanUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.VanBanUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<script type="text/javascript">
    function showDocumentDetail(id)
    {
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
                    <asp:DropDownList ID="ddlCoQuanBanHanh" runat="server" CssClass="input">
                    </asp:DropDownList>
                </td>
                <td>                    
                    <asp:DropDownList ID="ddlLoaiVanBan" runat="server" CssClass="input">
                    </asp:DropDownList>
                </td>
                <td>                    
                    <asp:DropDownList ID="ddlLinhVuc" runat="server" CssClass="input">
                    </asp:DropDownList>
                </td>
                <td>                    
                    <asp:DropDownList ID="ddlNguoiKy" runat="server" CssClass="input">
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
                <asp:Repeater ID="rptVanBan" runat="server" OnItemDataBound="rptVanBan_ItemDataBound">
                    <ItemTemplate>
                        <tr class="">
                            <td valign="top">
                                <%#Eval(FieldsName.DocumentsList.InternalName.DocumentNo)%>
                            </td>
                            <td>
                                <a style="font-weight: bold" href="javascript:void(0);" id="aLink" runat="server">
                                    <%#Eval(FieldsName.DocumentsList.InternalName.Title)%>
                                </a>
                                <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                                    id="vbId_<%=i%>">
                                    <b>Cơ quan ban hành:</b> <%#Eval(FieldsName.DocumentsList.InternalName.PublishPlace)%><br/>
                                    <b>Loại văn bản:</b> <%#Eval(FieldsName.DocumentsList.InternalName.DocumentType)%><br/>
                                    <b>Lĩnh vực:</b> <%#Eval(FieldsName.DocumentsList.InternalName.DocumentSubject)%><br/>
                                    <b>Người ký:</b> <%#Eval(FieldsName.DocumentsList.InternalName.SignaturePerson)%><br/>
                                    <b>Ngày hiệu lực:</b> <%#Eval(FieldsName.DocumentsList.InternalName.EffectedDate)%><br/>
                                    <b>Người hết hiệu lực:</b> <%#Eval(FieldsName.DocumentsList.InternalName.ExpiredDate)%><br/>
                                </div>
                            </td>
                            <td valign="top">
                                <%#Eval(FieldsName.DocumentsList.InternalName.EffectedDate)%><br/>
                            </td>
                        </tr>                        
                    </ItemTemplate>
                </asp:Repeater>
        </tbody>
        </table>
        <div class="fdtablePaginaterWrap fdtablePaginatorWrapBottom" id="vanbantb2443-fdtablePaginaterWrapBottom">
            <ul id="vanbantb2443-tablePaginaterClone" class="fdtablePaginater">
                <li>
                    <div class="first-page">
                        <span>«</span></div>
                </li>
                <li>
                    <div class="previous-page">
                        <span>‹</span></div>
                </li>
                <li><a href="#" title="Trang 1 trên 9" class="currentPage page-1" id="vanbantb2443-currentPageC">
                    <span>1</span></a></li><li><a href="#" title="Trang tiếp (Trang 2)" class="next-page"
                        id="vanbantb2443-nextPageC"><span>›</span></a></li><li><a href="#" title="Trang cuối (Trang 9)"
                            class="last-page"><span>»</span></a></li></ul>
        </div>
    </div>
</div>
