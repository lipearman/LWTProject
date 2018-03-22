function CreateClass(parentClass, properties) {
    var result = function() { 
        if(result.preparing)
            return delete(result.preparing);
        if(result.ctor)
            result.ctor.apply(this);
    };

    result.prototype = { };
    if(parentClass) {
        parentClass.preparing = true;
        result.prototype = new parentClass;
        result.base = parentClass;
    }
    if(properties) {
        var ctorName = "constructor";
        for(var name in properties)
            if(name != ctorName)
                result.prototype[name] = properties[name];
        var ctor = properties[ctorName];
        if(ctor)
            result.ctor = ctor;
    }
    return result;
}


PageModuleBase = CreateClass(null, {

    PendingCallbacks: { },

    DoCallback: function(sender, callback) {
        if(sender.InCallback()) {
            MailDemo.PendingCallbacks[sender.name] = callback;
            sender.EndCallback.RemoveHandler(MailDemo.DoEndCallback);
            sender.EndCallback.AddHandler(MailDemo.DoEndCallback);
        } else {
            callback();
        }
    },

    DoEndCallback: function(s, e) {
        var pendingCallback = MailDemo.PendingCallbacks[s.name];
        if(pendingCallback) {
            pendingCallback();
            delete MailDemo.PendingCallbacks[s.name];
        }
    },

    ChangeDemoState: function(view, command, key) {
        var prev = this.DemoState;
        var current = { View: view, Command: command, Key: key };

        if(prev && current && prev.View == current.View && prev.Command == current.Command && prev.Key == current.Key)
            return;

        this.DemoState = current;
        this.OnStateChanged();
        this.ShowMenuItems();
    },

    OnStateChanged: function() { },
    ShowMenuItems: function() { },

    Adjust: function() {
        var bodySelector = LeftPanel.IsExpandable() ? "Portrait" : "Landscape";
        $('body').removeClass("Portrait").removeClass("Landscape").addClass(bodySelector);
        MailDemo.ChangeExpandButtonsVisibility(LeftPanel.IsExpandable(), LeftPanel.IsExpanded());
    },

    ChangeLeftPaneExpandedState: function(expand) {
        if(expand)
            LeftPanel.Expand();
        else
            LeftPanel.Collapse();
    },
    ChangeExpandButtonsVisibility: function(expandable, expand) {
        ClientExpandPaneImage.SetVisible(expandable && !expand);
        ClientCollapsePaneImage.SetVisible(expandable && expand);
    },

    // Site master

    ClientLayout_Init: function(s, e) {
        ASPxClientUtils.AttachEventToElement(window, "resize", MailDemo.Adjust);
        if(ASPxClientUtils.touchUI) {
            ASPxClientUtils.AttachEventToElement(window, "orientationchange", function () {
                ASPxClientTouchUI.ensureOrientationChanged(MailDemo.Adjust);
            }, false);
        }
        MailDemo.Adjust();
        MailDemo.ShowMenuItems();
        MailDemo.ClientLayout_PaneResized();
    },
    ClientActionMenu_ItemClick: function(s, e) { },
    ClientLayout_PaneResized: function() { },

    ClientCollapsePaneImage_Click: function(s, e) {
        MailDemo.ChangeLeftPaneExpandedState(false);
        MailDemo.ChangeExpandButtonsVisibility(true, false);
    },
    ClientExpandPaneImage_Click: function(s, e) {
        MailDemo.ChangeLeftPaneExpandedState(true);
        MailDemo.ChangeExpandButtonsVisibility(true, true);
    },

    ClientInfoMenu_ItemClick: function(s, e) {
        if(e.item.parent && e.item.parent.name == "theme") {
            ASPxClientUtils.SetCookie("MailDemoCurrentTheme", e.item.name || "");
            e.processOnServer = true;
        }
    },

 
    ShowLoadingPanel: function(element) {
        this.loadingPanelTimer = window.setTimeout(function() {
            ClientLoadingPanel.ShowInElement(element);
        }, 500);
    },

    HideLoadingPanel: function() {
        if(this.loadingPanelTimer > -1) {
            window.clearTimeout(this.loadingPanelTimer);
            this.loadingPanelTimer = -1;
        }
        ClientLoadingPanel.Hide();
    },

    PostponeAction: function(action, canExecute) {
        var f = function() {
            if(!canExecute())
                window.setTimeout(f, 50);
            else
                action();
        };
        f();
    }
});

MailPageModule = CreateClass(PageModuleBase, {
    constructor: function() {
        this.DemoState = { View: "MailList" };
    },

    OnStateChanged: function() {
        var state = this.DemoState;
        if(state.View == "MailList")
            this.ShowMailGrid();
        if(state.View == "MailPreview")
            this.ShowPreview(state.Key);
        if(state.View == "MailForm")
            this.ShowMailForm(state.Command, state.Key);
    },

    OnSearchTextChanged: function() {
        var processed = MailPageModule.base.prototype.OnSearchTextChanged.call(MailDemo);
        if(processed) return;
        MailDemo.ChangeDemoState("MailList");
        MailDemo.DoCallback(ClientMailGrid, function() {
            ClientMailGrid.PerformCallback("Search");
        });
    },

    ClientLayout_PaneResized: function() {
        var state = MailDemo.DemoState;
        if(!state) return;
        
        if(state.View == "MailList"){
            ClientMailGrid.SetHeight(0);
            ClientMailGrid.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight());
        }
        if(state.View == "MailForm" && window.ClientMailEditor) {
            ClientMailEditor.SetHeight(0);
            ClientMailEditor.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight() - $("#MailForm").get(0).offsetHeight);
        }
        if(state.View == "MailPreview") {
            ClientMailPreviewPanel.SetHeight(0);
            ClientMailPreviewPanel.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight());
        }
    },

    ClientActionMenu_ItemClick: function(s, e) { 
        var command = e.item.name;
        var state = MailDemo.DemoState;
        switch(command) {
            case "new":
                MailDemo.ChangeDemoState("MailForm", "New");
                break;
            case "reply":
                MailDemo.ChangeDemoState("MailForm", "Reply", state.Key);
                break;
            case "back":
                MailDemo.ChangeDemoState("MailList");
                break;
            case "delete":
                if(!window.confirm("Confirm Delete?"))
                    return;
                var keys = [ ];
                if(state.View == "MailList") {
                    keys = ClientMailGrid.GetSelectedKeysOnPage();
                } else if(state.View == "MailPreview") {
                    keys = [ state.Key ];
                    MailDemo.ChangeDemoState("MailList");
                }
                if(keys.length > 0) {
                    MailDemo.DoCallback(ClientMailGrid, function() {
                        ClientMailGrid.PerformCallback("Delete|" + keys.join("|"));
                    });
                    MailDemo.MarkMessagesAsRead(true, keys);
                }
                break;
            case "send":
            case "save":
                if(window.ClientToEditor && !ASPxClientEdit.ValidateEditorsInContainerById("MailForm"))
                    return;
                var args = command == "send" ? "SendMail" : "SaveMail";
                if(state.Command === "EditDraft")
                    args += "|" + state.Key;
                MailDemo.ChangeDemoState("MailList");
                MailDemo.DoCallback(ClientMailGrid, function() {
                    ClientMailGrid.PerformCallback(args);
                });
                break;
            case "read":
            case "unread":
                var selectedKeys = ClientMailGrid.GetSelectedKeysOnPage();
                if(selectedKeys.length == 0)
                    return;
                ClientMailGrid.UnselectAllRowsOnPage();
                MailDemo.MarkMessagesAsRead(command == "read", selectedKeys);
                break;
        }
    },

    ShowMenuItems: function() { 
        var view = MailDemo.DemoState.View;

        ClientActionMenu.GetItemByName("new").SetVisible(view != "MailForm");
        ClientActionMenu.GetItemByName("send").SetVisible(view == "MailForm");
        ClientActionMenu.GetItemByName("save").SetVisible(view == "MailForm");
        ClientActionMenu.GetItemByName("reply").SetVisible(view == "MailPreview");
        ClientActionMenu.GetItemByName("back").SetVisible(view != "MailList");

        var hasSelectedMails = ClientMailGrid.GetSelectedKeysOnPage().length > 0;
        ClientActionMenu.GetItemByName("delete").SetVisible(view == "MailList" && hasSelectedMails || view == "MailPreview");
        
        var selectedNode = ClientMailTree.GetSelectedNode();
        var showMarkAs = view == "MailList" && hasSelectedMails && selectedNode.name != "Sent Items" && selectedNode.name != "Drafts";
        ClientActionMenu.GetItemByName("markAs").SetVisible(showMarkAs);

        ClientInfoMenu.GetItemByName("print").SetVisible(view == "MailList");
    },

    ClientMailFormPanel_Init: function(s, e) {
        MailDemo.DoCallback(s, function() {
            s.PerformCallback();
        });
    },

    ClientMailTree_Init: function(s, e) {
        s.cpPrevSelectedNode = s.GetSelectedNode();

        MailDemo.UpdateMailTreeUnreadInfo();
        MailDemo.UpdateMailGridUnreadInfo();
    },

    ClientMailTree_NodeClick: function(s, e) {
        if(s.cpPrevSelectedNode == s.GetSelectedNode())
            return;
        s.cpPrevSelectedNode = s.GetSelectedNode();

        MailDemo.ClearSearchBox();

        MailDemo.ShowMenuItems();
        MailDemo.ChangeDemoState("MailList");
        ClientMailGrid.cpResetVertPosition = true;

        MailDemo.DoCallback(ClientMailGrid, function() {
            ClientMailGrid.PerformCallback("FolderChanged");
        });
    },

    ClientMailGrid_Init: function(s, e) {
        MailDemo.UpdateMailGridKeyFolderHash();
    },

    ClientMailGrid_EndCallback: function(s, e) {
        MailDemo.ShowMenuItems();
        MailDemo.UpdateMailGridKeyFolderHash();
        MailDemo.UpdateMailGridUnreadInfo();
        if(ClientMailGrid.cpResetVertPosition) {
            ClientMailGrid.SetVerticalScrollPosition(0);
            ClientMailGrid.cpResetVertPosition = false;
        }
    },

    ClientMailGrid_RowClick: function(s, e) {
        var src = ASPxClientUtils.GetEventSource(e.htmlEvent);
        if(src.tagName == "TD" && src.className.indexOf("dxgvCommandColumn") != -1) // selection cell
            return;
        if(!s.IsDataRow(e.visibleIndex))
            return;
        var key = s.GetRowKey(e.visibleIndex);
        if(ClientMailTree.GetSelectedNode().name === "Drafts")
            MailDemo.ChangeDemoState("MailForm", "EditDraft", key);
        else 
            MailDemo.ChangeDemoState("MailPreview", "", key);
    },

    ClientMailGrid_SelectionChanged: function(s, e) {
        MailDemo.ShowMenuItems();
        MailDemo.UpdateMailGridUnreadInfo();
    },

    ClientMailEditor_Init: function(s, e) {
        if($(s.GetMainElement()).is(":visible")) {
            MailDemo.ClientLayout_PaneResized();
            window.setTimeout(function() { s.Focus(); }, 0);
        }
    },

    

    ShowMailGrid: function() {
        MailDemo.HideLoadingPanel();
        MailDemo.HideMailPreview();
        MailDemo.HideMailForm();

        ClientMailGrid.SetVisible(true);
        MailDemo.ClientLayout_PaneResized();
    },

 

 

    
});
 

(function() {  
    window.MailDemo = new MailPageModule();
})();