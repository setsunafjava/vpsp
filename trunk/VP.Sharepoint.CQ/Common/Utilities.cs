﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using VP.Sharepoint.CQ.Core.Helpers;
using VP.Sharepoint.CQ.Core.WebParts;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls.WebParts;

namespace VP.Sharepoint.CQ.Common
{
    public static class Utilities
    {
        /// <summary>
        /// Create a standard SharePoint view and add to given list
        /// </summary>
        /// <param name="list">List instance that contains added view</param>
        /// <param name="viewTitle">Title</param>
        /// <param name="viewFields">ViewField</param>
        /// <param name="query">FilterandGroupby</param>
        /// <param name="rowLimit">RowLimit</param>
        /// <param name="makeDefaultView">IsDefaultView</param>
        public static void AddStandardView(SPList list, string viewTitle, string[] viewFields, string query, int rowLimit, bool makeDefaultView)
        {
            AddStandardView(list, viewTitle, viewFields, query, rowLimit, makeDefaultView, String.Empty);
        }

        /// <summary>
        /// Create a standard SharePoint view and add to given list
        /// </summary>
        /// <param name="list">List instance that contains added view</param>
        /// <param name="viewTitle"></param>
        /// <param name="viewFields">Fields display on view</param>
        /// <param name="query">Filter and Groupby query</param>
        /// <param name="rowLimit"></param>
        /// <param name="makeDefaultView">TRUE to make view is default view of list</param>
        /// <param name="aggregations">Aggregation Expression String</param>
        public static void AddStandardView(SPList list, string viewTitle, string[] viewFields, string query, int rowLimit, bool makeDefaultView, String aggregations)
        {
            SPViewCollection availableViews = list.Views;
            SPView view = null;

            try
            {
                view = availableViews[viewTitle];

                // If view exsited, update new view fields
                if (viewFields != null)
                {
                    view.ViewFields.DeleteAll();
                    foreach (string viewField in viewFields)
                        view.ViewFields.Add(viewField);
                }
            }
            catch
            {
                // If view not exsited, 
                // Create new view
                StringCollection colViewCollection = new StringCollection();
                colViewCollection.AddRange(viewFields);
                view = availableViews.Add(viewTitle, colViewCollection, null, (uint)rowLimit, true, makeDefaultView);
            }

            if (view != null)
            {
                // Update view filter
                if (!string.IsNullOrEmpty(query))
                {
                    view.Query = query;
                }

                view.RowLimit = (uint)rowLimit;
                view.DefaultView = makeDefaultView;
                if (!String.IsNullOrEmpty(aggregations))
                {
                    view.Aggregations = aggregations;
                    view.AggregationsStatus = "No";
                }

                view.Update();
            }
        }

        /// <summary>
        /// Log exception to SharePoint log
        /// </summary>
        /// <param name="message"></param>
        public static void LogToULS(string message)
        {
            SPDiagnosticsService diagnosticsService = SPDiagnosticsService.Local;
            SPDiagnosticsCategory category = diagnosticsService.Areas["SharePoint Foundation"].Categories["General"];
            diagnosticsService.WriteTrace(93, category, TraceSeverity.High, "VP.Sharepoint.CQ: " + message, null);
        }

        /// <summary>
        /// Log exception to SharePoint log
        /// </summary>
        /// <param name="ex"></param>
        public static void LogToULS(Exception ex)
        {
            SPDiagnosticsService diagnosticsService = SPDiagnosticsService.Local;
            SPDiagnosticsCategory category = diagnosticsService.Areas["SharePoint Foundation"].Categories["General"];
            diagnosticsService.WriteTrace(93, category, TraceSeverity.High, "VP.Sharepoint.CQ: " + ex.ToString(), null);
        }

        /// <summary>
        /// Create a new empty SharePoint application page
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="pageName">Page name (include .aspx)</param>
        /// <returns></returns>
        public static SPFile CreateEmptySharePointSitePage(SPWeb web, string appCode, string pageName)
        {
            string listName = appCode + "SitePage";
            SPList sitePages = GetLibraryListByUrl(web, listName);
            
            if (sitePages == null)
            {
                Guid listId = web.Lists.Add(listName, string.Empty, SPListTemplateType.WebPageLibrary);
                sitePages = web.Lists[listId];
            }

            string pageUrl = web.Url + "/" + listName + "/" + pageName;

            SPFile newAspxPage = web.GetFile(pageUrl);

            if (!newAspxPage.Exists)
            {
                string microsoftAssemblyName = "Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c";
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<%@ Page language=\"C#\" MasterPageFile=\"~masterurl/default.master\" Inherits=\"{0}\" meta:webpartpageexpansion=\"full\" %>", microsoftAssemblyName);
                sb.AppendLine("<%@ Register Tagprefix=\"SharePoint\" Namespace=\"Microsoft.SharePoint.WebControls\" Assembly=\"Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
                sb.AppendLine("<%@ Register Tagprefix=\"Utilities\" Namespace=\"Microsoft.SharePoint.Utilities\" Assembly=\"Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
                sb.AppendLine("<%@ Import Namespace=\"Microsoft.SharePoint\" %>");
                sb.AppendLine("<%@ Assembly Name=\"Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");
                sb.AppendLine("<%@ Register Tagprefix=\"WebPartPages\" Namespace=\"Microsoft.SharePoint.WebPartPages\" Assembly=\"Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" %>");

                sb.AppendLine("<asp:Content ID=\"PlaceHolderPageTitle\" ContentPlaceHolderId=\"PlaceHolderPageTitle\" runat=\"server\"></asp:Content>");
                sb.AppendLine("<asp:Content ID=\"PlaceHolderPageTitleInTitleArea\" ContentPlaceHolderId=\"PlaceHolderPageTitleInTitleArea\" runat=\"server\"></asp:Content>");
                sb.AppendLine("<asp:Content ID=\"PlaceHolderPageDescription\" ContentPlaceHolderId=\"PlaceHolderPageDescription\" runat=\"server\"></asp:Content>");
                sb.AppendLine("<asp:Content ID=\"PlaceHolderMain\" ContentPlaceHolderId=\"PlaceHolderMain\" runat=\"server\">");
                sb.AppendLine("<SharePoint:ScriptLink Name=\"SP.UI.Dialog.js\" runat=\"server\" OnDemand=\"true\" Localizable=\"false\" />");
                sb.AppendLine("<SharePoint:ScriptLink Name=\"SP.Ribbon.js\" runat=\"server\" OnDemand=\"true\" Localizable=\"false\" />");
                sb.AppendLine("<table cellpadding=\"4\" cellspacing=\"0\" border=\"0\" width=\"100%\">");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td id=\"invisibleIfEmpty\" name=\"_invisibleIfEmpty\" valign=\"top\" width=\"100%\">");
                sb.AppendLine("<WebPartPages:WebPartZone runat=\"server\" Title=\"loc:Main\" ID=\"Main\" FrameType=\"TitleBarOnly\"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</asp:Content>");

                newAspxPage = sitePages.RootFolder.Files.Add(pageUrl, Encoding.UTF8.GetBytes(sb.ToString()));
            }

            return newAspxPage;
        }

        /// <summary>
        /// Add permission to list
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="list">Current list</param>        
        /// <param name="groupsAndPermissions">Group & Permission</param>
        public static void AddPermissionForList(SPWeb web, SPList list, Dictionary<string, SPRoleType> groupsAndPermissions)
        {
            if (groupsAndPermissions.Count <= 0) return;

            if (!list.HasUniqueRoleAssignments)
            {
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }
            else
            {
                web.AllowUnsafeUpdates = true;
                list.ResetRoleInheritance();
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }

            SPRoleAssignmentCollection roleAssignments = list.RoleAssignments;

            foreach (var item in groupsAndPermissions)
            {
                try
                {
                    SPRoleAssignment roleAssignment = new SPRoleAssignment(web.SiteGroups[item.Key]);
                    SPRoleDefinition roleDefinition = web.RoleDefinitions.GetByType(item.Value);
                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);

                    roleAssignments.Add(roleAssignment);
                }
                catch
                {
                    continue;
                }
            }

            web.AllowUnsafeUpdates = true;
            list.Update();
        }

        /// <summary>
        /// Apply permission for list
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="list">Current list</param>
        /// <param name="groupsAndPermissions">Array ListItem: Value - Permission Level; Text - Group Name</param>
        public static void AplyPermissionForList(SPWeb web, SPList list, params System.Web.UI.WebControls.ListItem[] groupsAndPermissions)
        {
            if (groupsAndPermissions.Length <= 0) return;

            if (!list.HasUniqueRoleAssignments)
            {
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }
            else
            {
                web.AllowUnsafeUpdates = true;
                list.ResetRoleInheritance();
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }

            SPRoleAssignmentCollection roleAssignments = list.RoleAssignments;

            foreach (System.Web.UI.WebControls.ListItem item in groupsAndPermissions)
            {
                try
                {
                    SPRoleAssignment roleAssignment = new SPRoleAssignment(web.SiteGroups[item.Text]);
                    SPRoleDefinition roleDefinition = web.RoleDefinitions[item.Value];
                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);

                    roleAssignments.Add(roleAssignment);
                }
                catch
                {
                    continue;
                }
            }

            web.AllowUnsafeUpdates = true;
            list.Update();
        }

        /// <summary>
        /// Add permission to list
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="list">Current list</param>        
        /// <param name="groupsAndPermissions">Group & Permission(Permission Name - String Or Type - SPRoleType)</param>
        public static void AddPermissionForList(SPWeb web, SPList list, Dictionary<string, object> groupsAndPermissions)
        {
            if (groupsAndPermissions.Count <= 0) return;

            if (!list.HasUniqueRoleAssignments)
            {
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }
            else
            {
                web.AllowUnsafeUpdates = true;
                list.ResetRoleInheritance();
                web.AllowUnsafeUpdates = true;
                list.BreakRoleInheritance(false);
            }

            SPRoleAssignmentCollection roleAssignments = list.RoleAssignments;

            foreach (var item in groupsAndPermissions)
            {
                try
                {
                    SPRoleAssignment roleAssignment = new SPRoleAssignment(web.SiteGroups[item.Key]);
                    SPRoleDefinition roleDefinition;

                    Type typeValue = item.Value.GetType();
                    if (typeValue.ToString().Equals("System.String", StringComparison.OrdinalIgnoreCase))
                    {
                        roleDefinition = web.RoleDefinitions[item.Value.ToString()];
                    }
                    else
                    {
                        roleDefinition = web.RoleDefinitions.GetByType((SPRoleType)item.Value);
                    }

                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);

                    roleAssignments.Add(roleAssignment);
                }
                catch
                {
                    continue;
                }
            }

            web.AllowUnsafeUpdates = true;
            list.Update();
        }
                
        /// <summary>
        /// Add Permission For Group
        /// </summary>
        /// <param name="web">current web</param>
        /// <param name="groupName">group name</param>
        /// <param name="type">Role type</param>
        public static void AddPermissionForGroup(SPWeb web, string groupName, SPRoleType type)
        {
            SPRoleDefinition roleDefinition = web.RoleDefinitions.GetByType(type);

            var assignment = new SPRoleAssignment(web.SiteGroups[groupName]);
            assignment.RoleDefinitionBindings.Add(roleDefinition);

            web.RoleAssignments.Add(assignment);
        }

        /// <summary>
        /// Add Permission For Group
        /// </summary>
        /// <param name="web">current web</param>
        /// <param name="groupName">group name</param>
        /// <param name="permissionLevel">permission level name</param>
        public static void AddPermissionForGroup(SPWeb web, string groupName, string permissionLevel)
        {
            SPRoleDefinition roleDefinition = web.RoleDefinitions[permissionLevel];

            var assignment = new SPRoleAssignment(web.SiteGroups[groupName]);
            assignment.RoleDefinitionBindings.Add(roleDefinition);

            web.RoleAssignments.Add(assignment);
        }

        /// <summary>
        /// Get value in resource file by key
        /// </summary>
        /// <param name="resourceFileName">Resource File Name</param>
        /// <param name="resourceKey">Resource Key</param>
        /// <returns></returns>
        public static string GetMessageFromResourceFile(string resourceFileName, string resourceKey)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;

            return SPUtility.GetLocalizedString(string.Format(CultureInfo.InvariantCulture, "$Resources:{0}", resourceKey), resourceFileName, (uint)lang);
        }

        /// <summary>
        ///   Convert <see cref = "SPFieldUserValueCollection" /> and <see cref = "SPFieldLookupValueCollection" /> object to string.
        /// </summary>
        /// <param name = "obj"></param>
        /// <returns></returns>
        public static string ConvertLookupToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (obj is SPFieldUserValueCollection)
            {
                var users = (SPFieldUserValueCollection)obj;
                return string.Join(";#", users.Select(item => item.LookupId + ";#" + item.LookupValue).ToArray());
            }

            if (obj is SPFieldLookupValueCollection)
            {
                var values = (SPFieldLookupValueCollection)obj;
                return string.Join(";#", values.Select(item => item.LookupId + ";#" + item.LookupValue).ToArray());
            }

            return obj.ToString();
        }

        /// <summary>
        ///   Remove Lookup Id from string or <see cref = "SPFieldUserValueCollection" />, <see cref = "SPFieldLookupValueCollection" />
        /// </summary>
        /// <param name = "obj"></param>
        /// <returns></returns>
        public static string RemoveLookupId(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (obj is SPFieldUserValueCollection)
            {
                var users = (SPFieldUserValueCollection)obj;
                return string.Join(";#", users.Select(item => item.LookupValue).ToArray());
            }

            if (obj is SPFieldLookupValueCollection)
            {
                var values = (SPFieldLookupValueCollection)obj;
                return string.Join(";#", values.Select(item => item.LookupValue).ToArray());
            }

            var split = obj.ToString().Split(new[] { ";#" }, StringSplitOptions.None);
            return string.Join(";#", split.Where((item, i) => i % 2 != 0).ToArray());
        }

        /// <summary>
        /// Cast object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Cast<T>(object obj)
        {
            return (T)obj;
        }

        /// <summary>
        /// Trim space of string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Trim(string value)
        {
            return value == null ? value : value.Trim();
        }

        /// <summary>
        /// Serialize given object into XmlElement.
        /// </summary>
        /// <param name="transformObject">Input object for serialization.</param>
        /// <returns>Returns serialized XmlElement.</returns>
        public static XmlElement Serialize(object transformObject)
        {
            XmlElement serializedElement = null;
            MemoryStream memStream = new MemoryStream();
            try
            {
                XmlSerializer serializer = new XmlSerializer(transformObject.GetType());
                serializer.Serialize(memStream, transformObject);
                memStream.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(memStream);
                serializedElement = xmlDoc.DocumentElement;
            }
            catch (Exception serializeException)
            {
                Utilities.LogToULS(serializeException.Message);
            }
            finally
            {
                memStream.Close();
            }
            return serializedElement;
        }

        /// <summary>
        /// Deserialize given XmlElement into object.
        /// </summary>
        /// <param name="xmlElement">xmlElement to deserialize.</param>
        /// <param name="tp">Type of resultant deserialized object.</param>
        /// <returns>Returns deserialized object.</returns>
        public static object Deserialize(XmlElement xmlElement, System.Type tp)
        {
            Object transformedObject = null;
            Stream memStream = StringToStream(xmlElement.OuterXml);
            try
            {
                XmlSerializer serializer = new XmlSerializer(tp);
                transformedObject = serializer.Deserialize(memStream);
            }
            catch (Exception deserializeException)
            {
                Utilities.LogToULS(deserializeException.Message);
            }
            finally
            {
                memStream.Close();
            }

            return transformedObject;
        }

        /// <summary>
        /// Conversion from string to stream.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>Returns stream.</returns>
        public static Stream StringToStream(String str)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);//new byte[str.Length];
                memStream = new MemoryStream(buffer);
            }
            catch (Exception stringToStreamException)
            {
                Utilities.LogToULS(stringToStreamException.Message);
            }
            finally
            {
                memStream.Position = 0;
            }

            return memStream;
        }

        /// <summary>
        /// Convert a string to datetime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string value)
        {
            if (string.IsNullOrEmpty(value)) return DateTime.MinValue;

            DateTime datetime;
            if (DateTime.TryParse(value, out datetime))
                return datetime;

            return DateTime.MinValue;
        }

        /// <summary>
        /// Convert a object date value to datetime
        /// </summary>
        /// <param name="value">Object Date Value</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(object value)
        {
            string dateValue = Convert.ToString(value, CultureInfo.InvariantCulture);

            return ConvertToDateTime(dateValue);
        }

        /// <summary>
        /// The function to convert a datetime object to string
        /// </summary>
        /// <param name="value">Obj datetime</param>
        /// <param name="format">Format of result</param>
        /// <returns></returns>
        public static string ConvertDateValueToString(object value, string format)
        {
            if (value == null) return string.Empty;

            DateTime dateValue = ConvertToDateTime(value);

            if (dateValue == DateTime.MinValue) return string.Empty;

            return dateValue.ToString(format, CultureInfo.InvariantCulture);

        }

        /// <summary>
        /// The fuction to Endcode text
        /// </summary>
        /// <param name="obj">The text object that will be encode</param>
        /// <returns></returns>
        public static string EncodeText(object obj)
        {
            string value = Convert.ToString(obj, CultureInfo.InvariantCulture);

            return string.IsNullOrEmpty(value) ? string.Empty : SPEncode.HtmlEncode(value);
        }

        /// <summary>
        /// This function to upload a file to document library
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="sourceFilePath">Physical part of file</param>
        /// <param name="targetDocumentLibraryPath">Url of file when upload to document library, ex: http://cureentWebUrl/DocLib/FileName </param>
        /// <returns></returns>
        public static SPFile UploadFileToDocumentLibrary(SPWeb web, string sourceFilePath, string targetDocumentLibraryPath)
        {
            // Create buffer to transfer file
            byte[] fileBuffer = new byte[1024];
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //Load the content from local file to stream
                    using (FileStream fsWorkbook = File.Open(sourceFilePath, FileMode.Open, FileAccess.Read))
                    {
                        //Get the start point
                        int startBuffer = fsWorkbook.Read(fileBuffer, 0, fileBuffer.Length);
                        for (int i = startBuffer; i > 0; i = fsWorkbook.Read(fileBuffer, 0, fileBuffer.Length))
                        {
                            stream.Write(fileBuffer, 0, i);
                        }
                    }

                    web.AllowUnsafeUpdates = true;
                    return web.Files.Add(targetDocumentLibraryPath, stream.ToArray(), true);
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
                throw;
            }
        }

        /// <summary>
        /// This function is used to upload resource files in application
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetDocumentLibraryPath"></param>
        /// <param name="web"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void UploadResourceFiles(string sourceFilePath, string targetDocumentLibraryPath, SPWeb web)
        {
            try
            {
                SPFile file = UploadFileToDocumentLibrary(web, sourceFilePath, targetDocumentLibraryPath);

                if (file == null) return;

                web.AllowUnsafeUpdates = true;
                file.Item.Update();
 }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
                throw;
            }
        }

        /// <summary>
        /// Create document library list to store resources: JS, CSS, IMAGE
        /// </summary>
        /// <param name="web">Current Web</param>
        public static void CreateDocLibToStoreResources(SPWeb web)
        {
            SPList sitePages = GetLibraryListByUrl(web, ListsName.InternalName.ResourcesList);
            
            if (sitePages == null)
            {
                web.Lists.Add(ListsName.InternalName.ResourcesList, string.Empty, SPListTemplateType.DocumentLibrary);
            }
        }

        /// <summary>
        /// Add UserControl to form of list
        /// </summary>
        /// <param name="web"></param>
        /// <param name="list"></param>
        /// <param name="userControlPath"></param>
        public static void AddForms(SPWeb web, SPList list, string userControlPath)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);

            if (containerWebPart != null)
            {
                containerWebPart.Title = string.Format("{0} - Custom Form", list.Title);
                containerWebPart.UserControlPath = userControlPath;
                WebPartHelper.AddWebPartToNewPage(web, list, containerWebPart);
                WebPartHelper.AddWebPartToEditPage(web, list, containerWebPart);
                WebPartHelper.AddWebPartToDisplayPage(web, list, containerWebPart);
            }

            WebPartHelper.HideDefaultWebPartOnNewPage(web, list);
            WebPartHelper.HideDefaultWebPartOnEditPage(web, list);
            WebPartHelper.HideDefaultWebPartOnDisplayPage(web, list);
        }

        /// <summary>
        /// Add UserControl to form of list
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="list">Custom List that is customized form</param>
        /// <param name="formType">Type of Form: 0-New, 1-Edit, 2-Display</param>
        /// <param name="userControlPath">Path of custom UserControl</param>
        public static void AddForms(SPWeb web, SPList list, int formType, string userControlPath)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);

            if (containerWebPart != null)
            {
                containerWebPart.Title = string.Format("{0} - Custom Form", list.Title);
                containerWebPart.UserControlPath = userControlPath;
            }

            switch (formType)
            {
                case 0:
                    WebPartHelper.AddWebPartToNewPage(web, list, containerWebPart);
                    WebPartHelper.HideDefaultWebPartOnNewPage(web, list);
                    break;
                case 1:
                    WebPartHelper.AddWebPartToEditPage(web, list, containerWebPart);
                    WebPartHelper.HideDefaultWebPartOnEditPage(web, list);
                    break;
                case 2:
                    WebPartHelper.AddWebPartToDisplayPage(web, list, containerWebPart);
                    WebPartHelper.HideDefaultWebPartOnDisplayPage(web, list);
                    break;
            }
        }

        /// <summary>
        /// This function to close form of list
        /// </summary>
        /// <param name="page"></param>
        public static void CloseForm(Page page)
        {
            var defaultView = string.Concat(SPContext.Current.Web.Url, "/", SPContext.Current.List.DefaultView.Url);
            try
            {
                var IsDlg = Convert.ToString(page.Request.Params["IsDlg"]);
                if (!string.IsNullOrEmpty(IsDlg) && IsDlg.Equals("1"))
                {
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "req", "window.frameElement.commitPopup();", true);
                }
                else
                {
                    var source = Convert.ToString(page.Request.QueryString["Source"]);
                    if (!string.IsNullOrEmpty(source))
                    {
                        SPUtility.Redirect(SPEncode.UrlDecodeAsUrl(source), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                    else
                        SPUtility.Redirect(SPEncode.UrlDecodeAsUrl(defaultView), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                SPUtility.Redirect(SPEncode.UrlDecodeAsUrl(defaultView), SPRedirectFlags.Static, HttpContext.Current);
            }
        }

        /// <summary>
        /// Set to to field of view
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="internalName">Internal Name of field</param>
        public static void SetMenuLink(SPList list, string internalName)
        {
            var field = list.Fields.GetFieldByInternalName(internalName);

            if (field == null) return;

            field.LinkToItem = true;
            field.ListItemMenu = true;
            field.LinkToItemAllowed = SPField.ListItemMenuState.Required;
            field.ListItemMenuAllowed = SPField.ListItemMenuState.Required;
            field.Update();
        }

        /// <summary>
        /// The function to get list by Url - English name
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="listUrl">URL of list - English name</param>
        public static SPList GetCustomListByUrl(SPWeb web, string listUrl)
        {
            SPList list = null;
            string url = web.Url + "/Lists/" + listUrl;

            try
            {
                list = web.GetList(url);
            }
            catch (FileNotFoundException ex)
            {
                Utilities.LogToULS(ex);                
            }

            return list;
        }

        /// <summary>
        /// The function to get library list by Url - English name
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="listUrl">URL of list - English name</param>
        public static SPList GetLibraryListByUrl(SPWeb web, string listUrl)
        {
            SPList list = null;
            string url = web.Url + "/" + listUrl;

            try
            {
                list = web.GetList(url);
            }
            catch (FileNotFoundException ex)
            {
                Utilities.LogToULS(ex);
            }

            return list;
        }

        /// <summary>
        /// Get User by field - Single
        /// </summary>
        /// <param name="item">Current SPListItem</param>
        /// <param name="internalName">Internal Name of field</param>
        /// <returns></returns>
        public static SPUser GetUserByField(SPListItem item, string internalName)
        {
            string userValue = Convert.ToString(item[internalName], CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(userValue)) return null;

            SPFieldUserValue spUserValue = new SPFieldUserValue(item.Web, userValue);

            if (spUserValue == null) return null;

            return spUserValue.User;
        }

        /// <summary>
        /// Get Users by field - Multi
        /// </summary>
        /// <param name="item">Current SPListItem</param>
        /// <param name="internalName">Internal Name of field</param>
        /// <returns></returns>
        public static List<SPUser> GetUsersByField(SPListItem item, string internalName)
        {
            string userValue = Convert.ToString(item[internalName], CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(userValue)) return null;

            SPFieldUserValueCollection spUserValue = new SPFieldUserValueCollection(item.Web, userValue);

            if (spUserValue == null) return null;

            List<SPUser> result = new List<SPUser>();

            foreach (SPFieldUserValue user in spUserValue)
            {
                if (user.User != null)
                    result.Add(user.User);
            }

            return result;
        }

        /// <summary>
        /// The function to start WF
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="listUrl">Url of current list - English name</param>
        /// <param name="currentItemId">Id of current item</param>
        /// <param name="wfName">Name of WF</param>
        public static void StartWf(SPWeb web, string listUrl, int currentItemId, string wfName)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = site.OpenWeb(web.ID))
                    {
                        var list = GetCustomListByUrl(adminWeb, listUrl);

                        if (list == null) return;

                        var item = list.GetItemById(currentItemId);

                        if (item == null) return;

                        SPWorkflowManager workflowManager = site.WorkflowManager;
                        SPWorkflowAssociation assoc = list.WorkflowAssociations.GetAssociationByName(wfName, CultureInfo.InvariantCulture);

                        var allowUnsafeUpdates = SecurityHelper.SetAllowUnsafeUpdates(adminWeb);
                        try
                        {
                            workflowManager.StartWorkflow(item, assoc, assoc.AssociationData, SPWorkflowRunOptions.Asynchronous);
                        }
                        catch (SPException ex)
                        {
                            LogToULS(ex);
                            Thread.Sleep(5000);
                            var ontherItem = list.GetItemById(currentItemId);
                            workflowManager.StartWorkflow(ontherItem, assoc, assoc.AssociationData, SPWorkflowRunOptions.Asynchronous);
                        }
                        finally
                        {
                            SecurityHelper.RollbackAllowUnsafeUpdates(adminWeb, allowUnsafeUpdates);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Create events for list
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="className">Class Name of EventReceiver: [namespace.classname]</param>
        /// <param name="eventsType">All events for list</param>
        public static void CreateEventReceivers(SPList list, string className, params SPEventReceiverType[] eventsType)
        {
            SPEventReceiverDefinitionCollection events = list.EventReceivers;
            string assemblyFullName = Assembly.GetExecutingAssembly().FullName;

            foreach (SPEventReceiverType eventType in eventsType)
            {
                list.ParentWeb.AllowUnsafeUpdates = true;
                events.Add(eventType, assemblyFullName, className);
            }
        }

        /// <summary>
        /// Add a UserControl to Page
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageTitle">Title of page</param>
        /// <param name="userControlName">UserControl Name, exclude extension</param>
        public static void AddUserControlToPage(SPWeb web, SPFile page, string pageTitle, string userControlName)
        {
            ContainerWebPart containerWebPart = WebPartHelper.GetContainerWebPart(web);
            if (containerWebPart != null)
            {
                containerWebPart.Title = pageTitle;
                containerWebPart.UserControlPath = "../UserControls/" + userControlName + ".ascx";

                WebPartHelper.AddWebPart(web, page.Url, containerWebPart, "Main", 0);
            }
        }

        /// <summary>
        /// Get Url of Parent site from relative Url of list
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="listUrlInConfig">Relative of List</param>
        /// <param name="listUrl">Return english name of list</param>
        /// <returns>Return Url of parent site</returns>
        public static string GetParentSiteUrlByListUrl(SPWeb web, string listUrlInConfig, out string listUrl)
        {
            string webAppUrl = web.Site.RootWeb.Site.WebApplication.AlternateUrls[0].IncomingUrl;
            listUrl = string.Empty;

            if (string.IsNullOrEmpty(listUrlInConfig)) return string.Empty;

            string[] strs = listUrlInConfig.Split(new[] { "/Lists/" }, StringSplitOptions.None);

            if (strs.Length <= 0) return string.Empty;

            if (strs.Length == 1)
            {
                listUrl = strs[0];
                listUrl = listUrl.TrimEnd('/');
                return string.Concat(webAppUrl, "/");
            }

            listUrl = strs[1];
            listUrl = listUrl.TrimEnd('/');

            if (listUrlInConfig.StartsWith("http"))
                return strs[0];

            return string.Concat(webAppUrl, strs[0]);
        }

        /// <summary>
        /// This function to get User of current site, the user can don't exist previous
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="loginName">LogiName of User</param>
        /// <returns>SPUser obj</returns>
        public static SPUser GetUserCurrentSite(SPWeb web, string loginName)
        {
            SPUser user = null;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            user = adminWeb.EnsureUser(loginName);
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });

            return user;
        }

        /// <summary>
        /// AddPermissionForWeb
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="groupsAndPermissions">Group & Permission(Permission Name - String Or Type - SPRoleType)</param>
        public static void AddPermissionForWeb(SPWeb web, Dictionary<string, object> groupsAndPermissions)
        {
            if (groupsAndPermissions.Count <= 0) return;

            foreach (var item in groupsAndPermissions)
            {
                Type typeValue = item.Value.GetType();
                if (typeValue.ToString().Equals("System.String", StringComparison.OrdinalIgnoreCase))
                {
                    AddPermissionForGroup(web, item.Key, item.Value.ToString());
                }
                else
                {
                    AddPermissionForGroup(web, item.Key, (SPRoleType)item.Value);
                }
            }

            web.AllowUnsafeUpdates = true;
            web.Update();
        }
    }
}