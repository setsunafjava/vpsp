using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public abstract class RibbonDelegateControl : Control, IPostBackEventHandler
    {
        private SPRibbon ribbon;
        private bool isRegistedRibbons;
        private List<IRibbonCommand> ribbonCommands;

        private const string RibbonCustomizationScript = @"
            Type.registerNamespace('RibbonCustomization');
            RibbonCustomization.PageComponent = function () {
                RibbonCustomization.PageComponent.initializeBase(this);
            }
            RibbonCustomization.PageComponent.initialize = function () {
                ExecuteOrDelayUntilScriptLoaded(Function.createDelegate(null, RibbonCustomization.PageComponent.initializePageComponent), 'SP.Ribbon.js');
            }
            RibbonCustomization.PageComponent.initializePageComponent = function () {
                var ribbonPageManager = SP.Ribbon.PageManager.get_instance();
                if (null !== ribbonPageManager) {
                    ribbonPageManager.addPageComponent(RibbonCustomization.PageComponent.instance);
                    ribbonPageManager.get_focusManager().requestFocusForComponent(RibbonCustomization.PageComponent.instance);
                }
            }
            RibbonCustomization.PageComponent.refreshRibbonStatus = function () {
                SP.Ribbon.PageManager.get_instance().get_commandDispatcher().executeCommand(Commands.CommandIds.ApplicationStateChanged, null);
            }
            RibbonCustomization.PageComponent.prototype = {
                getFocusedCommands: function () {
                    return [];
                },
                getGlobalCommands: function () {
                    return getGlobalCommands();
                },
                isFocusable: function () {
                    return true;
                },
                receiveFocus: function () {
                    return true;
                },
                yieldFocus: function () {
                    return true;
                },
                canHandleCommand: function (commandId) {
                    return commandEnabled(commandId);
                },
                handleCommand: function (commandId, properties, sequence) {
                    return handleCommand(commandId, properties, sequence);
                }
            }
            function RegisterRibbonCustomization() {
                RibbonCustomization.PageComponent.registerClass('RibbonCustomization.PageComponent', CUI.Page.PageComponent);
                RibbonCustomization.PageComponent.instance = new RibbonCustomization.PageComponent();
                RibbonCustomization.PageComponent.initialize();
            }
            ExecuteOrDelayUntilScriptLoaded(RegisterRibbonCustomization, 'CUI.js');";

        protected override void OnPreRender(EventArgs e)
        {
            var list = SPContext.Current.List;
            var listName = list != null ? list.Title : string.Empty;

            string viewName;
            var viewContext = SPContext.Current.ViewContext;
            if (viewContext != null && viewContext.View != null)
            {
                viewName = viewContext.View.Title;
            }
            else
            {
                viewName = string.Empty;
            }

            ribbon = SPRibbon.GetCurrent(Page);
            ribbonCommands = new List<IRibbonCommand>();

            RegisterRibbonExtension(SPContext.Current.Web, listName, viewName);

            if (isRegistedRibbons)
            {
                // Register required scripts
                ScriptLink.RegisterScriptAfterUI(Page, "CUI.js", false, true);
                ScriptLink.RegisterScriptAfterUI(Page, "SP.js", false, true);
                ScriptLink.RegisterScriptAfterUI(Page, "SP.Runtime.js", false, true);
                ScriptLink.RegisterScriptAfterUI(Page, "SP.Ribbon.js", false, true);

                // Register custom page component to handle custom ribbon commands
                if (!Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType(), "Register Ribbon Customization"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Register Ribbon Customization",
                                                                RibbonCustomizationScript, true);
                }

                // Register custom ribbon commands
                var ribbonScriptManager = new SPRibbonScriptManager();
                ribbonScriptManager.RegisterGetCommandsFunction(Page, "getGlobalCommands", ribbonCommands);
                ribbonScriptManager.RegisterCommandEnabledFunction(Page, "commandEnabled", ribbonCommands);
                ribbonScriptManager.RegisterHandleCommandFunction(Page, "handleCommand", ribbonCommands);
            }

            base.OnPreRender(e);
        }

        protected abstract void RegisterRibbonExtension(SPWeb web, string listName, string viewName);

        protected void RegisterRibbonExtension(RibbonTab ribbonTab)
        {
            RegisterRibbonExtension(ribbonTab, true);
        }

        protected void RegisterRibbonExtension(RibbonTab ribbonTab, bool registerGroupTemplates)
        {
            ribbon.RegisterDataExtension(ribbonTab.GetXmlDefinition(), "Ribbon.Tabs._children");

            if (registerGroupTemplates)
            {
                // Group Template
                var groupsTemplates =
                    ribbonTab.Groups.Select(item => item.GroupTemplate).Where(item => item != null).Distinct(new RibbonGroupTemplateComparer());
                foreach (var groupTemplate in groupsTemplates)
                {
                    ribbon.RegisterDataExtension(groupTemplate.GetXmlDefinition(), "Ribbon.Templates._children");
                }
            }

            // Select all command to register
            var commands = ribbonTab.Groups
                .SelectMany(item => item.Controls)
                .Select(item => item.Command)
                .Where(item => item != null);

            commands.Union(ribbonTab.Groups
                .SelectMany(item => item.Controls)
                .SelectMany(item => item.ChildControls)
                .Select(item => item.Command)
                .Where(item => item != null));
            ribbonCommands.AddRange(commands.Distinct(new RibbonCommandComparer()));

            isRegistedRibbons = true;
        }

        protected void RegisterRibbonExtension(RibbonGroup ribbonGroup, string location)
        {
            if (!location.EndsWith(".Groups._children"))
            {
                throw new ArgumentException(
                    "Group cannot be registered at this location, location must end with \".Groups._children\".");
            }
            var tabId = location.Replace(".Groups._children", "");

            // Ribbon Group
            ribbon.RegisterDataExtension(ribbonGroup.GetXmlDefinition(), location);

            // Ribbon Group Template
            ribbon.RegisterDataExtension(ribbonGroup.GroupTemplate.GetXmlDefinition(), "Ribbon.Templates._children");

            // MaxSize & Scale
            foreach (var maxSize in ribbonGroup.MaxSizes)
            {
                ribbon.RegisterDataExtension(maxSize.GetXmlDefinition(), tabId + ".Scaling._children");
            }

            foreach (var scale in ribbonGroup.Scales)
            {
                ribbon.RegisterDataExtension(scale.GetXmlDefinition(), tabId + ".Scaling._children");
            }

            // Select all command to register
            ribbonCommands.AddRange(ribbonGroup.Controls.Select(item => item.Command).Where(command => command != null));

            isRegistedRibbons = true;
        }

        public void RegisterRibbonExtension(IRibbonControl ribbonControl, string location)
        {
            ribbon.RegisterDataExtension(ribbonControl.GetXmlDefinition(), location);

            // Select all command to register
            if (ribbonControl.Command != null)
            {
                ribbonCommands.Add(ribbonControl.Command);
            }

            isRegistedRibbons = true;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            OnRibbonPostBack(RibbonMethodPostBackEventArgs.Deserialize(eventArgument));
        }

        private void OnRibbonPostBack(RibbonMethodPostBackEventArgs eventArgs)
        {
            if (eventArgs == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(eventArgs.MethodName))
            {
                return;
            }

            try
            {
                var method = GetType().GetMethod(eventArgs.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                method.Invoke(this, new object[] { eventArgs.Arguments });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                throw;
            }
        }
    }
}
