<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilesView.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ProfilesView" %>
<%@ Register TagPrefix="cl" Namespace="VP.Sharepoint.CQ.Core.WebControls" Assembly="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<table class="ms-formtable" style="width: 100%">
    <tr>
        <td style="vertical-align: top; width:120px; font-weight: normal;" class="ms-formlabel" valign="top">
            Chọn tổ chức
        </td>
        <td class="ms-formbody" style="font-size: 11px;">
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true"
                onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>
</table>

<cl:FlatDataView ID="viewProfiles" runat="server" ListName="ProfilesList" ShowRibbonTabs="false"
    ShowTotalItems="True" MenuField="Title">
    <ViewFields>
        <cl:TextFieldRef ID="fName" FieldName="Title" HeaderText="Họ và tên" runat="server" />
        <cl:TextFieldRef ID="fPosition" FieldName="Position" HeaderText="Vị trí" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef1" FieldName="CategoryName" HeaderText="Tổ chức" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef2" FieldName="Education" HeaderText="Trình độ" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef3" FieldName="Specialized" HeaderText="Chuyên ngành" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef4" FieldName="Phone" HeaderText="Điện thoại" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef5" FieldName="Mobile" HeaderText="Đi động" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef6" FieldName="Email" HeaderText="Email" runat="server" />
        <cl:TextFieldRef ID="TextFieldRef7" FieldName="Order" HeaderText="Order" runat="server" IsHidden="true" />
    </ViewFields>
    <SortFields>
        <cl:SortFieldRef ID="sOrder" FieldName="Order" SortDirection="Ascending" runat="server" />
	</SortFields>
</cl:FlatDataView>