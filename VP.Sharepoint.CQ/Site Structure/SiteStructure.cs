using System;
using System.IO;
using System.Security;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    /// <summary>
    /// SiteStructure
    /// </summary>
    [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
    [SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
    [SecuritySafeCritical]
    public class SiteStructure
    {
        #region Create Site Structure
        /// <summary>
        /// CreateSiteStructure
        /// </summary>
        /// <param name="web"></param>
        public static void CreateSiteStructure(SPWeb web)
        {
            //Upload file to doclib
            Console.WriteLine("Store resource: JS & CSS...");
            UpdateResources(web);
            //Create News list
            Console.WriteLine("Create News list");
            CreateNewsListStructre.CreateListStructure(web);
            MenuList.CreateListStructure(web);
            DocumentType.CreateListStructure(web);
            PublishPlace.CreateListStructure(web);
            DocumentSubject.CreateListStructure(web);
            SignaturePerson.CreateListStructure(web);
            DocumentsList.CreateListStructure(web);
            AlbumList.CreateListStructure(web);
            CategoryList.CreateListStructure(web);
            ImageLibrary.CreateListStructure(web);
            VideoLibrary.CreateListStructure(web);
            CreateDocLibs.CreateListStructure(web);
            AdvList.CreateListStructure(web);
            AdvStatisticList.CreateListStructure(web);
            IconLinkList.CreateListStructure(web);
            WebsiteLink.CreateListStructure(web);

            // Add QuichLaunch
            Console.WriteLine("Process CreateQuichLaunch..");
            QuickLaunchStructure.CreateQuickLaunch(web);

            ExternalNews.CreateListStructure(web);
            ExternalNewsLink.CreateListStructure(web);

            ResourceLibrary.CreateListStructure(web);
            StatisticsList.CreateListStructure(web);
            ProfilesList.CreateListStructure(web);
            
            Console.WriteLine("Deploy Successful!");
        }
        #endregion

        #region Create DocLib store JS & CSS
        /// <summary>
        /// UpdateResources
        /// </summary>
        /// <param name="web"></param>
        private static void UpdateResources(SPWeb web)
        {
            //Create resource list if not exists
            Utilities.CreateDocLibToStoreResources(web);
            // Upload files
            string resourcesPathCss = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\VP.Sharepoint.CQ\\Resources\\Css");
            string resourcesPathJavascript = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\VP.Sharepoint.CQ\\Resources\\Javascript");
            string resourcesPathImage = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\VP.Sharepoint.CQ\\Resources\\Images");
            string resourcesPathMasterPages = SPUtility.GetGenericSetupPath("TEMPLATE\\FEATURES\\VP.Sharepoint.CQ\\Resources\\MasterPages");
            UploadFileToDocumentLibrary(resourcesPathCss, web);
            UploadFileToDocumentLibrary(resourcesPathJavascript, web);
            UploadFileToDocumentLibrary(resourcesPathImage, web);
            UploadFileToDocumentLibrary(resourcesPathMasterPages, web);
        }
        /// <summary>
        /// UploadFileToDocumentLibrary
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetDocumentLibraryPath"></param>
        /// <param name="web"></param>
        /// <param name="name"></param>
        private static void UploadFileToDocumentLibrary(string sourceFilePath, string targetDocumentLibraryPath, SPWeb web, string name)
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
                    web.Files.Add(targetDocumentLibraryPath, stream.ToArray(), true);
                }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        /// <summary>
        /// UploadFileToDocumentLibrary
        /// </summary>
        /// <param name="sourceFolderPath"></param>
        /// <param name="web"></param>
        private static void UploadFileToDocumentLibrary(string sourceFolderPath, SPWeb web)
        {
            var sourceFolder = new DirectoryInfo(sourceFolderPath);
            var sourceFile = sourceFolder.GetFiles();
            if (sourceFile.Length > 0)
            {
                foreach (FileInfo file in sourceFolder.GetFiles())
                {
                    UploadFileToDocumentLibrary(file.FullName, string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", web.Url, ListsName.InternalName.ResourcesList, file.Name), web, file.Name);
                }
            }
        }
        #endregion
    }
}