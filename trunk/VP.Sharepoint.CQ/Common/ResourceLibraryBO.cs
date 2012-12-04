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
    public class ResourceLibraryBO
    {

        #region Bussiness for Resource library
        public static DataTable GetResourcesByCatId(SPWeb web, string catId)
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
                             SPList resourcesList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.ResourceLibrary);
                             SPList catList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                             GetResourcesByCatId(resourcesList, catId, ref dtTemp);
                             GetResourcesByCatId(catList, resourcesList, catId, ref dtTemp);
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
        public static void GetResourcesByCatId(SPList list,string catId,ref DataTable dt)
        {
            try
            {                

                //Get News
                string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.ResourceLibrary.InternalName.CategoryId,catId)
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

        public static void GetResourcesByCatId(SPList catList, SPList resourcesList, string catId, ref DataTable dt)
        {
            try
            {
                //Get Cat
                string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}'  Ascending='FALSE' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.ParentID, catId, FieldsName.CategoryList.InternalName.Order)                    
                };
                var items = catList.GetItems(query);
                if (items != null && items.Count > 0)
                {                    
                    foreach (SPListItem item in items)
                    {
                        GetResourcesByCatId(resourcesList, Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID]), ref dt);
                        GetResourcesByCatId(catList, resourcesList, Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID]), ref dt);
                    }
                }
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        #region GetDocumentsByCatId
        public static DataTable GetDocumentsByCatId(SPWeb web, string catId)
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
                            SPList documentList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.DocumentsList);
                            SPList catList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.CategoryList);
                            GetDocumentsByCatId(documentList, catId, ref dtTemp);
                            GetDocumentsByCatId(catList, documentList, catId, ref dtTemp);
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
        public static void GetDocumentsByCatId(SPList list, string catId, ref DataTable dt)
        {
            try
            {

                //Get document
                string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.DocumentsList.InternalName.CategoryId, catId)
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

        public static void GetDocumentsByCatId(SPList catList, SPList documentList, string catId, ref DataTable dt)
        {
            try
            {
                //Get Cat
                string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where><OrderBy><FieldRef Name='{2}' Ascending='FALSE' /></OrderBy>";
                var query = new SPQuery()
                {
                    Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.CategoryList.InternalName.ParentID, catId, FieldsName.CategoryList.InternalName.Order)
                };
                var items = catList.GetItems(query);
                if (items != null && items.Count > 0)
                {
                    foreach (SPListItem item in items)
                    {
                        GetDocumentsByCatId(documentList, Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID]), ref dt);
                        GetDocumentsByCatId(catList, documentList, Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID]), ref dt);
                    }
                }
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion

        #region Update number of download document   
        public static void UpdateDownloadCount(SPWeb web,string itemId)
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
                            SPList list = Utilities.GetCustomListByUrl(web, ListsName.InternalName.ResourceLibrary);
                            SPListItem listItem = list.GetItemById(Convert.ToInt32(itemId));

                            if (listItem != null)
                            {
                                int downloadcount = 0;
                                if (listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] != null && Convert.ToString(listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount]) != string.Empty)
                                {
                                    downloadcount = Convert.ToInt32(listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount]);
                                }
                                listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] = downloadcount + 1;
                                listItem.Update();
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
        }
        #endregion
    }
}
