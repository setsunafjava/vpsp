using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class FileDetailUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public string itemId = string.Empty;
        public string catId = string.Empty;
        public string title = string.Empty;
        public string author = string.Empty;
        public string sizeOfFile = string.Empty;
        public string postedDate = string.Empty;
        public string downloadCount = "0";
        public string fileName = string.Empty;
        public string imgThumb = string.Empty;
        public string urlDownload = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.Request.QueryString["ID"] != null && Page.Request.QueryString["ID"] != string.Empty)
                {
                    itemId = Convert.ToString(Page.Request.QueryString["ID"]);
                }

                if (Page.Request.QueryString["CatId"] != null && Page.Request.QueryString["CatId"] != string.Empty)
                {
                    catId = Convert.ToString(Page.Request.QueryString["CatId"]);
                }

                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                    {
                        using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                        {
                            try
                            {
                                adminWeb.AllowUnsafeUpdates = true;
                                SPList list = Utilities.GetCustomListByUrl(CurrentWeb, ListsName.InternalName.ResourceLibrary);
                                SPListItem listItem = list.GetItemById(Convert.ToInt32(itemId));
                                if (listItem != null)
                                {
                                    title = Convert.ToString(listItem[FieldsName.ResourceLibrary.InternalName.Title]);
                                    author = Convert.ToString(listItem[FieldsName.ResourceLibrary.InternalName.Author]);
                                    postedDate = Convert.ToDateTime(listItem[FieldsName.ResourceLibrary.InternalName.PostedDate]).ToString("dd/MM/yyyy");
                                    if (listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] != null && listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] != string.Empty)
                                        downloadCount = Convert.ToString(listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount]);
                                    urlDownload = listItem[FieldsName.ResourceLibrary.InternalName.FileUrl].ToString();
                                    SPFile OriFile = CurrentWeb.GetFile(listItem[FieldsName.ResourceLibrary.InternalName.FileUrl].ToString());
                                    sizeOfFile = string.Format("{0:0.00}",(decimal)OriFile.Length / 1048576);
                                    fileName = OriFile.Name;
                                    imgThumb = Convert.ToString(listItem[FieldsName.ResourceLibrary.InternalName.FileUrl]);

                                    ltrTitle.Text = title;
                                    ltrAuthor.Text = author;
                                    ltrDate.Text = postedDate;
                                    ltrDownloadCount.Text = downloadCount;
                                    ltrFileUrl.Text = fileName;
                                    ltrSize.Text = sizeOfFile;
                                    imgDownload.Src = DocLibUrl + "/images_download.jpg";
                                    imgAnh.Src = imgThumb;
                                    aDownload.Attributes.Add("onclick", "DownloadFile('" + urlDownload + "')");
                                }
                            }
                            catch (SPException ex)
                            {
                                Utilities.LogToULS(ex);
                            }
                        }
                    }
                });


                if (Page.Request.Params["__EVENTARGUMENT"] != null)
                    if (Convert.ToString(Page.Request.Params["__EVENTARGUMENT"]) == "UpdateDownloadCount")
                    {
                        UpdateDownloadCount();                        
                    }
            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }
        #endregion
        protected void UpdateDownloadCount()
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(CurrentWeb.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(CurrentWeb.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            CurrentWeb.AllowUnsafeUpdates = true;
                            SPList list = Utilities.GetCustomListByUrl(CurrentWeb, ListsName.InternalName.ResourceLibrary);
                            SPListItem listItem = list.GetItemById(Convert.ToInt32(itemId));

                            if (listItem != null)
                            {
                                int downloadcount = 0;
                                if (listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] != null && listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] != string.Empty)
                                {
                                    downloadcount = Convert.ToInt32(listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount]);
                                }
                                listItem[FieldsName.ResourceLibrary.InternalName.DownloadCount] = downloadcount + 1;
                                listItem.Update();

                                //ltrScript.Text = "<script>location.href=" + urlDownload + "</script>";
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
