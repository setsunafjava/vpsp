using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web;
using Microsoft.SharePoint.Utilities;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.WebPartPages;
using System.ComponentModel;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class HitCountUC : FrontEndUC
    {
        private delegate void MethodInvoker(SPWeb web, HttpContext ctx);
        [WebBrowsable(true)]
        [FriendlyName("Số lượt truy cập")]
        [Description("Số lượt truy cập")]
        [Category("Thông tin khác")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("1")]
        public int HitCountNumber
        {
            get;
            set;
        }
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MethodInvoker runHitCount = new MethodInvoker(UpdateHitCount);
                runHitCount.BeginInvoke(CurrentWeb, HttpContext.Current, null, null);
            }
        }
        #endregion

        private void UpdateHitCount(SPWeb currentweb, HttpContext ctx)
        {
            var cDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
            var cLoginName = "Khách (không đăng nhập)";
            var cURL = ctx.Request.Url.AbsoluteUri.ToString();

            if (currentweb.CurrentUser == null || string.IsNullOrEmpty(currentweb.CurrentUser.LoginName))
            {
                cLoginName = "Khách (không đăng nhập)";
            }
            else
            {
                cLoginName = currentweb.CurrentUser.LoginName;
            }
            var cIP = ctx.Request.UserHostAddress;
            var cBrowser = ctx.Request.Browser.Browser;
            var camlQuery = "<Where><And><Eq>" +
                            "<FieldRef Name='" + FieldsName.StatisticsList.InternalName.UserUrl + "' />" +
                            "<Value Type='Text'>" + cURL + "</Value></Eq>" +
                            "<And><Eq><FieldRef Name='" + FieldsName.StatisticsList.InternalName.Title + "' />" +
                            "<Value Type='Text'>" + cLoginName + "</Value></Eq>" +
                            "<And><Eq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTime='FALSE'>" + cDate +
                            "</Value></Eq>" +
                            "<And><Eq><FieldRef Name='" + FieldsName.StatisticsList.InternalName.UserBrowser + "' /><Value Type='Text'>" + cBrowser +
                            "</Value></Eq><Eq><FieldRef Name='" + FieldsName.StatisticsList.InternalName.UserIP + "' /><Value Type='Text'>" + cIP +
                            "</Value></Eq>" +
                            "</And></And></And></And></Where>";

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(currentweb.Site.ID))
                {
                    using (var web = site.OpenWeb(currentweb.ID))
                    {
                        try
                        {
                            SPQuery spQuery = new SPQuery
                            {
                                Query = camlQuery,
                                RowLimit = 1
                            };
                            SPList list = Utilities.GetCustomListByUrl(web, ListsName.InternalName.StatisticsList);
                            if (HitCountNumber == 0)
                            {
                                HitCountNumber = list.ItemCount;
                            }
                            if (list != null)
                            {
                                SPListItemCollection items = list.GetItems(spQuery);
                                if (items == null || items.Count <= 0)
                                {
                                    HitCountNumber++;
                                    var item = list.AddItem();
                                    item[FieldsName.StatisticsList.InternalName.Title] = cLoginName;
                                    item[FieldsName.StatisticsList.InternalName.UserUrl] = cURL;
                                    item[FieldsName.StatisticsList.InternalName.UserIP] = cIP;
                                    item[FieldsName.StatisticsList.InternalName.UserBrowser] = cBrowser;
                                    web.AllowUnsafeUpdates = true;
                                    item.Update();
                                }
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
    }
}
