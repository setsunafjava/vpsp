using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.SharePoint;

namespace VP.Sharepoint.CQ.Core.Helpers
{
    /// <summary>
    ///   Security permission for SharePoint
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        ///   Breaks inheritance of role assignments from the parent Web site.
        /// </summary>
        /// <param name = "web"></param>
        public static void BreakRoleInheritance(SPWeb web)
        {
            BreakRoleInheritance(web, true);
        }

        /// <summary>
        /// The function to Break permission of Web
        /// </summary>
        /// <param name="web">Current Web</param>
        /// <param name="copyRoleAssigements">Copy permission of parrent?</param>
        public static void BreakRoleInheritance(SPWeb web, bool copyRoleAssigements)
        {
            if (web.IsRootWeb)
            {
                return;
            }

            web.BreakRoleInheritance(copyRoleAssigements);

            if (!web.HasUniqueRoleDefinitions)
            {
                web.RoleDefinitions.BreakInheritance(false, copyRoleAssigements);
            }
        }

        /// <summary>
        ///   Breaks the role assignment inheritance for the list and gives the current list its own copy of the role assignments.
        /// </summary>
        /// <param name = "list"></param>
        public static void BreakRoleInheritance(SPList list)
        {
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(false);
                list.Update();
            }
        }

        /// <summary>
        ///   Breaks the role assignment inheritance for the list and gives the current list its own copy of the role assignments.
        /// </summary>
        /// <param name = "list"></param>
        /// <param name = "copyRoleAssigements"></param>
        public static void BreakRoleInheritance(SPList list, bool copyRoleAssigements)
        {
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(copyRoleAssigements);
                list.Update();
            }
        }

        /// <summary>
        ///   Adds a new permission level to the web.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "name"></param>
        /// <param name = "description"></param>
        /// <param name = "permissions"></param>
        public static void CreatePermissionLevel(SPWeb web, string name, string description,
                                                 SPBasePermissions permissions)
        {
            if (!web.IsRootWeb && !web.HasUniqueRoleDefinitions)
            {
                throw new NotSupportedException("This web must be broken permission before adding new permission level.");
            }

            var roleDefinition = GetRoleDefinition(web, name);
            if (roleDefinition != null)
            {
                if (roleDefinition.BasePermissions != permissions || roleDefinition.Description != description)
                {
                    roleDefinition.BasePermissions = permissions;
                    roleDefinition.Description = description;
                    roleDefinition.Update();
                }
            }
            else
            {
                roleDefinition = new SPRoleDefinition
                                     {
                                         Name = name,
                                         Description = description,
                                         BasePermissions = permissions
                                     };
                web.RoleDefinitions.Add(roleDefinition);
            }
        }

        /// <summary>
        ///   Add a group to the site groups.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "name"></param>
        /// <param name = "owner"></param>
        /// <param name = "description"></param>
        /// <returns></returns>
        public static SPGroup AddGroup(SPWeb web, string name, SPMember owner, string description)
        {
            if (web.SiteGroups.Cast<SPGroup>().Any(g => g.Name == name))
            {
                return web.SiteGroups[name];
            }

            web.SiteGroups.Add(name, owner, null, description);
            var group = web.SiteGroups[name];
            return group;
        }

        /// <summary>
        ///   Adds the specified users to the group.
        /// </summary>
        /// <param name = "group"></param>
        /// <param name = "users"></param>
        public static void AddUsersToGroup(SPGroup group, params SPUser[] users)
        {
            foreach (var user in users)
            {
                group.AddUser(user);
            }
            group.Update();
        }

        private static SPRoleDefinition GetRoleDefinition(SPWeb web, string name)
        {
            return web.RoleDefinitions.Cast<SPRoleDefinition>().FirstOrDefault(item => item.Name == name);
        }

        /// <summary>
        /// Get a group within a web site collection
        /// </summary>
        /// <param name="web"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SPGroup GetGroup(SPWeb web, string name)
        {
            return web.SiteGroups.Cast<SPGroup>().FirstOrDefault(item => item.Name == name);
        }

        /// <summary>
        ///   Checks permissions of the current user for a specified set of rights and returns a Boolean value.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "permissionLevel"></param>
        /// <returns></returns>
        public static bool DoesUserHavePermissions(SPWeb web, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(web, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return web.DoesUserHavePermissions(roleDefinition.BasePermissions);
        }

        /// <summary>
        ///   Checks permissions of the specified user for a specified set of rights and returns a Boolean value.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "loginName"></param>
        /// <param name = "permissionLevel"></param>
        /// <returns></returns>
        public static bool DoesUserHavePermissions(SPWeb web, string loginName, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(web, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return web.DoesUserHavePermissions(loginName, roleDefinition.BasePermissions);
        }

        /// <summary>
        ///   Checks permissions of the current user for a specified set of rights and returns a Boolean value.
        /// </summary>
        /// <param name = "list"></param>
        /// <param name = "permissionLevel"></param>
        /// <returns></returns>
        public static bool DoesUserHavePermissions(SPList list, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(list.ParentWeb, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return list.DoesUserHavePermissions(roleDefinition.BasePermissions);
        }

        public static bool DoesUserHavePermissions(SPList list, SPUser user, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(list.ParentWeb, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return list.DoesUserHavePermissions(user, roleDefinition.BasePermissions);
        }

        public static bool DoesUserHavePermissionLevel(SPWeb web, string permissionLevel)
        {
            return DoesUserHavePermissionLevel(web, web.CurrentUser.LoginName, permissionLevel);
        }

        public static bool DoesUserHavePermissionLevel(SPWeb web, string loginName, string permissionLevel)
        {
            if (!web.DoesUserHavePermissions(SPBasePermissions.ManageWeb))
            {
                return DoesUserHavePermissionLevel(web.Site.ID, web.ID, loginName, permissionLevel);
            }

            var permissionInfo = web.GetUserEffectivePermissionInfo(loginName);
            return permissionInfo.RoleAssignments
                .SelectMany(roleAssignment => roleAssignment.RoleDefinitionBindings.Cast<SPRoleDefinition>())
                .Any(roleDefinition => roleDefinition.Name == permissionLevel);
        }

        private static bool DoesUserHavePermissionLevel(Guid siteId, Guid webId, string loginName, string permissionLevel)
        {
            var result = false;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(siteId))
                {
                    using (var web = site.OpenWeb(webId))
                    {
                        result = DoesUserHavePermissionLevel(web, loginName, permissionLevel);
                    }
                }
            });
            return result;
        }

        public static bool DoesUserHavePermissionLevel(SPList list, string permissionLevel)
        {
            return DoesUserHavePermissionLevel(list, list.ParentWeb.CurrentUser.LoginName, permissionLevel);
        }

        public static bool DoesUserHavePermissionLevel(SPList list, string loginName, string permissionLevel)
        {
            var web = list.ParentWeb;

            if (!web.DoesUserHavePermissions(SPBasePermissions.ManageWeb))
            {
                return DoesUserHavePermissionLevel(web.Site.ID, web.ID, list, loginName, permissionLevel);
            }

            var permissionInfo = list.GetUserEffectivePermissionInfo(loginName);
            return permissionInfo.RoleAssignments
                .SelectMany(roleAssignment => roleAssignment.RoleDefinitionBindings.Cast<SPRoleDefinition>())
                .Any(roleDefinition => roleDefinition.Name == permissionLevel);
        }

        private static bool DoesUserHavePermissionLevel(Guid siteId, Guid webId, SPList list, string loginName, string permissionLevel)
        {
            var result = false;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (var site = new SPSite(siteId))
                {
                    using (var web = site.OpenWeb(webId))
                    {
                        var newList = web.Lists[list.ID];
                        result = DoesUserHavePermissionLevel(newList, loginName, permissionLevel);
                    }
                }
            });
            return result;
        }

        #region Obsolete

        /// <summary>
        ///   Checks permissions of the current user for a specified set of rights and returns a Boolean value.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "permissionLevel"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool HasPermissionLevel(SPWeb web, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(web, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return web.DoesUserHavePermissions(roleDefinition.BasePermissions);
        }

        /// <summary>
        ///   Checks permissions of the specified user for a specified set of rights and returns a Boolean value.
        /// </summary>
        /// <param name = "web"></param>
        /// <param name = "loginName"></param>
        /// <param name = "permissionLevel"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool HasPermissionLevel(SPWeb web, string loginName, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(web, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return web.DoesUserHavePermissions(loginName, roleDefinition.BasePermissions);
        }

        /// <summary>
        ///   Checks permissions of the current user for a specified set of rights and returns a Boolean value.
        /// </summary>
        /// <param name = "list"></param>
        /// <param name = "permissionLevel"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool HasPermissionLevel(SPList list, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(list.ParentWeb, permissionLevel);
            if (roleDefinition == null)
            {
                return false;
            }

            return list.DoesUserHavePermissions(roleDefinition.BasePermissions);
        }

        #endregion

        /// <summary>
        /// Add role assignment for the principal into web
        /// </summary>
        /// <param name="web"></param>
        /// <param name="principal"></param>
        /// <param name="permissionLevel"></param>
        public static void AddRoleAssignment(SPWeb web, SPPrincipal principal, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(web, permissionLevel);
            if (roleDefinition == null)
            {
                throw new ArgumentNullException(permissionLevel);
            }

            var roleAssignment = new SPRoleAssignment(principal);
            roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
            web.RoleAssignments.Add(roleAssignment);
        }

        /// <summary>
        /// Add role assignment for the group into web
        /// </summary>
        /// <param name="web"></param>
        /// <param name="groupName"></param>
        /// <param name="permissionLevel"></param>
        public static void AddRoleAssignment(SPWeb web, string groupName, string permissionLevel)
        {
            var group = GetGroup(web, groupName);
            AddRoleAssignment(web, group, permissionLevel);
        }

        /// <summary>
        /// Add role assignment for the group into list
        /// </summary>
        /// <param name="web"></param>
        /// <param name="list"></param>
        /// <param name="groupName"></param>
        /// <param name="permissionLevel"></param>
        public static void AddRoleAssignment(SPWeb web, SPList list, string groupName, string permissionLevel)
        {
            var group = GetGroup(web, groupName);
            AddRoleAssignment(web, list, group, permissionLevel);
        }

        /// <summary>
        /// Add role assignment for the principal into list
        /// </summary>
        /// <param name="web"></param>
        /// <param name="list"></param>
        /// <param name="principal"></param>
        /// <param name="permissionLevel"></param>
        public static void AddRoleAssignment(SPWeb web, SPList list, SPPrincipal principal, string permissionLevel)
        {
            var roleDefinition = GetRoleDefinition(web, permissionLevel);
            if (roleDefinition == null)
            {
                throw new ArgumentNullException(permissionLevel);
            }

            var roleAssignment = new SPRoleAssignment(principal);
            roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
            list.RoleAssignments.Add(roleAssignment);
        }

        /// <summary>
        /// Clear role assignment of principal from web
        /// </summary>
        /// <param name="web"></param>
        /// <param name="principal"></param>
        public static void ClearRoleAssignment(SPWeb web, SPPrincipal principal)
        {
            var roleAssignment = web.RoleAssignments.GetAssignmentByPrincipal(principal);
            roleAssignment.RoleDefinitionBindings.RemoveAll();
        }

        /// <summary>
        /// Clear role assignment of group from web
        /// </summary>
        /// <param name="web"></param>
        /// <param name="groupName"></param>
        public static void ClearRoleAssignment(SPWeb web, string groupName)
        {
            var group = GetGroup(web, groupName);
            ClearRoleAssignment(web, group);
        }

        /// <summary>
        /// Check login name is exists in Active Directory domain
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static bool IsUserInAD(string loginName)
        {
            try
            {
#pragma warning disable 168
                var sid = (SecurityIdentifier)new NTAccount(loginName).Translate(typeof(SecurityIdentifier));
#pragma warning restore 168
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool SetAllowUnsafeUpdates(SPWeb web)
        {
            if (!web.AllowUnsafeUpdates)
            {
                web.AllowUnsafeUpdates = true;
                return true;
            }
            return false;
        }

        internal static void RollbackAllowUnsafeUpdates(SPWeb web, bool status)
        {
            if (status)
            {
                web.AllowUnsafeUpdates = false;
            }
        }
    }
}