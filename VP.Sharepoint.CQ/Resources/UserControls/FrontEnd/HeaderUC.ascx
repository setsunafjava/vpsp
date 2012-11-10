<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.HeaderUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<link rel="stylesheet" type="text/css" href="<%=DocLibUrl%>/COREV4.CSS" />
<link rel="stylesheet" type="text/css" href="<%=DocLibUrl%>/styles.css" />
<link rel="stylesheet" type="text/css" href="<%=DocLibUrl%>/tabcontent.css" />
<link rel="stylesheet" type="text/css" href="<%=DocLibUrl%>/simpletree.css" />
<!--[if lte IE 6]>
<link rel="stylesheet" type="text/css" href="<%=DocLibUrl%>/ie6.css">
<![endif]-->
<!--[if lte IE 7]>
<link rel="stylesheet" type="text/css" href="<%=DocLibUrl%>/ie7.css">
<![endif]-->
<script type="text/javascript" src="<%=DocLibUrl%>/jquery-1.7.1.js"></script>
<script type="text/javascript" src="<%=DocLibUrl%>/tabcontent.js"></script>
<script type="text/javascript" src="<%=DocLibUrl%>/script.js"></script>
<script type="text/javascript" src="<%=DocLibUrl%>/simpletreemenu.js"></script>

<div id="header">
	<div class="banner">
		<img src="<%=DocLibUrl%>/header.jpg" />
	</div>
</div>