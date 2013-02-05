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
        [FriendlyName("Tổng số truy cập")]
        [Description("Tổng số truy cập")]
        [Category("Thông tin khác")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("1")]
        public int HitCountNumber
        {
            get;
            set;
        }
        [WebBrowsable(true)]
        [FriendlyName("Số đang truy cập")]
        [Description("Số đang truy cập")]
        [Category("Thông tin khác")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("1")]
        public int CurrentHitCountNumber
        {
            get;
            set;
        }
        [WebBrowsable(true)]
        [FriendlyName("Số truy cập trong ngày")]
        [Description("Số truy cập trong ngày")]
        [Category("Thông tin khác")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("1")]
        public int DayHitCountNumber
        {
            get;
            set;
        }
        [WebBrowsable(true)]
        [FriendlyName("Số truy cập trong tuần")]
        [Description("Số truy cập trong tuần")]
        [Category("Thông tin khác")]
        [WebPartStorage(Storage.Shared)]
        [Personalizable(PersonalizationScope.Shared)]
        [DefaultValue("1")]
        public int WeekHitCountNumber
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
                dvBG.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                dvHitCount.InnerText = HitCountNumber.ToString();
                dvBGDay.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                dvHitCountDay.InnerText = DayHitCountNumber.ToString();
                dvBGNow.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                dvHitCountNow.InnerText = CurrentHitCountNumber.ToString();
                dvBGWeek.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                dvHitCountWeek.InnerText = WeekHitCountNumber.ToString();
            }
        }
        #endregion

        private void UpdateHitCount(SPWeb currentweb, HttpContext ctx)
        {
            var cDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
            var sessionDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now.AddMinutes(-15));
            int diff = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            var srartDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now.AddDays(-1 * diff));
            var endDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now.AddDays(7- diff));
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
                            "<And><Geq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='TRUE'>" + sessionDate + "</Value></Geq>" +
                            "<And><Eq><FieldRef Name='" + FieldsName.StatisticsList.InternalName.UserBrowser + "' /><Value Type='Text'>" + cBrowser +
                            "</Value></Eq><Eq><FieldRef Name='" + FieldsName.StatisticsList.InternalName.UserIP + "' /><Value Type='Text'>" + cIP +
                            "</Value></Eq>" +
                            "</And></And></And></And></Where>";

            var camlQueryNow = "<Where>" +
                                "<Geq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='TRUE'>" + sessionDate + "</Value></Geq>" +
                                "</Where>";

            var camlQueryDay = "<Where>" +
                                "<Eq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today /></Value></Eq>" +
                                "</Where>";

            var camlQueryWeek = "<Where><And><Geq>" +
                                "<FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today Offset='" + diff + "'></Today></Value></Geq>" +
                                "<Leq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today Offset='" + (diff - 7) + "'></Today></Value></Leq>" +
                                "</And></Where>";

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
                                RowLimit = 1,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPQuery spQueryNow = new SPQuery
                            {
                                Query = camlQueryNow,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPQuery spQueryDay = new SPQuery
                            {
                                Query = camlQueryDay,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPQuery spQueryWeek = new SPQuery
                            {
                                Query = camlQueryWeek,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPList list = Utilities.GetCustomListByUrl(web, ListsName.InternalName.StatisticsList);

                            SPList listConfig = Utilities.GetCustomListByUrl(web, "AllConfigVP");

                            var oldNumber = 0;
                            if (listConfig != null)
                            {
                                SPQuery spQueryConfig = new SPQuery
                                {
                                    Query = "<Where>" +
                                            "<Eq><FieldRef Name='Title' /><Value Type='Text'>OldNumber</Value></Eq>" +
                                            "</Where>",
                                    RowLimit = 1
                                };
                                var configItems = listConfig.GetItems(spQueryConfig);
                                if (configItems != null && configItems.Count > 0)
                                {
                                    try
                                    {
                                        oldNumber = Convert.ToInt32(configItems[0]["Value"]);
                                    }
                                    catch (SPException) { }
                                    catch (Exception){}
                                }
                            }

                            var itemCount = list.ItemCount;
                            dvHitCount.InnerText = (itemCount + oldNumber).ToString();
                            if (HitCountNumber == 0)
                            {
                                HitCountNumber = itemCount + oldNumber;
                                dvHitCount.InnerText = HitCountNumber.ToString();
                            }
                            if (list != null)
                            {
                                SPListItemCollection itemsNow = list.GetItems(spQueryNow);
                                if (itemsNow != null && itemsNow.Count > 0)
                                {
                                    CurrentHitCountNumber = itemsNow.Count;
                                    dvHitCountNow.InnerText = CurrentHitCountNumber.ToString();
                                }

                                SPListItemCollection itemsDay = list.GetItems(spQueryDay);
                                if (itemsDay != null && itemsDay.Count > 0)
                                {
                                    DayHitCountNumber = itemsDay.Count;
                                    dvHitCountDay.InnerText = DayHitCountNumber.ToString();
                                }

                                SPListItemCollection itemsWeek = list.GetItems(spQueryWeek);
                                if (itemsWeek != null && itemsWeek.Count > 0)
                                {
                                    WeekHitCountNumber = itemsWeek.Count;
                                    dvHitCountWeek.InnerText = WeekHitCountNumber.ToString();
                                }
                                if (itemCount <= 4900)
                                {
                                    SPListItemCollection items = list.GetItems(spQuery);
                                    if (items == null || items.Count <= 0)
                                    {
                                        HitCountNumber++;
                                        //dvHitCount.InnerText = HitCountNumber.ToString();
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
