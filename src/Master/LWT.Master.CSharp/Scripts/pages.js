/// <reference path="jquery-3.1.1.intellisense.js" />
/// <reference path="jquery-3.1.1.js" />
(function($, dxbsDemo) {
    var MIDDLE_SIZE = 992,
        THEME_COOKIE_KEY = "DXBSCurrentTheme",
        DEFAULT_THEME = "Default";
    hljs.initHighlightingOnLoad();
    dxbsDemo.onThemeMenuClick = function(s, e) {
        var theme = e.item.name;
        hidePanels();
        $(window.top.document.body).fadeOut(200, function() {
            var $themeLink = $("#themeLink");
            var path = "../Content/";
            if(theme.toLowerCase() !== DEFAULT_THEME.toLowerCase())
                path += theme + "/";
            $themeLink.attr("href", path + "bootstrap.min.css");
            $body = $(document.body);
            $body.removeClass(removeThemeClass);
            $body.addClass("theme-" + theme.toLowerCase());
            if(window.top.document != document) {
                var $topbody = $(window.top.document.body);
                $topbody.removeClass(removeThemeClass);
                $topbody.addClass("theme-" + theme.toLowerCase());
            }
            $(window.top.document.body).fadeIn(300);
        });
        ASPxClientUtils.SetCookie(THEME_COOKIE_KEY, theme);
    };
    dxbsDemo.onScreenSizeValueChanged = function(s, e) {
        var resolution = s.GetValue();
        if(window == window.top)
            document.location = URI(document.location.href).addQuery("resolution", resolution).toString();
        else if(resolution === "FullScreen")
            window.top.document.location = URI(window.top.document.location.href).removeQuery("resolution").toString();
        else {
            dxbsDemo.setFrameResolution(resolution);
            var url = URI(window.top.document.location.href).setQuery("resolution", resolution).toString();
            window.top.history.pushState({ resolution: resolution }, resolution, url);
        }
    };
    dxbsDemo.setFrameResolution = function(resolution) {
        var demoContainer = $(window.top.document).find(".demoContainer");
        demoContainer.removeClass("Large Medium Small XtraSmall").addClass(resolution);
        dxbsDemo.updateNodesWithResolution(navTreeView.GetRootNode(), resolution);
    };
    dxbsDemo.updateNodesWithResolution = function(parent, resolution) {
        var count = parent.GetNodeCount();
        for(var i = 0; i < count; i++) {
            var node = parent.GetNode(i);
            var navigateUrl = node.GetNavigateUrl();
            node.SetNavigateUrl(URI(navigateUrl).setQuery("resolution", resolution));
            dxbsDemo.updateNodesWithResolution(node, resolution);
        }
    };
    dxbsDemo.onHistoryPopState = function(evt) {
        if(!window || window.top === window) return;
        var resolution = URI(window.top.document.location.href).query(true)["resolution"];
        if(resolution)
            dxbsDemo.setFrameResolution(resolution)
    };

    dxbsDemo.toastTimeoutID = -1;
    dxbsDemo.showToast = function(message, additionalMessage, timeout) {
        if (dxbsDemo.toastTimeoutID > 0) {
            clearTimeout(dxbsDemo.toastTimoutID);
            dxbsDemo.toastTimeoutID = -1;
        }
        $(".demo-toast").remove();
        if (additionalMessage)
            message += "<br><em>" + additionalMessage + "</em>";
        $("<div class=\"alert alert-success alert-dismissible fade in demo-toast \" role=\"alert\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button>" + message + "</div>")
            .appendTo("body");
        timeout = timeout || 5000;
        dxbsDemo.toastTimeoutID = setTimeout(dxbsDemo.hideToast, timeout);
    };
    dxbsDemo.hideToast = function() {
        $(".demo-toast").alert('close');
        dxbsDemo.toastTimoutID = -1;
    }

    var dbCreateTimer = -1,
        dbCreateProgressTimer = -1,
        dbCreateContainer = null,
        dbCreator = {};
    dbCreator.onCreateDbButtonClick = function(s, e) {
        dbCreateContainer = s.GetMainElement().parentNode;
        $(dbCreateContainer).find(".message").text("Creating Database...");
        s.SetVisible(false);
        progressBar.SetVisible(true);

        createDbCallback.PerformCallback("create");
        dbCreateProgressTimer = window.setInterval(function() {
            createDbCallback.PerformCallback("getRecordCount");
        }.bind(dxbsDemo.dbCreator), 1000);
    };
    dbCreator.onCallbackComplete = function(s, e) {
        if(e.parameter == "create") {
            window.clearTimeout(dbCreateTimer);
            if(eval(e.result)) {
                window.clearInterval(dbCreateProgressTimer);
                document.location.reload();
            } else
                dbCreateTimer = window.setTimeout(function() { createDbCallback.PerformCallback("create") }, 1000);
        }
        else if(e.parameter == "getRecordCount") {
            progressBar.SetValue(Math.round(e.result));
        }
    };
    dbCreator.onCallbackError = function(s, e) {
        e.handled = true;
        window.clearInterval(this.progressTimer);
        alert(e.message);
        document.location.reload();
    };
    dxbsDemo.dbCreator = dbCreator;

    var search = {
        searchTimeout: null,
        lastSearch: null,
        selectedItem: null,
        isInCallback: false,

        onSearchBoxGotFocus: function() {
            if(search.listenerTimeout)
                clearTimeout(search.listenerTimeout);
            search.listenerTimeout = setInterval(function() {
                var text = searchEditor.GetValue();
                if(search.lastText !== text) {
                    search.lastText = text;
                    search.doSearch(text);
                }
            }, 100);
        },
        onSearchBoxLostFocus: function() {
            if(search.listenerTimeout)
                clearTimeout(search.listenerTimeout);
            search.listenerTimeout = null;
        },


        doSearch: function(text) {
            search.selectedItem = null;
            if(text && text.length > 2) {
                searchResults.PerformCallback(text);
                search.setContainerVisiblity(true);
            }
            else
                search.setContainerVisiblity(false);
        },
        onSearchPopupBeginCallback: function() {
            search.isInCallback = true;
        },
        onSearchPopupEndCallback: function() {
            search.isInCallback = false;
        },
        setContainerVisiblity: function(visible) {
            if(visible)
                $(searchResults.GetMainElement()).slideDown();
            else
                $(searchResults.GetMainElement()).slideUp();
        }
    };
    dxbsDemo.search = search;

    var eventMonitor = {
        timerId: -1,
        pendingUpdates: [],

        trace: function(sender, e, eventName, frameIndex, encodeHtml) {
            var name = sender.name;
            var lastSeparator = name.lastIndexOf("_");
            if(lastSeparator > -1)
                name = name.substr(lastSeparator + 1);
            var html = eventMonitor.createTraceHtml(name, eventName, e, encodeHtml);

            $(".eventLogPanel")
                .filter(function() {
                    var events = $(this).data("events").split(",");
                    if(events.indexOf(eventName) > -1) {
                        var control = $(this).data("control");
                        return !control || control === name;
                    }
                })
                .each(function() {
                    var $panel = $(this);
                    var $eventCheckBox = $panel.find("[name=" + eventName + "]");
                    if($eventCheckBox.is(":checked")) {
                        eventMonitor.pendingUpdates.unshift(html);
                        if(eventMonitor.timerId < 0) {
                            eventMonitor.timerId = window.setTimeout(function() {
                                eventMonitor.Update($panel, frameIndex);
                            }, 0);
                        }
                    }
                });
        },

        createTraceHtml: function(name, eventName, e, encodeHtml) {
            var builder = ["<div class=\"alert alert-info\" style=\"display: none\">"];
            eventMonitor.buildTraceRowHtml(builder, "Sender", name, 100);
            eventMonitor.buildTraceRowHtml(builder, "EventType", eventName.replace(/_/g, " "));
            var count = 0;
            for(var child in e) {
                if(typeof e[child] == "function") continue;
                var childValue = e[child];
                if(typeof e[child] == "object") {
                    var objName = e[child] ? (e[child].name || e[child].NAME) : "";
                    if(e[child] && (e[child].text && typeof e[child].text != "function"))
                        childValue = "[text: '" + e[child].text + "']";
                    if(e[child] && e[child].tagName)
                        childValue = "[object HTMLElement]";
                    else if(e[child] && e[child] instanceof jQuery)
                        childValue = "[object jQuery]";
                    else if(e[child] && e[child] instanceof jQuery.Event)
                        childValue = "[object jQuery.Event]";
                    if(objName)
                        childValue = "[name: '" + objName + "']";
                    if(e[child] && e[child].getOptions) {
                        var strOptions = [];
                        var options = e[child].getOptions();
                        if(objName)
                            strOptions.push("name: '" + objName + "'");
                        if(options.type)
                            strOptions.push("type: '" + options.type + "'");
                        if(options.symbol)
                            strOptions.push("symbol: '" + options.symbol + "'");
                        childValue = "[" + strOptions.join(", ") + "]";
                    }
                }
                if(encodeHtml)
                    childValue = eventMonitor.escapeHtml(childValue);
                eventMonitor.buildTraceRowHtml(builder, count ? "" : "Arguments", child + " = " + childValue);
                count++;
            }
            builder.push("</div>");
            return builder.join("");
        },

        buildTraceRowHtml: function(builder, name, text, width) {
            builder.push("<div><span");
            if(width)
                builder.push(" style='width: ", width, "px'");
            builder.push(">");
            if(name)
                builder.push("<b>", name, ":</b>");
            builder.push("</span><span>", text, "</span></div>");
        },

        getLogElement: function($panel) {
            return $panel.find(".panel-body");
        },

        Update: function($panel, frameIndex) {
            var self = eventMonitor;
            var $element = self.getLogElement($panel);

            if($.isNumeric(frameIndex))
                $element = $($element.get(frameIndex));

            $element.html(self.pendingUpdates.join("") + $element.html());
            $element.children(".alert").slideDown("fast", function() {
                $(this).css("height", "").css("padding", "").css("margin", "");
            });
            self.timerId = -1;
            self.pendingUpdates = [];
        },
        init: function() {
            $(".eventLogPanel").find(".clearbtn").on("click", function() {
                $(this).parent().find(".panel-body").html("");
            });
        },

        escapeHtml: function(str) {
            return str.replace(/&/g, '&amp;').replace(/"/g, '&quot;').replace(/'/g, '&#39;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
        }
    };
    dxbsDemo.eventMonitor = eventMonitor;
    $(window).on("load", function() {
        eventMonitor.init();
    });

    var uploadedFilesContainer = {
        uploadControl: null,
        addFile: function(sender, fileName, fileUrl, fileSize) {
            var self = uploadedFilesContainer;
            if(!sender || sender != self.uploadControl) {
                self.clear();
                self.getMainElement().hide();
                self.getMainElement().insertAfter(sender.GetMainElement());
                self.getMainElement().show("slow");
            }
            self.uploadControl = sender;
            var html = self.buildHtml(fileName, fileUrl, fileSize);
            self.update(html);
        },

        buildHtml: function(fileName, fileUrl, fileSize) {
            var html = ["<div><span class=\"fileName\"><a href=\""];
            html.push(fileUrl);
            html.push("\" class=\"alert-link\">");
            html.push(fileName);
            html.push("</a></span><span class=\"fileSize\">");
            html.push(fileSize);
            html.push("</span></div>");
            return html.join("");
        },

        getMainElement: function() {
            return $(".uploadFileContainer");
        },

        getInnerElement: function() {
            return $(".uploadFileContainer .panel-body");
        },

        update: function(html) {
            var self = uploadedFilesContainer;
            var $element = self.getInnerElement();
            $element.html($element.html() + html);
        },

        clear: function() {
            var $element = uploadedFilesContainer.getInnerElement();
            $element.html("");
        },

        hide: function() {
            uploadedFilesContainer.clear();
            var $element = uploadedFilesContainer.getMainElement();
            $element.hide();
        }
    };
    dxbsDemo.uploadedFilesContainer = uploadedFilesContainer;

    $(window).on("load", function() {
        initScrollPlugin();
        initNavButtons();
        initHistoryApi();
        initExamplesCode();
        initScrollSpy();
        initQrCode();
    });

    function hidePanels() {
        $(document.body).removeClass("show-nav");
        $(document.body).removeClass("show-settings");
    }

    function removeThemeClass(index, className) {
        return (className.match(/(^|\s)theme-\S+/g) || []).join(' ');
    }

    function initScrollPlugin() {
        var $activeNode = $("#navTreeView .active");
        if(!$activeNode.is(":mcsInSight"))
            $("#sidebar > .sidebar-body").mCustomScrollbar("scrollTo", $activeNode, {
                scrollInertia: 0
            });
    }

    function initNavButtons() {
        var $sidebar = $("#sidebar");
        $("#collapse-button").click(function() {
            if($(window).width() >= MIDDLE_SIZE) {
                $(document.body).toggleClass("collapse-nav");
                if(!$(document.body).hasClass("collapse-nav"))
                    ASPxClientUtils.DeleteCookie("collapse-nav");
                else
                    ASPxClientUtils.SetCookie("collapse-nav", "true");
            }
            else
                $(document.body).toggleClass("show-nav");
        });

        $("#settingsButton").click(function() {
            $(document.body).removeClass("show-nav");
            $(document.body).toggleClass("show-settings");
        });

        $(window).resize(hidePanels);

        $("#subMenu a[href]").click(function() {
            var $a = $(this);
            $('html, body').animate({
                scrollTop: $($a.attr('href')).offset().top - 80
            }, 300);
        });
    }

    function initHistoryApi() {
        if(window.top !== window)
            ASPxClientUtils.AttachEventToElement(window.top, 'popstate', dxbsDemo.onHistoryPopState);
    }

    function initExamplesCode() {
        $(".code").each(function() {
            $code = $(this);
            var $buttons = $code.find("button[data-code]");
            var $pres = $code.find("pre[data-code]");
            $buttons.each(function() {
                var codeType = $(this).attr("data-code");
                $(this).click(function() {
                    $buttons.each(function() {
                        $(this).toggleClass("active", $(this).attr("data-code") === codeType);
                    });
                    $pres.each(function() {
                        $(this).toggleClass("hidden", $(this).attr("data-code") !== codeType);
                    });
                });
            });
            new Clipboard($code.find(".btn-copy").get(0), {
                text: function() {
                    return $pres.filter(function() { return !$(this).hasClass("hidden") }).text();
                }
            });
        });
    }

    function initScrollSpy() {
        $('body').scrollspy({ target: '#subMenu', offset: 90 });
    }

    function initQrCode() {
        var link = URI(document.location.href)
            .removeQuery("resolution")
            .removeQuery("frame")
            .toString();
        $("#qrLink")
            .qrcode({
                width: 128,
                height: 128,
                text: link
            });
    }
})(jQuery, window.dxbsDemo || (window.dxbsDemo = {}));