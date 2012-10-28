<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsList.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.NewsList" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div id="part1">
    <table class="ms-formtable" style="width: 100%">
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblTitle" runat="server" FieldName="Title" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtTitle" FieldName="Title"/>
            </td>
        </tr>      
        <asp:Label ID="lblTest" runat="server" Text="Test"></asp:Label>
         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblNewsGroup" runat="server" FieldName="NewsGroup" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtNewsGroup" FieldName="NewsGroup"/>
                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblDescription" runat="server" FieldName="Description" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:RichTextField runat="server" ID="txtDescription" FieldName="Description"/>
            </td>
        </tr>      

        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblContent" runat="server" FieldName="Content" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:RichTextField runat="server" ID="txtContent" FieldName="Content"/>
            </td>
        </tr>  

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblStatus" runat="server" FieldName="Status" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtStatus" FieldName="Status"/>
            </td>
        </tr>      

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblPostedDate" runat="server" FieldName="PostedDate" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DateTimeField runat="server" ID="txtPostedDate" FieldName="PostedDate" />
            </td>
        </tr>      

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblImageThumb" runat="server" FieldName="ImageThumb" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtImageThubm" FieldName="ImageThumb"/>
            </td>
        </tr>    
        
          <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblSmallImageThumb" runat="server" FieldName="ImageSmallThumb" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtSmallImageThumb" FieldName="ImageSmallThumb"/>
            </td>
        </tr>      
        
        <tr id="idAttachmentsRow">
            <td style="width: 190px; vertical-align: top; font-weight: normal;" class="ms-formlabel"
                valign="top">
                <SharePoint:FieldLabel ID="labelAttachments" runat="server" FieldName="Attachments" />
            </td>
            <td class="ms-formbody" id="tdAttachments" style="font-size: 11px;">
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="vertical-align: top; font-weight: normal;" class="ms-formlabel"
                valign="top">
                <table>
                    <tr>
                        <td width="99%">
                            <SharePoint:CreatedModifiedInfo ID="CreatedModifiedInfo1" runat="server" />
                        </td>
                        <td class="ms-ButtonHeightWidth">
                            <SharePoint:SaveButton runat="server" ID="saveButton" />
                        </td>
                        <td class="ms-ButtonHeightWidth">
                            <SharePoint:GoBackButton ID="goBackButton" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="divAttachments">
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#partAttachment").appendTo("#divAttachments");
        $("#idAttachmentsTable").appendTo("#tdAttachments");
    });
</script>
