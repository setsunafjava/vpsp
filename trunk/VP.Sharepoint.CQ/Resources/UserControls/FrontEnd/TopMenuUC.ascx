<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" CodeBehind="TopMenuUC.ascx.cs" Inherits="VP.Sharepoint.CQ.UserControls.TopMenuUC" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<div class="top_menu">
    <div class="menu">
        <ul id="nav">
            <li><a runat="server" id="aHome">Trang chủ</a></li>
            <asp:Repeater ID="rptMenu" runat="server">
                <ItemTemplate>
                    <li <asp:Literal ID="ltrStyle" runat="server"></asp:Literal>><a runat="server" id="aLink"></a>
                        <asp:Repeater ID="rptSubMenu" runat="server">
                            <HeaderTemplate><ul></HeaderTemplate>
                            <ItemTemplate>
                                <li><a runat="server" id="aLink"></a></li>
                            </ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                        </asp:Repeater>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="search">
        <input type="text" id="txtData" name="q" onkeypress="return BBEnterPress();" style="border: 0px;" />
        <a href="#">Tìm kiếm</a>
    </div>
    <div class="cleaner">
    </div>
</div>
<!-------------End top menu------------------------>
<div class="bg_bottom_top_menu">
    <div class="inner_content_bottom_topMenu">
        <div class="time_date">
            Hôm nay, ngày <div id="timeVP"></div></div>
        <div class="set_hompage">
            <a href="#">Đặt làm trang chủ</a></div>
        <div class="RSS">
            <a href="#">RSS</a></div>
        <div class="cleaner">
        </div>
    </div>
</div>

<script type="text/javascript">
    $('#nav > li').hover(onOver, onOut);
    function onOver() {
        $('#nav > li.current').each(function (index) {
            $(this).removeClass('current').addClass('current-temp');
        });
    };

    function onOut() {
        $('#nav > li.current-temp').each(function (index) {
            $(this).removeClass('current-temp').addClass('current');
        });
    };

    function urlencode(str) {



        var ret = str;



        ret = ret.toString();



        ret = encodeURIComponent(ret);



        ret = ret.replace(/%20/g, '+');



        ret = ret.replace(/%22/g, "");

        ret = ret.replace(/\'/g, "");

        ret = ret.replace(/%2F/g, "");

        ret = ret.replace(/%3C/g, "");

        ret = ret.replace(/%3E/g, "");

        ret = ret.replace(/%3F/g, "");

        ret = ret.replace(/%25/g, "");

        ret = ret.replace(/\*/g, "");

        ret = ret.replace(/%7C/g, "");



        return ret;

    }



    function timkiem() {

        var link;

        var tk = document.getElementById("txtData").value;

        if (tk == "") {

            link = "TimKiem.aspx?KeyWord=" + urlencode(tk);
        }

        else {

            link = "TimKiem.aspx?KeyWord=" + urlencode(tk);
        }

        //alert(link);

        location.href = link;

    }

    function ganValue(t) {

        document.getElementById("txtData").value = t;

    }
</script>

<script type="text/javascript">

var dateFormat = function () {
	var	token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
			val = String(val);
			len = len || 2;
			while (val.length < len) val = "0" + val;
			return val;
		};

	// Regexes and supporting functions are cached through closure
	return function (date, mask, utc) {
		var dF = dateFormat;

		// You can't provide utc if you skip other args (use the "UTC:" mask prefix)
		if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
			mask = date;
			date = undefined;
		}

		// Passing date through Date applies Date.parse, if necessary
		date = date ? new Date(date) : new Date;
		if (isNaN(date)) throw SyntaxError("invalid date");

		mask = String(dF.masks[mask] || mask || dF.masks["default"]);

		// Allow setting the utc argument via the mask
		if (mask.slice(0, 4) == "UTC:") {
			mask = mask.slice(4);
			utc = true;
		}

		var	_ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
				d:    d,
				dd:   pad(d),
				ddd:  dF.i18n.dayNames[D],
				dddd: dF.i18n.dayNames[D + 7],
				m:    m + 1,
				mm:   pad(m + 1),
				mmm:  dF.i18n.monthNames[m],
				mmmm: dF.i18n.monthNames[m + 12],
				yy:   String(y).slice(2),
				yyyy: y,
				h:    H % 12 || 12,
				hh:   pad(H % 12 || 12),
				H:    H,
				HH:   pad(H),
				M:    M,
				MM:   pad(M),
				s:    s,
				ss:   pad(s),
				l:    pad(L, 3),
				L:    pad(L > 99 ? Math.round(L / 10) : L),
				t:    H < 12 ? "a"  : "p",
				tt:   H < 12 ? "am" : "pm",
				T:    H < 12 ? "A"  : "P",
				TT:   H < 12 ? "AM" : "PM",
				Z:    utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
				o:    (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
				S:    ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

		return mask.replace(token, function ($0) {
			return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
		});
	};
}();

// Some common format strings
dateFormat.masks = {
	"default":      "ddd mmm dd yyyy HH:MM:ss",
	shortDate:      "m/d/yy",
	mediumDate:     "mmm d, yyyy",
	longDate:       "mmmm d, yyyy",
	fullDate:       "dddd, mmmm d, yyyy",
	shortTime:      "h:MM TT",
	mediumTime:     "h:MM:ss TT",
	longTime:       "h:MM:ss TT Z",
	isoDate:        "yyyy-mm-dd",
	isoTime:        "HH:MM:ss",
	isoDateTime:    "yyyy-mm-dd'T'HH:MM:ss",
	isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
	dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
	monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
	return dateFormat(this, mask, utc);
};



document.getElementById("timeVP").innerHTML= dateFormat(new Date(), "dd/mm/yyyy, h:MM:ss TT");
</script>