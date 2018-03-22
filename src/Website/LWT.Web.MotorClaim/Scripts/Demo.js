﻿function CreateClass(parentClass, properties) {
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

    ClientSearchBox_KeyPress: function(s, e) {
        e = e.htmlEvent;
        if(e.keyCode === 13) {
            // prevent default browser form submission
            if(e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;
        }
    },

    ClientSearchBox_KeyDown: function(s, e) {
        window.clearTimeout(MailDemo.searchBoxTimer);
        MailDemo.searchBoxTimer = window.setTimeout(function() { 
            MailDemo.OnSearchTextChanged(); 
        }, 1200);
    },

    ClientSearchBox_TextChanged: function(s, e) {
        MailDemo.OnSearchTextChanged();
    },

    OnSearchTextChanged: function() {
        window.clearTimeout(MailDemo.searchBoxTimer);
        var searchText = ClientSearchBox.GetText();
        if(ClientHiddenField.Get("SearchText") == searchText)
            return true;
        ClientHiddenField.Set("SearchText", searchText);
    },

    ClearSearchBox: function() {
        ClientHiddenField.Set("SearchText", "");
        ClientSearchBox.SetText("");
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

    ClientAddressBookPopup_PopUp: function(s, e) {
        var emails = ClientToEditor.GetText().split(",");
        for(var i = 0; i < emails.length; i++)
            emails[i] = ASPxClientUtils.Trim(emails[i]);
        ClientAddressesList.UnselectAll();
        ClientAddressesList.SelectValues(emails);
    },

    ClientAddressBookPopupOkButton_Click: function(s, e) {
        ClientAddressBookPopup.Hide();

        var emails = ClientToEditor.GetText().split(",");
        for(var i = 0; i < emails.length; i++)
            emails[i] = ASPxClientUtils.Trim(emails[i]);
        for(var i = emails.length - 1; i >= 0; i--) { 
            var email = emails[i];
            var item = ClientAddressesList.FindItemByValue(email);
            if(email === "" || ClientAddressesList.FindItemByValue(email))
                emails.splice(i, 1);
        }
        emails = emails.concat(ClientAddressesList.GetSelectedValues());
        ClientToEditor.SetText(emails.join(", "));
    },

    ClientAddressBookPopupCancelButton_Click: function(s, e) {
        ClientAddressBookPopup.Hide();
    },

    ShowMailGrid: function() {
        MailDemo.HideLoadingPanel();
        MailDemo.HideMailPreview();
        MailDemo.HideMailForm();

        ClientMailGrid.SetVisible(true);
        MailDemo.ClientLayout_PaneResized();
    },

    HideMailGrid: function() {
        ClientMailGrid.SetVisible(false);
    },

    ShowPreview: function(key) {
        MailDemo.HideLoadingPanel();
        MailDemo.HideMailGrid();
        MailDemo.HideMailForm();

        ClientMailPreviewPanel.SetContentHtml("");
        ClientMailPreviewPanel.SetVisible(true);
        MailDemo.DoCallback(ClientMailPreviewPanel, function() {
            ClientMailPreviewPanel.PerformCallback(key);
        });
        MailDemo.MarkMessagesAsRead(true, [ key ]);
    },

    HideMailPreview: function() {
        ClientMailPreviewPanel.SetVisible(false);
    },

    ShowMailForm: function(command, key) {
        MailDemo.HideMailGrid();
        MailDemo.HideMailPreview();
        
        ClientToEditor.SetValue("");
        ClientToEditor.SetIsValid(true);
        ClientSubjectEditor.SetValue("");
        ClientMailEditor.SetHtml("");
        ClientMailEditor.Focus();

        ClientMailFormPanel.SetVisible(true);
        ClientMailEditor.AdjustControl();
        MailDemo.ClientLayout_PaneResized();

        if(command == "Reply" || command == "EditDraft") {
            ClientMailGrid.GetValuesOnCustomCallback("MailForm|" + command + "|" + key, function(values) {
                var setValuesFunc = function() {
                    MailDemo.HideLoadingPanel();
                    if(!values) 
                        return;
                    ClientToEditor.SetValue(values["To"]);
                    ClientSubjectEditor.SetValue(values["Subject"]);
                    ClientMailEditor.SetHtml(values["Text"]);
                    ClientMailEditor.Focus();
                };
                MailDemo.PostponeAction(setValuesFunc, function() { return !!window.ClientMailEditor });
            });
            MailDemo.ShowLoadingPanel(ClientMailFormPanel.GetMainElement());
        }
    },

    HideMailForm: function() {
        if(window.ClientAddressBookPopup)
            ClientAddressBookPopup.Hide();
        ClientMailFormPanel.SetVisible(false);
    },

    // MarkAsRead

    UpdateMailTreeUnreadInfo: function() {
        MailDemo.IterateMailTreeNodes(function(node) {
            if(!node.cpUnreadMessagesCount)
                node.cpUnreadMessagesCount = 0;
            var unreadMessages = ClientMailTree.cpUnreadMessagesHash[node.name];
            if(!unreadMessages || unreadMessages.length == node.cpUnreadMessagesCount)
                return;
            var nodeText = node.GetText();
            var match = nodeText.match(/^(\w+) \(\d+\)$/);
            if(match && match.length == 2)
                nodeText = match[1];
            MailDemo.UpdateNodeUnreadState(node, nodeText, unreadMessages.length);
            node.cpUnreadMessagesCount = unreadMessages.length;
        });
        ClientMailTree.AdjustControl();
    },

    UpdateNodeUnreadState: function(node, nodeText, unreadMessageCount) {
        if(unreadMessageCount == 0) {
            node.SetText(nodeText);
            node.GetHtmlElement().className = node.GetHtmlElement().className.replace(" unread", "");
        } else {
            node.SetText(nodeText + " (" + unreadMessageCount + ")");
            if(!node.GetHtmlElement().className.match("unread"))
                node.GetHtmlElement().className += " unread";
        }
        node.treeView.CorrectControlWidth();
    },

    IterateMailTreeNodes: function(action) {
        var stack = [ ClientMailTree.GetRootNode() ];
        while(stack.length > 0) {
            var node = stack.pop();
            action(node);
            for(var i = 0; i < node.GetNodeCount(); i++)
                stack.push(node.GetNode(i));
        }
    },

    MarkMessagesAsRead: function(read, keys) {
        var sendCallback = false;
        var keyMap = MailDemo.GetMailKeyMap(keys);
        MailDemo.IterateMailTreeNodes(function(node) {
            var markedKeys = keyMap[node.name];
            if(!markedKeys || markedKeys.length == 0)
                return;
            var unreadMessages = ClientMailTree.cpUnreadMessagesHash[node.name];
            if(!unreadMessages)
                ClientMailTree.cpUnreadMessagesHash[node.name] = unreadMessages = [ ];
            for(var i = 0; i < markedKeys.length; i++) {
                var key = markedKeys[i];
                var index = ASPxClientUtils.ArrayIndexOf(unreadMessages, key);
                if(read && index !== -1) {
                    unreadMessages.splice(index, 1);
                    sendCallback = true;
                }
                if(!read && index === -1) {
                    unreadMessages.push(key);
                    sendCallback = true;
                }
            }
        });
        MailDemo.UpdateMailTreeUnreadInfo();
        MailDemo.UpdateMailGridUnreadInfo();
        if(sendCallback)
            ClientMailGrid.GetValuesOnCustomCallback("MarkAs" + "|" + (read ? "Read" : "Unread") + "|" + keys.join("|"));
    },


    GetMailKeyMap: function(keys) {
        var result = { };
        var selectedNode = ClientMailTree.GetSelectedNode();
        if(selectedNode.name === "Inbox") {
            for(var i = 0; i < keys.length; i++) {
                var key = keys[i];
                var folderName = ClientMailGrid.cpKeyFolderHash[key];
                if(folderName) {
                    result[folderName] = result[folderName] || [ ];
                    result[folderName].push(key);
                }
            }
        } else {
            result[selectedNode.name] = keys;
        }
        return result;
    },

    UpdateMailGridUnreadInfo: function() {
        var unreadKeys = [ ];
        for(var folderName in ClientMailTree.cpUnreadMessagesHash)
            unreadKeys = unreadKeys.concat(ClientMailTree.cpUnreadMessagesHash[folderName]);

        var startIndex = ClientMailGrid.GetTopVisibleIndex();
        var count = ClientMailGrid.GetVisibleRowsOnPage();
        for(var i = startIndex; i < startIndex + count; i++) {
            if(ClientMailGrid.IsGroupRow(i))
                continue;
            var key = ClientMailGrid.GetRowKey(i);
            if(!ASPxClientUtils.IsExists(key))
                continue;
            var read = ASPxClientUtils.ArrayIndexOf(unreadKeys, key.toString()) < 0;
            var row = ClientMailGrid.GetItem(i);
            var hasMarker = row.className.match("unread");
            if(read && hasMarker)
                row.className = row.className.replace(" unread", "");
            if(!read && !hasMarker)
                row.className += " unread";
        }
    },

    UpdateMailGridKeyFolderHash: function() {
        var hash = { };
        for(var folderName in ClientMailGrid.cpVisibleMailKeysHash) {
            var keys = ClientMailGrid.cpVisibleMailKeysHash[folderName];
            if(!keys || keys.length == 0)
                continue;
            hash[folderName] = [ ];
            for(var i = 0; i < keys.length; i++)
                hash[keys[i]] = folderName;
        }
        ClientMailGrid.cpKeyFolderHash = hash;
    }

});

CalendarPageModule = CreateClass(PageModuleBase, {
    constructor: function() {
        this.DemoState = { View: "Scheduler" };
        document.body.style.overflow = "auto";
    },

    ClientActionMenu_ItemClick: function(s, e) { 
        var command = e.item.name;
        if(command == "new")
            ClientScheduler.RaiseCallback("MNUVIEW|NewAppointment");
    },

    ShowMenuItems: function() { 
        ClientActionMenu.GetItemByName("new").SetVisible(true);
    },

    ClientResourceCheckBox_CheckedChanged: function(s, e) {
        MailDemo.DoCallback(ClientScheduler, function() { 
            ClientScheduler.Refresh();
        });
    },

    ClientScheduler_AppointmentDoubleClick: function(s, e) {
        s.ShowAppointmentFormByClientId(e.appointmentId);
        e.handled = true;
    }

});

ContactsPageModule = CreateClass(PageModuleBase, { 
    constructor: function() {
        this.DemoState = { View: "ContactList" };
        document.body.style.overflow = "auto";
    },

    OnStateChanged: function() {
        var state = this.DemoState;
        if(state.View == "ContactList")
            this.ShowContactDataView();
        if(state.View == "ContactForm")
            this.ShowContactForm(state.Command, state.Key);
    },
    
    OnSearchTextChanged: function() {
        var processed = ContactsPageModule.base.prototype.OnSearchTextChanged.call(MailDemo);
        if(processed) return;
        MailDemo.ChangeDemoState("ContactList");
    },

    ClientActionMenu_ItemClick: function(s, e) { 
        var command = e.item.name;
        var state = MailDemo.DemoState;

        if(command == "new") {
            MailDemo.ChangeDemoState("ContactForm", "New");
        } else if(command == "back") {
            MailDemo.ChangeDemoState("ContactList");
        } else if(command == "save") {
            if(window.ClientContactPhoneEditor && !ASPxClientEdit.ValidateEditorsInContainerById("ContactForm"))
                return;
            var args = "SaveContact|" + state.Command;
            if(state.Command === "Edit")
                args += "|" + state.Key + "|" + ClientContactPhotoImage.cpNeedUpdateContactPhoto;
            
            MailDemo.ChangeDemoState("ContactList");
            MailDemo.DoCallback(ClientContactCardView, function() {
                ClientContactCardView.PerformCallback(args);
            });
        }
    },

    ShowMenuItems: function() { 
        var view = this.DemoState.View;

        ClientActionMenu.GetItemByName("new").SetVisible(view == "ContactList");
        ClientActionMenu.GetItemByName("save").SetVisible(view == "ContactForm");
        ClientActionMenu.GetItemByName("back").SetVisible(view == "ContactForm");

        ClientInfoMenu.GetItemByName("print").SetVisible(view == "ContactList");
    },

    ClientContactFormPanel_Init: function(s, e) {
        MailDemo.DoCallback(s, function() {
            s.PerformCallback();
        });
    },

    ClientContactAddressBookList_SelectedIndexChanged: function(s, e) {
        MailDemo.ClearSearchBox();
        MailDemo.ChangeDemoState("ContactList");
        MailDemo.DoCallback(ClientContactCardView, function() {
            ClientContactCardView.PerformCallback("AddressBookChanged");
        });
    },

    ClientContactSortByCombo_SelectedIndexChanged: function(s, e) {
        MailDemo.ChangeDemoState("ContactList");
        MailDemo.DoCallback(ClientContactCardView, function() {
            ClientContactCardView.PerformCallback("SortByChanged");
        });
    },

    ClientContactSortDirectionCombo_SelectedIndexChanged: function(s, e) {
        MailDemo.ChangeDemoState("ContactList");
        MailDemo.DoCallback(ClientContactCardView, function() {
            ClientContactCardView.PerformCallback("SortDirectionChanged");
        });
    },

    ClientEditContactImage_Click: function(s, e) {
        MailDemo.ChangeDemoState("ContactForm", "Edit", s.cpContactKey);
    },

    ClientDeleteContactImage_Click: function(s, e) {
        if(!window.confirm("Confirm Delete?"))
            return;
        MailDemo.DoCallback(ClientContactCardView, function() {
            ClientContactCardView.PerformCallback("Delete" + "|" + s.cpContactKey);
        });
    },

    ClientContactCountryEditor_SelectedIndexChanged: function(s, e) {
        MailDemo.DoCallback(ClientContactCityEditor, function() {
            ClientContactCityEditor.PerformCallback(s.GetValue());
        });
    },

    ShowContactDataView: function() {
        MailDemo.HideLoadingPanel();
        MailDemo.HideContactForm();

        ClientContactCardView.SetVisible(true);
    },

    HideContactDataView: function() {
        ClientContactCardView.SetVisible(false);
    },

    ShowContactForm: function(command, key) {
        if(window.ClientContactPhotoImage) {           
            ClientContactPhotoImage.Clear();
            ClientContactNameEditor.SetText("");
            ClientContactEmailEditor.SetText("");
            ClientContactAddressEditor.SetText("");
            ClientContactCountryEditor.SetText("");

            ClientContactCityEditor.SetText("");
            ClientContactCityEditor.ClearItems();

            ClientContactPhoneEditor.SetText("");

            ClientContactEmailEditor.SetIsValid(true);
            ClientContactNameEditor.SetIsValid(true);
            ClientContactPhoneEditor.SetIsValid(true);
        }
        MailDemo.HideContactDataView();
        ClientContactFormPanel.SetVisible(true);
        if(command == "Edit") {
            MailDemo.ShowLoadingPanel(ClientContactFormPanel.GetMainElement());
            var updateContactInfo = function(){
                MailDemo.DoCallback(ClientContactPhotoImage, function() { ClientContactPhotoImage.PerformCallback(command + "|" + key); })
            };            
            MailDemo.PostponeAction(updateContactInfo, function() { return !!window.ClientContactPhotoImage } );
        }
    },

    HideContactForm: function() {
        ClientContactFormPanel.SetVisible(false);
    },

    ClientContactPhotoImage_EndCallback: function(s, e) {        
        MailDemo.HideLoadingPanel();
        if(!s.cpContactInfoUpdating)
            MailDemo.ChangeDemoState("ContactForm", "New");
        else {
            setValuesFunc = function() {
                MailDemo.HideLoadingPanel();
                if(!s.cpContactInfo) return;
                var values = s.cpContactInfo;
                
                ClientContactNameEditor.SetText(values["Name"]);

                ClientContactEmailEditor.SetText(values["Email"]);
                ClientContactEmailEditor.SetIsValid(true);

                ClientContactAddressEditor.SetText(values["Address"]);
                ClientContactCountryEditor.SetText(values["Country"]);
                ClientContactCityEditor.SetText(values["City"]);

                ClientContactPhoneEditor.SetText(values["Phone"]);
                ClientContactPhoneEditor.SetIsValid(true);
                
                if(ClientContactCountryEditor.GetSelectedItem()) {
                    MailDemo.DoCallback(ClientContactCityEditor, function() {
                        ClientContactCityEditor.PerformCallback(ClientContactCountryEditor.GetValue());
                    });
                    var handler = function(sender, e) {
                        sender.SetText(values["City"]);
                        sender.EndCallback.RemoveHandler(handler);
                    };
                    ClientContactCityEditor.EndCallback.AddHandler(handler);
                }
            };
            s.cpContactInfoUpdating = false;
            MailDemo.PostponeAction(setValuesFunc, function() { return !!window.ClientContactPhoneEditor });
        }
    },
    ClientContactPhotoImage_ValueChanged: function(s, e) {
        s.cpNeedUpdateContactPhoto = !s.cpContactInfoUpdating;
    }
});

FeedsPageModule = CreateClass(PageModuleBase, {
    constructor: function() {
        this.DemoState = { View: "FeedList" };
    },
    
    OnStateChanged: function() {
        var state = this.DemoState;
        if(state.View == "FeedList")
            this.ShowFeedGrid();
        if(state.View == "FeedItemPreview")
            this.ShowFeedItemPreview(state.Key);
    },

    OnSearchTextChanged: function() {
        var processed = FeedsPageModule.base.prototype.OnSearchTextChanged.call(MailDemo);
        if(processed) return;
        MailDemo.ChangeDemoState("FeedList");
        MailDemo.DoCallback(ClientFeedGrid, function() {
            ClientFeedGrid.PerformCallback("Search");
        });
    },

    ClientActionMenu_ItemClick: function(s, e) { 
        var command = e.item.name;
        if(command == "back")
            MailDemo.ChangeDemoState("FeedList");
    },

    ShowMenuItems: function() { 
        var view = this.DemoState.View;
        ClientActionMenu.GetItemByName("back").SetVisible(view == "FeedItemPreview");
    },

    ClientLayout_PaneResized: function() {
        if(MailDemo.DemoState.View == "FeedList")
            ClientFeedGrid.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight());
        if(MailDemo.DemoState.View == "FeedItemPreview") 
            ClientFeedItemPreviewPanel.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight());
    },
    
    ClientFeedGrid_RowClick: function(s, e) {
        if(s.IsDataRow(e.visibleIndex))
            MailDemo.ChangeDemoState("FeedItemPreview", "", s.GetRowKey(e.visibleIndex));
    },

    ClientFeedNavBar_ItemClick: function(s, e) {
        MailDemo.ClearSearchBox();
        MailDemo.ChangeDemoState("FeedList");
        MailDemo.DoCallback(ClientFeedGrid, function() {
            ClientFeedGrid.PerformCallback("FeedChanged");
        });
    },

    ShowFeedGrid: function() {
        MailDemo.HideLoadingPanel();
        MailDemo.HideFeedItemPreview();

        ClientFeedGrid.SetVisible(true);
        MailDemo.ClientLayout_PaneResized();
    },

    HideFeedGrid: function() {
        ClientFeedGrid.SetVisible(false);
    },

    ShowFeedItemPreview: function(key) {
        MailDemo.HideFeedGrid();

        ClientFeedItemPreviewPanel.SetVisible(true);
        MailDemo.DoCallback(ClientFeedItemPreviewPanel, function() {
            ClientFeedItemPreviewPanel.PerformCallback(key);
        });
    },

    HideFeedItemPreview: function() {
        ClientFeedItemPreviewPanel.SetContentHtml("");
        ClientFeedItemPreviewPanel.SetVisible(false);
    }
});

PrintPageModule = CreateClass(PageModuleBase, {
    
    ClientPrintLayout_Init: function(s, e) {
        document.body.style.overflow = "auto";
    },

    PrintAddressFilterCombo_SelectedIndexChanged: function(s, e) {
        ClientHiddenField.Set("AddressBook", s.GetValue());
        ClientContactReportViewer.Refresh();
    },

    PrintStartDateEdit_DateChanged: function(s, e) {
        MailDemo.ValidatePrintDateInteval();
        ClientScheduleReportViewer.Refresh();
    },

    PrintEndDateEdit_DateChanged: function(s, e) {
        MailDemo.ValidatePrintDateInteval();
        ClientScheduleReportViewer.Refresh();
    },

    ValidatePrintDateInteval: function() {
        var start = ClientPrintStartDateEdit.GetDate();
        var end = ClientPrintEndDateEdit.GetDate();

        if(start > end) {
            end.setTime(start.getTime() + 3600000 * 24);
            ClientPrintEndDateEdit.SetDate(end);
        }
        ClientHiddenField.Set("StartDate", ClientPrintStartDateEdit.GetValueString());
        ClientHiddenField.Set("EndDate", ClientPrintEndDateEdit.GetValueString());
    }
});

(function() {
    var pageModule;
    var bodyElement = $("body");
    if(bodyElement.hasClass("mail"))
        pageModule = new MailPageModule();
    else if(bodyElement.hasClass("contacts"))
        pageModule = new ContactsPageModule();
    else if(bodyElement.hasClass("calendar"))
        pageModule = new CalendarPageModule();
    else if(bodyElement.hasClass("feeds"))
        pageModule = new FeedsPageModule();
    else if(bodyElement.hasClass("print"))
        pageModule = new PrintPageModule();
    else
        pageModule = new PageModuleBase();
    window.MailDemo = pageModule;
})();