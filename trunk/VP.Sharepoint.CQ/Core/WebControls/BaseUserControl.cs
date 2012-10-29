using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class BaseUserControl : UserControl, IPostBackEventHandler
    {
        protected SPRibbon ribbon;

        /// <summary>
        ///   Get the page ribbon instance
        /// </summary>
        public SPRibbon Ribbon
        {
            get { return ribbon ?? (ribbon = SPRibbon.GetCurrent(Page)); }
        }

        /// <summary>
        ///   Identify if the form is in the upload mode
        /// </summary>
        protected bool UploadMode
        {
            get
            {
                return (((SPContext.Current.FormContext.FormMode == SPControlMode.Edit) && (HttpContext.Current != null)) &&
                        (HttpContext.Current.Request.QueryString["Mode"] == "Upload"));
            }
        }

        #region Ribbon Support

        protected const string RibbonCustomizationScript =
            @"
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

        private List<IRibbonCommand> ribbonCommands;

        /// <summary>
        ///   Store the list of custom ribbon commands
        /// </summary>
        protected List<IRibbonCommand> RibbonCommands
        {
            get { return ribbonCommands ?? (ribbonCommands = new List<IRibbonCommand>()); }
        }

        /// <summary>
        ///   Deserialize the JSON event argument and raise post back event for the custom ribbon commands
        /// </summary>
        /// <param name = "eventArgument">JSON event arguments</param>
        public void RaisePostBackEvent(string eventArgument)
        {
            OnRibbonPostBack(RibbonMethodPostBackEventArgs.Deserialize(eventArgument));
            OnRibbonPostBack(new JavaScriptSerializer().Deserialize<RibbonPostBackEventArgs>(eventArgument));
        }

        /// <summary>
        ///   Register the XML extension for the ribbon
        /// </summary>
        /// <param name = "xmlExtension">Ribbon XML extension string</param>
        /// <param name = "location">Location of the ribbon definition to register</param>
        public void RegisterRibbonExtension(string xmlExtension, string location)
        {
            var ribbonExtension = new XmlDocument();
            ribbonExtension.LoadXml(xmlExtension);

            // Register the ribbon extension
            SPRibbon.GetCurrent(Page).RegisterDataExtension(ribbonExtension.FirstChild, location);
        }

        /// <summary>
        ///   Register the required script for the ribbon extension
        /// </summary>
        protected void RegisterRibbonScript()
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
            ribbonScriptManager.RegisterGetCommandsFunction(Page, "getGlobalCommands", RibbonCommands);
            ribbonScriptManager.RegisterCommandEnabledFunction(Page, "commandEnabled", RibbonCommands);
            ribbonScriptManager.RegisterHandleCommandFunction(Page, "handleCommand", RibbonCommands);
        }

        /// <summary>
        ///   Register the ribbon script on prerender
        /// </summary>
        /// <param name = "e">Event arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            RegisterRibbonScript();
            base.OnPreRender(e);
        }

        /// <summary>
        ///   Post back event handler for the custom ribbon commands
        /// </summary>
        /// <param name = "e">Event arguments</param>
        protected virtual void OnRibbonPostBack(RibbonPostBackEventArgs e)
        {
        }

        /// <summary>
        /// Initially show the ribbon tab based on its tab id
        /// </summary>
        /// <param name = "tabId">Id of the tab</param>
        public void ShowRibbonTab(string tabId)
        {
            LoadRibbonTab(tabId);
            Ribbon.Minimized = false;
            Ribbon.CommandUIVisible = true;
            Ribbon.InitialTabId = tabId;
        }

        /// <summary>
        /// Initially show the ribbon tab
        /// </summary>
        /// <param name="ribbonTab">Ribbon tab</param>
        public void ShowRibbonTab(RibbonTab ribbonTab)
        {
            ShowRibbonTab(ribbonTab.Id);
        }

        /// <summary>
        /// Load the ribbon tab to the ribbon bar without showing it based on its tab id
        /// </summary>
        /// <param name = "tabId">Id of the tab</param>
        public void LoadRibbonTab(string tabId)
        {
            Ribbon.MakeTabAvailable(tabId);
        }

        /// <summary>
        /// Load the ribbon tab to the ribbon bar without showing it
        /// </summary>
        /// <param name="ribbonTab">Ribbon tab</param>
        public void LoadRibbonTab(RibbonTab ribbonTab)
        {
            LoadRibbonTab(ribbonTab.Id);
        }

        /// <summary>
        ///   Hide the ribbon tab, group or control (button, checkbox, etc.) based on its id
        /// </summary>
        /// <param name = "controlId">Id of the control</param>
        public void HideRibbonControl(string controlId)
        {
            Ribbon.TrimById(controlId);
        }

        #endregion

        #region Register Contextual Group

        public void RegisterContextualGroup(RibbonContextualGroup contextualGroup)
        {
            Ribbon.RegisterDataExtension(contextualGroup.GetXmlDefinition(), "Ribbon.ContextualTabs._children");
            // Group Template
            var groupsTemplates =
                contextualGroup.Tabs.SelectMany(item => item.Groups).Select(item => item.GroupTemplate).Where(item => item != null).Distinct(
                    new RibbonGroupTemplateComparer());
            foreach (var groupTemplate in groupsTemplates)
            {
                Ribbon.RegisterDataExtension(groupTemplate.GetXmlDefinition(), "Ribbon.Templates._children");
            }

            // Select all command to register
            RibbonCommands.AddRange(
                contextualGroup.Tabs.SelectMany(item => item.Groups).SelectMany(item => item.Controls).Select(item => item.Command).Where(
                    item => item != null).Distinct(new RibbonCommandComparer()));
        }

        #endregion

        #region Ribbon Tab

        public void RegisterRibbonGroupTemplate(RibbonGroupTemplate groupTemplate)
        {
            Ribbon.RegisterDataExtension(groupTemplate.GetXmlDefinition(), "Ribbon.Templates._children");
        }

        public void RegisterRibbonTab(RibbonTab ribbonTab)
        {
            RegisterRibbonTab(ribbonTab, true);
        }

        public void RegisterRibbonTab(RibbonTab ribbonTab, bool registerGroupTemplates)
        {
            // Tab
            Ribbon.RegisterDataExtension(ribbonTab.GetXmlDefinition(), "Ribbon.Tabs._children");

            if (registerGroupTemplates)
            {
                // Group Template
                var groupsTemplates =
                    ribbonTab.Groups.Select(item => item.GroupTemplate).Where(item => item != null).Distinct(
                        new RibbonGroupTemplateComparer());
                foreach (var groupTemplate in groupsTemplates)
                {
                    Ribbon.RegisterDataExtension(groupTemplate.GetXmlDefinition(), "Ribbon.Templates._children");
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
            RibbonCommands.AddRange(commands.Distinct(new RibbonCommandComparer()));
        }

        #endregion

        #region Ribbon Control

        public void RegisterRibbonControl(IRibbonControl ribbonControl, string location)
        {
            Ribbon.RegisterDataExtension(ribbonControl.GetXmlDefinition(), location);

            // Select all command to register
            if (ribbonControl.Command != null)
            {
                RibbonCommands.Add(ribbonControl.Command);
            }
        }

        #endregion

        #region Ribbon Group

        public void RegisterRibbonGroup(RibbonGroup ribbonGroup, string location)
        {
            if (!location.EndsWith(".Groups._children"))
            {
                throw new ArgumentException(
                    "Group cannot be registered at this location, location must end with \".Groups._children\".");
            }
            var tabId = location.Replace(".Groups._children", "");

            // Ribbon Group
            Ribbon.RegisterDataExtension(ribbonGroup.GetXmlDefinition(), location);

            // Ribbon Group Template
            Ribbon.RegisterDataExtension(ribbonGroup.GroupTemplate.GetXmlDefinition(), "Ribbon.Templates._children");

            // MaxSize & Scale
            foreach (var maxSize in ribbonGroup.MaxSizes)
            {
                Ribbon.RegisterDataExtension(maxSize.GetXmlDefinition(), tabId + ".Scaling._children");    
            }

            foreach (var scale in ribbonGroup.Scales)
            {
                Ribbon.RegisterDataExtension(scale.GetXmlDefinition(), tabId + ".Scaling._children");    
            }

            // Select all command to register
            RibbonCommands.AddRange(ribbonGroup.Controls.Select(item => item.Command).Where(command => command != null));
        }

        #endregion

        #region Ribbon Command

        public void RegisterRibbonCommand(IRibbonCommand command)
        {
            RibbonCommands.Add(command);
        }

        #endregion

        #region Handler Ribbon Postback

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
                var method = GetType().BaseType.GetMethod(eventArgs.MethodName,
                                                          BindingFlags.Instance | BindingFlags.Public |
                                                          BindingFlags.NonPublic);
                method.Invoke(this, new object[] {eventArgs.Arguments});
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

        #endregion
    }
}
