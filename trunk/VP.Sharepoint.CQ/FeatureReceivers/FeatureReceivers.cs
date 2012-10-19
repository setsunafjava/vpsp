using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Security;
    using System.Security.Permissions;

    public class FeatureReceivers : SPFeatureReceiver
    {
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var web = (SPWeb)properties.Feature.Parent;
            if (web == null) return;
            // Create Site Structure                
            SiteStructure.CreateSiteStructure(web);
            //Set Anonymous
            web.AnonymousState = SPWeb.WebAnonymousState.On;
            web.AllowUnsafeUpdates = true;
            web.Update();
            //Create pages
            PagesStructure.Create(web);
        }
    }
}

