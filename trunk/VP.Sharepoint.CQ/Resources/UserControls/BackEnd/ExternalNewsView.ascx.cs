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
                viewRSS.WhereCondition = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Eq></Where>";
                viewNews.WhereCondition = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Eq></Where>";
            }
        }
        #endregion

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewRSS.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.ExternalNewsLink.InternalName.NewsGroup + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
            viewNews.WhereCondition = @"<Where><Eq><FieldRef Name='" + FieldsName.ExternalNews.InternalName.NewsGroup + "' /><Value Type='Text'>" + ddlCategory.SelectedValue + "</Value></Eq></Where>";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //viewRSS.GetSelectedItems
            //var reader = XmlReader.Create("http://vnexpress.net/rss/gl/xa-hoi.rss");
            //var feed = SyndicationFeed.Load(reader);
            var reader = new XmlTextReader("http://vnexpress.net/rss/gl/xa-hoi.rss");
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            foreach (DataRow item in ds.Tables["item"].Rows)
            {
                AddNews(CurrentWeb, ListsName.InternalName.ExternalNewsList, ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, "http://vnexpress.net/rss/gl/xa-hoi.rss", item);
            }
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {

        }

        private void AddNews(SPWeb web, string listName, string catID, string catName, string rssLink, DataRow rss)
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
                            var list = Utilities.GetCustomListByUrl(adminWeb, listName);
                            var item = list.AddItem();
                            item[FieldsName.ExternalNews.InternalName.Title] = rss["title"];
                            item[FieldsName.ExternalNews.InternalName.LinkPath] = rss["link"];
                            item[FieldsName.ExternalNews.InternalName.NewsGroup] = catID;
                            item[FieldsName.ExternalNews.InternalName.NewsGroupName] = catName;
                            item[FieldsName.ExternalNews.InternalName.RSSLink] = rssLink;
                            var desc = Convert.ToString(rss["description"]);
                            var imgUrl = string.Empty;
                            if (desc.Contains("<img"))
                            {
                                var str = desc.Split(new string[] { "<img" }, 2, StringSplitOptions.None)[1];
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
                            else
                            {
                                item[FieldsName.ExternalNews.InternalName.Description] = desc;
                            }
                            if (!string.IsNullOrEmpty(imgUrl))
                            {
                                item[FieldsName.ExternalNews.InternalName.ImageThumb] = imgUrl;
                                SPFieldUrlValue imgDsp = new SPFieldUrlValue();
                                imgDsp.Description = item.Title;
                                imgDsp.Url = imgUrl;
                                CurrentItem[FieldsName.NewsList.InternalName.ImageDsp] = imgDsp;
                            }
                            adminWeb.AllowUnsafeUpdates = true;
                            item.Update();
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
