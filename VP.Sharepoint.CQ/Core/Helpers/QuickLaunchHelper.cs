using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using Microsoft.SharePoint.Publishing.Navigation;

namespace VP.Sharepoint.CQ.Common
{
    public class QuickLaunchHelper
    {
        private SPNavigationNodeCollection nodes = null;

        public QuickLaunchHelper(SPWeb web, bool isInheritTopLinkBarOfParent)
        {
            nodes = web.Navigation.QuickLaunch;

            // before create new delete all old items
            // leave 02 node cannot be deleted

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].Delete();
            }

            if (isInheritTopLinkBarOfParent && !web.IsRootWeb)
                web.Navigation.UseShared = true;
        }

        #region Obsolete - Only for URL is a URI
        /*
        public SPNavigationNode AddHeading(string title, string url, string group)
        {
            if (!url.Equals("javascript:void(0)"))
            {
                Uri uri = new Uri(url);
                return AddHeading(title, uri, group);
            }
            else
            {
                if (nodes != null)
                {
                    SPNavigationNode nodeItem = SPNavigationSiteMapNode.CreateSPNavigationNode(title, url, Microsoft.SharePoint.Publishing.NodeTypes.Heading, nodes);
                    nodeItem.Properties.Add("Audience", ";;;;" + group);
                    nodeItem.Update();

                    return nodeItem;
                }

                return null;
            }
        }

        public SPNavigationNode AddHeading(string title, Uri url, string group)
        {   
            if (nodes != null)
            {
                SPNavigationNode nodeItem = SPNavigationSiteMapNode.CreateSPNavigationNode(title, url.ToString(), Microsoft.SharePoint.Publishing.NodeTypes.Heading, nodes);
                nodeItem.Properties.Add("Audience", ";;;;" + group);
                nodeItem.Update();

                return nodeItem;
            }

            return null;
        }
        */
        #endregion

        public SPNavigationNode AddHeading(string title, string url, string group)
        {
            if (nodes != null)
            {
                SPNavigationNode nodeItem = SPNavigationSiteMapNode.CreateSPNavigationNode(title, url, Microsoft.SharePoint.Publishing.NodeTypes.Heading, nodes);
                nodeItem.Properties.Add("Audience", ";;;;" + group);
                nodeItem.Update();

                return nodeItem;
            }
            return null;
        }

        #region Obsolete - Only for URL is a URI
        /*
        public SPNavigationNode AddNavigationLink(SPNavigationNode parentNode, string title, Uri url, string group)
        {
            SPNavigationNode subNode = new SPNavigationNode(title, url.ToString());
            parentNode.Children.Add(subNode, parentNode);
            subNode.Properties.Add("Audience", ";;;;" + group);
            subNode.Update();
            parentNode.Update();

            return subNode;
        }

        public SPNavigationNode AddNavigationLink(SPNavigationNode parentNode, string title, string url, string group)
        {
            Uri uri = new Uri(url);
            return AddNavigationLink(parentNode, title, uri, group);
        }
        */
        #endregion

        public static SPNavigationNode AddNavigationLink(SPNavigationNode parentNode, string title, string url, string group)
        {
            var subNode = new SPNavigationNode(title, url);
            parentNode.Children.Add(subNode, parentNode);
            subNode.Properties.Add("Audience", ";;;;" + group);
            subNode.Update();
            parentNode.Update();

            return subNode;
        }
    }
}