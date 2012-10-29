using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    internal static class LocalizationHelper
    {
        public static string GetStringFromCoreResource(string key)
        {
            return SPUtility.GetLocalizedString("$Resources: " + key, "core",
                                                (uint) Thread.CurrentThread.CurrentUICulture.LCID);
        }
        public static string GetString(string resource, string key)
        {
            return HttpContext.GetGlobalResourceObject(resource, key) as string;
        }
    }
}
