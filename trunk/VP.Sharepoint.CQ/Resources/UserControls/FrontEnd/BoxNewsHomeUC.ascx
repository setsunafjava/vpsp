<%@ Assembly Name="VP.Sharepoint.CQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e4de45e7b80d7217" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoxNewsHomeUC.ascx.cs"
    Inherits="VP.Sharepoint.CQ.UserControls.BoxNewsHomeUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<div>
    <div class="hot_news-content">
        <div class="artical_hottest">
            <!-------------slide News------------------------>
            <div id="gallery">
                <a href="#" class="show">
                    <img src="images/images_slidenews/flowing-rock.jpg" alt="Flowing Rock" width="580"
                        height="360" title="" alt="" rel="" /><h3>
                            Hai nữ thủ khoa từng... trượt đại học</h3>
                    Các bạn ấy cũng đã từng thi trượt đại học rồi sau đó quyết tâm thi lại vào năm sau
                    và thi đậu với số điểm cao nhất. "/> </a><a href="#">
                        <img src="images/images_slidenews/grass-blades.jpg" alt="Grass Blades" width="580"
                            height="360" title="" alt="" rel="<h3>Đường tội lỗi của nữ giáo viên xuân sắc</h3>Người đàn bà đẹp được đào tạo ở nước ngoài, đứng trên bục giảng nhiều năm đâu ngờ có ngày lại giam thân trong chốn lao tù không biết ngày được trở về. " />
                    </a>
                <div class="caption">
                    <div class="content">
                    </div>
                </div>
            </div>
            <!-------------End Slide News------------------------>
        </div>
    </div>
    <div class="tab_content_News">
        <div class="info_tab_content">
            <ul id="countrytabs" class="shadetabs">
                <li><a href="#" rel="country1" class="selected">Mới nhất</a></li>
                <li><a href="#" rel="country2">Đọc nhiều</a></li>
            </ul>
            <div class="inner_content_tab">
                <div id="country1" class="tabcontent">
                    <ul>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Khai hội Yên Tử</a><span>(ngày 20/02/2012)</span></li>
                    </ul>
                </div>
                <div id="country2" class="tabcontent">
                    <ul>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                        <li><a href="#">Đẩy lùi tình trạng suy thoái đạo đức trong cán bộ</a><span>(ngày 20/02/2012)</span></li>
                    </ul>
                </div>
                <script type="text/javascript">

                    var countries = new ddtabcontent("countrytabs")
                    countries.setpersist(true)
                    countries.setselectedClassTarget("link") //"link" or "linkparent"
                    countries.init()
								
                </script>
                <div class="cleaner">
                </div>
            </div>
        </div>
    </div>
    <div class="cleaner">
    </div>
</div>
