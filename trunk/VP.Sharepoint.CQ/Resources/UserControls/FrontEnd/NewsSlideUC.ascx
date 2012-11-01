<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@Import Namespace="VP.Sharepoint.CQ.Common" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsSlideUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.NewsSlideUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<script type="text/javascript" src="<%=DocLibUrl%>/slide_news.js"></script>
<script type="text/javascript" src="<%=DocLibUrl%>/slider.js" charset="utf-8"></script>
<div class="hot_news-content">
    <div class="artical_hottest">
        <!-------------Home News------------------------>
        <div id="gallery">
            <asp:Repeater ID="rptNewsHome" runat="server" OnItemDataBound="rptNewsHome_ItemDataBound">
                <ItemTemplate>
                    <a href="#" class="show" runat="server" id="aImg">
                        <img src="<%=DocLibUrl%>/flowing-rock.jpg" width="580" height="360" runat="server" id="imgNewsHome"
                            title="" alt="" rel="<h3>Hai nữ thủ khoa từng... trượt đại học</h3>Các bạn ấy cũng đã từng thi trượt đại học rồi sau đó quyết tâm thi lại vào năm sau và thi đậu với số điểm cao nhất. " /></a>                    
                </ItemTemplate>
            </asp:Repeater>
            <div class="caption">
                <div class="content">
                </div>
            </div>
        </div>
        <!-------------End Home News------------------------>
    </div>
</div>
<div class="tab_content_News">
    <div class="info_tab_content">
        <ul id="countrytabs" class="shadetabs">
            <li><a href="#" rel="country1" class="selected">Mới nhất</a></li>
            <li><a href="#" rel="country2">Đọc nhiều</a></li>
        </ul>
        <div class="inner_content_tab">
            <div id="country1" class="tabcontent">
                <ul>
                    <asp:Repeater ID="rptMoiNhat" runat="server" OnItemDataBound="rptMoiNhat_ItemDataBound">
                        <ItemTemplate>
                            <li><a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title) %></a><span>(ngày <%#Eval(FieldsName.NewsList.InternalName.PostedDate) %>)</span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div id="country2" class="tabcontent">
                <ul>
                     <asp:Repeater ID="rptDocNhieu" runat="server" OnItemDataBound="rptDocNhieu_ItemDataBound">
                        <ItemTemplate>
                            <li><a id="aLink" runat="server"><%#Eval(FieldsName.NewsList.InternalName.Title)%></a><span>(ngày <%#Eval(FieldsName.NewsList.InternalName.PostedDate) %>)</span></li>                    
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <script type="text/javascript">

                var countries = new ddtabcontent("countrytabs")
                countries.setpersist(true)
                countries.setselectedClassTarget("link") //"link" or "linkparent"
                countries.init()
								
            </script>
            <div class="cleaner">
            </div>
        </div>
    </div>
</div>
