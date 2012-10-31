<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VanBanUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.VanBanUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="sub_page">
    <div class="title_name_content">
        Sở Giáo dục và đào tạo</div>
    <div class="content_follow">
        <table>
            <tr>
                <td>
                    <select onchange="modvanban_showFilter();" class="input" name="cqbanhanh" id="cqbanhanh">
                        <option value="">-- Cơ quan ban hành --</option>
                        <option value="HEAD147">Bộ Giáo dục và Đào tạo</option>
                    </select>
                </td>
                <td>
                    <select onchange="modvanban_showFilter();" class="input" name="cqbanhanh" id="cqbanhanh">
                        <option value="">-- Các loại Văn Bản--</option>
                    </select>
                </td>
                <td>
                    <select onchange="modvanban_showFilter();" class="input" name="cqbanhanh" id="cqbanhanh">
                        <option value="">-- Lĩnh vực--</option>
                    </select>
                </td>
                <td>
                    <select onchange="modvanban_showFilter();" class="input" name="cqbanhanh" id="cqbanhanh">
                        <option value="">-- Người ký--</option>
                    </select>
                </td>
            </tr>
        </table>
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
                <tr class="">
                    <td valign="top">
                        ...................
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('844');">Báo cáo
                            Tổng kết Đề án “Xây dựng xã hội học tập giai đoạn 2012-2020” </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details844">
                        </div>
                    </td>
                    <td valign="top">
                        10/09/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số: 891 /SGDĐT - GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('843');">Về việc
                            tập huấn giáo viên cốt cán về nội dung giáo dục bảo vệ tài nguyên môi trường biển,
                            hải đảo cấp THCS, THPT </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details843">
                        </div>
                    </td>
                    <td valign="top">
                        10/09/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số: 54 /KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('842');">Kế hoạch
                            đẩy mạnh công tác tuyên truyền cổ động “Năm An toàn giao thông &ndash; 2012"
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details842">
                        </div>
                    </td>
                    <td valign="top">
                        07/09/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số: 857/SGD ĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('819');">Về việc
                            triển khai công tác giáo dục ATGT đầu năm học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details819">
                        </div>
                    </td>
                    <td valign="top">
                        30/08/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số: 848/SGDĐT-KTQLCLGD
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('818');">Hội nghị
                            Tổng kết năm học 2011 - 2012 và hướng dẫn nhiệm vụ năm học 2012 - 2013 về Khảo thí
                            và Kiểm định chất lượng giáo dục </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details818">
                        </div>
                    </td>
                    <td valign="top">
                        28/08/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số: 976 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('817');">Quyết
                            định về việc công nhận kết quả xét tuyển dụng viên chức các đơn vị trực thuộc Sở
                            Giáo dục và Đào tạo năm học: 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details817">
                        </div>
                    </td>
                    <td valign="top">
                        28/08/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số: 842/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('815');">Về việc
                            phát động tham gia cuộc thi “tìm hiểu chính sách, pháp luật về bình đẳng giới”
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details815">
                        </div>
                    </td>
                    <td valign="top">
                        27/08/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số:840 /SGDĐT-CNTT-QLTB&amp;TV-TTr
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('811');">Về việc
                            mời dự Hội nghị Tổng kết và triển khai phương hướng, nhiệm vụ năm học 2012-2013
                            về công tác CNTT-QLTB&amp;TV và Thanh Tra </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details811">
                        </div>
                    </td>
                    <td valign="top">
                        24/08/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số: 813 /SGDĐT-KHTC
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('810');">Về việc
                            nộp báo cáo thống kê năm học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details810">
                        </div>
                    </td>
                    <td valign="top">
                        23/08/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số: 835/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('809');">Về việc
                            báo cáo tình hình sau khai giảng. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details809">
                        </div>
                    </td>
                    <td valign="top">
                        23/08/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số 94/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('805');">Về việc
                            treo cờ Tổ quốc và nghỉ Quốc khánh 02/9/2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details805">
                        </div>
                    </td>
                    <td valign="top">
                        22/08/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số: 813/SGDĐT - VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('803');">Về việc
                            lập danh sách và kêu gọi hỗ trợ học sinh có hoàn cảnh khó khăn </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details803">
                        </div>
                    </td>
                    <td valign="top">
                        20/08/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số: 814/SGDĐT.CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('801');">Về việc
                            triển khai tài liệu tuyên truyền, giáo dục phòng, chống tác hại của trò chơi trực
                            tuyến đối với HSSV </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details801">
                        </div>
                    </td>
                    <td valign="top">
                        20/08/2012
                    </td>
                </tr>
                <tr class="alt">
                    <td valign="top">
                        Số: 794/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('799');">Về việc
                            xây dựng nội dung chương trình Lễ khai giảng năm học 2012 - 2013. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details799">
                        </div>
                    </td>
                    <td valign="top">
                        17/08/2012
                    </td>
                </tr>
                <tr class="">
                    <td valign="top">
                        Số: 783/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('795');">Về việc
                            chấn chỉnh việc thu các khoản ngoài quy định trong các cơ sở giáo dục </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details795">
                        </div>
                    </td>
                    <td valign="top">
                        14/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 787/SGDĐT.CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('796');">Về việc
                            đề nghị nâng cấp, chỉnh trang đường FTTH ở các đơn vị </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details796">
                        </div>
                    </td>
                    <td valign="top">
                        14/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 651/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('802');">Về việc
                            hướng dẫn khen thưởng các doanh nghiệp và các nhà hảo tâm có đóng góp, hỗ trợ cho
                            sự nghiệp giáo dục, đào tạo. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details802">
                        </div>
                    </td>
                    <td valign="top">
                        13/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 882 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('794');">Về việc
                            thành lập đoàn tham dự Hội thảo quốc gia môn Lịch sử tại Đà Nẵng </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details794">
                        </div>
                    </td>
                    <td valign="top">
                        10/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 879/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('787');">Quyết
                            định về việc khen thưởng thành tích trong thực hiện phong trào thi đua "Xây dựng
                            trường học thân thiện, học sinh tích cực" </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details787">
                        </div>
                    </td>
                    <td valign="top">
                        09/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 866/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('773');">Quyết
                            định về việc khen thưởng thành tích 4 năm thực hiện phong trào thi đua "Xây dựng
                            trường học thân thiện, học sinh tích cực" </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details773">
                        </div>
                    </td>
                    <td valign="top">
                        06/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 865/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('772');">Về việc
                            khen thưởng các đơn vị hoàn thành tốt Nhiệm vụ năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details772">
                        </div>
                    </td>
                    <td valign="top">
                        06/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 754 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('770');">Về việc
                            hướng dẫn tổ chức “Tuần sinh hoạt tập thể” đầu năm học </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details770">
                        </div>
                    </td>
                    <td valign="top">
                        06/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 863 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('769');">Về việc
                            thành lập đoàn tham dự lớp tập huấn Giáo viên cốt cán về nội dung giáo dục bảo vệ
                            tài nguyên và môi trường biển, hải đảo cấp THCS và THPT </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details769">
                        </div>
                    </td>
                    <td valign="top">
                        06/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 748/SGDĐT-GDTH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('768');">Về việc
                            mời dự Hội nghị Tổng kết năm học 2011-2012 và triển khai phương hướng nhiệm vụ năm
                            học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details768">
                        </div>
                    </td>
                    <td valign="top">
                        03/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số 17/TM-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('767');">Thư mời
                            dự Hội nghị tổng kết năm học 2011-2012, triển khai phương hướng nhiệm vụ năm học
                            2012-2013 và sơ kết 04 năm thực hiện phong trào thi đua "Xây dựng trường học thân
                            thiện, học sinh tích cực" </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details767">
                        </div>
                    </td>
                    <td valign="top">
                        02/08/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 714 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('762');">Về việc
                            hướng dẫn dạy học tăng cường môn tiếng Anh cấp THCS theo Đề án Ngoại ngữ Quốc gia
                            2020 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details762">
                        </div>
                    </td>
                    <td valign="top">
                        25/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 83/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('760');">Về kết
                            quả họp Hội đồng thi đua-khen thưởng Sở GD&amp;ĐT xét các đơn vị dẫn đầu năm học
                            2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details760">
                        </div>
                    </td>
                    <td valign="top">
                        20/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 47/KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('759');">Kế hoạch
                            phát động phong trào thi đua kỷ niệm 10 năm thành lập tỉnh và thành lập Sở Giáo
                            dục và Đào tạo Hậu Giang </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details759">
                        </div>
                    </td>
                    <td valign="top">
                        20/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 693/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('758');">Về việc
                            đôn đốc nộp bài và triển khai cuộc thi “Thiết kế bài giảng điện tử e-Learning” năm
                            học 2011-2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details758">
                        </div>
                    </td>
                    <td valign="top">
                        20/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 45/KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('757');">Kế hoạch
                            chuẩn bị tựu trường và khai giảng năm học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details757">
                        </div>
                    </td>
                    <td valign="top">
                        18/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 657/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('755');">Về việc
                            đề xuất nhiệm vụ khoa học và công nghệ năm 2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details755">
                        </div>
                    </td>
                    <td valign="top">
                        13/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 80 /TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('754');">Ý kiến
                            kết luận của Đồng chí Giám đốc Sở GD&amp;ĐT tại Hội nghị Giao ban lãnh đạo Sở GD&amp;ĐT,
                            Trưởng phòng GD&amp;ĐT, Giám đốc Trung tâm GDTX tỉnh, huyện, thị xã, thành phố và
                            Hiệu trưởng. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details754">
                        </div>
                    </td>
                    <td valign="top">
                        13/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 42/KHLT-SGDĐT-CĐGDT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('749');">Kế hoạch
                            tham gia hội thi sáng tạo kỹ thuật tỉnh Hậu Giang lần V- Năm 2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details749">
                        </div>
                    </td>
                    <td valign="top">
                        06/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 627/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('748');">Văn bản
                            về việc mời dự hội nghị giao ban </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details748">
                        </div>
                    </td>
                    <td valign="top">
                        06/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 721 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('744');">Về việc
                            thành lập đoàn tham dự lớp tập huấn Chuyên Địa lý tại Đà Nẵng </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details744">
                        </div>
                    </td>
                    <td valign="top">
                        05/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 720 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('743');">Quyết
                            định về việc thành lập Ban tổ chức, Báo cáo viên lớp tập huấn CBQL, giáo viên về
                            giáo dục giá trị sống, kỹ năng sống; tư vấn học đường; biên soạn đề kiểm tra, đánh
                            giá môn GDCD, Côn </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details743">
                        </div>
                    </td>
                    <td valign="top">
                        04/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 613 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('742');">Văn bản
                            về việc nộp hồ sơ xin hưởng học bổng “Thắp sáng niềm tin" </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details742">
                        </div>
                    </td>
                    <td valign="top">
                        03/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 41 /KHLT-SGDĐT-CĐGDT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('741');">Kế hoạch
                            tổng kết 5 năm thực hiện và kế hoạch tiếp tục thực hiện cuộc vận động “Mỗi thầy,
                            cô giáo là một tấm gương đạo đức, tự học và sáng tạo” </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details741">
                        </div>
                    </td>
                    <td valign="top">
                        03/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 617 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('740');">Văn bản
                            về việc viết tham luận tham gia hội thảo Quốc gia Môn Lịch sử </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details740">
                        </div>
                    </td>
                    <td valign="top">
                        03/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 610 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('737');">Việc việc
                            tập huấn CBQL, giáo viên về giáo dục giá trị sống, kỹ năng sống; tư vấn học đường
                            cho học sinh; biên soạn đề kiểm tra, đánh giá môn GDCD, Công nghệ </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details737">
                        </div>
                    </td>
                    <td valign="top">
                        02/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 609 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('736');">Văn bản
                            về việc bồi dưỡng giáo viên Tiếng Pháp hè 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details736">
                        </div>
                    </td>
                    <td valign="top">
                        02/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 702/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('735');">Văn bản
                            về việc thành lập đoàn tham dự hội nghị tập huấn về phổ biến giáo dục pháp luật
                            tại Đà Nẵng </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details735">
                        </div>
                    </td>
                    <td valign="top">
                        02/07/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 73 /TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('728');">Về việc
                            tuyển dụng viên chức ngành Giáo dục và Đào tạo năm học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details728">
                        </div>
                    </td>
                    <td valign="top">
                        26/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 644/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('726');">Quyết
                            định về việc phân công lãnh đạo Sở Giáo dục và Đào tạo trong công tác lãnh đạo,
                            chỉ đạo quản lý và điều hành </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details726">
                        </div>
                    </td>
                    <td valign="top">
                        26/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 548 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('722');">Về việc
                            triệu tập giáo viên Tiếng Anh cấp THCS và THPT tham gia ôn tập và khảo sát trình
                            độ </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details722">
                        </div>
                    </td>
                    <td valign="top">
                        18/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 546 /SGDĐT-GDTH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('721');">V/v triệu
                            tập giáo viên dạy Tiếng Anh Tiểu học tham dự lớp ôn tập TOEFL-ITP (450 điểm)
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details721">
                        </div>
                    </td>
                    <td valign="top">
                        15/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 534 /SGDĐT - GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('720');">V/v đăng
                            ký giáo viên tập huấn tiếng Anh theo đề án 1400/QĐ-TTg </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details720">
                        </div>
                    </td>
                    <td valign="top">
                        14/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 519 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('719');">V/v Tổ
                            chức đợt cao điểm về phòng,chống ma túy </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details719">
                        </div>
                    </td>
                    <td valign="top">
                        11/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Kế hoạch xét thi đua năm học 2011-2012
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('716');">Văn bản
                            về việc xét thi đua tại các đơn vị năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details716">
                        </div>
                    </td>
                    <td valign="top">
                        07/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 491 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('709');">V/v thành
                            lập đoàn tham dự hội nghị tập huấn về Mô hình trường học mới </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details709">
                        </div>
                    </td>
                    <td valign="top">
                        01/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Thi đua khen thưởng
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('707');">Bảng tóm
                            tắt sáng kiến kinh nghiệm và thang điểm chấm sáng kiến chiến sĩ thi đua cấp cơ sở,
                            Tỉnh </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details707">
                        </div>
                    </td>
                    <td valign="top">
                        01/06/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 485 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('706');">V/v báo
                            cáo tình hình thanh thiếu niên tự sát </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details706">
                        </div>
                    </td>
                    <td valign="top">
                        29/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 480/SGDĐT-KHTC
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('705');">V/v nộp
                            báo cáo thống kê năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details705">
                        </div>
                    </td>
                    <td valign="top">
                        28/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 472/ SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('702');">V/v Hướng
                            dẫn tuyển sinh vào các lớp đầu cấp năm học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details702">
                        </div>
                    </td>
                    <td valign="top">
                        25/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 463/SGDĐT - GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('701');">V/v Nhắc
                            nhở HSSV chấp hành pháp luật về bảo đảm trật tự ATGT trong dịp hè 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details701">
                        </div>
                    </td>
                    <td valign="top">
                        24/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Thi đua khen thưởng 2011-2012
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('700');">Những
                            văn bản về thi đua khen thưởng năm học 2011-2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details700">
                        </div>
                    </td>
                    <td valign="top">
                        24/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 475/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('699');">V/v Ban
                            hành Quy chế làm việc của Sở Giáo dục và Đào tạo Hậu Giang </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details699">
                        </div>
                    </td>
                    <td valign="top">
                        23/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 453/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('698');">V/v Phát
                            động cuộc thi “Thiết kế hồ sơ bài giảng điện tử e-Learning” năm học 2011-2012 cho
                            khối Tiểu học </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details698">
                        </div>
                    </td>
                    <td valign="top">
                        22/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 453/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('697');">V/v Phát
                            động cuộc thi “Thiết kế hồ sơ bài giảng điện tử e-Learning” năm học 2011-2012 cho
                            khối Tiểu học </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details697">
                        </div>
                    </td>
                    <td valign="top">
                        22/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số 53/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('687');">Thông
                            báo về viêc giới thiệu chức danh và chữ ký. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details687">
                        </div>
                    </td>
                    <td valign="top">
                        18/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 26 /KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('689');">Kế hoạch
                            công tác hè 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details689">
                        </div>
                    </td>
                    <td valign="top">
                        18/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 13/TM-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('690');">Thư mời
                            dự Tổng kết cuộc thi Chung tay cải cách thủ tục hành chính </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details690">
                        </div>
                    </td>
                    <td valign="top">
                        18/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 418/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('674');">V/v gửi
                            tóm tắt đề tài, sáng kiến cấp tỉnh năm học 2010-2011 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details674">
                        </div>
                    </td>
                    <td valign="top">
                        09/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 405 /SGDĐT-KHTC
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('670');">V/v hoàn
                            thành hồ sơ xin cấp giấy chứng nhận quyền sử dụng đất tại cơ sở giáo dục trên địa
                            bàn tỉnh Hậu Giang </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details670">
                        </div>
                    </td>
                    <td valign="top">
                        04/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 401/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('668');">V/v trang
                            bị tài liệu phục vụ công tác Y tế trường học - năm học 2011 &ndash; 2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details668">
                        </div>
                    </td>
                    <td valign="top">
                        03/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số 402/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('667');">V/v Khảo
                            sát thực trạng học sinh chơi trò chơi trực tuyến. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details667">
                        </div>
                    </td>
                    <td valign="top">
                        03/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 397 /HĐNGND-NGƯT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('640');">V/v thông
                            báo kết quả sơ duyệt và lấy ý kiến thăm dò dư luận xét tặng danh hiệu NGND-NGƯT
                            lần thứ 12 năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details640">
                        </div>
                    </td>
                    <td valign="top">
                        02/05/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 47/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('633');">Thông
                            báo về việc xét tuyển hệ Dự bị đại học Dân tộc năm học 2012 - 2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details633">
                        </div>
                    </td>
                    <td valign="top">
                        27/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 387/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('639');">V/v phát
                            động cuộc thi “Thiết kế hồ sơ bài giảng điện tử e-Learning” do Bộ GD&amp;ĐT tổ chức
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details639">
                        </div>
                    </td>
                    <td valign="top">
                        26/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 45/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('594');">V/v tiếp
                            xúc cử tri kỳ họp thứ ba </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details594">
                        </div>
                    </td>
                    <td valign="top">
                        26/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số:381/SGDĐT - GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('567');">V/v giới
                            thiệu sách Hướng dẫn sử dụng Atlat Địa lý Việt Nam </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details567">
                        </div>
                    </td>
                    <td valign="top">
                        25/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 44/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('563');">V/v treo
                            cờ Tổ quốc và nghỉ Lễ 30/4 - 1/5 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details563">
                        </div>
                    </td>
                    <td valign="top">
                        24/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 22/KHLT-SGDĐT-CĐGDT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('570');">Kế hoạch
                            tổ chức Đại hội công đoàn cơ sở tiến tới Đại hội Công đoàn Giáo dục tỉnh Hậu Giang
                            lần thứ V, nhiệm kỳ 2013 &ndash; 2018 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details570">
                        </div>
                    </td>
                    <td valign="top">
                        24/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 20 /KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('555');">Kế hoạch
                            thực hiện Đề án “Nâng cao chất lượng công tác phổ biến giáo dục pháp luật trong
                            nhà trường” năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details555">
                        </div>
                    </td>
                    <td valign="top">
                        23/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        NGND-NGUT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('557');">V/v lấy
                            ý kiến xét tặng danh hiệu nhà giáo nhân dân, nhà giáo ưu tú năm 2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details557">
                        </div>
                    </td>
                    <td valign="top">
                        20/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 354/SGDĐT - GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('531');">V/v thông
                            báo Atlat Địa lý Việt Nam bị giả </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details531">
                        </div>
                    </td>
                    <td valign="top">
                        19/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 353 /CVLT-SGDĐT-CĐN
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('528');">V/v chấn
                            chỉnh đạo đức, tác phong và lề lối làm việc của cán bộ, giáo viên </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details528">
                        </div>
                    </td>
                    <td valign="top">
                        19/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 18/KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('514');">Kế hoạch
                            phát động phong trào xây dựng cơ quan, trường học đạt an toàn về An ninh trật tự
                            năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details514">
                        </div>
                    </td>
                    <td valign="top">
                        17/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 334 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('506');">V/v Hướng
                            dẫn tổ chức cuộc thi giải Toán và Olympic Tiếng Anh cấp toàn quốc qua Internet năm
                            học 2011- 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details506">
                        </div>
                    </td>
                    <td valign="top">
                        16/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 328/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('496');">V/v Mời
                            dự họp </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details496">
                        </div>
                    </td>
                    <td valign="top">
                        13/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số:297/SGDĐT.CNTT.QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('485');">V/v Mời
                            dự khai mạc và tổng kết Hội thi “Giáo viên và học sinh sáng tạo” cấp tỉnh năm học
                            2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details485">
                        </div>
                    </td>
                    <td valign="top">
                        06/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số:01/KH-BCĐ
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('486');">Kế hoạch
                            công tác phòng, chống tham nhũng năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details486">
                        </div>
                    </td>
                    <td valign="top">
                        03/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số 31/TB - SGDDT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('482');">V/V giới
                            thiệu chức danh và chữ ký </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details482">
                        </div>
                    </td>
                    <td valign="top">
                        03/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Phòng Khảo thí-QLCLGD
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('455');">Thông
                            báo kết quả thi chọn học sinh giỏi thực hành cấp tỉnh lớp 9 THCS năm học 2011-2012
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details455">
                        </div>
                    </td>
                    <td valign="top">
                        03/04/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 284 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('460');">V/v Tổ
                            chức Hội thảo nâng cao chất lượng bồi dưỡng ôn thi tốt nghiệp THPT năm 2012
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details460">
                        </div>
                    </td>
                    <td valign="top">
                        30/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 280/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('458');">V/v tổ
                            chức ôn thi tốt nghiệp THPT năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details458">
                        </div>
                    </td>
                    <td valign="top">
                        29/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 13 /KH-BCĐ-THTT,HSTC
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('457');">Kế hoạch
                            tiếp đoàn kiểm tra của Bộ GD&amp;ĐT về việc thực hiện phong trào thi đua “Xây dựng
                            trường học thân thiện, học sinh tích cực” năm học 2011 &ndash; 2012 đơn vị tỉnh
                            Hậu Giang </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details457">
                        </div>
                    </td>
                    <td valign="top">
                        29/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 278/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('459');">V/v Mời
                            dự hội nghị giao ban </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details459">
                        </div>
                    </td>
                    <td valign="top">
                        28/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 268 /SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('451');">V/v xét
                            tặng Kỷ niệm chương “Vì sự nghiệp Giáo dục” năm 2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details451">
                        </div>
                    </td>
                    <td valign="top">
                        27/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 260 /SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('441');">V/v Tập
                            huấn xét tặng danh hiệu NGND-NGUT </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details441">
                        </div>
                    </td>
                    <td valign="top">
                        23/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 27/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('440');">V/v treo
                            cờ Tổ quốc và nghỉ Giỗ Tổ Hùng Vương năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details440">
                        </div>
                    </td>
                    <td valign="top">
                        22/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 238/SGDĐT- VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('437');">V/v treo
                            băng gôn, khẩu hiệu hưởng ứng Ngày Nước thế giới năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details437">
                        </div>
                    </td>
                    <td valign="top">
                        20/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 264 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('431');">V/v thành
                            lập đoàn tham dự hội thảo về “Kinh nghiệm khai thác và ứng dụng CNTT, xây dựng hồ
                            sơ dạy học và hướng dẫn học sinh nghiên cứu khoa học” </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details431">
                        </div>
                    </td>
                    <td valign="top">
                        16/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 265/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('430');">V/v thành
                            lập đoàn tham dự hội thảo về “Nâng cao chất lượng sinh hoạt khoa học ở trường THCS
                            và THPT” tại Kiên Giang </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details430">
                        </div>
                    </td>
                    <td valign="top">
                        16/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 226/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('426');">V/v Hướng
                            dẫn kiểm tra học kỳ II THCS, THPT năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details426">
                        </div>
                    </td>
                    <td valign="top">
                        14/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 254/QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('425');">V/v thành
                            lập đoàn tham dự lớp tập huấn Công tác tư vấn học đường </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details425">
                        </div>
                    </td>
                    <td valign="top">
                        13/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 215 /SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('424');">V/v báo
                            cáo và thuyết minh thiết bị dạy học tự làm bậc Tiểu học và bậc Mầm non </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details424">
                        </div>
                    </td>
                    <td valign="top">
                        13/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 201 /SGDĐT-KTQLCLGD
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('418');">V/v mời
                            dự Lễ khai mạc kỳ thi chọn học sinh giỏi cấp tỉnh thực hành Lý &ndash; Hóa &ndash;
                            Sinh lớp 12 THPT năm học 2011 - 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details418">
                        </div>
                    </td>
                    <td valign="top">
                        09/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 05 /TM-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('417');">Hội nghị
                            tổng kết công tác kế hoạch- tài chính năm 2011 và triển khai kế hoạch chỉ tiêu năm
                            2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details417">
                        </div>
                    </td>
                    <td valign="top">
                        09/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 06 /TM-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('416');">Dự Khai
                            mạc và Tổng kết Hội thi Giáo viên dạy giỏi Trung cấp chuyên nghiệp lần thứ II -
                            năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details416">
                        </div>
                    </td>
                    <td valign="top">
                        09/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 236 /QĐ-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('409');">V/v thành
                            lập đoàn tham dự lớp tập huấn về Giáo dục giá trị sống, kỹ năng sống cho học sinh
                            THCS, THPT </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details409">
                        </div>
                    </td>
                    <td valign="top">
                        05/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 12 /KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('408');">Kế hoạch
                            tham gia cuộc thi “Chung tay cải cách thủ tục hành chính”; </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details408">
                        </div>
                    </td>
                    <td valign="top">
                        05/03/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        số 168/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('399');">V/v triển
                            khai thực hiện Thông tư 07/2012/TT-BGDĐT của Bộ GD&amp;ĐT về xét tặng danh hiệu
                            Nhà giáo nhân dân, nhà giáo ưu tú. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details399">
                        </div>
                    </td>
                    <td valign="top">
                        29/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 169/SGDĐT-CĐGDT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('398');">V/v mời
                            dự khai mạc hội thi “Giỏi kiến thức, khéo tay, nhanh trí và họp mặt kỷ niệm ngày
                            Quốc tế Phụ nữ 08/3/2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details398">
                        </div>
                    </td>
                    <td valign="top">
                        28/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 01/QĐ-CĐGDT-UBKT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('390');">V/v thành
                            lập đoàn kiểm tra công tác hoạt động công đoàn Năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details390">
                        </div>
                    </td>
                    <td valign="top">
                        23/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        số 131/KTKĐCLGD-KT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('397');">Về việc
                            Thông báo kết quả thi chọn học sinh giỏi năm 2012. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details397">
                        </div>
                    </td>
                    <td valign="top">
                        23/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 14/TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('389');">V/v chuẩn
                            bị đón Đoàn kiểm tra của Bộ Giáo dục và Đào tạo về việc thực hiện phong trào thi
                            đua “Xây dựng trường học thân thiện, học sinh tích cực” năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details389">
                        </div>
                    </td>
                    <td valign="top">
                        22/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 07/HD-CĐGDT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('388');">V/v vận
                            động CB,GV,CNV đóng góp xây dựng trường học từ nay đến năm 2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details388">
                        </div>
                    </td>
                    <td valign="top">
                        22/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 11/KH-BCĐ
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('387');">Về việc
                            trồng cây trong khuôn viên trường học </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details387">
                        </div>
                    </td>
                    <td valign="top">
                        22/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 150/SGDĐT-GDTX-GDCN
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('386');">V/v đăng
                            ký mua tài liệu về tuyển sinh ĐH, CĐ năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details386">
                        </div>
                    </td>
                    <td valign="top">
                        22/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số 137/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('376');">V/v Hình
                            thức tuyển sinh vào lớp 10 năm học 2012-2013 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details376">
                        </div>
                    </td>
                    <td valign="top">
                        21/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số : 140 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('372');">V/v Dự
                            hội giảng Lịch sử - Địa lý địa phương </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details372">
                        </div>
                    </td>
                    <td valign="top">
                        21/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 12 /TB - SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('368');">V/v thời
                            gian, địa điểm tổ chức thi và thang điểm chấm thi của Hội thi cán bộ, giáo viên
                            thư viện giỏi cấp tỉnh năm học 2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details368">
                        </div>
                    </td>
                    <td valign="top">
                        17/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số 133/SGDĐT-CNTT-QLTB&amp;TV
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('369');">V/v Mời
                            dự khai mạc và bế mạc Hội thi “cán bộ, giáo viên thư viện giỏi” cấp tỉnh năm học
                            2011-2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details369">
                        </div>
                    </td>
                    <td valign="top">
                        17/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 05 /KH-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('307');">Thực hiện
                            "Năm an toàn giao thông 2012" </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details307">
                        </div>
                    </td>
                    <td valign="top">
                        10/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 103/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('305');">V/v hưởng
                            ứng Chiến dịch Giờ Trái Đất năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details305">
                        </div>
                    </td>
                    <td valign="top">
                        09/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 93 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('299');">V/v Hướng
                            dẫn tổ chức thi vòng 20 cuộc thi Olympic tiếng Anh trên Internet năm học 2011-2012
                            . </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details299">
                        </div>
                    </td>
                    <td valign="top">
                        08/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 82 /SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('298');">V/v viết
                            bài tham luận cho Hội thảo trao đổi kinh nghiệm “Nâng cao chất lượng sinh hoạt khoa
                            học ở các trường THCS và THPT” </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details298">
                        </div>
                    </td>
                    <td valign="top">
                        07/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 81/SGDĐT-GDTH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('323');">V/v triệu
                            tập giáo viên Tiếng Anh tham gia lớp ôn tập TOEFL-ITP </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details323">
                        </div>
                    </td>
                    <td valign="top">
                        06/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 19/BC-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('291');">Sơ kết
                            học kỳ I năm học 2011-2012 và triển khai nhiệm vụ trọng tâm học kỳ II năm học 2011
                            - 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details291">
                        </div>
                    </td>
                    <td valign="top">
                        02/02/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 58 /SGDĐT-GDMN
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('263');">V/v điều
                            chỉnh thời gian Hội nghị Sơ kết học kỳ I và triển khai phương hướng, nhiệm vụ học
                            kỳ II năm học 2011-2012 ngành học Mầm non và Tiểu học </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details263">
                        </div>
                    </td>
                    <td valign="top">
                        20/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 55/SGDĐT-GDMN-GDTH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('261');">V/v mời
                            dự Hội nghị sơ kết học kỳ I và triển khai phương hướng, nhiệm vụ học kỳ II năm học
                            2011-2012 ngành học Mầm non và Tiểu học </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details261">
                        </div>
                    </td>
                    <td valign="top">
                        19/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 56 /SGDĐT-KTQLCLGD
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('262');">V/v triệu
                            tập cán bộ, quản lý, giáo viên tham gia lớp tập huấn triển khai giới thiệu PISA
                            các tỉnh, thành phố </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details262">
                        </div>
                    </td>
                    <td valign="top">
                        19/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 52/SGDĐT-VP
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('260');">V/v Điều
                            chỉnh thời gian báo cáo sau Tết. </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details260">
                        </div>
                    </td>
                    <td valign="top">
                        18/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 50/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('252');">V/v Hội
                            nghị sơ kết học kì I năm học 2011-2012 GDTrH </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details252">
                        </div>
                    </td>
                    <td valign="top">
                        18/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số : 40/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('251');">V/v Báo
                            cáo số liệu HSSV tham gia Bảo hiểm y tế </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details251">
                        </div>
                    </td>
                    <td valign="top">
                        13/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số : 31/SGDĐT-GDTrH
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('248');">V/v Điều
                            chỉnh thời gian HN triển khai kế hoạch hưởng ứng Năm an toàn giao thông 2012
                        </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details248">
                        </div>
                    </td>
                    <td valign="top">
                        11/01/2012
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">
                        Số: 02-TB-SGDĐT
                    </td>
                    <td>
                        <a style="font-weight: bold" href="javascript: modvanban_showDetails('249');">V/v nghỉ
                            tết và công tác tổ chức đón tết Nguyên Đán Nhâm Thìn năm 2012 </a>
                        <div style="display: none; border-top: 1px dashed #336666; margin-top: 10px" class="vanban_details"
                            id="vanban_details249">
                        </div>
                    </td>
                    <td valign="top">
                        09/01/2012
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="fdtablePaginaterWrap fdtablePaginatorWrapBottom" id="vanbantb2443-fdtablePaginaterWrapBottom">
            <ul id="vanbantb2443-tablePaginaterClone" class="fdtablePaginater">
                <li>
                    <div class="first-page">
                        <span>«</span></div>
                </li>
                <li>
                    <div class="previous-page">
                        <span>‹</span></div>
                </li>
                <li><a href="#" title="Trang 1 trên 9" class="currentPage page-1" id="vanbantb2443-currentPageC">
                    <span>1</span></a></li><li><a href="#" title="Trang tiếp (Trang 2)" class="next-page"
                        id="vanbantb2443-nextPageC"><span>›</span></a></li><li><a href="#" title="Trang cuối (Trang 9)"
                            class="last-page"><span>»</span></a></li></ul>
        </div>
    </div>
</div>
