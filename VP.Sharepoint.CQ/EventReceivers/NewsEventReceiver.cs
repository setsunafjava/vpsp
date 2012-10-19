using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Common;
using System.Globalization;

namespace VP.Sharepoint.CQ.EventReceivers
{
    public class NewsEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            this.EventFiringEnabled = false;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(properties.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(properties.Web.ID))
                    {
                        //var list = web.Lists.GetList(properties.ListId, true);
                        //var item = list.GetItemById(properties.ListItemId);
                        //try
                        //{
                        //    SetPermission(web, item);
                        //}
                        //finally
                        //{
                        //    this.EventFiringEnabled = true;
                        //}
                      
                    }
                   
                }
            });
        }

        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            this.EventFiringEnabled = false;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(properties.Web.Site.ID))
                {
                    using (var web = site.OpenWeb(properties.Web.ID))
                    {
                        //var list = web.Lists.GetList(properties.ListId, true);
                        //var item = list.GetItemById(properties.ListItemId);
                        //try
                        //{
                        //    SetPermission(web, item);
                        //}
                        //finally
                        //{
                        //    this.EventFiringEnabled = true;
                        //}                        
                    }
                    
                }
            });
          
        }
    }
}
