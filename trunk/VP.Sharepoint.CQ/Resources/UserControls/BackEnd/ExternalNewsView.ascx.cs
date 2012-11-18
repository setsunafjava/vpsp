using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class ExternalNewsView : BackEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void Page_Load(object sender, EventArgs e)
        {
            viewRSS.EnableAddNewItem = false;
            viewNews.EnableAddNewItem = false;
            if (!Page.IsPostBack)
            {
                Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.InternalName.CategoryLevel);
                Utilities.BindToDropDown(CurrentWeb, ddlCat, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.InternalName.CategoryLevel);
                viewRSS.WhereCondition = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Eq></Where>";
                viewNews.WhereCondition = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Eq></Where>";
            }
        }
        #endregion

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewRSS.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.ExternalNewsLink.InternalName.NewsGroup + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
            viewNews.WhereCondition = @"<Where><And><IsNotNull><FieldRef Name='" + FieldsName.NewsList.InternalName.NewsUrl + "' /></IsNotNull><Eq><FieldRef Name='" + FieldsName.NewsList.InternalName.NewsGroup + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></And></Where>";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var catID = ddlCategory.SelectedValue;
            var catName = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catID, "Text", "Title");
            //var reader = new XmlTextReader("http://vnexpress.net/rss/gl/xa-hoi.rss");
            //DataSet ds = new DataSet();
            //ds.ReadXml(reader);
            //foreach (DataRow item in ds.Tables["item"].Rows)
            //{
            //    AddNews(CurrentWeb, ListsName.InternalName.ExternalNewsList, ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, "http://vnexpress.net/rss/gl/xa-hoi.rss", item);
            //}
            AddNews(CurrentWeb, catID, catName);
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {

        }

        private void AddNews(SPWeb web, string catID, string catName) {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var adminSite = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = adminSite.OpenWeb(web.ID))
                    {
                        try
                        {
                            adminWeb.AllowUnsafeUpdates = true;
                            var rssList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.ExternalNewsLinkList);
                            var newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                            string caml = @"<Where><Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq></Where>";
                            var query = new SPQuery()
                            {
                                Query = string.Format(CultureInfo.InvariantCulture, caml, FieldsName.ExternalNewsLink.InternalName.NewsGroup, catID)
                            };
                            var items = rssList.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                foreach (SPListItem item in items)
                                {
                                    AddNews(adminWeb, newsList, catID, catName, Convert.ToString(item[FieldsName.ExternalNewsLink.InternalName.LinkPath]));
                                }
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

        private void AddNews(SPWeb web, SPList list, string catID, string catName, string rssLink) {
            var reader = new XmlTextReader(rssLink);
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            foreach (DataRow item in ds.Tables["item"].Rows)
            {
                AddNews(web, list, catID, catName, rssLink, item);
            }
        }

        private void AddNews(SPWeb adminWeb, SPList list, string catID, string catName, string rssLink, DataRow rss)
        {
            try
            {
                adminWeb.AllowUnsafeUpdates = true;
                var item = list.AddItem();
                item[FieldsName.NewsList.InternalName.Title] = rss["title"];
                item[FieldsName.NewsList.InternalName.NewsUrl] = rss["link"];
                item[FieldsName.NewsList.InternalName.NewsGroup] = catID;
                item[FieldsName.NewsList.InternalName.NewsGroupName] = catName;
                item[FieldsName.NewsList.InternalName.RSSLink] = rssLink;
                var desc = Convert.ToString(rss["description"]);
                var imgUrl = string.Empty;
                if (desc.Contains("<img"))
                {
                    var str = desc.Split(new string[] { "<img" }, 2, StringSplitOptions.None)[1];
                    desc = str.Split(new string[] { ">" }, 2, StringSplitOptions.None)[1];
                    str = str.Split(new string[] { ">" }, 2, StringSplitOptions.None)[0];
                    if (str.Contains("\""))
                    {
                        imgUrl = str.Split('\"')[1];
                    }
                    else if (str.Contains("'"))
                    {
                        imgUrl = str.Split('\'')[1];
                    }
                    else
                    {
                        str = str.Split('=')[1];
                        var str1 = str.Split('.')[0];
                        var str2 = str.Split('.')[1];
                        str2 = str2.Replace("/", "").Replace(" ", "").Replace("\"", "").Replace("'", "");
                        imgUrl = str1 + "." + str2;
                    }
                }
                item[FieldsName.NewsList.InternalName.Description] = desc;
                if (!string.IsNullOrEmpty(imgUrl))
                {
                    item[FieldsName.NewsList.InternalName.ImageThumb] = imgUrl;
                    SPFieldUrlValue imgDsp = new SPFieldUrlValue();
                    imgDsp.Description = item.Title;
                    imgDsp.Url = imgUrl;
                    item[FieldsName.NewsList.InternalName.ImageDsp] = imgDsp;
                }
                adminWeb.AllowUnsafeUpdates = true;
                item.Update();
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }

        private void AddMainNews(SPWeb web, string catID, string catName)
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
                            var newsList = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                            AddMainNews(adminWeb, newsList, catID, catName, txtRSS.Text);
                        }
                        catch (SPException ex)
                        {
                            Utilities.LogToULS(ex);
                        }
                    }
                }
            });
        }

        private void AddMainNews(SPWeb web, SPList list, string catID, string catName, string rssLink)
        {
            var reader = new XmlTextReader(rssLink);
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            foreach (DataRow item in ds.Tables["item"].Rows)
            {
                AddMainNews(web, list, catID, catName, rssLink, item);
            }
        }

        private void AddMainNews(SPWeb adminWeb, SPList list, string catID, string catName, string rssLink, DataRow rss)
        {
            try
            {
                adminWeb.AllowUnsafeUpdates = true;
                var item = list.AddItem();
                item[FieldsName.NewsList.InternalName.Title] = rss["title"];
                item[FieldsName.NewsList.InternalName.NewsUrl] = rss["link"];
                item[FieldsName.NewsList.InternalName.NewsGroup] = catID;
                item[FieldsName.NewsList.InternalName.NewsGroupName] = catName;
                var desc = Convert.ToString(rss["description"]);
                item[FieldsName.NewsList.InternalName.Content] = desc;
                var imgUrl = string.Empty;
                if (desc.Contains("<img"))
                {
                    var str = desc.Split(new string[] { "<img" }, 2, StringSplitOptions.None)[1];
                    desc = str.Split(new string[] { ">" }, 2, StringSplitOptions.None)[1];
                    str = str.Split(new string[] { ">" }, 2, StringSplitOptions.None)[0];
                    if (str.Contains("\""))
                    {
                        imgUrl = str.Split('\"')[1];
                    }
                    else if (str.Contains("'"))
                    {
                        imgUrl = str.Split('\'')[1];
                    }
                    else
                    {
                        str = str.Split('=')[1];
                        var str1 = str.Split('.')[0];
                        var str2 = str.Split('.')[1];
                        str2 = str2.Replace("/", "").Replace(" ", "").Replace("\"", "").Replace("'", "");
                        imgUrl = str1 + "." + str2;
                    }
                }
                item[FieldsName.NewsList.InternalName.Description] = desc;
                if (!string.IsNullOrEmpty(imgUrl))
                {
                    item[FieldsName.NewsList.InternalName.ImageThumb] = imgUrl;
                    item[FieldsName.NewsList.InternalName.ImageSmallThumb] = imgUrl;
                    item[FieldsName.NewsList.InternalName.ImageHot] = imgUrl;
                    SPFieldUrlValue imgDsp = new SPFieldUrlValue();
                    imgDsp.Description = item.Title;
                    imgDsp.Url = imgUrl;
                    item[FieldsName.NewsList.InternalName.ImageDsp] = imgDsp;
                }
                adminWeb.AllowUnsafeUpdates = true;
                item.Update();
            }
            catch (SPException ex)
            {
                Utilities.LogToULS(ex);
            }
        }

        protected void btnGetNews_Click(object sender, EventArgs e)
        {
            var catID = ddlCat.SelectedValue;
            var catName = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catID, "Text", "Title");
            AddMainNews(CurrentWeb, catID, catName);
        }

        protected void btnCopyNews_Click(object sender, EventArgs e)
        {
            var catID = ddlCat.SelectedValue;
            var catName = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, catID, "Text", "Title");
            var newsUrl = txtUrl.Text + "/sovp/news/topic" + txtNewCat.Text + "/xxx";

            WebClient webClient = new WebClient();
            byte[] reqHTML;
            reqHTML = webClient.DownloadData(newsUrl);
            UTF8Encoding objUTF8 = new UTF8Encoding();
            string source = objUTF8.GetString(reqHTML);

            if (source.Contains("<table id=\"newslisttbl\""))
            {
                source = source.Split(new string[] { "<table id=\"newslisttbl\"" }, 2, StringSplitOptions.None)[1];
                source = source.Split(new string[] { "</table>" }, 2, StringSplitOptions.None)[0];
                var newsArr = source.Split(new string[] { "</tr>" }, StringSplitOptions.None);
                for (int i = 0; i < newsArr.Length - 1; i++)
                {
                    CopyNews(CurrentWeb, catID, catName, newsArr[i]);
                }
            }
        }

        private void CopyNews(SPWeb web, string catID, string catName, string source)
        {
            var descValue = source.Split(new string[] { "<div class=\"new-desc\">" }, 2, StringSplitOptions.None)[1];
            descValue = descValue.Split(new string[] { "</div>" }, 2, StringSplitOptions.None)[0];
            var remainValue = source.Split(new string[] { "<div class=\"new-desc\">" }, 2, StringSplitOptions.None)[0];
            var linkValue = remainValue.Split(new string[] { "href=" }, 2, StringSplitOptions.None)[1];
            var imgValue = remainValue.Split(new string[] { "href=" }, 2, StringSplitOptions.None)[0];
            var linkTitle = linkValue.Split(new string[] { ">" }, 2, StringSplitOptions.None)[1];
            linkTitle = linkTitle.Split(new string[] { "</a>" }, 2, StringSplitOptions.None)[0];
            linkValue = linkValue.Split(new string[] { ">" }, 2, StringSplitOptions.None)[0];
            linkValue = linkValue.Replace("\"", "").Replace("'", "");
            imgValue = imgValue.Split(new string[] { "src=" }, 2, StringSplitOptions.None)[1];
            imgValue = imgValue.Split(new string[] { ">" }, 2, StringSplitOptions.None)[0];
            imgValue = imgValue.Replace("\"", "").Replace("'", "").Replace("../", "");
            imgValue = txtUrl.Text + "/" + imgValue;
            var newsID = linkValue.Split(new string[] { "&new=" }, 2, StringSplitOptions.None)[1];
            linkValue = txtUrl.Text + "/sovp/news/new" + newsID + "/xxx";
            var imgExt = imgValue.Split(new string[] { "." }, StringSplitOptions.None)[imgValue.Split('.').Length - 1];
            var fileName = newsID + "." + imgExt;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgValue);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(web.Site.ID))
                {
                    using (var adminWeb = site.OpenWeb(web.ID))
                    {
                        var imgUrl = string.Empty;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            // Create a 4K buffer to chunk the file

                            byte[] MyBuffer = new byte[4096];

                            int BytesRead;

                            // Read the chunk of the web response into the buffer

                            while (0 < (BytesRead = receiveStream.Read(MyBuffer, 0, MyBuffer.Length)))
                            {

                                // Write the chunk from the buffer to the file

                                stream.Write(MyBuffer, 0, BytesRead);

                            }
                            var fuThumbName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now), fileName);
                            SPFile file = Utilities.UploadFileToDocumentLibrary(web, stream, string.Format(CultureInfo.InvariantCulture,
                                "{0}/{1}/{2}", WebUrl, ListsName.InternalName.NewsImagesList, fuThumbName));
                            imgUrl = file.Url;
                        }

                        
                        var list = Utilities.GetCustomListByUrl(adminWeb, ListsName.InternalName.NewsList);
                        var item = list.AddItem();
                        item[FieldsName.NewsList.InternalName.Title] = linkTitle;
                        item[FieldsName.NewsList.InternalName.NewsGroup] = catID;
                        item[FieldsName.NewsList.InternalName.NewsGroupName] = catName;
                        item[FieldsName.NewsList.InternalName.Description] = descValue;
                        item[FieldsName.NewsList.InternalName.ImageThumb] = imgUrl;

                        if (!string.IsNullOrEmpty(imgUrl))
                        {
                            SPFieldUrlValue imgDsp = new SPFieldUrlValue();
                            imgDsp.Description = item.Title;
                            var webUrl = web.ServerRelativeUrl;
                            if (webUrl.Equals("/"))
                            {
                                webUrl = "";
                            }
                            imgDsp.Url = webUrl + "/" + imgUrl;
                            item[FieldsName.NewsList.InternalName.ImageDsp] = imgDsp;
                        }

                        WebClient webClient = new WebClient();
                        byte[] reqHTML;
                        reqHTML = webClient.DownloadData(linkValue);
                        UTF8Encoding objUTF8 = new UTF8Encoding();
                        string detail = objUTF8.GetString(reqHTML);

                        detail = detail.Split(new string[] { "<div class=\"new-detail-content\">" }, 2, StringSplitOptions.None)[1];
                        detail = detail.Split(new string[] { "<div id=\"new-reference\">" }, 2, StringSplitOptions.None)[0];
                        detail = detail.Substring(0, detail.Length - 6);
                        item[FieldsName.NewsList.InternalName.Content] = detail;

                        adminWeb.AllowUnsafeUpdates = true;
                        item.Update();
                    }
                }
            });
        }
    }
}
