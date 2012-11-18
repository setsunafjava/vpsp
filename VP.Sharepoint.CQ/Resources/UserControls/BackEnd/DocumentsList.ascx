<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentsList.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.DocumentsList" %>
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

        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblDocumentNo" runat="server" FieldName="DocumentNo" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtDocumentNo" FieldName="DocumentNo"/>
            </td>
        </tr>
         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblCategory" runat="server" FieldName="CategoryId" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">                
                <asp:Label ID="lblCatDisplay" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblDocumentSubject" runat="server" FieldName="DocumentSubject" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:LookupField runat="server" ID="txtDocumentSubject" FieldName="DocumentSubject"/>
            </td>
        </tr>

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblDocumentType" runat="server" FieldName="DocumentType" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:LookupField runat="server" ID="txtDocumentType" FieldName="DocumentType"/>
            </td>
        </tr>

        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblPublishPlace" runat="server" FieldName="PublishPlace" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:LookupField runat="server" ID="txtPublishPlace" FieldName="PublishPlace"/>
            </td>
        </tr>

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblSignaturePerson" runat="server" FieldName="SignaturePerson" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:LookupField runat="server" ID="txtSignaturePerson" FieldName="SignaturePerson"/>
            </td>
        </tr>

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblEffectedDate" runat="server" FieldName="EffectedDate" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DateTimeField runat="server" ID="txtEffectedDate" FieldName="EffectedDate"/>
            </td>
        </tr>

        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblExpiredDate" runat="server" FieldName="ExpiredDate" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DateTimeField runat="server" ID="txtExpiredDate" FieldName="ExpiredDate"/>
            </td>
        </tr>
         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblLinkToFile" runat="server" FieldName="FilePath" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <asp:FileUpload ID="fuFile" runat="server" />
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

<input type="hidden" runat="server" id="hidMenuLevel" />
<script type="text/javascript">
    $(document).ready(function () {
        $("#partAttachment").appendTo("#divAttachments");
        $("#idAttachmentsTable").appendTo("#tdAttachments");
    });
</script>
