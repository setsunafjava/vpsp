using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using VP.Sharepoint.CQ.Core.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Menu = Microsoft.SharePoint.WebControls.Menu;
using System.Linq.Expressions;
using VP.Sharepoint.CQ.Common;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public abstract class BaseDataView : WebControl, IPostBackEventHandler
    {
        #region Privates

        private ArrayList groupFields;
        private ArrayList sortFields;
        private ArrayList viewFields;
        private IList<MenuTemplate> headerMenuTemplates;
        private IList<Menu> headerMenus;
        private MenuTemplate contextMenuTemplate;
        private LinkButton btnPrevious;
        private LinkButton btnNext;
        private int viewCounter;
        private bool? enableAddNewItem;
        private bool? enableDeleteItem;
        private bool? enableEditItem;
        private Hashtable contextMenus;
        private HiddenField hdfSelectedItems;
        private StateManager groupStateManager;
        private CollapsedGroupManager collapsedGroupManager;
        private bool isFilter;
        
        #endregion

        protected BaseDataView()
            : base(HtmlTextWriterTag.Table)
        {
        }

        [Browsable(true)]
        [DisplayName("ViewFields")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ViewFieldRefCollectionEditor), typeof(UITypeEditor))]
        public ArrayList ViewFields
        {
            get { return viewFields ?? (viewFields = new ArrayList()); }
        }

        [Browsable(true)]
        [DisplayName("GroupFields")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ViewFieldRefCollectionEditor), typeof(UITypeEditor))]
        public ArrayList GroupFields
        {
            get { return groupFields ?? (groupFields = new ArrayList()); }
        }

        [Browsable(true)]
        [DisplayName("SortFields")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof (SortFieldRefCollectionEditor), typeof (UITypeEditor))]
        public ArrayList SortFields
        {
            get { return sortFields ?? (sortFields = new ArrayList()); }
        }

        public DataTable DataSource { get; set; }

        /// <summary>
        /// Indicates whether display form of item show in dialog. 
        /// </summary>
        [Obsolete]
        public virtual bool DisplayItemInDialog
        { 
            get; set;
        }

        protected string NextPagePosition { get; set; }

        protected string PrevPagePosition { get; set; }

        /// <summary>
        /// Internal sort field name
        /// </summary>
        protected string SortField { get; set; }

        protected string SortDir { get; set; }

        [Browsable(false)]
        [DefaultValue(1)]
        public virtual int CurrentPage
        {
            get
            {
                var value = ViewState["CurrentPage"];
                if (value != null)
                {
                    return (int) value;
                }
                return 1;
            }
            set { ViewState["CurrentPage"] = value; }
        }

        [Browsable(true)]
        public string MenuField
        {
            get
            {
                var value = ViewState["MenuField"];
                if (value != null)
                {
                    return (string)value;
                }
                return string.Empty;
            }
            set { ViewState["MenuField"] = value; }
        }

        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowTotalItems
        {
            get
            {
                var value = ViewState["ShowTotalItems"];
                if (value != null)
                {
                    return (bool) value;
                }
                return false;
            }
            set { ViewState["ShowTotalItems"] = value; }
        }

        [Browsable(true)]
        [DefaultValue("Total items:")]
        public string TotalItemsText
        {
            get
            {
                var value = ViewState["TotalItemsText"];
                if (value != null)
                {
                    return (string) value;
                }
                return "Total items:";
            }
            set { ViewState["TotalItemsText"] = value; }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        public virtual bool EnableAddNewItem
        {
            get
            {
                if (!enableAddNewItem.HasValue)
                {
                    return false;
                }
                return enableAddNewItem.Value;
            }
            set { enableAddNewItem = value; }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        public virtual bool EnableEditItem
        {
            get
            {
                if (!enableEditItem.HasValue)
                {
                    return false;
                }
                return enableEditItem.Value;
            }
            set { enableEditItem = value; }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        public virtual bool EnableDeleteItem
        {
            get
            {
                if (!enableDeleteItem.HasValue)
                {
                    return false;
                }
                return enableDeleteItem.Value;
            }
            set { enableDeleteItem = value; }
        }

        [DefaultValue(100)]
        public virtual int RowLimit
        {
            get
            {
                var value = ViewState["RowLimit"];
                if (value != null)
                {
                    return (int)value;
                }
                return 100;
            }
            set { ViewState["RowLimit"] = value; }
        }

        protected virtual bool HasNextPage { get; set; }

        protected virtual int StartItemIndex
        {
            get { return (CurrentPage * RowLimit) - RowLimit + 1; }
        }

        protected virtual int EndItemIndex
        {
            get { return StartItemIndex + DataSource.Rows.Count - 1; }
        }

        protected virtual bool ThresholdException { get; set; }

        protected virtual bool RequiredAggregations
        {
            get
            {
                if (ThresholdException)
                {
                    return false;
                }
                return ShowTotalItems || GroupFields.Cast<IGroupFieldRef>().Any(item => item.CountGroupItems) ||
                       ViewFields.Cast<BaseFieldRef>().Any(item => item.CountFieldData || item.SumFieldData);
            }
        }

        public event ContextMenuEventHandler ContextMenuRender;

        public virtual void OnContextMenuRender(ContextMenuEventArgs args)
        {
            if (ContextMenuRender != null)
            {
                ContextMenuRender(args);
            }
        }

        protected abstract void BindDataSource();

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            #region Restore filter & sorter

            var filterFields = Page.Request.QueryString.AllKeys.Where(k => k != null && k.StartsWith("FilterField"));
            foreach (var filterField in filterFields)
            {
                var fieldName = Page.Request.QueryString[filterField];
                var viewField = ViewFields.Cast<BaseFieldRef>().Where(f => !f.IsHidden).FirstOrDefault(f => f.InternalFieldName == fieldName);
                if (viewField != null)
                {
                    var surfix = filterField.Replace("FilterField", "");
                    viewField.IsFilter = true;
                    viewField.FilterValue = Page.Request.QueryString["FilterValue" + surfix];
                }
            }

            SortField = Page.Request.QueryString["SortField"];
            SortDir = Page.Request.QueryString["SortDir"];
            if (!string.IsNullOrEmpty(SortDir))
            {
                SortDir = SortDir.ToUpperInvariant();
            }

            #endregion

            viewCounter = new Random(1).Next(100, 1000);
            var lcid = Thread.CurrentThread.CurrentUICulture.LCID;
            isFilter = !string.IsNullOrEmpty(Page.Request.QueryString["Filter"]);

            #region Header Menu

            headerMenuTemplates = new List<MenuTemplate>();

            foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(f => f.Sortable || f.Filterable))
            {
                var headerMenuTemplate = new MenuTemplate { ID = "FilterMenuTemplate_" + viewField.InternalFieldName, CompactMode = true };
                Controls.Add(headerMenuTemplate);

                if (viewField.Sortable)
                {
                    var ascMenuItemTemplate = new MenuItemTemplate
                    {
                        ID = "FilterMenuTemplate_SortAsc_" + viewField.InternalFieldName,
                        Text = SPResource.GetString(Strings.VersionsSortAscending, new object[0]),
                        ImageUrl = string.Format("/_layouts/{0}/images/sortazlang.gif", lcid),
                        ClientOnClickNavigateUrl = "%SORT_URL_BY_ASC%"
                    };
                    headerMenuTemplate.Controls.Add(ascMenuItemTemplate);

                    var descMenuItemTemplate = new MenuItemTemplate
                    {
                        ID = "FilterMenuTemplate_SortDesc_" + viewField.InternalFieldName,
                        Text = SPResource.GetString(Strings.VersionsSortDescending, new object[0]),
                        ImageUrl = string.Format("/_layouts/{0}/images/sortzalang.gif", lcid),
                        ClientOnClickNavigateUrl = "%SORT_URL_BY_DESC%",
                    };
                    headerMenuTemplate.Controls.Add(descMenuItemTemplate);

                    headerMenuTemplate.Controls.Add(new MenuSeparatorTemplate { ID = "FilterMenuTemplate_Separator_" + viewField.InternalFieldName });
                }

                if (viewField.Filterable)
                {
                    var clearFilterMenuTeplate = new MenuItemTemplate
                                                     {
                                                         ID = "FilterMenuTemplate_ClearFilter_" + viewField.InternalFieldName,
                                                         Text = SPResource.GetString(Strings.ClearFilterFromField, new object[] {"%FIELDDISPLAYNAME%"}),
                                                         ClientOnClickNavigateUrl = "%CLEAR_FILTER_URL%",
                                                         ImageUrl = viewField.IsFilter
                                                                        ? "/_layouts/images/filteroff.gif"
                                                                        : "/_layouts/images/filteroffdisabled.gif",
                                                     };

                    clearFilterMenuTeplate.Attributes["clearFilterItem"] = "true";
                    headerMenuTemplate.Controls.Add(clearFilterMenuTeplate);
                }

                headerMenuTemplates.Add(headerMenuTemplate);
            }

            headerMenus = new List<Menu>();
            var urlBuilder = new UrlBuilder(Page.Request.RawUrl);
            urlBuilder.RemoveAllSortQueryString();

            foreach (var field in ViewFields.Cast<BaseFieldRef>().Where(f => f.Sortable || f.Filterable))
            {
                var menu = new Menu(field.HeaderText, string.Empty)
                               {
                                   SuppressBubbleIfPostback = true,
                                   UseMaximumWidth = true,
                                   TemplateId = "FilterMenuTemplate_" + field.InternalFieldName,
                                   ID = string.Concat("FilterMenu", ViewFields.IndexOf(field)),
                                   AlignmentElementOverrideClientId = string.Format("th{0}", field.InternalFieldName),
                               };

                if (field.IsFilter)
                {
                    var urlBuilderForFilter = new UrlBuilder(Page.Request.RawUrl);
                    menu.TokenNamesAndValues.Add("CLEAR_FILTER_URL", SPHttpUtility.NoEncode(urlBuilderForFilter.GetUrlWithoutFilterValue(field.InternalFieldName)));
                }

                urlBuilder.AddQueryString("SortField", field.InternalFieldName);

                urlBuilder.AddQueryString("SortDir", "Asc");
                menu.TokenNamesAndValues.Add("SORT_URL_BY_ASC", SPHttpUtility.NoEncode(urlBuilder.ToString()));

                urlBuilder.AddQueryString("SortDir", "Desc");
                menu.TokenNamesAndValues.Add("SORT_URL_BY_DESC", SPHttpUtility.NoEncode(urlBuilder.ToString()));

                menu.TokenNamesAndValues.Add("FIELDDISPLAYNAME", SPHttpUtility.NoEncode(field.HeaderText));
                menu.TokenNamesAndValues.Add("FIELDNAME", SPHttpUtility.NoEncode(field.InternalFieldName));

                headerMenus.Add(menu);
                Controls.Add(menu);
            }

            #endregion

            #region Context Menu

            contextMenuTemplate = new MenuTemplate { ID = "contextMenuTemplate" + this.ID, CompactMode = true };
            Controls.Add(contextMenuTemplate);

            var viewItem = new MenuItemTemplate
            {
                Text = SPResource.GetString(Strings.DisplayFormTitleViewItem),
                //ClientOnClickNavigateUrl = "%WEBURL%/_layouts/listform.aspx?PageType=4&ListId=%LISTID%&ID=%ITEMID%",
                ClientOnClickScript = "FW_OpenDisplayDialog(\"%WEBURL%/_layouts/listform.aspx?PageType=4&ListId=%LISTID%&ID=%ITEMID%&Source=%SOURCE%\");return false;"
            };
            contextMenuTemplate.Controls.Add(viewItem);

            var editItem = new MenuItemTemplate
            {
                ID = "EditItem" + this.ID,
                Text = SPResource.GetString(Strings.ButtonTextEditItem),
                ImageUrl = "~/_layouts/images/edititem.gif",
                //ClientOnClickNavigateUrl = "%WEBURL%/_layouts/listform.aspx?PageType=6&ListId=%LISTID%&ID=%ITEMID%",
                ClientOnClickScript = "FW_OpenDisplayDialog(\"%WEBURL%/_layouts/listform.aspx?PageType=6&ListId=%LISTID%&ID=%ITEMID%&Source=%SOURCE%\");return false;"
            };
            contextMenuTemplate.Controls.Add(editItem);

            contextMenuTemplate.Controls.Add(new MenuSeparatorTemplate());

            var deleteItem = new MenuItemTemplate
            {
                ID = "DeleteItem" + this.ID,
                Text = SPResource.GetString(Strings.ButtonTextDeleteItem),
                ImageUrl = "~/_layouts/images/delitem.gif",
                ClientOnClickNavigateUrl = string.Concat("javascript:if(confirm(L_STSRecycleConfirm_Text))", Page.ClientScript.GetPostBackEventReference(this, "_ITEMDELETE;%ITEMID%;%LISTID%"))
            };
            contextMenuTemplate.Controls.Add(deleteItem);

            #endregion

            #region Paging

            btnPrevious = new LinkButton
            {
                Text = string.Format("<img border=\"0\" alt=\"Previous\" src=\"/_layouts/{0}/images/prev.gif\">", lcid)
            };
            Controls.Add(btnPrevious);

            btnNext = new LinkButton
            {
                Text = string.Format("<img border=\"0\" alt=\"Next\" src=\"/_layouts/{0}/images/next.gif\">", lcid)
            };
            Controls.Add(btnNext);

            #endregion

            hdfSelectedItems = new HiddenField();
            Controls.Add(hdfSelectedItems);

            groupStateManager = new StateManager();
            Controls.Add(groupStateManager);

            collapsedGroupManager = new CollapsedGroupManager();
            Controls.Add(collapsedGroupManager);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();

            btnPrevious.Click += PreviousPaging;
            btnNext.Click += NextPaging;

            // Breadcrumb
            if (SPContext.Current.ViewContext != null && SPContext.Current.ViewContext.View != null)
            {
                var placeHolderPageTitleInTitleArea = Page.Master.FindControl("PlaceHolderPageTitleInTitleArea");
                if (placeHolderPageTitleInTitleArea != null)
                {
                    placeHolderPageTitleInTitleArea.Controls.Add(new DataViewSelectorMenu());
                }
            }
        }

        protected virtual void OnNextPaging()
        {
            
        }

        protected virtual void OnPreviousPaging()
        {

        }

        private void NextPaging(object sender, EventArgs e)
        {
            CurrentPage++;
            groupStateManager.Clear();
            OnNextPaging();
        }

        private void PreviousPaging(object sender, EventArgs e)
        {
            CurrentPage--;
            groupStateManager.Clear();
            OnPreviousPaging();
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, DataSource.Rows.Count > 0 ? "ms-listviewtable" : "ms-emptyView");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute("onmouseover", string.Format("EnsureSelectionHandler(window.event,this,{0})", viewCounter));
            writer.AddAttribute(HtmlTextWriterAttribute.Dir, "none");
            writer.AddAttribute("handledeleteinit", "true");
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            BindDataSource();

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("jquery-1.7.1.js"))
            {
                Utilities.LoadJS(SPContext.Current.Web, this.Page, "jquery-1.7.1.js");
            }
            if (!Page.ClientScript.IsClientScriptIncludeRegistered("DataViewBehavior.js"))
            {
                Utilities.LoadJS(SPContext.Current.Web, this.Page, "DataViewBehavior.js");
            }

            // Clear selected items;
            hdfSelectedItems.Value = "";

            // function FW_OnChildColumn);
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_OnChildColumn"))
            {
                var fncOnChildColumn = new StringBuilder();
                fncOnChildColumn.BeginFunction("function FW_OnChildColumn(elm)");

                fncOnChildColumn.Append("var i;");
                fncOnChildColumn.BeginFunction("for (i=0; i < elm.childNodes.length; i++)");
                fncOnChildColumn.Append("var child=elm.childNodes[i];");

                fncOnChildColumn.BeginFunction(
                    "if (child.nodeType==1 && child.tagName==\"DIV\" && child.getAttribute(\"CtxNum\") !=null)");
                fncOnChildColumn.Append("DeferCall('FW_OnMouseOverFilter', child);break;");
                fncOnChildColumn.EndFunction();

                fncOnChildColumn.EndFunction();

                fncOnChildColumn.EndFunction();

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_OnChildColumn", fncOnChildColumn.ToString(),
                                                            true);
            }

            // function FW_OnMouseOverFilter
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_OnMouseOverFilter"))
            {
                var fncOnMouseOverFilter = new StringBuilder();
                fncOnMouseOverFilter.BeginFunction("function FW_OnMouseOverFilter(elm)");
                fncOnMouseOverFilter.Append("if (!IsFilterMenuEnabled()) return false;");
                fncOnMouseOverFilter.Append("if (IsFilterMenuOn() || bMenuLoadInProgress) return false;");
                fncOnMouseOverFilter.Append("if (window.location.href.search(\"[?&]Filter=1\") !=-1) return false;");
                fncOnMouseOverFilter.Append("if (elm.FilterDisable==\"TRUE\") return false;");
                fncOnMouseOverFilter.Append("if (IsFieldNotFilterable(elm) && IsFieldNotSortable(elm)) return false;");
                fncOnMouseOverFilter.Append("if (filterTable==elm) return;");
                fncOnMouseOverFilter.Append("if (filterTable !=null) FW_OnMouseOutFilter();");
                fncOnMouseOverFilter.Append("filterTable=elm;");
                fncOnMouseOverFilter.Append("var isTable=filterTable.tagName==\"TABLE\";");

                fncOnMouseOverFilter.BeginFunction("if (isTable)");
                fncOnMouseOverFilter.Append("filterTable.className=\"ms-selectedtitle\";");
                fncOnMouseOverFilter.Append("filterTable.onmouseout=FW_OnMouseOutFilter;");
                fncOnMouseOverFilter.EndFunction();

                fncOnMouseOverFilter.BeginFunction("else");
                fncOnMouseOverFilter.Append("var par=filterTable.parentNode;");
                fncOnMouseOverFilter.Append("par.onmouseout=FW_OnMouseOutFilter;");
                fncOnMouseOverFilter.Append("CreateCtxImg(par, FW_OnMouseOutFilter);");
                fncOnMouseOverFilter.EndFunction();

                fncOnMouseOverFilter.EndFunction();

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_OnMouseOverFilter",
                                                            fncOnMouseOverFilter.ToString(), true);
            }

            // function FW_OnMouseOutFilter
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_OnMouseOutFilter"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_OnMouseOutFilter",
                                                            "function FW_OnMouseOutFilter(evt){OnMouseOutFilter(evt);}",
                                                            true);
            }

            // function FW_ShowHideGroup
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_ShowHideGroup"))
            {
                var fncShowHideGroup = new StringBuilder();
                fncShowHideGroup.BeginFunction("function FW_ShowHideGroup(groupId, uniqueGroupId)");
                fncShowHideGroup.Append("var self = $(\"tbody[groupId=\" + groupId + \"]\");");
                fncShowHideGroup.Append("var ex = $(\"tbody[groupId^=\" + groupId + \"-\" + \"]\");");
                fncShowHideGroup.Append("var isCollapsed = self.attr('isCollapsed');");

                fncShowHideGroup.BeginFunction("if(isCollapsed == 'false')");
                fncShowHideGroup.Append("self.attr('isCollapsed', 'true');");
                fncShowHideGroup.AppendFormat("$('#{0}' + uniqueGroupId).val('true');", groupStateManager.ClientID);
                fncShowHideGroup.Append(
                    "$(\"img[groupId=\" + groupId + \"]\").attr('src', '/_layouts/images/plus.gif');");

                fncShowHideGroup.Append("$.each(ex, function(index, value){");
                fncShowHideGroup.Append("$(this).hide();");
                fncShowHideGroup.Append("var hideByGroupId = $(this).attr('hideByGroupId');");
                fncShowHideGroup.Append("if(hideByGroupId == undefined || hideByGroupId == ''){$(this).attr('hideByGroupId', groupId);}");
                fncShowHideGroup.Append("});");

                fncShowHideGroup.EndFunction();

                fncShowHideGroup.BeginFunction("else");
                fncShowHideGroup.Append("self.attr('isCollapsed', 'false');");
                fncShowHideGroup.AppendFormat("$('#{0}' + uniqueGroupId).val('false');", groupStateManager.ClientID);
                fncShowHideGroup.Append(
                    "$(\"img[groupId=\" + groupId + \"]\").attr('src', '/_layouts/images/minus.gif');");

                fncShowHideGroup.Append("$.each(ex, function(index, value){");
                fncShowHideGroup.Append("var _this = $(this);");
                fncShowHideGroup.BeginFunction("if(_this.attr('hideByGroupId') + '' == groupId)");
                fncShowHideGroup.Append("_this.show();");
                fncShowHideGroup.Append("_this.attr('hideByGroupId', '');");
                fncShowHideGroup.EndFunction();
                fncShowHideGroup.Append("});");

                fncShowHideGroup.EndFunction();

                fncShowHideGroup.EndFunction();
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_ShowHideGroup", fncShowHideGroup.ToString(),
                                                            true);
            }

            // function FW_ToggleCheckBox
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_ToggleCheckBox"))
            {
                var fncToggleCheckBox = new StringBuilder();
                fncToggleCheckBox.BeginFunction("function FW_ToggleCheckBox(elm)");
                fncToggleCheckBox.Append("try{if(window.event.srcElement.tagName.toLowerCase() == 'a') return;}catch(er){}");
                fncToggleCheckBox.Append("$(elm).toggleClass('s4-itm-selected');");

                fncToggleCheckBox.BeginFunction("if($(elm).hasClass('s4-itm-selected'))");
                fncToggleCheckBox.Append("$(\"input[type='checkbox']\", $(elm)).attr('checked', 'checked');");
                fncToggleCheckBox.EndFunction();

                fncToggleCheckBox.BeginFunction("else");
                fncToggleCheckBox.Append("$(\"input[type='checkbox']\", $(elm)).removeAttr('checked');");
                fncToggleCheckBox.Append("$('#chkToggleAllItems').removeAttr('checked');");
                fncToggleCheckBox.EndFunction();

                fncToggleCheckBox.Append("RibbonCustomization.PageComponent.refreshRibbonStatus();");
                fncToggleCheckBox.Append("refreshSelectedItemsValue();");
                fncToggleCheckBox.EndFunction();
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_ToggleCheckBox", fncToggleCheckBox.ToString(),
                                                            true);
            }

            // function FW_OpenDisplayDialog
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_OpenDisplayDialog"))
            {
                var fncOpenDisplayDialog = new StringBuilder();
                fncOpenDisplayDialog.BeginFunction("function FW_OpenDisplayDialog(url, e)");
                fncOpenDisplayDialog.Append("url = url + '';");
                fncOpenDisplayDialog.Append("var options = SP.UI.$create_DialogOptions();");
                fncOpenDisplayDialog.Append("options.url = url;");
                fncOpenDisplayDialog.Append(
                    "options.dialogReturnValueCallback = Function.createDelegate(null, function(result, target){if(result == SP.UI.DialogResult.OK){SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);}});");
                fncOpenDisplayDialog.Append("SP.UI.ModalDialog.showModalDialog(options);");

                // Cancel default event
                fncOpenDisplayDialog.BeginFunction("if(e != undefined)");
                fncOpenDisplayDialog.Append("var eventObject = jQuery.Event(e);");
                fncOpenDisplayDialog.Append("eventObject.preventDefault();");
                fncOpenDisplayDialog.Append("eventObject.isDefaultPrevented();");
                fncOpenDisplayDialog.Append("eventObject.stopPropagation();");
                fncOpenDisplayDialog.Append("eventObject.isPropagationStopped();");
                fncOpenDisplayDialog.Append("eventObject.stopImmediatePropagation();");
                fncOpenDisplayDialog.Append("eventObject.isImmediatePropagationStopped();");
                fncOpenDisplayDialog.EndFunction();

                fncOpenDisplayDialog.Append("return false;");
                fncOpenDisplayDialog.EndFunction();

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_OpenDisplayDialog",
                                                            fncOpenDisplayDialog.ToString(), true);
            }

            // function FW_ToggleAllItems
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_ToggleAllItems"))
            {
                var fncToggleAllItems = new StringBuilder();
                fncToggleAllItems.BeginFunction("function FW_ToggleAllItems(obj, ctx)");
                fncToggleAllItems.Append("var checked = $(obj).attr('checked');");

                fncToggleAllItems.BeginFunction("if(checked == true)");
                fncToggleAllItems.Append("$(\"input[class='s4-itm-cbx']\").attr('checked', 'checked');");
                fncToggleAllItems.Append("$(\"input[class='s4-itm-cbx']\").parent().parent().addClass('s4-itm-selected');");
                fncToggleAllItems.EndFunction();

                fncToggleAllItems.BeginFunction("else");
                fncToggleAllItems.Append("$(\"input[class='s4-itm-cbx']\").removeAttr('checked');");
                fncToggleAllItems.Append("$(\"input[class='s4-itm-cbx']\").parent().parent().removeClass('s4-itm-selected');");
                fncToggleAllItems.EndFunction();

                fncToggleAllItems.Append("RibbonCustomization.PageComponent.refreshRibbonStatus();");
                fncToggleAllItems.Append("refreshSelectedItemsValue();");

                fncToggleAllItems.EndFunction();
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_ToggleAllItems", fncToggleAllItems.ToString(),
                                                            true);
            }

            // function FW_OnItem
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_OnItem"))
            {
                var fncOnItem = new StringBuilder();
                fncOnItem.BeginFunction("function FW_OnItem(elm)");
                fncOnItem.Append("OnItem(elm);");
                fncOnItem.Append("var div = $(\"div.s4-ctx\", $(elm).parent());");
                fncOnItem.Append("var altclick = div.attr('altclick');");
                fncOnItem.Append("div.unbind().click(function(){eval(altclick);});");
                fncOnItem.EndFunction();

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_OnItem", fncOnItem.ToString(), true);
            }

            // function FW_BindDataForMenu
            if (!Page.ClientScript.IsClientScriptBlockRegistered("FW_BindDataForMenu"))
            {
                var fncBindDataForMenu = new StringBuilder();
                fncBindDataForMenu.BeginFunction(
                    "function FW_BindDataForMenu(menuTemplateId, menuId, fieldName, keys, values, clientId)");
                fncBindDataForMenu.Append("var menuTemplate = document.getElementById(menuTemplateId);");
                fncBindDataForMenu.Append("var menu = MMU_GetMenuFromClientId(menuId);");
                fncBindDataForMenu.Append("var items = keys.split(';#');");
                fncBindDataForMenu.Append("var values = values.split(';#');");
                fncBindDataForMenu.Append("if(keys == ';;No Filder') items = [];");

                // Remove old items
                fncBindDataForMenu.BeginFunction(
                    "for(var menuIndex=0; menuIndex < menuTemplate.childNodes.length; menuIndex++)");
                fncBindDataForMenu.Append("var menuChild=menuTemplate.childNodes[menuIndex];");

                fncBindDataForMenu.BeginFunction("if(menuChild.nodeName != '#text')");
                fncBindDataForMenu.Append(
                    "if (menuChild.getAttribute('isFilterItem')=='true'){menuTemplate.removeChild(menuChild);--menuIndex;}");
                fncBindDataForMenu.EndFunction();

                fncBindDataForMenu.EndFunction();

                // Add new items
                fncBindDataForMenu.BeginFunction("for(menuIndex = 0; menuIndex < items.length; menuIndex++)");
                fncBindDataForMenu.Append(
                    "var script = \"__doPostBack('\" + clientId + \"','FILTER;' + '\" + fieldName + \"' + \" + \"';' + '\" + items[menuIndex] + \"')\";");
                fncBindDataForMenu.Append("menuItem = CAMOpt(menuTemplate, unescape(values[menuIndex]), script);");
                fncBindDataForMenu.Append("menuItem.setAttribute('isFilterItem', 'true');");
                fncBindDataForMenu.EndFunction();

                fncBindDataForMenu.EndFunction();
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "FW_BindDataForMenu",
                                                            fncBindDataForMenu.ToString(), true);
            }

            // function getSelectedItems: return id collection of selected items
            var fncGetSelectedItems = new StringBuilder();
            fncGetSelectedItems.BeginFunction("function getSelectedItems()");
            fncGetSelectedItems.Append("var result = [];");
            fncGetSelectedItems.AppendFormat("var selected = $(\"input[type='checkbox'][class='s4-itm-cbx'][ctx='{0}']:checked\");", viewCounter);
            fncGetSelectedItems.Append(
                "$.each(selected, function(index,value){var refId = $(value).attr('refId'); var refListId = $(value).attr('refListId');result.push({refListId:refListId,refId:refId});});");
            fncGetSelectedItems.Append("return result;");
            fncGetSelectedItems.EndFunction();
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "getSelectedItems", fncGetSelectedItems.ToString(), true);

            // function refresh selected items value
            var fncRefreshSelectedItemsValue = new StringBuilder();
            fncRefreshSelectedItemsValue.BeginFunction("function refreshSelectedItemsValue()");
            fncRefreshSelectedItemsValue.Append("var items = getSelectedItems();");
            fncRefreshSelectedItemsValue.Append("var arr = [];");
            fncRefreshSelectedItemsValue.Append("$.each(items, function(i, item){arr.push('[' + item.refListId + ':' + item.refId + ']');});");
            fncRefreshSelectedItemsValue.AppendFormat("$('#{0}').val(arr.join(','));", hdfSelectedItems.ClientID);
            fncRefreshSelectedItemsValue.EndFunction();
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "refreshSelectedItemsValue", fncRefreshSelectedItemsValue.ToString(), true);

            // Set last field for ViewFields
            ViewFields.Cast<BaseFieldRef>().Where(f => !f.IsHidden).Last().IsLastField = true;

            // Build group tree
            for (var i = 1; i < GroupFields.Count; i++)
            {
                var groupField = (IGroupFieldRef)GroupFields[i];
                groupField.ParentGroup = (IGroupFieldRef)GroupFields[i - 1];
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ThresholdException)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write("Advanced features of this view was disabled because the number of items in this list exceeds the list view threshold.");
                writer.RenderEndTag(); // span
            }

            hdfSelectedItems.RenderControl(writer);

            foreach (var headerMenuTemplate in headerMenuTemplates)
            {
                headerMenuTemplate.RenderControl(writer);
            }
            
            contextMenuTemplate.RenderControl(writer);

            // Hidden div container menu
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            foreach (var viewField in ViewFields.Cast<BaseFieldRef>())
            {
                if (viewField.Filterable && !viewField.IsFilter)
                {
                    var internalFieldName = viewField.InternalFieldName;
                    var headerMenu =
                        headerMenus.First(m => m.TemplateId.EndsWith("FilterMenuTemplate_" + internalFieldName));
                    headerMenu.DisabledMenuItemIds = "FilterMenuTemplate_ClearFilter_" + internalFieldName;
                }
            }
            
            foreach (var headerMenu in headerMenus)
            {
                headerMenu.RenderControl(writer);
            }

            contextMenus = new Hashtable();

            var hiddenMenuItemIds = new List<string>();

            if (!EnableEditItem)
            {
                hiddenMenuItemIds.Add("EditItem" + this.ID);
            }

            if (!EnableDeleteItem)
            {
                hiddenMenuItemIds.Add("DeleteItem" + this.ID);
            }

            foreach (DataRow item in DataSource.Rows)
            {
                var contextMenu = new Menu("")
                {
                    SuppressBubbleIfPostback = false,
                    UseMaximumWidth = true,
                    TemplateId = "contextMenuTemplate" + this.ID,
                    ID = string.Format("itemContextMenu_{0}_{1}_{2}", item["ListId"], item["ID"], item["RowIndex"]).Replace("-", "")
                };

                if (hiddenMenuItemIds.Count > 0)
                {
                    contextMenu.HiddenMenuItemIds = string.Join(",", hiddenMenuItemIds.ToArray());
                }

                contextMenu.TokenNamesAndValues.Add("SOURCE", SPEncode.UrlEncode(Page.Request.RawUrl));
                contextMenu.TokenNamesAndValues.Add("WEBURL", SPContext.Current.Web.Url);
                contextMenu.TokenNamesAndValues.Add("LISTID", item["ListId"].ToString());
                contextMenu.TokenNamesAndValues.Add("ITEMID", item["ID"].ToString());
                contextMenus.Add(string.Format("{0}_{1}_{2}", item["ListId"], item["ID"], item["RowIndex"]), contextMenu);
                Controls.Add(contextMenu);
                contextMenu.RenderControl(writer);
            }

            writer.RenderEndTag(); // div

            RenderBeginTag(writer);

            // Header
            RenderHeader(writer);

            if (GroupFields.Count > 0 && !ThresholdException)
            {
                var groupField = (IGroupFieldRef)GroupFields[0];
                var groups = groupField.GetGroupBy(DataSource);
                
                var groupId = 0;

                foreach (var @group in groups)
                {
                    groupId++;
                    var predicate = PredicateBuilderExtensions.True<DataRow>();
                    // Begin with group level is 1
                    RenderGroup(writer, groupField, 1, groupId.ToString(), @group, predicate);
                }
            }
            else
            {
                // Rows
                RenderRows(writer, "0", DataSource.AsEnumerable());
            }

            if (DataSource.Rows.Count == 0 && CurrentPage == 1)
            {
                RenderEmptyData(writer);
            }

            RenderEndTag(writer);

            if (DataSource.Rows.Count > 0)
            {
                RenderPaging(writer);
            }
            else
            {
                var pageFirstRow = Page.Request.QueryString["PageFirstRow"];
                if (!string.IsNullOrEmpty(pageFirstRow) && Convert.ToInt32(pageFirstRow) > 1)
                {
                    RenderPaging(writer);
                }
            }

            RenderEndLine(writer);

            if (EnableAddNewItem)
            {
                RenderAddNewLink(writer);
            }

            groupStateManager.RenderControl(writer);
            collapsedGroupManager.RenderControl(writer);

            // Script
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);

            writer.WriteLine("$(document).ready(function(){");

            if (isFilter)
            {
                // Load drop down list filter
                var urlBuilder = new UrlBuilder(Utilities.GetWebUrl(SPContext.Current.Web.Url) + "/_layouts/filter.aspx");
                urlBuilder.AddQueryString("RootFolder", Page.Request.QueryString["RootFolder"]);
                urlBuilder.AddQueryString("ListId", SPContext.Current.ListId.ToString());
                urlBuilder.AddQueryString("ViewId", SPContext.Current.ViewContext.ViewId.ToString());
                urlBuilder.AddQueryString("FilterOnly", "1");
                urlBuilder.AddQueryString("Filter", "1");

                var index = 1;
                foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(f => f.IsFilter))
                {
                    urlBuilder.AddQueryString("FilterField" + index, viewField.InternalFieldName);
                    urlBuilder.AddQueryString("FilterValue" + index, viewField.FilterValue);
                    index++;
                }

                foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(f => f.Filterable && !f.IsHidden))
                {
                    urlBuilder.AddQueryString("FieldInternalName", viewField.InternalFieldName);
                    writer.Write(string.Format("VP.Sharepoint.CQ.Core.DataView.loadAllFieldFilters('{0}','{1}');", DataViewUtils.Escape(urlBuilder.ToString()), viewField.InternalFieldName));
                }
            }

            writer.Write("});");
            writer.RenderEndTag(); // script

            // Temporary element
            foreach (var viewField in ViewFields.Cast<BaseFieldRef>())
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "hiddenFilterHostFor_" + viewField.InternalFieldName);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
        }

        private void RenderHeader(HtmlTextWriter writer)
        {
            if (isFilter)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("&nbsp;");
                writer.RenderEndTag(); // td

                foreach(var viewfield in ViewFields.Cast<BaseFieldRef>())
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "divHeaderOf_" + viewfield.InternalFieldName);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderEndTag(); // div
                    writer.RenderEndTag(); // td
                }

                writer.RenderEndTag(); // tr
                writer.RenderEndTag(); // tbody
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-viewheadertr ms-vhltr");
            writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            // Th
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vh-icon");
            writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "chkToggleAllItems");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute("onclick", string.Format("FW_ToggleAllItems(this, '{0}')", viewCounter));
            writer.AddAttribute(HtmlTextWriterAttribute.Title, LocalizationHelper.GetString("wss", "select_deselect_all"));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input

            writer.RenderEndTag(); // th

            foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(f => !f.IsHidden))
            {
                RenderHeaderField(writer, viewField);
            }

            writer.RenderEndTag(); // tr

            writer.RenderEndTag(); // tbody
        }

        private void RenderHeaderField(HtmlTextWriter writer, BaseFieldRef fieldRef)
        {
            var showMenuScript = new StringBuilder();
            if (fieldRef.Filterable || fieldRef.Sortable)
            {
                var headerMenuTemplate = headerMenuTemplates.First(t => t.ID == "FilterMenuTemplate_" + fieldRef.InternalFieldName);
                var indexOf = headerMenuTemplates.IndexOf(headerMenuTemplate);
                var headerMenu = headerMenus[indexOf];

                if (fieldRef.Filterable)
                {
                    var urlBuilder = new UrlBuilder(Utilities.GetWebUrl(SPContext.Current.Web.Url) + "/_layouts/filter.aspx");
                    urlBuilder.AddQueryString("RootFolder", Page.Request.QueryString["RootFolder"]);
                    urlBuilder.AddQueryString("ListId", SPContext.Current.ListId.ToString());
                    urlBuilder.AddQueryString("ViewId", SPContext.Current.ViewContext.ViewId.ToString());
                    urlBuilder.AddQueryString("FieldInternalName", fieldRef.InternalFieldName);
                    urlBuilder.AddQueryString("FilterOnly", "1");
                    urlBuilder.AddQueryString("Filter", "1");

                    var index = 1;
                    foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(f => f.IsFilter))
                    {
                        urlBuilder.AddQueryString("FilterField" + index, viewField.InternalFieldName);
                        urlBuilder.AddQueryString("FilterValue" + index, viewField.FilterValue);
                        index++;
                    }

                    showMenuScript.AppendFormat("VP.Sharepoint.CQ.Core.DataView.loadFieldFilterValues('{0}','{1}','{2}','{3}','{4}','{5}');", headerMenuTemplate.ClientID, headerMenu.ClientID, SPContext.Current.ViewContext.ViewId, fieldRef.InternalFieldName, SPContext.Current.Web.Url, DataViewUtils.Escape(urlBuilder.ToString()));
                }
                else
                {
                    showMenuScript.AppendFormat("CoreInvoke('MMU_Open',byid('{0}'), MMU_GetMenuFromClientId('{1}'),window.event,true, 'th{2}', 0); return false;",
                        headerMenuTemplate.ClientID, headerMenu.ClientID, fieldRef.InternalFieldName);    
                }
            }

            switch (fieldRef.FieldType)
            {
                case SPFieldType.Attachments:
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, "12");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vh-icon");
                    writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap");
                    writer.AddAttribute("onmouseover", "FW_OnChildColumn(this)");
                    writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("th{0}", fieldRef.FieldName));
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vh-div");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "Attachments");
                    writer.AddAttribute("ctxnum", viewCounter.ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "diidSortAttachments");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "return false;");
                    writer.AddAttribute("onfocus", "OnFocusFilter(this)");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/attachhd.gif");
                    writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();

                    if (fieldRef.InternalFieldName == SortField)
                    {
                        writer.Write(SortDir == "ASC"
                                            ? "<img alt=\"Ascending\" src=\"/_layouts/images/sort.gif\" border=\"0\" />"
                                            : "<img alt=\"Descending\" src=\"/_layouts/images/rsort.gif\" border=\"0\" />");
                    }

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-hidden");
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, "1");
                    writer.AddAttribute(HtmlTextWriterAttribute.Height, "1");
                    writer.AddAttribute(HtmlTextWriterAttribute.Border, "1");
                    writer.AddAttribute(HtmlTextWriterAttribute.Alt, LocalizationHelper.GetString("wss", "OpenMenuKeyAccessible"));
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/blank.gif");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();

                    writer.RenderEndTag(); // a

                    writer.Write("<img border=\"0\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");
                    writer.Write(fieldRef.IsFilter
                                        ? "<img alt=\"\" src=\"/_layouts/images/filter.gif\" border=\"0\" />"
                                        : "<img border=\"0\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");   
                    
                    writer.RenderEndTag(); // div

                    if (fieldRef.Filterable || fieldRef.Sortable)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-ctx");
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, showMenuScript.ToString());
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);

                        writer.Write("<span>&nbsp;</span>");

                        writer.AddAttribute(HtmlTextWriterAttribute.Title, LocalizationHelper.GetString("wss", "open_menu"));
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, showMenuScript.ToString());
                        writer.AddAttribute("onfocus", "FW_OnChildColumn(this.parentNode.parentNode); return false;");
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.RenderEndTag();

                        writer.Write("<span>&nbsp;</span>");

                        writer.RenderEndTag(); // div   
                    }

                    writer.RenderEndTag(); //th
                    break;
                default:
                    if (!fieldRef.Width.IsEmpty)
                    {
                        writer.AddStyleAttribute(HtmlTextWriterStyle.Width, fieldRef.Width.ToString());
                    }

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vh2");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                    writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
                    writer.AddAttribute("onmouseover", "FW_OnChildColumn(this);");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("th{0}", fieldRef.InternalFieldName));
                    // Fix style for ASP.NET WebPart
                    writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "normal !important");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vh-div");
                    writer.AddAttribute("filterable", fieldRef.Filterable.ToString().ToUpperInvariant());
                    writer.AddAttribute("filterdisable", (!fieldRef.Filterable).ToString().ToUpper());
                    writer.AddAttribute("fieldtype", fieldRef.FieldType.ToString());
                    writer.AddAttribute("sortable", fieldRef.Sortable.ToString().ToUpperInvariant());
                    writer.AddAttribute("ctxnum", viewCounter.ToString());
                    if (fieldRef.Filterable || fieldRef.Sortable)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, showMenuScript.ToString());
                    }

                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    if (fieldRef.TextAlignRight)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-numHeader");
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    }

                    writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("diidSort{0}", fieldRef.FieldName));
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(fieldRef.HeaderText);
                    if (fieldRef.InternalFieldName == SortField)
                    {
                        writer.Write(SortDir == "ASC"
                                         ? "<img alt=\"Ascending\" src=\"/_layouts/images/sort.gif\" border=\"0\" />"
                                         : "<img alt=\"Descending\" src=\"/_layouts/images/rsort.gif\" border=\"0\" />");
                    }
                    writer.Write(
                        "<img width=\"1\" height=\"1\" border=\"0\" alt=\"Use SHIFT+ENTER to open the menu (new window).\" class=\"ms-hidden\" src=\"/_layouts/images/blank.gif\" />");
                    writer.RenderEndTag(); //a

                    writer.Write("<img border=\"0\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");
                    writer.Write(fieldRef.IsFilter
                                     ? "<img alt=\"\" src=\"/_layouts/images/filter.gif\" border=\"0\" />"
                                     : "<img border=\"0\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");

                    if (fieldRef.TextAlignRight)
                    {
                        writer.RenderEndTag(); // div align right
                    }

                    writer.RenderEndTag(); //div

                    if (fieldRef.Filterable || fieldRef.Sortable)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-ctx");
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, showMenuScript.ToString());
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.Write("<span>&nbsp;</span>");

                        writer.AddAttribute(HtmlTextWriterAttribute.Title, LocalizationHelper.GetString("wss", "open_menu"));
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, showMenuScript.ToString());
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.RenderEndTag(); //a

                        writer.Write("<span>&nbsp;</span>");
                        writer.RenderEndTag(); //div
                    }

                    writer.RenderEndTag(); //th
                    break;
            }
        }

        private void RenderGroup(HtmlTextWriter writer, IGroupFieldRef groupField, int groupLevel, string groupId,
                                 IGrouping<object, DataRow> @group, Expression<Func<DataRow, bool>> filter)
        {
            Func<DataRow, bool> whereCondition = null;

            writer.AddAttribute("groupId", groupId);
            writer.AddAttribute("isCollapsed", "false");

            var uniqueGroupId = groupField.InternalFieldName + groupId.Replace("-", "_");
            var state = groupStateManager.GetState(uniqueGroupId);
            if (string.IsNullOrEmpty(state))
            {
                state = groupField.CollapsedGroup ? "true" : "false";
                groupStateManager.AddState(uniqueGroupId, state);
            }

            collapsedGroupManager.SetState(groupLevel, groupId, state == "true");

            writer.AddAttribute("defaultCollapsed", state);

            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, groupLevel == 1 ? "ms-gb" : "ms-gb2");
            writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "100");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            for (var i = 1; i < groupLevel; i++)
            {
                writer.Write("<img width=\"10\" height=\"1\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("FW_ShowHideGroup('{0}', '{1}')", groupId, uniqueGroupId));
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/images/minus.gif");
            writer.AddAttribute("groupId", groupId);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            writer.Write("&nbsp;");

            writer.Write(groupField.HeaderText);

            writer.RenderEndTag(); // a

            if (!string.IsNullOrEmpty(groupField.HeaderText))
            {
                writer.Write("&nbsp;:&nbsp;");    
            }

            groupField.RenderCell(writer, group);

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // tbody

            if (groupLevel < GroupFields.Count)
            {
                var nextGroupField = (IGroupFieldRef) GroupFields[groupLevel];

                var groups = nextGroupField.GetGroupBy(group);
                var nextGroupId = 0;
                foreach (var nextGroup in groups)
                {
                    nextGroupId++;
                    RenderGroup(writer, nextGroupField, groupLevel + 1, string.Format("{0}-{1}", groupId, nextGroupId),
                                nextGroup, filter);
                }
            }
            else
            {
                RenderRows(writer, groupId + "-all", group);
            }

            // Todo: old group sum position (on group bottom)
        }

        private void RenderRows(HtmlTextWriter writer, string groupId, IEnumerable<DataRow> items)
        {
            writer.AddAttribute("groupId", groupId);
            writer.AddAttribute("isCollapsed", "false");
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

            var altRow = false;
            var rowIndex = 1;
            foreach (var item in items)
            {
                RenderRow(writer, item, altRow, string.Format("{0}-{1}", groupId, rowIndex));
                rowIndex++;
                altRow = !altRow;
            }
            writer.RenderEndTag(); // tbody
        }

        private void RenderRow(HtmlTextWriter writer, DataRow item, bool alt, string rowId)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, alt ? "ms-itmhover ms-alternating" : "ms-itmhover");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "FW_ToggleCheckBox(this)");
            writer.AddAttribute("ctx", viewCounter.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb-itmcbx ms-vb-firstCell");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-itm-cbx");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute("refListId", item["ListId"].ToString());
            writer.AddAttribute("refId", item["ID"].ToString());
            writer.AddAttribute("ctx", viewCounter.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input

            writer.RenderEndTag(); // td

            foreach (var field in ViewFields.Cast<BaseFieldRef>().Where(f => !f.IsHidden))
            {
                RenderCell(writer, field, item, rowId);
            }

            writer.RenderEndTag(); // tr
        }

            private void RenderCell(HtmlTextWriter writer, BaseFieldRef fieldRef, DataRow item, string rowId)
            {
                var isMenuCell = fieldRef.FieldName == MenuField;
                if (isMenuCell)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class,
                                        fieldRef.IsLastField ? "ms-vb-title ms-vb-lastCell" : "ms-vb-title");

                    writer.AddAttribute("onmouseover", "FW_OnChildColumn(this)");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("td_{0}_{1}", fieldRef.InternalFieldName, rowId));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, fieldRef.IsLastField ? "ms-vb2 ms-vb-lastCell" : "ms-vb2");
                }

                if (fieldRef.TextAlignRight)
                {
                    writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
                }

                // Fix white-space
                if (!isMenuCell)
                {
                    switch (fieldRef.FieldType)
                    {
                        case SPFieldType.Number:
                        case SPFieldType.Currency:
                            writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                            break;
                        default:
                            writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-wrap");    
                            break;
                    }
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                if (isMenuCell)
                {
                    var contextMenu = (Menu)
                        contextMenus[string.Format("{0}_{1}_{2}", item["ListId"], item["ID"], item["RowIndex"])];
                    var showMenuScript = string.Format(
                        "CoreInvoke('MMU_Open',byid('{0}'), MMU_GetMenuFromClientId('{1}'),window.event,true, 'td_{2}_{3}', 0);",
                        contextMenuTemplate.ClientID, contextMenu.ClientID, fieldRef.InternalFieldName, rowId);

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
                    writer.AddAttribute("onmouseover", "FW_OnItem(this)");
                    writer.AddAttribute("ctxname", viewCounter.ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    bool showItemIndialog;
                    var href = GetDisplayItemLink(item, out showItemIndialog);
                
                    var contextMenuEventArgs = new ContextMenuEventArgs {Row = item, Href = href};
                    OnContextMenuRender(contextMenuEventArgs);

                    // Fix white-space
                    switch (fieldRef.FieldType)
                    {
                        case SPFieldType.Number:
                        case SPFieldType.Currency:
                            writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                            break;
                        default:
                            writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-wrap");
                            break;
                    }
                    
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, contextMenuEventArgs.Href);

                    if (showItemIndialog)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "return FW_OpenDisplayDialog(this, window.event);");    
                    }
                
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    fieldRef.RenderCell(writer, item);

                    writer.RenderEndTag(); // a

                    // New Icon
                    var created = GetCreateDateTime(item);
                    if (created.Date.Equals(DateTime.Today))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Title, "New");
                        writer.AddAttribute(HtmlTextWriterAttribute.Alt, "New");
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-newgif");
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, "/_layouts/1033/images/new.gif");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);
                        writer.RenderEndTag(); // img
                    }

                    writer.RenderEndTag(); // div

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "s4-ctx");
                    writer.AddAttribute("altclick", showMenuScript);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write("<span>&nbsp;</span>");

                    writer.AddAttribute(HtmlTextWriterAttribute.Title, LocalizationHelper.GetString("wss", "open_menu"));
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.RenderEndTag();

                    writer.Write("<span>&nbsp;</span>");
                    writer.RenderEndTag(); // div
                }
                else
                {
                    fieldRef.RenderCell(writer, item);
                }

                writer.RenderEndTag(); // td
            }

        protected virtual DateTime GetCreateDateTime(DataRow item)
        {
            return (DateTime) item["Created"];
        }

        protected abstract string GetDisplayItemLink(DataRow item, out bool showItemInDialog);
        
        private void RenderPaging(HtmlTextWriter writer)
        {
            var urlBuilder = new UrlBuilder(Page.Request.Url);
            var pageFirstRow = urlBuilder.GetQueryStringValue<int>("PageFirstRow");
            if (pageFirstRow <= 0)
            {
                pageFirstRow = 1;
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "tblCustomPaging");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-bottompaging");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-bottompagingline1");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"1\" alt=\"\" src=\"/_layouts/images/blank.gif\">");
            writer.RenderEndTag(); // td
            writer.RenderEndTag(); // tr

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-bottompagingline2");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"1\" alt=\"\" src=\"/_layouts/images/blank.gif\">");
            writer.RenderEndTag(); // td
            writer.RenderEndTag(); // tr

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "bottomPagingCellWPQ1");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            if (pageFirstRow > 1)
            {
                urlBuilder.AppendQueryString(PrevPagePosition);
                urlBuilder.AddQueryString("PageFirstRow", (pageFirstRow - RowLimit).ToString());

                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:");
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("RefreshPageTo(window.event, unescape('{0}')); return false;", DataViewUtils.Escape(urlBuilder.ToString())));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("<img alt=\"Previous\" src=\"/_layouts/1033/images/prev.gif\" border=\"0\" />");
                writer.RenderEndTag();

                writer.RenderEndTag(); // td    
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-paging");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            if (DataSource.Rows.Count > 0)
            {
                var endItemIndex = pageFirstRow + DataSource.Rows.Count - 1;
                writer.Write(string.Format("{0} - {1}", pageFirstRow, endItemIndex));
            }

            writer.RenderEndTag(); // td

            if (!string.IsNullOrEmpty(NextPagePosition))
            {
                urlBuilder.AppendQueryString(NextPagePosition);
                urlBuilder.AddQueryString("PageFirstRow", (pageFirstRow + RowLimit).ToString());
                urlBuilder.RemoveQueryString("PagedPrev");

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:");
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("RefreshPageTo(window.event, unescape('{0}')); return false;", DataViewUtils.Escape(urlBuilder.ToString())));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("<img alt=\"Next\" src=\"/_layouts/1033/images/next.gif\" border=\"0\" />");
                writer.RenderEndTag();
                writer.RenderEndTag(); // td
            }

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // tbody
            writer.RenderEndTag(); // table

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-bottompagingline3");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"1\" alt=\"\" src=\"/_layouts/images/blank.gif\">");
            writer.RenderEndTag(); // td
            writer.RenderEndTag(); // tr

            writer.RenderEndTag(); // tbody

            writer.RenderEndTag(); // table
        }

        protected virtual void RenderEmptyData(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "100");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-vb");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("There are no items to show in this view.");
            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // tbody
        }

        private static void RenderEndLine(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "Hero-WPQ1");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-partline");
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"1\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");
            writer.RenderEndTag(); // td
            writer.RenderEndTag(); // tr

            // Add new item

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<img width=\"1\" height=\"5\" alt=\"\" src=\"/_layouts/images/blank.gif\" />");
            writer.RenderEndTag(); // td
            writer.RenderEndTag(); // tr

            writer.RenderEndTag(); // tbody
            writer.RenderEndTag(); // table
        }

        protected virtual void RenderAddNewLink(HtmlTextWriter writer)
        {
        }

        protected virtual void RenderTotalItems(HtmlTextWriter writer, int totalItems)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "100");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "15px");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.RenderBeginTag(HtmlTextWriterTag.B);
            writer.Write("{0} {1}", TotalItemsText, totalItems);
            writer.RenderEndTag(); // b

            writer.RenderEndTag(); // td

            writer.RenderEndTag(); // tr
            writer.RenderEndTag(); // tbody
        }

        /// <summary>
        /// Get total items in view
        /// </summary>
        /// <returns></returns>
        protected abstract int GetTotalItems();

        /// <summary>
        /// Get total items has data of field
        /// </summary>
        /// <param name="fieldRef"></param>
        /// <param name="countCondition"></param>
        /// <returns></returns>
        protected abstract int GetCountFieldData(BaseFieldRef fieldRef, Func<DataRow, bool> countCondition);
        
        /// <summary>
        /// Get sum field data
        /// </summary>
        /// <param name="fieldRef"></param>
        /// <returns></returns>
        protected abstract double GetSumFieldData(BaseFieldRef fieldRef);

        /// <summary>
        /// Get sum field data
        /// </summary>
        /// <param name="fieldRef"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        protected abstract double GetSumFieldData(BaseFieldRef fieldRef, Func<DataRow, bool> whereCondition);

        /// <summary>
        /// Count items in group
        /// </summary>
        /// <param name="fieldRef"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected abstract int CountGroupItems(IGroupFieldRef fieldRef, Func<DataRow, bool> filter);

        protected abstract IDictionary<string, string> GetFilterValues(BaseFieldRef fieldRef);

        private static T GetObjectData<T>(DataRow item, string fieldName)
        {
            var value = item[fieldName];
            if (value == null || value is DBNull)
            {
                return default(T);
            }

            if (value is string)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return default(T);
                }
            }

            return (T) value;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            var args = eventArgument.Split(';');
            switch (args[0])
            {
                case "_ITEMDELETE":
                    try
                    {
                        var itemId = Convert.ToInt32(args[1]);
                        var listId = new Guid(args[2]);
                        var list = SPContext.Current.Web.Lists[listId];
                        var itemToDelete = list.GetItemById(itemId);
                        if (SPContext.Current.Site.WebApplication.RecycleBinEnabled)
                        {
                            itemToDelete.Recycle();    
                        }
                        else
                        {
                            itemToDelete.Delete();
                        }
                    }
                    catch(Exception ex)
                    {
                        SPUtility.TransferToErrorPage(ex.Message);
                    }
                    break;
            }
        }

        protected virtual void OnFieldFilter()
        {
        }

        protected virtual void OnFieldSorting()
        {
        }

        /// <summary>
        /// Return selected items.
        /// </summary>
        /// <returns>Return selected items with format [ListId:ItemId],[ListId:ItemId]...</returns>
        public string GetSelectedItems()
        {
            EnsureChildControls();
            return hdfSelectedItems.Value;
        }

        public class CollapsedGroupManager : Control
        {
            private readonly Hashtable states;

            public CollapsedGroupManager()
            {
                states = new Hashtable();
            }

            public void SetState(int groupLevel, string groupId, bool collapsed)
            {
                if (!states.ContainsKey(groupLevel))
                {
                    states.Add(groupLevel, new List<KeyValuePair<string, bool>>());
                }

                var level = (List<KeyValuePair<string, bool>>)states[groupLevel];
                level.Add(new KeyValuePair<string, bool>(groupId, collapsed));
            }

            protected override void Render(HtmlTextWriter writer)
            {
                var script = new StringBuilder();
                foreach (var pair in from groupLevel in states.Keys.Cast<int>().OrderByDescending(item => item)
                                     select (List<KeyValuePair<string, bool>>)states[groupLevel]
                                         into level
                                         from pair in level
                                         where pair.Value
                                         select pair)
                {
                    script.AppendFormat("FW_ShowHideGroup('{0}', {1});", pair.Key, pair.Value ? "true" : "false");
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                writer.RenderBeginTag(HtmlTextWriterTag.Script);
                writer.Write("$(document).ready(function(){");
                writer.Write(script);
                writer.Write("});");
                writer.RenderEndTag(); // script

                if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "CollapsedGroupManager", script.ToString(), true);
                }
            }
        }
    }

    public delegate void ContextMenuEventHandler(ContextMenuEventArgs args);

    public class ContextMenuEventArgs
    {
        public DataRow Row { get; set; }

        public string Href { get; set; }
    }
}
