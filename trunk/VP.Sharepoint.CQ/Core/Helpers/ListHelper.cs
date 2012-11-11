using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.SharePoint;
using System.Globalization;
using VP.Sharepoint.CQ.Common;


namespace VP.Sharepoint.CQ.Core.Helpers
{
    /// <summary>
    ///   A helper support create a new list instance
    /// </summary>
    public class ListHelper
    {
        private readonly List<BaseFieldCreator> creators;
        private readonly SPWeb web;

        public ListHelper(SPWeb web)
        {
            this.web = web;
            creators = new List<BaseFieldCreator>();
            ListTemplateType = SPListTemplateType.GenericList;
            Views = new List<ViewCreator>();
            EnableAttachments = true;            
        }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SPListTemplateType ListTemplateType { get; set; }

        public bool OnQuickLaunch { get; set; }

        /// <summary>
        /// Indicates whether throttling for this list is disable.
        /// </summary>
        public bool DisableListThrottling { get; set; }
        
        /// <summary>
        /// Gets or sets a Boolean value that specifies whether attachments can be added to items in the list.
        /// </summary>
        public bool EnableAttachments { get; set; }

        public IList<ViewCreator> Views { get; private set; }

        /// <summary>
        /// Create a list with fields
        /// </summary>
        /// <returns></returns>
        public SPList Apply()
        {
            var list = CreateList(Name, Title, Description, ListTemplateType, OnQuickLaunch, DisableListThrottling);

            if (!EnableAttachments)
            {
                list.EnableAttachments = false;
            }

            list.Update();

            // Create views
            foreach (var viewCreator in Views)
            {
                viewCreator.Apply(list);
            }
            
            return list;
        }

        private SPList CreateList(string name, string title, string description, SPListTemplateType listTemplateType, bool onQuickLaunch, bool disableListThrottling)
        {
            SPList list = null;            
            try
            {
                string url = string.Empty;
                if (listTemplateType == SPListTemplateType.PictureLibrary ||
                    listTemplateType == SPListTemplateType.DocumentLibrary ||
                    listTemplateType == SPListTemplateType.WebPageLibrary ||
                    listTemplateType == SPListTemplateType.XMLForm)
                    url = Utilities.GetWebUrl(web.Url) + "/" + name;
                else
                    url = Utilities.GetWebUrl(web.Url) + "/Lists/" + name;

                list = web.GetList(url);
            }
            catch (FileNotFoundException fx) 
            {
                Utilities.LogToULS(fx);
            }

            if (list == null)
            {
                var id = web.Lists.Add(name, description, listTemplateType);                
                list = web.Lists[id];
            }

            try
            {   
                list.OnQuickLaunch = onQuickLaunch;

                foreach (var creator in creators)
                {
                    creator.CreateField(list);
                }

                if (disableListThrottling)
                {
                    list.EnableThrottling = false;
                }

                list.Title = title;

                list.Update();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                // web.Lists[list.ID].Delete();             
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "Cannot create a list '{0}' because: {1}", title, ex.Message), ex);
            }
        }

        public void AddField(BaseFieldCreator creator)
        {
            creators.Add(creator);
        }

        /// <summary>
        /// Indicates whether throttling for this list is disable.
        /// </summary>
        public static void DisableListThreshold(SPList list)
        {
            list.EnableThrottling = false;
            list.Update();
        }

        /// <summary>
        /// Indicates whether field will indexed.
        /// </summary>
        /// <param name="field"></param>
        public static void CreateIndexedField(SPField field)
        {
            if (!field.Indexed)
            {
                field.Indexed = true;
                field.Update();
            }
        }

        /// <summary>
        /// Indicates whether field will indexed.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fieldName"></param>
        public static void CreateIndexedField(SPList list, string fieldName)
        {
            var field = list.Fields[fieldName];
            CreateIndexedField(field);
        }

        /// <summary>
        /// Delete user custom action item button for a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="actionId"></param>
        public static void DeleteUserCustomAction(SPList list, string actionId)
        {
            var btnCustomRibbon = list.UserCustomActions.FirstOrDefault(c => c.Name == actionId);
            if (btnCustomRibbon != null)
            {
                btnCustomRibbon.Delete();
                list.Update();
            }
        }
    }
}