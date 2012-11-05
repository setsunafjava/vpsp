<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryView.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.CategoryView" %>
<%@ Register Tagprefix="cl" Namespace="VP.Sharepoint.CQ.Core.WebControls" Assembly="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<cl:RecursiveDataView ID="viewMenu" runat="server" ListName="CategoryList" ShowRibbonTabs="false" ShowTotalItems="True" MenuField="Title" 
    FieldIDName="CategoryID" FieldParentName="ParentID" FieldOrderName="Order" FieldLevelName="CategoryLevel" FieldRecursiveName="Title">
    <ViewFields>
	    <cl:TextFieldRef ID="fName" FieldName="Title" HeaderText="Tên chuyên mục" runat="server"/>
        <cl:TextFieldRef ID="TextFieldRef1" FieldName="CategoryID" runat="server" IsHidden="true" />
        <cl:TextFieldRef ID="TextFieldRef2" FieldName="ParentID" runat="server" IsHidden="true" />
        <cl:TextFieldRef ID="TextFieldRef3" FieldName="Order" runat="server" IsHidden="true" />
        <cl:TextFieldRef ID="TextFieldRef4" FieldName="CategoryLevel" runat="server" IsHidden="true" />
    </ViewFields>
	<%--<GroupFields>
		<cl:TextFieldRef FieldName="Title" HeaderText ="Key Kostenstelle" SortDirection="Ascending" runat="server" CollapsedGroup="True" />
    </GroupFields>	
	<SortFields>
        <cl:SortFieldRef FieldName="Title" SortDirection="Ascending" runat="server" />
	</SortFields>--%>
</cl:RecursiveDataView>
