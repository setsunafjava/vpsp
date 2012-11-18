using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.SharePoint;
using System.Data;

namespace VP.Sharepoint.CQ.Common
{
    public class NewsBO
    {
        public static Dictionary<string, string> BoxNewsPosition = new Dictionary<string, string> { {"boxnewshomebig1","Trang chủ - box tin to 1"},
                                                                                                    {"boxnewshomebig2","Trang chủ - box tin to 2"},
                                                                                                    {"boxnewshomesmall1","Trang chủ - box tin nhỏ 1"},
                                                                                                    {"boxnewshomesmall2","Trang chủ - box tin nhỏ 2"},
                                                                                                    {"boxnewshomesmall3","Trang chủ - box tin nhỏ 3"},
                                                                                                    {"boxgiaitri","Trang chủ - Chuyên mục giải trí"}};

        #region Bussiness for Category
        public static void BindRepeaterCat(SPWeb web,Repeater rpt,string listName, string newsPosition)
        {
            var newPos = newsPosition;
            if (BoxNewsPosition.ContainsKey(newsPosition))
            {
                newPos = BoxNewsPosition[newsPosition];
            }
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

        public static DataTable GetCategoryByStatus(SPWeb web, string catStatus, UInt32 rowLimit)
        {
            DataTable dt=null;
            //var newPos = BoxNewsPosition[newsPosition];\
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Choice'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' /><FieldRef Name='{3}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml,FieldsName.CategoryList.InternalName.Type,catStatus, FieldsName.CategoryList.InternalName.CategoryLevel, FieldsName.CategoryList.InternalName.Order),
                                RowLimit = rowLimit
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                            var items = list.GetItems(query);
                            dt = items.GetDataTable();
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return dt;
        }


        public static DataTable GetCategoryByParent(SPWeb web, string parentId)
        {
            DataTable dt = null;
            //var newPos = BoxNewsPosition[newsPosition];\
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' /><FieldRef Name='{3}' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.ParentID, parentId, FieldsName.CategoryList.InternalName.CategoryLevel, FieldsName.CategoryList.InternalName.Order),                                
                            };
                            var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                            var items = list.GetItems(query);
                            dt = items.GetDataTable();
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return dt;
        }       

        #endregion

        #region Bussiness for News
        public static DataTable GetNewsByCatId(SPWeb web, string catId)
        {
            DataTable dtTemp = null;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;                            
                             SPList newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                             SPList catList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                             GetNewsByCatId(newsList, catId, ref dtTemp);
                             GetNewsByCatId(catList, newsList,catId, ref dtTemp);
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return dtTemp;
        }

        public static DataTable GetNewsOtherByCatId(SPWeb web, string catId,int newsId)
        {
            DataTable dtTemp = null;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            SPList newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                            SPList catList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                            //GetOtherNewsByCatId(newsList, catId, newsId, ref dtTemp);
                            GetOtherNewsByCatId(newsList, catId, newsId, ref dtTemp);
                            GetNewsByCatId(catList, newsList, catId, ref dtTemp);
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return dtTemp;
        }
        public static void GetNewsByCatId(SPList list,string catId,ref DataTable dt)
        {
            try
            {                

                //Get News
                string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='TRUE' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.NewsList.InternalName.NewsGroup,catId)
                };
                var items = list.GetItems(query);
                if (items!=null&&items.Count>0)
                {
                    if (dt==null)
                    {
                        dt = items.GetDataTable().Clone();
                    }
                    foreach (DataRow dr in items.GetDataTable().Rows)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }

        public static void GetOtherNewsByCatId(SPList list, string catId,int newsId, ref DataTable dt)
        {
            try
            {

                //Get News
                string caml = @"<Where><And><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq><Neq><FieldRef Name='{2}' /><Value Type='Text'>{3}</Value></Neq></And></Where><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.NewsList.InternalName.NewsGroup, catId,"ID",newsId)
                };
                var items = list.GetItems(query);
                if (items != null && items.Count > 0)
                {
                    if (dt == null)
                    {
                        dt = items.GetDataTable().Clone();
                    }
                    foreach (DataRow dr in items.GetDataTable().Rows)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }

        public static void GetNewsByCatId(SPList catList, SPList newsList, string catId, ref DataTable dt)
        {
            try
            {
                //Get Cat
                string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' /><FieldRef Name='{3}' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.ParentID, catId, FieldsName.CategoryList.InternalName.Order,"ID")
                };
                var items = catList.GetItems(query);
                if (items != null && items.Count > 0)
                {                    
                    foreach (SPListItem item in items)
                    {
                        GetNewsByCatId(newsList, Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID]), ref dt);
                        GetNewsByCatId(catList, newsList, Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID]), ref dt);
                    }
                }
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        #region Update view count
        public static void UpdateViewCount(SPWeb web, SPListItem item)
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
                            web.AllowUnsafeUpdates = true;
                            SPList newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                            if (item!=null)
                            {
                                int viewCount = 0;
                                if (Convert.ToString(item[FieldsName.NewsList.InternalName.NewsCount]) != string.Empty)
                                {
                                    viewCount = Convert.ToInt32(item[FieldsName.NewsList.InternalName.NewsCount]);
                                }
                                item[FieldsName.NewsList.InternalName.NewsCount] = viewCount + 1;
                                item.Update();
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
        #endregion

        #region GetLatestNews
        public static DataTable GetLatestNews(SPWeb web)
        {
            DataTable dtTemp = null;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            SPList newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                            //Get News
                            string caml = @"<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = caml,                                
                            };
                            dtTemp = newsList.GetItems(query).GetDataTable();
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return dtTemp;
        }
        #endregion

        #region GetMostViewCount
        public static DataTable GetMostViewCount(SPWeb web)
        {
            DataTable dtTemp = null;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            SPList newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                            //Get News
                            string caml = @"<OrderBy><FieldRef Name='{0}' Ascending='FALSE' /></OrderBy>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(caml,FieldsName.NewsList.InternalName.NewsCount)
                            };
                            dtTemp = newsList.GetItems(query).GetDataTable();
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
            return dtTemp;
        }
        #endregion
    }
}
