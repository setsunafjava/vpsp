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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FoldersByFolderUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.FoldersByFolderUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="mod_Corner_Right">
    <div class="bg_title_ModNews">
        <div class="title_cate_News">
            <div class="name_F_Right">
                chuyên mục
            </div>
        </div>
        <div class="content_F_Right">
            <ul id="treemenu1" class="treeview">
                <asp:Repeater ID="rptTree" runat="server" OnItemDataBound="rptTree_ItemDataBound">
                    <ItemTemplate>
                        <li><a id="aLink" runat="server">
                            <%#Eval(FieldsName.CategoryList.InternalName.Title) %></a>
                            <ul>
                                <asp:Repeater ID="rptChild1" runat="server" OnItemDataBound="rptChild1_ItemDataBound">
                                    <ItemTemplate>
                                        <li class="submenu"><a id="aLink" runat="server">THPT</a>
                                            <ul style="margin-top: 7px;">
                                                <asp:Repeater ID="rptChild2" runat="server" OnItemDataBound="rptChild2_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li class="submenu"><a id="aLink" runat="server">Khối 10</a>
                                                            <ul style="margin-top: 7px;">
                                                                <asp:Repeater ID="rptChild3" runat="server" OnItemDataBound="rptChild3_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <li><a id="aLink" runat="server">Toán</a> </li>
                                                                        <%--<ul style="margin-top: 7px;">
                                                                                <li><a href="#">Toán</a> </li>
                                                                                <li><a href="#">Lý</a> </li>
                                                                            </ul>--%>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ul>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <%--<asp:Literal ID="ltrSubMenu" runat="server"></asp:Literal>--%>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <%--<li>Tài liệu tham khảo
                    <ul>
                        <li class="submenu">THPT
                            <ul style="margin-top: 7px;">
                                <li class="submenu">Khối 10
                                    <ul style="margin-top: 7px;">
                                        <li><a href="#">Toán</a> </li>
                                        <li><a href="#">Lý</a> </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>--%>
            </ul>
            <script type="text/javascript">

                //ddtreemenu.createTree(treeid, enablepersist, opt_persist_in_days (default is 1))

                ddtreemenu.createTree("treemenu1", true);
                //                ddtreemenu.createTree("treemenu2", false)
						
            </script>
        </div>
    </div>
</div>
