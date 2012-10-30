Type.registerNamespace("VP.Sharepoint.CQ.Core");

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}

VP.Sharepoint.CQ.Core.DataView = function (id) {
    this.initialize(id);
};

VP.Sharepoint.CQ.Core.DataView.prototype = {
    initialize: function (id) {
        // Fix style
        $('.ms-WPBody').removeClass('ms-WPBody');

        var self = this;

        this.id = id;
        this.viewCounter = $("#" + id + "_ViewCounter").val();
        this.groupStateManagerId = $("#" + id + "_GroupStateManagerId").val();

        // Hook click event for Toggle All Items checkbox
        var hiddenCheckbox = $("tr.ms-viewheadertr th input[type='checkbox']:first").hide();
        var checkbox = $("<input id='chkToggleAllItems' type='checkbox' class='s4-selectAllCbx'></input>");
        checkbox.attr('title', hiddenCheckbox.attr('title'));
        checkbox.click(function () {
            self.toggleAllItems(this);
        });
        checkbox.insertAfter(hiddenCheckbox);
    },
    toggleAllItems: function (obj) {
        var checked = $(obj).attr('checked');

        if (checked == true) {
            $("input[class='s4-itm-cbx']").attr('checked', 'checked');
            $("input[class='s4-itm-cbx']").parent().parent().addClass('s4-itm-selected');
        }
        else {
            $("input[class='s4-itm-cbx']").removeAttr('checked');
            $("input[class='s4-itm-cbx']").parent().parent().removeClass('s4-itm-selected');
        }

        RibbonCustomization.PageComponent.refreshRibbonStatus();
        this.refreshSelectedItemsValue();
    },
    showHideGroup: function (groupId, uniqueGroupId) {
        var self = $("tbody[groupId=" + groupId + "]");
        var ex = $("tbody[groupId^=" + groupId + "-" + "]");
        var isCollapsed = self.attr('isCollapsed');
        if (isCollapsed == 'false') {
            self.attr('isCollapsed', 'true');
            $('#' + this.groupStateManagerId + uniqueGroupId).val('true');
            $("img[groupId=" + groupId + "]").attr('src', '/_layouts/images/plus.gif');

            $.each(ex, function (index, value) {
                $(this).hide();
                var hideByGroupId = $(this).attr('hideByGroupId');
                if (hideByGroupId == undefined || hideByGroupId == '') {
                    $(this).attr('hideByGroupId', groupId);
                }
            });
        }
        else {
            self.attr('isCollapsed', 'false');
            $('#' + this.groupStateManagerId + uniqueGroupId).val('false');
            $("img[groupId=" + groupId + "]").attr('src', '/_layouts/images/minus.gif');

            $.each(ex, function (index, value) {
                var _this = $(this);
                if (_this.attr('hideByGroupId') + '' == groupId) {
                    _this.show();
                    _this.attr('hideByGroupId', '');
                }
            });
        }
    },
    refreshSelectedItemsValue: function () {
        var items = this.getSelectedItems();
        var arr = [];
        $.each(items, function (i, item) {
            arr.push('[' + item.refListId + ':' + item.refId + ']');
        });
        $('#' + this.id + '_SelectedItems').val(arr.join(','));
    },
    getSelectedItems: function () {
        var result = [];
        var selected = $(String.format("input[type='checkbox'][class='s4-itm-cbx'][ctx='{0}']:checked", this.viewCounter));
        $.each(selected, function (index, value) {
            var refId = $(value).attr('refId');
            var refListId = $(value).attr('refListId');
            result.push({ refListId: refListId, refId: refId });
        });
        return result;
    },
    toggleCheckBox: function (elm) {
        try { if (event.srcElement.tagName.toLowerCase() == 'a') return; } catch (er) { }

        $(elm).toggleClass('s4-itm-selected');

        if ($(elm).hasClass('s4-itm-selected')) {
            $("input[type='checkbox']", $(elm)).attr('checked', 'checked');
        }

        else {
            $("input[type='checkbox']", $(elm)).removeAttr('checked');
            $('#chkToggleAllItems').removeAttr('checked');
        }

        RibbonCustomization.PageComponent.refreshRibbonStatus();
        this.refreshSelectedItemsValue();
    }
};
VP.Sharepoint.CQ.Core.DataView.ShowHideGroup = function (groupId, uniqueGroupId) {
    var self = $("tbody[groupId=" + groupId + "]");
    var ex = $("tbody[groupId^=" + groupId + "-" + "]");
    var isCollapsed = self.attr('isCollapsed');
    if (isCollapsed == 'false') {
        self.attr('isCollapsed', 'true');
        $('#' + this.groupStateManagerId + uniqueGroupId).val('true');
        $("img[groupId=" + groupId + "]").attr('src', '/_layouts/images/plus.gif');

        $.each(ex, function (index, value) {
            $(this).hide();
            var hideByGroupId = $(this).attr('hideByGroupId');
            if (hideByGroupId == undefined || hideByGroupId == '') { $(this).attr('hideByGroupId', groupId); }
        });
    }
    else {
        self.attr('isCollapsed', 'false');
        $('#' + this.groupStateManagerId + uniqueGroupId).val('false');
        $("img[groupId=" + groupId + "]").attr('src', '/_layouts/images/minus.gif');

        $.each(ex, function (index, value) {
            var _this = $(this);
            if (_this.attr('hideByGroupId') + '' == groupId) {
                _this.show();
                _this.attr('hideByGroupId', '');
            }
        });
    }
};

VP.Sharepoint.CQ.Core.DataView.OnChildColumn = function (elm) {
    var i;
    for (i = 0; i < elm.childNodes.length; i++) {
        var child = elm.childNodes[i];
        if (child.nodeType == 1 && child.tagName == "DIV" && child.getAttribute("CtxNum") != null) {
            DeferCall('VP.Sharepoint.CQ.Core.DataView.OnMouseOverFilter', child); break;
        }
    }
};

VP.Sharepoint.CQ.Core.DataView.OnMouseOverFilter = function (elm) {
    if (!IsFilterMenuEnabled()) return false;
    if (IsFilterMenuOn() || bMenuLoadInProgress) return false;
    if (window.location.href.search("[?&]Filter=1") != -1) return false;
    if (elm.FilterDisable == "TRUE") return false;
    if (IsFieldNotFilterable(elm) && IsFieldNotSortable(elm)) return false;
    if (filterTable == elm) return;
    if (filterTable != null) VP.Sharepoint.CQ.Core.DataView.OnMouseOutFilter();
    filterTable = elm;
    var isTable = filterTable.tagName == "TABLE";
    if (isTable) {
        filterTable.className = "ms-selectedtitle";
        filterTable.onmouseout = VP.Sharepoint.CQ.Core.DataView.OnMouseOutFilter;
    }
    else {
        var par = filterTable.parentNode;
        par.onmouseout = VP.Sharepoint.CQ.Core.DataView.OnMouseOutFilter;
        CreateCtxImg(par, VP.Sharepoint.CQ.Core.DataView.OnMouseOutFilter);
    }
};

VP.Sharepoint.CQ.Core.DataView.OnMouseOutFilter = function (evt) {
    OnMouseOutFilter(evt);
};

VP.Sharepoint.CQ.Core.DataView.OnItem = function (elm, event) {
    OnItem(elm);
    var div = $("div.s4-ctx", $(elm).parent());
    var altclick = div.attr('altclick');
    div.unbind().click(function () {
        eval(altclick); 
    });
};

VP.Sharepoint.CQ.Core.DataView.OpenDialog = function (url, e) {
    url = url + '';
    var options = SP.UI.$create_DialogOptions();
    options.url = url;
    options.dialogReturnValueCallback = Function.createDelegate(null, function (result, target) {
        if (result == SP.UI.DialogResult.OK) {
            SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);
        }
    });
    SP.UI.ModalDialog.showModalDialog(options);

    // Cancel default event
    if (e != undefined) {
        var eventObject = jQuery.Event(e);
        eventObject.preventDefault();
        eventObject.isDefaultPrevented();
        eventObject.stopPropagation();
        eventObject.isPropagationStopped();
        eventObject.stopImmediatePropagation();
        eventObject.isImmediatePropagationStopped();
    }

    return false;
};

VP.Sharepoint.CQ.Core.DataView.loadFieldFilterValues = function (menuTemplateId, menuId, viewId, internalFieldName, webUrl, filterUrl) {
    var menuTemplate = document.getElementById(menuTemplateId);
    var addLoading = true;
    for (var menuIndex = 0; menuIndex < menuTemplate.childNodes.length; menuIndex++) {
        var menuChild = menuTemplate.childNodes[menuIndex];
        if (menuChild.nodeName != '#text') {
            if (menuChild.getAttribute('isFilterItem') == 'true') {
                addLoading = false;
                break;
            }
        }
    }

    if (addLoading) {
        var menuItem = CAMOpt(menuTemplate, "Loading....", "");
        menuItem.setAttribute('isFilterItem', 'true');
        menuItem.setAttribute('disabled', 'disabled');
        filterUrl = unescape(filterUrl);
        $("#hiddenFilterHostFor_" + internalFieldName).load(filterUrl + " select", function (responseText) {
            VP.Sharepoint.CQ.Core.DataView.bindValuesForFilter(menuTemplateId, menuId, viewId, internalFieldName, responseText);
        });

        CoreInvoke('MMU_Open', byid(menuTemplateId), MMU_GetMenuFromClientId(menuId), window.event, true, 'th' + internalFieldName, 0);
    } else {
        var select = $("#hiddenFilterHostFor_" + internalFieldName).find("select");
        if (select.length == 0) {
            alert($("#hiddenFilterHostFor_" + internalFieldName + " span").html());
        }
        else {
            CoreInvoke('MMU_Open', byid(menuTemplateId), MMU_GetMenuFromClientId(menuId), window.event, true, 'th' + internalFieldName, 0);
        }
    }
};

VP.Sharepoint.CQ.Core.DataView.bindValuesForFilter = function (menuTemplateId, menuId, viewId, internalFieldName, responseText) {
    var select = $("#hiddenFilterHostFor_" + internalFieldName).find("select");
    if (select.length == 0) {
        var pattern = /<span class="ms-descriptiontext">(.*?)<\/span>/;
        matches = responseText.match(pattern);
        if (matches) {
            $("#hiddenFilterHostFor_" + internalFieldName).empty().append(matches[0]);
            alert($("#hiddenFilterHostFor_" + internalFieldName + " span").html());
        }
        return;
    }

    var menuTemplate = document.getElementById(menuTemplateId);

    // Clear loading message
    for (var menuIndex = 0; menuIndex < menuTemplate.childNodes.length; menuIndex++) {
        var menuChild = menuTemplate.childNodes[menuIndex];
        if (menuChild.nodeName != '#text') {
            if (menuChild.getAttribute('isFilterItem') == 'true') {
                menuTemplate.removeChild(menuChild); --menuIndex;
            }
        }
    }

    // Remove in UI
    $(String.format("ul.ms-MenuUIUL li[text='{0}']", "Loading....")).remove();

    var options = $("option", select);
    if (options.length > 500) {
        var strUrl = window.location.href;
        strUrl = StURLSetVar2(strUrl, "Filter", "1");
        strUrl = StURLSetVar2(strUrl, "View", viewId);
        strUrl = "javascript:SubmitFormPost('" + strUrl + "')";
        VP.Sharepoint.CQ.Core.DataView.addMenuItem(menuTemplate, L_FilterMode_Text, strUrl);
    }
    else {
        $(options).each(function (i) {
            if (i > 0) {
                var option = $(this);
                var script = String.format("FilterField('{0}', '{1}', unescape('{2}'), {3})", viewId, internalFieldName, escape(option.val()), i);
                VP.Sharepoint.CQ.Core.DataView.addMenuItem(menuTemplate, option.text(), script);
            }
        });
    }

    $("#hiddenFilterHostFor_" + internalFieldName).empty().append("<select></select>");
    CoreInvoke('MMU_Open', byid(menuTemplateId), MMU_GetMenuFromClientId(menuId), window.event, true, 'th' + internalFieldName, 0);
};

VP.Sharepoint.CQ.Core.DataView.addMenuItem = function (menuTemplate, text, script) {
    script = script.replace(/'/g, '\"');
    var menuItem = CAMOpt(menuTemplate, text, script);
    menuItem.setAttribute('isFilterItem', 'true');
};

VP.Sharepoint.CQ.Core.DataView.loadAllFieldFilters = function (filterUrl, internalFieldName) {
    filterUrl = unescape(filterUrl);
    $("#divHeaderOf_" + internalFieldName).load(filterUrl + " select");
};
