<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExternalNewsView.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ExternalNewsView" %>
<%@ Register TagPrefix="cl" Namespace="VP.Sharepoint.CQ.Core.WebControls" Assembly="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<table class="ms-formtable" style="width: 100%">
    <tr>
        <td style="vertical-align: top; width:120px; font-weight: normal;" class="ms-formlabel" valign="top">
            Chọn chuyên mục
        </td>
        <td class="ms-formbody" style="font-size: 11px;">
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true"
                onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>
</table>
<div>
    <input id="btnRSS" type="button" value="Thêm mới link RSS" onclick="OpenAddNewRSS()" />
</div>
<cl:FlatDataView ID="viewRSS" runat="server" ListName="ExternalNewsLinkList" ShowRibbonTabs="false"
    ShowTotalItems="True" MenuField="Title">
    <ViewFields>
        <cl:TextFieldRef ID="fName" FieldName="Title" HeaderText="Tên RSS" runat="server" />
        <cl:TextFieldRef ID="fRSS" FieldName="LinkPath" HeaderText="Link RSS" runat="server" />
    </ViewFields>
</cl:FlatDataView>
<br />
<div>
    <asp:Button ID="btnUpdate" runat="server" Text="Lấy tin tức mới nhất" 
        onclick="btnUpdate_Click" />
    <asp:Button ID="btnStatus" runat="server" Text="Cập nhật trạng thái" 
        onclick="btnStatus_Click" /><br />
    &nbsp;<asp:DropDownList ID="ddlCat" runat="server"></asp:DropDownList>&nbsp;Nhập link RSS
    <asp:TextBox ID="txtRSS" runat="server"></asp:TextBox>&nbsp;
    <asp:Button ID="btnGetNews" runat="server" Text="Lấy tin tức cho tin chính" 
        onclick="btnGetNews_Click" />
        &nbsp;Nhập url trang cũ
    <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>&nbsp;Nhóm tin
    <asp:TextBox ID="txtNewCat" runat="server" Width="50"></asp:TextBox>&nbsp;
    <asp:Button ID="btnCopyNews" runat="server" Text="Copy tin từ site cũ" 
        onclick="btnCopyNews_Click" />
</div>
<br />

<cl:FlatDataView ID="viewNews" runat="server" ListName="NewsList" ShowRibbonTabs="false"
    ShowTotalItems="True" MenuField="Title">
    <ViewFields>
        <cl:UrlFieldRef ID="TextFieldRef3" FieldName="ImageDsp" HeaderText="Ảnh đại diện" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef1" FieldName="Title" HeaderText="Tiêu đề" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef2" FieldName="NewsUrl" HeaderText="Link bài viết" runat="server" />
        <cl:ChoiceFieldRef ID="TextFieldRef4" FieldName="ShowHide" HeaderText="Trạng thái" runat="server" />
    </ViewFields>
</cl:FlatDataView>

<script type="text/javascript">
    function OpenAddNewRSS() {
        var options = SP.UI.$create_DialogOptions();
        options.url = '../../Lists/ExternalNewsLinkList/NewForm.aspx?CatID=' + document.getElementById('<%=ddlCategory.ClientID%>').value;
        options.title = 'Thêm mới link RSS';
        options.dialogReturnValueCallback = Function.createDelegate(null, VP_CallbackRefreshPage);
        SP.UI.ModalDialog.showModalDialog(options);
    }

    function VP_CallbackRefreshPage(dialogResult, returnValue) {
        if (dialogResult == SP.UI.DialogResult.OK) {
            SP.UI.Notify.addNotification('Đã thực hiện thành công!');
            SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);
        }
    }
</script>