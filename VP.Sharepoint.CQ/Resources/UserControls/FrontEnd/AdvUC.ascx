<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="AdvUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.AdvUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Repeater ID="rptAdv" runat="server">
    <HeaderTemplate><asp:Literal ID="ltrHeader" runat="server"></asp:Literal></HeaderTemplate>
    <ItemTemplate>
        <div>
            <asp:LinkButton ID="aLink" runat="server">
                <asp:Literal ID="ltrQC" runat="server"></asp:Literal></asp:LinkButton>
        </div>
    </ItemTemplate>
    <SeparatorTemplate><div style="height:5px; width:100%">&nbsp;</div></SeparatorTemplate>
    <FooterTemplate><asp:Literal ID="ltrFooter" runat="server"></asp:Literal></FooterTemplate>
</asp:Repeater>
<asp:HiddenField ID="hdQC" runat="server" />
<script type="text/javascript">
    function SetValueAlink(id, value) {
        document.getElementById(id).value = value;
    }
</script>
