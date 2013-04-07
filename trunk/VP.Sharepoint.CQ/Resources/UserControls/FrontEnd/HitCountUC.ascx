<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="HitCountUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.HitCountUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<style type="text/css">
    .title_satistic
    {
        margin-bottom: 10px;
    }
    .detail_counter
    {
        margin-top: 10px;
    }
    .detail_counter div
    {
        padding-top: 2px;
        padding-bottom: 2px;
    }
    .detail_counter div .number_online
    {
        padding-left: 60px;
    }
</style>
<div class="statistic">
    <div class="mod_Corner_Right">
        <div class="bg_title_ModNews">
            <div class="title_cate_News">
                <div class="name_F_Right">
                    Thống kê truy cập
                </div>
            </div>
        </div>
        <div style="text-align:center;margin-top:10px" id="divCurrentHit"></div>
        <div style="position:absolute;left:-10000px;"><asp:Literal ID="lblCurrent" runat="server"></asp:Literal></div>
        <table width="100%">
            <tr>
                <td width="60%" style="padding-left:60px;"><img src="/ResourcesList/icon1.jpg" />Hôm nay</td><td runat="server" id="tdToday">15893</td>
            </tr>
            <tr>
                <td style="padding-left:60px;"><img src="/ResourcesList/icon2.jpg" />Hôm qua</td><td runat="server" id="tdYesterday">15893</td>
            </tr>
            <tr>
                <td style="padding-left:60px;"><img src="/ResourcesList/icon3.jpg" />Tuần này</td><td runat="server" id="tdThisWeek">15893</td>
            </tr>
            <tr>
                <td style="padding-left:60px;"><img src="/ResourcesList/icon4.jpg" />Tháng này</td><td runat="server" id="tdThisMonth">15893</td>
            </tr>
            <tr>
                <td style="padding-left:60px;"><img src="/ResourcesList/icon5.jpg" />Tất cả</td><td runat="server" id="tdAll">15893</td>
            </tr>
        </table>
    </div>
</div>
<script type="text/javascript">
    var currentHit = document.getElementById("spCurrent").innerText.toString();
    //currentHit = "123";
    if (currentHit.length >= 5) {
        for (var i = 0; i < currentHit.length; i++) {
            document.getElementById("divCurrentHit").innerHTML += "<img src='/ResourcesList/" + currentHit[i] + ".png' alt='" + currentHit + "' />";
        }
    }
    else {
        for (var i = 0; i < 5 - currentHit.length; i++) {
            document.getElementById("divCurrentHit").innerHTML += "<img src='/ResourcesList/0.png' alt='" + currentHit + "' />";
        }
        for (var i = 0; i < currentHit.length; i++) {
            document.getElementById("divCurrentHit").innerHTML += "<img src='/ResourcesList/" + currentHit[i] + ".png' alt='" + currentHit + "' />";
        }
    }
</script>
