using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace VP.Sharepoint.CQ.Common
{
    public class AdvBO
    {
        public static Dictionary<string, string> AdvPosition = new Dictionary<string, string> { {"advhomeleft1","Trang chủ - bên trái - thứ nhất"},
                                                                                                {"advhomeleft2","Trang chủ - bên trái - thứ 2"},
                                                                                                {"advhomeleft3","Trang chủ - bên trái - thứ 3"},
                                                                                                {"advhomeleft4","Trang chủ - bên trái - thứ 4"},
                                                                                                {"advhomeleft5","Trang chủ - bên trái - thứ 5"},
                                                                                                {"advhomeleft6","Trang chủ - bên trái - thứ 6"},
                                                                                                {"advhomecenter","Trang chủ - ở giữa"},
                                                                                                {"advhomeright","Trang chủ - bên phải"},
                                                                                                {"advnews","Trang tin - bên phải"},
                                                                                                {"advnewsdetail","Trang tin chi tiết - bên phải"},
                                                                                                {"advorganization","Trang sơ đồ tổ chức - bên phải"},
                                                                                                {"advlibrary","Trang thư viện file - bên phải"},
                                                                                                {"advlibrarydetail","Trang thư viện chi tiết - bên phải"},
                                                                                                {"advabout","Trang giới thiệu - bên phải"},
                                                                                                {"advdocument","Trang văn bản - bên phải"},
                                                                                                {"advstatistic","Trang thống kê - bên phải"}};
        /// <summary>
        /// BindAdv
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <param name="rptMenu"></param>
        /// <param name="menuPosition"></param>
        public static void BindAdv(SPWeb web, string listName, Repeater rptAdv, string advPosition) {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where>
                                                <And>
                                                    <Eq>
                                                        <FieldRef Name='{0}' />
                                                        <Value Type='Choice'>{1}</Value>
                                                    </Eq>
                                                    <And>
                                                        <Eq>
                                                            <FieldRef Name='{2}' />
                                                            <Value Type='Choice'>Hiện</Value>
                                                        </Eq>
                                                        <And>
                                                            <Leq>
                                                                <FieldRef Name='{3}' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{4}</Value>
                                                            </Leq>
                                                            <Geq>
                                                                <FieldRef Name='{5}' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>{4}</Value>
                                                            </Geq>
                                                         </And>
                                                    </And>
                                                </And>
                                            </Where>
                                            <OrderBy><FieldRef Name='{6}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, 
                                                                                    FieldsName.AdvList.InternalName.AdvPosition,
                                                                                    AdvPosition[advPosition], 
                                                                                    FieldsName.AdvList.InternalName.AdvStatus,
                                                                                    FieldsName.AdvList.InternalName.AdvStartDate,
                                                                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now),
                                                                                    FieldsName.AdvList.InternalName.AdvEndDate,
                                                                                    FieldsName.AdvList.InternalName.AdvOrder)
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, listName);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                rptAdv.DataSource = items.GetDataTable();
                                rptAdv.DataBind();
                            }
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
        }


        public static void UpdateAdv(SPWeb web, string listName, string advID, HttpContext ctx, ref string advUrl, ref string advOpen)
        {
            var advUrlTemp = string.Empty;
            var advOpenTemp = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where>
                                                <Eq>
                                                    <FieldRef Name='{0}' />
                                                    <Value Type='Text'>{1}</Value>
                                                </Eq>
                                            </Where>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml,
                                                                                    FieldsName.AdvList.InternalName.AdvID,
                                                                                    advID)
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, listName);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                var item = items[0];
                                var newOrder = Utilities.ConvertToInt(Convert.ToString(item[FieldsName.AdvList.InternalName.AdvOrder])) + 1;
                                item[FieldsName.AdvList.InternalName.AdvOrder] = newOrder;
                                adminWeb.AllowUnsafeUpdates = true;
                                item.SystemUpdate(false);
                                advUrlTemp = Convert.ToString(item[FieldsName.AdvList.InternalName.AdvUrl]);
                                advOpenTemp = Convert.ToString(item[FieldsName.AdvList.InternalName.AdvOpenType]);
                                if ("Mở cửa sổ mới".Equals(advOpenTemp))
                                {
                                    advOpenTemp = "_blank";
                                }
                                var qcList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.AdvStatisticList);
                                var qcItem = qcList.AddItem();
                                qcItem[FieldsName.AdvStatisticList.InternalName.Title] = item.Title;
                                qcItem[FieldsName.AdvStatisticList.InternalName.AdvID] = advID;
                                qcItem[FieldsName.AdvStatisticList.InternalName.UserIP] = ctx.Request.UserHostAddress;
                                qcItem[FieldsName.AdvStatisticList.InternalName.UserBrowser] = ctx.Request.Browser.Browser;
                                qcItem[FieldsName.AdvStatisticList.InternalName.UserUrl] = ctx.Request.Url.AbsoluteUri;
                                adminWeb.AllowUnsafeUpdates = true;
                                qcItem.Update();
                            }
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            advUrl = advUrlTemp;
            advOpen = advOpenTemp;
        }
    }
}
