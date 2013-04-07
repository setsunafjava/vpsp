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
using System.Text;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class HitCountUC : FrontEndUC
    {
        private delegate void MethodInvoker(SPWeb web, HttpContext ctx);
        public static int HitCountNumber = 1;
        public static int CurrentHitCountNumber = 1;
        public static int DayHitCountNumber = 1;
        public static int YesterdayHitCountNumber = 1;
        public static int WeekHitCountNumber = 1;
        public static int MonthHitCountNumber = 1;
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
                //dvBG.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                //dvHitCount.InnerText = HitCountNumber.ToString();
                //dvBGDay.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                //dvHitCountDay.InnerText = DayHitCountNumber.ToString();
                //dvBGNow.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                //dvHitCountNow.InnerText = CurrentHitCountNumber.ToString();
                //dvBGWeek.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                //dvHitCountWeek.InnerText = WeekHitCountNumber.ToString();
                //dvBGMonth.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                //dvHitCountMonth.InnerText = MonthHitCountNumber.ToString();
                //dvBGYesterday.Attributes.Add("style", "background-image: url('" + DocLibUrl + "/statistic.jpg'); width: 118px; height: 35px;");
                //dvHitCountYesterday.InnerText = YesterdayHitCountNumber.ToString();

                tdAll.InnerText = HitCountNumber.ToString();
                tdToday.InnerText = DayHitCountNumber.ToString();
                lblCurrent.Text = "<span id='spCurrent'>" + CurrentHitCountNumber.ToString() + "</span>";
                tdThisWeek.InnerText = WeekHitCountNumber.ToString();
                tdThisMonth.InnerText = MonthHitCountNumber.ToString();
                tdYesterday.InnerText = YesterdayHitCountNumber.ToString();
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
            var firstOfThisMonth = SPUtility.CreateISO8601DateTimeFromSystemDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            var lastOfThisMonth = SPUtility.CreateISO8601DateTimeFromSystemDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1));
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

            var camlQueryYesterday = "<Where>" +
                                "<Eq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today Offset='1' /></Value></Eq>" +
                                "</Where>";

            var camlQueryWeek = "<Where><And><Geq>" +
                                "<FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today Offset='" + diff + "'></Today></Value></Geq>" +
                                "<Leq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today Offset='" + (diff - 7) + "'></Today></Value></Leq>" +
                                "</And></Where>";

            var camlQueryMonth = "<Where><And><Geq>" +
                                "<FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'>"+firstOfThisMonth+"</Value></Geq>" +
                                "<Leq><FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'>" + lastOfThisMonth + "</Value></Leq>" +
                                "</And></Where>";

            var camlQueryLastMonth = "<Where><Lt>" +
                                "<FieldRef Name='" + Constants.Created + "' /><Value Type='DateTime' IncludeTimeValue='FALSE'>" + firstOfThisMonth + "</Value></Lt></And></Where>";

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
                            SPQuery spQueryYesterday = new SPQuery
                            {
                                Query = camlQueryYesterday,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPQuery spQueryWeek = new SPQuery
                            {
                                Query = camlQueryWeek,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPQuery spQueryMonth = new SPQuery
                            {
                                Query = camlQueryMonth,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPQuery spQueryLastMonth = new SPQuery
                            {
                                Query = camlQueryLastMonth,
                                //QueryThrottleMode = SPQueryThrottleOption.Override
                            };
                            SPList list = Utilities.GetCustomListByUrl(web, ListsName.InternalName.StatisticsList);

                            SPList listConfig = Utilities.GetCustomListByUrl(web, "AllConfigVP");

                            var oldNumber = 0;
                            SPListItem configItem = null;
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
                                        configItem = configItems[0];
                                    }
                                    catch (SPException) { }
                                    catch (Exception){}
                                }
                            }

                            //HitCountNumber = oldNumber + 1;
                            tdAll.InnerText = oldNumber.ToString();
                            if (list != null)
                            {
                                SPListItemCollection itemsNow = list.GetItems(spQueryNow);
                                if (itemsNow != null && itemsNow.Count > 0)
                                {
                                    CurrentHitCountNumber = itemsNow.Count;
                                    lblCurrent.Text = "<span id='spCurrent'>" + CurrentHitCountNumber.ToString() + "</span>";
                                }

                                SPListItemCollection itemsDay = list.GetItems(spQueryDay);
                                if (itemsDay != null && itemsDay.Count > 0)
                                {
                                    DayHitCountNumber = itemsDay.Count;
                                    tdToday.InnerText = DayHitCountNumber.ToString();
                                }

                                SPListItemCollection itemsYesterday = list.GetItems(spQueryYesterday);
                                if (itemsYesterday != null && itemsYesterday.Count > 0)
                                {
                                    YesterdayHitCountNumber = itemsYesterday.Count;
                                    tdYesterday.InnerText = YesterdayHitCountNumber.ToString();
                                }

                                SPListItemCollection itemsWeek = list.GetItems(spQueryWeek);
                                if (itemsWeek != null && itemsWeek.Count > 0)
                                {
                                    WeekHitCountNumber = itemsWeek.Count;
                                    tdThisWeek.InnerText = WeekHitCountNumber.ToString();
                                }
                                SPListItemCollection itemsMonth = list.GetItems(spQueryMonth);
                                if (itemsMonth != null && itemsMonth.Count > 0)
                                {
                                    MonthHitCountNumber = itemsMonth.Count;
                                    tdThisMonth.InnerText = MonthHitCountNumber.ToString();
                                }
                                SPListItemCollection itemsLastMonth = list.GetItems(spQueryLastMonth);
                                if (itemsLastMonth != null && itemsLastMonth.Count > 0)
                                {
                                    StringBuilder sbDelete = new StringBuilder();
                                    sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");

                                    string command = "<Method>" +
                                                        "<SetList Scope=\"Request\">" + list.ID + "</SetList>" +
                                                        "<SetVar Name=\"ID\">{0}</SetVar>" +
                                                        "<SetVar Name=\"Cmd\">Delete</SetVar>" +
                                                    "</Method>";

                                    foreach (SPListItem item in itemsLastMonth)
                                    {
                                        sbDelete.Append(string.Format(command, item.ID.ToString()));
                                    }
                                    sbDelete.Append("</Batch>");

                                    web.AllowUnsafeUpdates = true;
                                    //Run the Batch command
                                    web.ProcessBatchData(sbDelete.ToString());
                                }
                                if (list.ItemCount <= 4900)
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

                                        configItem["Value"] = oldNumber + 1;
                                        web.AllowUnsafeUpdates = true;
                                        configItem.SystemUpdate(false);
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
