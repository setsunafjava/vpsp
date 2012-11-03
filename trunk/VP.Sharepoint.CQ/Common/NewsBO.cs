using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Common
{
    public class NewsBO
    {
        public static Dictionary<string, string> BoxNewsPosition = new Dictionary<string, string> { {"boxnewshomebig1","Trang chủ - box tin to 1"},
                                                                                                    {"boxnewshomebig2","Trang chủ - box tin to 2"},
                                                                                                    {"boxnewshomesmall1","Trang chủ - box tin nhỏ 1"},
                                                                                                    {"boxnewshomesmall2","Trang chủ - box tin nhỏ 2"},
                                                                                                    {"boxnewshomesmall3","Trang chủ - box tin nhỏ 3"}};
        public static void BindRepeaterCat(SPWeb web,Repeater rpt,string listName, string newsPosition)
        {
            var newPos = BoxNewsPosition[newsPosition];
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Choice'>{1}</Value></Eq><Eq><FieldRef Name='{2}' /><Value Type='Choice'>{3}</Value></Eq></And></Where><OrderBy><FieldRef Name='{4}' /><FieldRef Name='{5}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.NewsPossition, newPos, FieldsName.CategoryList.InternalName.Type, "Tin tức", FieldsName.CategoryList.InternalName.CategoryLevel, FieldsName.CategoryList.InternalName.Order),
                                RowLimit=3                                
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, listName);
                            var items = list.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                rpt.DataSource = items.GetDataTable();
                                rpt.DataBind();
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
