<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuList.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.MenuList" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div id="part1">
    <table class="ms-formtable" style="width: 100%">
        <tr>
            <td style="width: 120px; vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblTitle" runat="server" FieldName="Title" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtTitle" FieldName="Title"/>
            </td>
        </tr> 
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblDescription" runat="server" FieldName="Description" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:NoteField runat="server" ID="txtDescription" FieldName="Description" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblParentName" runat="server" FieldName="ParentName" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <asp:Label ID="lblParentNameDsp" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlParentName" runat="server"></asp:DropDownList>
            </td>
        </tr>     
         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblMenuType" runat="server" FieldName="MenuType" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlMenuType" FieldName="MenuType"/>
            </td>
        </tr>      
        <tr id="trMenuUrl">
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblMenuUrl" runat="server" FieldName="MenuUrl" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtMenuUrl" FieldName="MenuUrl"/>
            </td>
        </tr>
        <tr id="trCatName">
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblCatName" runat="server" FieldName="CatName" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <asp:Label ID="lblCatDisplay" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
            </td>
        </tr>      
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblStatus" runat="server" FieldName="Status" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlStatus" FieldName="Status"/>
            </td>
        </tr>  
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblOpenType" runat="server" FieldName="OpenType" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlOpenType" FieldName="OpenType"/>
            </td>
        </tr>
         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblMenuOrde" runat="server" FieldName="MenuOrder" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:NumberField runat="server" ID="txtMenuOrder" FieldName="MenuOrder"/>
            </td>
        </tr>      

         <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="lblMenuPosition" runat="server" FieldName="MenuPosition" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:CheckBoxChoiceField runat="server" ID="chkMenuPosition" FieldName="MenuPosition" />
            </td>
        </tr>      
        <tr id="idAttachmentsRow">
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel"
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
<input runat="server" type="hidden" id="hidMenuLevel" />
<script type="text/javascript">
    $(document).ready(function () {
        $("#partAttachment").appendTo("#divAttachments");
        $("#idAttachmentsTable").appendTo("#tdAttachments");
        if ($("[id*='_ddlMenuType_']")) {
            SetShowHideControl();
            $("[id*='_ddlMenuType_']").change(function () {
                SetShowHideControl();
            });
        }
    });

    function SetShowHideControl() {
        if ($("[id*='_ddlMenuType_']")) {
            var selectedValue = $("[id*='_ddlMenuType_']").val();
            if (selectedValue == 'Link tới chuyên mục') {
                $("#trCatName").show();
                $("#trMenuUrl").hide();
            }
            else if (selectedValue == 'Đường link xác định') {
                $("#trCatName").hide();
                $("#trMenuUrl").show();
            }
        }
    }
</script>
