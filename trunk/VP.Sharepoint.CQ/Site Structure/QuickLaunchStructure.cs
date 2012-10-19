using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Common;
using Microsoft.SharePoint.Client.Utilities;
namespace VP.Sharepoint.CQ
{
    static class QuickLaunchStructure
    {
        public static void CreateQuickLaunch(SPWeb web)
        {
            web.AllowUnsafeUpdates = true;
            //var quickLaunch = new QuickLaunchHelper(web, true);
            //var groups = string.Empty;
            //var urlList = web.Url + "/Lists/" + ListsName.English.CreateAccountRequest;
            //var createAccountRequest = web.GetList(urlList);

            //var Heading = quickLaunch.AddHeading(HttpUtility.HtmlEncode(Constants.ForSubmitterHeader), string.Empty, groups);

            ////Add view to quicklaunch
            //var view = createAccountRequest.Views[Constants.CreateAccountRequestView1];
            //if (view != null)
            //{
            //    QuickLaunchHelper.AddNavigationLink(Heading, HttpUtility.HtmlEncode(Constants.CreateAccountRequestView1), web.Url + "/" + view.Url, groups);
            //}

            //view = createAccountRequest.Views[Constants.CreateAccountRequestView2];
            //if (view != null)
            //{
            //    QuickLaunchHelper.AddNavigationLink(Heading, HttpUtility.HtmlEncode(Constants.CreateAccountRequestView2), web.Url + "/" + view.Url, groups);
            //}
        }
    }
}
