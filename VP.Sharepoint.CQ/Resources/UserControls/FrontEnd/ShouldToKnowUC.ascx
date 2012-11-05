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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShouldToKnowUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ShouldToKnowUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="contact_adv">
    Liên hệ quảng cáo: Hotline 0904 555 888</div>
<div class="info_more">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_title_typ_News">
                Bạn nên biết</div>
            <div class="link_cate_more">
                <ul>
                    <%--<li><a href="#">Thông tin doanh nghiệp</a></li>|
                    <li><a href="#">Tư vấn tiêu dùng</a></li>|
                    <li><a href="#">Nhà hàng - Khách sạn</a></li>
                    <li><a href="#">Tuyển dụng</a></li>
                    <li><a href="#">Mua bán</a></li>--%>
                    <asp:Repeater ID="rptCat" runat="server" OnItemDataBound="rptCat_ItemDataBound">
                        <ItemTemplate>
                            <li><a id="aLink" runat="server"><%#Eval(FieldsName.CategoryList.InternalName.Title) %></a></li>
                            <asp:Literal ID="ltrAdd" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="cleaner">
            </div>
        </div>
        <div class="inner_infoMore">
            <asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
                <ItemTemplate>
                    <div class="P1">
                        <div class="name_P">
                            <a id="aTitle" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %></a>
                            <div class="link_web_P">
                                <a id="aDesc" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Description) %></a></div>
                        </div>
                        <div class="img_short_content">
                            <div class="img_thumb">
                                <a id="aImg" runat="server"><img src="<%=DocLibUrl%>/tulanh_ex.jpg"></a></div>
                            <div class="short_info" id="dvContent" runat="server">
                                <%#Eval(FieldsName.NewsList.InternalName.Content) %>
                            </div>
                            <div class="cleaner">
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
