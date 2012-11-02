using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using System.Globalization;

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
        /// BindMenu
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <param name="rptMenu"></param>
        /// <param name="menuPosition"></param>
        public static void BindMenu(SPWeb web, string listName, Repeater rptMenu, string menuPosition) {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><And><IsNull><FieldRef Name='{0}' /></IsNull><Eq><FieldRef Name='{1}' /><Value Type='MultiChoice'>{2}</Value></Eq></And></Where><OrderBy><FieldRef Name='{3}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.MenuList.InternalName.ParentID, FieldsName.MenuList.InternalName.MenuPosition, menuPosition, FieldsName.MenuList.InternalName.MenuOrder)
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, listName);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                rptMenu.DataSource = items.GetDataTable();
                                rptMenu.DataBind();
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

        /// <summary>
        /// BindMenu
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <param name="rptMenu"></param>
        /// <param name="menuPosition"></param>
        /// <param name="menuParent"></param>
        public static void BindMenu(SPWeb web, string listName, Repeater rptMenu, string menuPosition, string menuParent)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq><Eq><FieldRef Name='{2}' /><Value Type='MultiChoice'>{3}</Value></Eq></And></Where><OrderBy><FieldRef Name='{4}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.MenuList.InternalName.ParentID, menuParent, FieldsName.MenuList.InternalName.MenuPosition, menuPosition, FieldsName.MenuList.InternalName.MenuOrder)
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, listName);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                rptMenu.DataSource = items.GetDataTable();
                                rptMenu.DataBind();
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
    }
}
