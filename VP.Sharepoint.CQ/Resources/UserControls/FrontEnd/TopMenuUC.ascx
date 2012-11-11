<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.TopMenuUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="top_menu">
    <div class="menu">
        <ul id="nav">
            <li><a href='<%=HomeUrl%>' <%=CurrentStyle%>>Trang chủ</a></li>
            <asp:Repeater ID="rptMenu" runat="server" 
                onitemdatabound="rptMenu_ItemDataBound">
                <ItemTemplate>
                    <li <asp:Literal ID="ltrStyle" runat="server"></asp:Literal>><a runat="server" id="aLink"></a>
                        <asp:Repeater ID="rptSubMenu" runat="server">
                            <HeaderTemplate><ul></HeaderTemplate>
                            <ItemTemplate>
                                <li><a runat="server" id="aLink"></a></li>
                            </ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                        </asp:Repeater>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="search">
        <input type="text" id="txtData" name="q" onkeypress="return BBEnterPress();" style="border: 0px;" />
        <a href="#">Tìm kiếm</a>
    </div>
    <%--<div class="language">
        <span>
            <img src="<%=DocLibUrl%>/english.jpg" /></span><span><a href="#">English</a></span>
    </div>--%>
    <div class="cleaner">
    </div>
</div>
<!-------------End top menu------------------------>
<div class="bg_bottom_top_menu">
    <div class="inner_content_bottom_topMenu">
        <div class="time_date">
            Hôm nay, ngày 22/02/2012 10:33:55 AM</div>
        <div class="set_hompage">
            <a href="#">Đặt làm trang chủ</a></div>
        <div class="RSS">
            <a href="#">RSS</a></div>
        <div class="cleaner">
        </div>
    </div>
</div>

<script type="text/javascript">
    $('#nav > li').hover(onOver, onOut);
    function onOver() {
        $('#nav > li.current').each(function (index) {
            $(this).removeClass('current').addClass('current-temp');
        });
    };

    function onOut() {
        $('#nav > li.current-temp').each(function (index) {
            $(this).removeClass('current-temp').addClass('current');
        });
    };

    function urlencode(str) {



        var ret = str;



        ret = ret.toString();



        ret = encodeURIComponent(ret);



        ret = ret.replace(/%20/g, '+');



        ret = ret.replace(/%22/g, "");

        ret = ret.replace(/\'/g, "");

        ret = ret.replace(/%2F/g, "");

        ret = ret.replace(/%3C/g, "");

        ret = ret.replace(/%3E/g, "");

        ret = ret.replace(/%3F/g, "");

        ret = ret.replace(/%25/g, "");

        ret = ret.replace(/\*/g, "");

        ret = ret.replace(/%7C/g, "");



        return ret;

    }



    function timkiem() {

        var link;

        var tk = document.getElementById("txtData").value;

        if (tk == "") {

            link = "TimKiem.aspx?KeyWord=" + urlencode(tk);
        }

        else {

            link = "TimKiem.aspx?KeyWord=" + urlencode(tk);
        }

        //alert(link);

        location.href = link;

    }

    function ganValue(t) {

        document.getElementById("txtData").value = t;

    }
</script>