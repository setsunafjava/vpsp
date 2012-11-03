<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvList.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.AdvList" %>
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
                <SharePoint:FieldLabel ID="lblDescription" runat="server" FieldName="Description" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:NoteField runat="server" ID="txtDescription" FieldName="Description"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel1" runat="server" FieldName="AdvType" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlAdvType" FieldName="AdvType"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel2" runat="server" FieldName="AdvFile" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">                
                <asp:FileUpload ID="fuFile" runat="server" />
                <asp:Literal ID="ltrBr" runat="server"></asp:Literal>
                <asp:HyperLink ID="linkFile" runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel3" runat="server" FieldName="AdvUrl" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtAdvUrl" FieldName="AdvUrl"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel4" runat="server" FieldName="AdvOpenType" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlAdvOpenType" FieldName="AdvOpenType"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel5" runat="server" FieldName="AdvWidth" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:NumberField runat="server" ID="txtAdvWidth" FieldName="AdvWidth"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel6" runat="server" FieldName="AdvHeight" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:NumberField runat="server" ID="txtAdvHeight" FieldName="AdvHeight"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel7" runat="server" FieldName="AdvStartDate" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DateTimeField runat="server" ID="txtAdvStartDate" FieldName="AdvStartDate"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel8" runat="server" FieldName="AdvEndDate" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DateTimeField runat="server" ID="txtAdvEndDate" FieldName="AdvEndDate"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel9" runat="server" FieldName="AdvPosition" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlAdvPosition" FieldName="AdvPosition"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel10" runat="server" FieldName="AdvStatus" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:DropDownChoiceField runat="server" ID="ddlAdvStatus" FieldName="AdvStatus"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel15" runat="server" FieldName="AdvOrder" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:NumberField runat="server" ID="txtAdvOrder" FieldName="AdvOrder"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel11" runat="server" FieldName="CustomerName" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtCustomerName" FieldName="CustomerName"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel12" runat="server" FieldName="CustomerAddress" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtCustomerAddress" FieldName="CustomerAddress"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel13" runat="server" FieldName="CustomerPhone" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtCustomerPhone" FieldName="CustomerPhone"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-weight: normal;" class="ms-formlabel" valign="top">
                <SharePoint:FieldLabel ID="FieldLabel14" runat="server" FieldName="CustomerMobile" />
            </td>
            <td class="ms-formbody" style="font-size: 11px;">
                <SharePoint:TextField runat="server" ID="txtCustomerMobile" FieldName="CustomerMobile"/>
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
