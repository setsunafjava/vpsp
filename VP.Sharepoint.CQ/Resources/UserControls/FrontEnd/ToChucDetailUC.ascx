<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToChucDetailUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.ToChucDetailUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="sub_page">
    <div runat="server" id="divName" class="title_name_content">
        lãnh đạo sở</div>
    <div class="content_follow">
        <div style="width: 96%; padding: 3px; text-align: left;">
            <table width="100%" cellspacing="1" cellpadding="1" border="0">
                <tbody>
                    <tr>
                        <td align="center">
                            <b runat="server" id="bName" style="font-size: 18px; font-weight: bold; text-transform: uppercase;
                                color: #FF6600">lãnh đạo sở </b>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            --------oo00oo--------
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div align="center">
                                <font runat="server" id="fDesc" size="2" face="Arial">Sở Giáo dục và Đào tạo tỉnh Hậu
                                    Giang có 01 Giám đốc và 04 Phó Giám đốc. </font>
                            </div>
                            <asp:Repeater ID="rptTC" runat="server">
                                <ItemTemplate>
                                    <p>
                                        <font size="2" face="Arial"><strong><u>
                                            <%#Eval("Position")%></u>:&nbsp;&nbsp;<%#Eval("Title")%></strong></font></p>
                                    <ul type="disc">
                                        <li class="MsoNormal" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto;
                                            mso-list: l0 level1 lfo1; tab-stops: list .5in"><span style="font-size: 10.0pt; font-family: Arial">
                                                <%#Eval("Description")%></span></li>
                                        <li class="MsoNormal" style=""><span style="font-size: 10pt; font-family: Arial;">-
                                            Điện thoại:
                                            <%#Eval("Mobile")%></span></li>
                                        <li class="MsoNormal" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto;
                                            mso-list: l0 level1 lfo1; tab-stops: list .5in"><span new="" times="" style="font-size: 10.0pt;
                                                font-family: Arial; mso-fareast-font-family: ">- Email: <u>
                                                    <%#Eval("Email")%></u>&nbsp;&nbsp; </span></li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <br />
    <div class="title_name_content">
        Tin hoạt động</div>
    <div class="content_follow">
        <asp:Repeater ID="rptListNews" runat="server" OnItemDataBound="rptListNews_ItemDataBound">
            <ItemTemplate>
                <div class="news_artical">
                    <div class="name_artical">
                        <a id="aLink" runat="server">
                            <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.NewsList.InternalName.Title) %></a>
                        <span class="time_update" id="spDate" runat="server"></span>
                    </div>
                    <div class="img_thumbail_shortcontent">
                        <div class="img_thumbail">
                            <a id="aImg" runat="server">
                                <img id="imgNews" runat="server" src="<%=DocLibUrl%>/17_7_1345027592_43_nu1.jpg" /></div>
                        </a>
                        <div class="short_content" id="dvDesc" runat="server">
                            Giáo dục bắt buộc ở Anh bắt đầu vào bậc tiểu học lúc 5 tuổi. Học sinh tiểu học sẽ
                            học từ năm nhất lên đến năm thứ sáu mà không phải qua một kỳ thi nào, tuy nhiên
                            sẽ có cuộc kiểm tra khả năng học sinh khi lên 7 tuổi. Học sinh được chú trọng vào
                            việc học bằng cách tự khám phá hơn là học thuộc lòng.
                        </div>
                        <div class="cleaner">
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br />
    <div class="title_name_content">
        Văn bản điều hành</div>
    <div class="content_follow">
        <table class="vanbantb no-arrow rowstyle-alt colstyle-alt paginate-15 max-pages-2"
            id="vanbantb2443" width="100%" style="margin-top: 10px;">
            <thead>
                <tr>
                    <th width="15%">
                        Số/ký hiệu
                    </th>
                    <th>
                        Trích yếu/về việc
                    </th>
                    <th width="15%">
                        Ban hành
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptVanBan" runat="server" OnItemDataBound="rptVanBan_ItemDataBound">
                    <ItemTemplate>
                        <tr class="">
                            <td valign="top">
                                <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.DocumentNo)%>
                            </td>
                            <td>
                                <a style="font-weight: bold" href="javascript:void(0);" id="aLink" runat="server">
                                    <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.Title)%>
                                </a>
                                <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                                    id="vbId_<%=i%>">
                                    <b>Cơ quan ban hành:</b>
                                    <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.PublishPlace)%><br />
                                    <b>Loại văn bản:</b>
                                    <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.DocumentType)%><br />
                                    <b>Lĩnh vực:</b>
                                    <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.DocumentSubject)%><br />
                                    <b>Người ký:</b>
                                    <%#Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.SignaturePerson)%><br />
                                    <b>Ngày hiệu lực:</b>
                                    <%#Convert.ToDateTime(Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.EffectedDate)).ToString("dd/MM/yyyy")%><br />
                                    <b>Người hết hiệu lực:</b>
                                    <%#Convert.ToDateTime(Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.ExpiredDate)).ToString("dd/MM/yyyy")%><br />
                                </div>
                            </td>
                            <td valign="top">
                                <%#Convert.ToDateTime(Eval(VP.Sharepoint.CQ.Common.FieldsName.DocumentsList.InternalName.EffectedDate)).ToString("dd/MM/yyyy")%><br />
                            </td>
                        </tr>
                        <%i++; %>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</div>
<script type="text/javascript">
    function showDocumentDetail(id) {
        var divId = document.getElementById(id);
        if (divId.style.display == "none")
            divId.style.display = "block";
        else
            divId.style.display = "none";
    }
</script>
